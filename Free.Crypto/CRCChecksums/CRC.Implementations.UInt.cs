﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Free.Core;
using UInt128 = Free.Core.UInt128;

namespace Free.Crypto.CRCChecksums
{
    public static partial class CRC
    {
        /// <summary>
        /// Implements <see cref="ICRC"/> and <see cref="ICRC{T}"/> for a maximum 32-bit width CRC for unreflected input (MSBit first).
        /// </summary>
        /// <threadsafety static="false" instance="false"/>
        public class UnreflectedUInt : ICRC<uint>
        {
            const int BytesPreThread = 4 * 1024; // IMPORTANT: Must be a power-of-two!

            int width;
            uint register, init, xorOutput;
            uint[] table;
            bool reflectOutput;
            uint[] combineMatrix;

            /// <summary>
            /// Creates an instance of a CRC algorithm with the behaviour according to the parameters.
            /// </summary>
            /// <param name="polynomial">The polynomial to use. Unreflected and filled in the least significant bits without the leading 1.</param>
            /// <param name="table">The table generated from the polynomial to use. Use <see cref="GenerateTable(uint, int)"/> with the polynomial and its width to generate the table.</param>
            /// <param name="init">The initial value of the register. Unreflected and filled in the least significant bits.</param>
            /// <param name="refOut">Set <b>true</b>, if register is to be reflected before XORing with <paramref name="xorOut"/> and output.</param>
            /// <param name="xorOut">Value to be XORed with the reflected or unreflected register depending on <paramref name="refOut"/> before output. Filled in the least significant bits.</param>
            /// <param name="width">The width of the polynomial in bits. Must be greater than 0 and less than 33. Default is 32.</param>
            public UnreflectedUInt(uint polynomial, uint[] table, uint init, bool refOut, uint xorOut, int width = 32)
            {
                if (width <= 0 || width > 32)
                {
                    throw new ArgumentOutOfRangeException("width", "Must be greater than 0 and less than 33.");
                }

                if (table == null)
                {
                    throw new ArgumentNullException("table");
                }

                if (table.Length < 256)
                {
                    throw new ArgumentOutOfRangeException("table", "Must have at least 256 elements.");
                }

                // Gets value (2^width)-1.
                uint mask = (((1u << (width - 1)) - 1u) << 1) | 1u;

                polynomial &= mask;
                init &= mask;
                xorOut &= mask;

                init <<= 32 - width;

                this.width = width;
                this.table = table;
                register = this.init = init;
                reflectOutput = refOut;
                xorOutput = xorOut;

                #region Create combineMatrix
                uint[] mat1 = new uint[width], mat2 = new uint[width]; // Create matrices (for inplace squaring operation)

                // Fill matrix with 1-bit-shift-operation (bit 0 of register becomes bit 1, bit 1 becomes bit 2 and so on, when multiplied with this matrix)
                for (uint n = 0, row = 2; n < width - 1; n++, row <<= 1)
                {
                    mat1[n] = row;
                }

                mat1[width - 1] = polynomial; // and the polynomial (will be multiplied(XORed) into the register if top-bit is 1, when multiplied with this matrix)

                // Square to create 2-bit-operation matrix
                MatrixSquare(mat2, mat1, width);

                // Square again to create 4-bit-operation matrix
                MatrixSquare(mat1, mat2, width);

                int length = BytesPreThread;
                do
                {
                    // Square again to create the next power-of-two-bytes-operation matrix
                    MatrixSquare(mat2, mat1, width);
                    combineMatrix = mat2;

                    length >>= 1;

                    // Already done?
                    if (length == 0)
                    {
                        break;
                    }

                    // Square again to create the next power-of-two-bytes-operation matrix
                    MatrixSquare(mat1, mat2, width);
                    combineMatrix = mat1;

                    length >>= 1;
                } while (length != 0);
                #endregion
            }

            /// <summary>
            /// Creates an instance of a CRC algorithm with the behaviour according to the parameters.
            /// </summary>
            /// <param name="polynomial">The polynomial to use. Unreflected and filled in the least significant bits without the leading 1.</param>
            /// <param name="init">The initial value of the register. Unreflected and filled in the least significant bits.</param>
            /// <param name="refOut">Set <b>true</b>, if register is to be reflected before XORing with <paramref name="xorOut"/> and output.</param>
            /// <param name="xorOut">Value to be XORed with the reflected or unreflected register depending on <paramref name="refOut"/> before output. Filled in the least significant bits.</param>
            /// <param name="width">The width of the polynomial in bits. Must be greater than 0 and less than 33. Default is 32.</param>
            public UnreflectedUInt(uint polynomial, uint init, bool refOut, uint xorOut, int width = 32)
                : this(polynomial, GenerateTable(polynomial, width), init, refOut, xorOut, width) { }

            /// <summary>
            /// Gets/sets the register (not necessarely the CRC value). Can be utilized to cache the value; useful in situations where a chain of blocks need to be checked, and reseting would need to reprocess the whole chain from the start.
            /// </summary>
            public uint Register
            {
                get { return register; }
                set { register = value; }
            }

            /// <summary>
            /// Gets the CRC value (not the register) for the message bytes processed so far.
            /// </summary>
            public uint Value { get { return (reflectOutput ? BitOrder.Reflect(register) : register >> (32 - width)) ^ xorOutput; } }

            /// <summary>
            /// Gets the CRC value (not the register) for the message bytes processed so far as a 32-bit value.
            /// </summary>
            /// <returns>The CRC value as <b>uint</b>.</returns>
            public uint GetCRC() { return Value; }

            /// <summary>
            /// Gets the CRC value (not the register) for the message bytes processed so far as a 64-bit value.
            /// </summary>
            /// <returns>The CRC value as <b>ulong</b>.</returns>
            public ulong GetCRCAsULong() { return Value; }

            /// <summary>
            /// Gets the CRC value (not the register) for the message bytes processed so far as a 128-bit value.
            /// </summary>
            /// <returns>The CRC value as <see cref="UInt128"/>.</returns>
            public UInt128 GetCRCAsUInt128() { return Value; }

            /// <summary>
            /// Processes a single message byte.
            /// </summary>
            /// <param name="value">The value to add to the CRC.</param>
            /// <returns>A reference to <b>this</b> instance.</returns>
            public ICRC Add(byte value)
            {
                register = (register << 8) ^ table[((register >> 24) ^ value) & 0xFF];

                return this;
            }

            /// <summary>
            /// Processes message bytes.
            /// </summary>
            /// <param name="data">The data to add to the CRC.</param>
            /// <param name="offset">Location in the array where to start in bytes.</param>
            /// <param name="count">Number of bytes.</param>
            /// <returns>A reference to <b>this</b> instance.</returns>
            public ICRC Add(byte[] data, int offset = 0, int count = 0)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                if (offset < 0 || offset > data.Length)
                {
                    throw new ArgumentOutOfRangeException("offset", "Must be non-negative and less than or equal to the length of data in bytes.");
                }

                if (count < 0)
                {
                    throw new ArgumentOutOfRangeException("count", "Must be non-negative.");
                }

                if (offset + count > data.Length)
                {
                    throw new ArgumentOutOfRangeException("count", "Must be less than or equal to the length of data in bytes minus the offset argument.");
                }

                if (count == 0)
                {
                    count = data.Length - offset;
                    if (count == 0)
                    {
                        return this;
                    }
                }

                #region Not enough bytes => regular case
                if (count < BytesPreThread)
                {
                    // local copies of values and references make the code much faster
                    uint[] lTable = table;
                    int lCount = count;
                    uint lRegister = register;

                    unsafe
                    {
                        fixed (byte* lDataFixed = data)
                        {
                            byte* lData = lDataFixed + offset;
                            do
                            {
                                lRegister = (lRegister << 8) ^ lTable[((lRegister >> 24) ^ *lData++) & 0xFF];
                            }
                            while (--lCount != 0);
                        }
                    }

                    register = lRegister;

                    return this;
                }
                #endregion

                int threads = count / BytesPreThread;
                int firstcounts = count % BytesPreThread;
                if (firstcounts != 0)
                {
                    threads++;
                }
                else
                {
                    firstcounts = BytesPreThread;
                }

                uint[] registers = new uint[threads];

                unsafe
                {
                    fixed (byte* lDataFixed = data)
                    {
                        byte* lData = lDataFixed + offset;

                        Parallel.For(0, threads,
                            (n) =>
                            {
                                uint[] lTable = table;
                                int lCount = BytesPreThread;
                                uint lRegister = 0;
                                byte* i = lData;

                                if (n == 0)
                                {
                                    lRegister = register;
                                    lCount = firstcounts;
                                }
                                else
                                {
                                    i += (n - 1) * BytesPreThread + firstcounts;
                                }

                                do
                                {
                                    lRegister = (lRegister << 8) ^ lTable[((lRegister >> 24) ^ *i++) & 0xFF];
                                }
                                while (--lCount != 0);

                                registers[n] = lRegister >> (32 - width);
                            }
                        );
                    }
                }

                uint reg = registers[0];
                for (int i = 1; i < threads; i++)
                {
                    reg = MatrixMult(combineMatrix, reg);
                    reg ^= registers[i];
                }

                register = reg << (32 - width);

                return this;
            }

            /// <summary>
            /// Processes message bytes.
            /// </summary>
            /// <param name="data">The data to add to the CRC.</param>
            /// <param name="offset">Location in the array where to start in bytes.</param>
            /// <param name="count">Number of bytes.</param>
            /// <returns>A reference to <b>this</b> instance.</returns>
            public ICRC Add(List<byte> data, int offset = 0, int count = 0)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                if (offset < 0 || offset > data.Count)
                {
                    throw new ArgumentOutOfRangeException("offset", "Must be non-negative and less than or equal to the length of data in bytes.");
                }

                if (count < 0)
                {
                    throw new ArgumentOutOfRangeException("count", "Must be non-negative.");
                }

                if (offset + count > data.Count)
                {
                    throw new ArgumentOutOfRangeException("count", "Must be less than or equal to the length of data in bytes minus the offset argument.");
                }

                if (count == 0)
                {
                    count = data.Count - offset;
                    if (count == 0)
                    {
                        return this;
                    }
                }

                #region Not enough bytes => regular case
                if (count < BytesPreThread)
                {
                    // local copies of values and references make the code much faster
                    uint[] lTable = table;
                    int lCount = count;
                    uint lRegister = register;
                    List<byte> lData = data;
                    int lOffset = offset;

                    do
                    {
                        lRegister = (lRegister << 8) ^ lTable[((lRegister >> 24) ^ lData[lOffset++]) & 0xFF];
                    }
                    while (--lCount != 0);

                    register = lRegister;

                    return this;
                }
                #endregion

                int threads = count / BytesPreThread;
                int firstcounts = count % BytesPreThread;
                if (firstcounts != 0)
                {
                    threads++;
                }
                else
                {
                    firstcounts = BytesPreThread;
                }

                uint[] registers = new uint[threads];

                Parallel.For(0, threads,
                    (n) =>
                    {
                        uint[] lTable = table;
                        int lCount = BytesPreThread;
                        uint lRegister = 0;
                        List<byte> lData = data;
                        int lOffset = offset;

                        if (n == 0)
                        {
                            lRegister = register;
                            lCount = firstcounts;
                        }
                        else
                        {
                            lOffset += (n - 1) * BytesPreThread + firstcounts;
                        }

                        do
                        {
                            lRegister = (lRegister << 8) ^ lTable[((lRegister >> 24) ^ lData[lOffset++]) & 0xFF];
                        }
                        while (--lCount != 0);

                        registers[n] = lRegister >> (32 - width);
                    }
                );

                uint reg = registers[0];
                for (int i = 1; i < threads; i++)
                {
                    reg = MatrixMult(combineMatrix, reg);
                    reg ^= registers[i];
                }

                register = reg << (32 - width);

                return this;
            }

            /// <summary>
            /// Processes message bytes.
            /// </summary>
            /// <param name="data">The data to add to the CRC.</param>
            /// <returns>A reference to <b>this</b> instance.</returns>
            public ICRC Add(IEnumerable<byte> data)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                // local copies of values and references make the code much faster
                IEnumerable<byte> lData = data;
                uint lRegister = register;
                uint[] lTable = table;

                foreach (byte value in lData)
                {
                    lRegister = (lRegister << 8) ^ lTable[((lRegister >> 24) ^ value) & 0xFF];
                }

                register = lRegister;

                return this;
            }

            /// <summary>
            /// Resets the instance to the 'no message bytes processed yet'-state.
            /// </summary>
            public void Reset() { register = init; }
        }

        /// <summary>
        /// Implements <see cref="ICRC"/> and <see cref="ICRC{T}"/> for a maximum 32-bit width CRC for reflected input (LSBit first).
        /// </summary>
        /// <threadsafety static="false" instance="false"/>
        public class ReflectedUInt : ICRC<uint>
        {
            const int BytesPreThread = 4 * 1024; // IMPORTANT: Must be a power-of-two!

            int width;
            uint register, init, xorOutput;
            uint[] table;
            bool reflectOutput;
            uint[] combineMatrix;

            /// <summary>
            /// Creates an instance of a CRC algorithm with the behaviour according to the parameters.
            /// </summary>
            /// <param name="polynomial">The polynomial to use. Unreflected and filled in the least significant bits without the leading 1.</param>
            /// <param name="table">The table generated from the polynomial to use. Use <see cref="GenerateTableReflected(uint, int)"/> with the polynomial and its width to generate the table.</param>
            /// <param name="init">The initial value of the register. Unreflected and filled in the least significant bits.</param>
            /// <param name="refOut">Set <b>true</b>, if register is to be reflected before XORing with <paramref name="xorOut"/> and output.</param>
            /// <param name="xorOut">Value to be XORed with the reflected or unreflected register depending on <paramref name="refOut"/> before output. Filled in the least significant bits.</param>
            /// <param name="width">The width of the polynomial in bits. Must be greater than 0 and less than 33. Default is 32.</param>
            public ReflectedUInt(uint polynomial, uint[] table, uint init, bool refOut, uint xorOut, int width = 32)
            {
                if (width <= 0 || width > 32)
                {
                    throw new ArgumentOutOfRangeException("width", "Must be greater than 0 and less than 33.");
                }

                if (table == null)
                {
                    throw new ArgumentNullException("table");
                }

                if (table.Length < 256)
                {
                    throw new ArgumentOutOfRangeException("table", "Must have at least 256 elements.");
                }

                // Gets value (2^width)-1.
                uint mask = (((1u << (width - 1)) - 1u) << 1) | 1u;

                polynomial = BitOrder.Reflect(polynomial, width) & mask;
                init = BitOrder.Reflect(init, width) & mask;
                xorOut &= mask;

                this.width = width;
                this.table = table;
                register = this.init = init;
                reflectOutput = refOut;
                xorOutput = xorOut;

                #region Create combineMatrix
                uint[] mat1 = new uint[width], mat2 = new uint[width]; // Create matrices (for inplace squaring operation)

                // Fill matrix with 1-bit-shift-operation (bit 1 of register becomes bit 0, bit 2 becomes bit 1 and so on, when multiplied with this matrix)
                for (uint n = 1, row = 1; n < width; n++, row <<= 1)
                {
                    mat1[n] = row;
                }

                mat1[0] = polynomial; // and the polynomial (will be multiplied(XORed) into the register if top-bit (bit 0) is 1, when multiplied with this matrix)

                // Square to create 2-bit-operation matrix
                MatrixSquare(mat2, mat1, width);

                // Square again to create 4-bit-operation matrix
                MatrixSquare(mat1, mat2, width);

                int length = BytesPreThread;
                do
                {
                    // Square again to create the next power-of-two-bytes-operation matrix
                    MatrixSquare(mat2, mat1, width);
                    combineMatrix = mat2;

                    length >>= 1;

                    // Already done?
                    if (length == 0)
                    {
                        break;
                    }

                    // Square again to create the next power-of-two-bytes-operation matrix
                    MatrixSquare(mat1, mat2, width);
                    combineMatrix = mat1;

                    length >>= 1;
                } while (length != 0);
                #endregion
            }

            /// <summary>
            /// Creates an instance of a CRC algorithm with the behaviour according to the parameters.
            /// </summary>
            /// <param name="polynomial">The polynomial to use. Unreflected and filled in the least significant bits without the leading 1.</param>
            /// <param name="init">The initial value of the register. Unreflected and filled in the least significant bits.</param>
            /// <param name="refOut">Set <b>true</b>, if register is to be reflected before XORing with <paramref name="xorOut"/> and output.</param>
            /// <param name="xorOut">Value to be XORed with the reflected or unreflected register depending on <paramref name="refOut"/> before output. Filled in the least significant bits.</param>
            /// <param name="width">The width of the polynomial in bits. Must be greater than 0 and less than 33. Default is 32.</param>
            public ReflectedUInt(uint polynomial, uint init, bool refOut, uint xorOut, int width = 32)
                : this(polynomial, GenerateTableReflected(polynomial, width), init, refOut, xorOut, width) { }

            /// <summary>
            /// Gets/sets the register (not necessarely the CRC value). Can be utilized to cache the value; useful in situations where a chain of blocks need to be checked, and reseting would need to reprocess the whole chain from the start.
            /// </summary>
            public uint Register
            {
                get { return register; }
                set { register = value; }
            }

            /// <summary>
            /// Gets the CRC value (not the register) for the message bytes processed so far.
            /// </summary>
            public uint Value { get { return (reflectOutput ? register : BitOrder.Reflect(register, width)) ^ xorOutput; } }

            /// <summary>
            /// Gets the CRC value (not the register) for the message bytes processed so far as a 32-bit value.
            /// </summary>
            /// <returns>The CRC value as <b>uint</b>.</returns>
            public uint GetCRC() { return Value; }

            /// <summary>
            /// Gets the CRC value (not the register) for the message bytes processed so far as a 64-bit value.
            /// </summary>
            /// <returns>The CRC value as <b>ulong</b>.</returns>
            public ulong GetCRCAsULong() { return Value; }

            /// <summary>
            /// Gets the CRC value (not the register) for the message bytes processed so far as a 128-bit value.
            /// </summary>
            /// <returns>The CRC value as <see cref="UInt128"/>.</returns>
            public UInt128 GetCRCAsUInt128() { return Value; }

            /// <summary>
            /// Processes a single message byte.
            /// </summary>
            /// <param name="value">The value to add to the CRC.</param>
            /// <returns>A reference to <b>this</b> instance.</returns>
            public ICRC Add(byte value)
            {
                register = (register >> 8) ^ table[(register ^ value) & 0xFF];

                return this;
            }

            /// <summary>
            /// Processes message bytes.
            /// </summary>
            /// <param name="data">The data to add to the CRC.</param>
            /// <param name="offset">Location in the array where to start in bytes.</param>
            /// <param name="count">Number of bytes.</param>
            /// <returns>A reference to <b>this</b> instance.</returns>
            public ICRC Add(byte[] data, int offset = 0, int count = 0)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                if (offset < 0 || offset > data.Length)
                {
                    throw new ArgumentOutOfRangeException("offset", "Must be non-negative and less than or equal to the length of data in bytes.");
                }

                if (count < 0)
                {
                    throw new ArgumentOutOfRangeException("count", "Must be non-negative.");
                }

                if (offset + count > data.Length)
                {
                    throw new ArgumentOutOfRangeException("count", "Must be less than or equal to the length of data in bytes minus the offset argument.");
                }

                if (count == 0)
                {
                    count = data.Length - offset;
                    if (count == 0)
                    {
                        return this;
                    }
                }

                #region Not enough bytes => regular case
                if (count < BytesPreThread)
                {
                    // local copies of values and references make the code much faster
                    uint[] lTable = table;
                    int lCount = count;
                    uint lRegister = register;

                    unsafe
                    {
                        fixed (byte* lDataFixed = data)
                        {
                            byte* lData = lDataFixed + offset;
                            do
                            {
                                lRegister = (lRegister >> 8) ^ lTable[(lRegister ^ *lData++) & 0xFF];
                            }
                            while (--lCount != 0);
                        }
                    }

                    register = lRegister;

                    return this;
                }
                #endregion

                int threads = count / BytesPreThread;
                int firstcounts = count % BytesPreThread;
                if (firstcounts != 0)
                {
                    threads++;
                }
                else
                {
                    firstcounts = BytesPreThread;
                }

                uint[] registers = new uint[threads];

                unsafe
                {
                    fixed (byte* lDataFixed = data)
                    {
                        byte* lData = lDataFixed + offset;

                        Parallel.For(0, threads,
                            (n) =>
                            {
                                uint[] lTable = table;
                                int lCount = BytesPreThread;
                                uint lRegister = 0;
                                byte* i = lData;

                                if (n == 0)
                                {
                                    lRegister = register;
                                    lCount = firstcounts;
                                }
                                else
                                {
                                    i += (n - 1) * BytesPreThread + firstcounts;
                                }

                                do
                                {
                                    lRegister = (lRegister >> 8) ^ lTable[(lRegister ^ *i++) & 0xFF];
                                }
                                while (--lCount != 0);

                                registers[n] = lRegister;
                            }
                        );
                    }
                }

                uint reg = registers[0];
                for (int i = 1; i < threads; i++)
                {
                    reg = MatrixMult(combineMatrix, reg);
                    reg ^= registers[i];
                }

                register = reg;

                return this;
            }

            /// <summary>
            /// Processes message bytes.
            /// </summary>
            /// <param name="data">The data to add to the CRC.</param>
            /// <param name="offset">Location in the array where to start in bytes.</param>
            /// <param name="count">Number of bytes.</param>
            /// <returns>A reference to <b>this</b> instance.</returns>
            public ICRC Add(List<byte> data, int offset = 0, int count = 0)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                if (offset < 0 || offset > data.Count)
                {
                    throw new ArgumentOutOfRangeException("offset", "Must be non-negative and less than or equal to the length of data in bytes.");
                }

                if (count < 0)
                {
                    throw new ArgumentOutOfRangeException("count", "Must be non-negative.");
                }

                if (offset + count > data.Count)
                {
                    throw new ArgumentOutOfRangeException("count", "Must be less than or equal to the length of data in bytes minus the offset argument.");
                }

                if (count == 0)
                {
                    count = data.Count - offset;
                    if (count == 0)
                    {
                        return this;
                    }
                }

                #region Not enough bytes => regular case
                if (count < BytesPreThread)
                {
                    // local copies of values and references make the code much faster
                    uint[] lTable = table;
                    int lCount = count;
                    uint lRegister = register;
                    List<byte> lData = data;
                    int lOffset = offset;

                    do
                    {
                        lRegister = (lRegister >> 8) ^ lTable[(lRegister ^ lData[lOffset++]) & 0xFF];
                    }
                    while (--lCount != 0);

                    register = lRegister;

                    return this;
                }
                #endregion

                int threads = count / BytesPreThread;
                int firstcounts = count % BytesPreThread;
                if (firstcounts != 0)
                {
                    threads++;
                }
                else
                {
                    firstcounts = BytesPreThread;
                }

                uint[] registers = new uint[threads];

                Parallel.For(0, threads,
                    (n) =>
                    {
                        uint[] lTable = table;
                        int lCount = BytesPreThread;
                        uint lRegister = 0;
                        List<byte> lData = data;
                        int lOffset = offset;

                        if (n == 0)
                        {
                            lRegister = register;
                            lCount = firstcounts;
                        }
                        else
                        {
                            lOffset += (n - 1) * BytesPreThread + firstcounts;
                        }

                        do
                        {
                            lRegister = (lRegister >> 8) ^ lTable[(lRegister ^ lData[lOffset++]) & 0xFF];
                        }
                        while (--lCount != 0);

                        registers[n] = lRegister;
                    }
                );

                uint reg = registers[0];
                for (int i = 1; i < threads; i++)
                {
                    reg = MatrixMult(combineMatrix, reg);
                    reg ^= registers[i];
                }

                register = reg;

                return this;
            }

            /// <summary>
            /// Processes message bytes.
            /// </summary>
            /// <param name="data">The data to add to the CRC.</param>
            /// <returns>A reference to <b>this</b> instance.</returns>
            public ICRC Add(IEnumerable<byte> data)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                // local copies of values and references make the code much faster
                IEnumerable<byte> lData = data;
                uint lRegister = register;
                uint[] lTable = table;

                foreach (byte value in lData)
                {
                    lRegister = (lRegister >> 8) ^ lTable[(lRegister ^ value) & 0xFF];
                }

                register = lRegister;

                return this;
            }

            /// <summary>
            /// Resets the instance to the 'no message bytes processed yet'-state.
            /// </summary>
            public void Reset() { register = init; }
        }
    }
}
