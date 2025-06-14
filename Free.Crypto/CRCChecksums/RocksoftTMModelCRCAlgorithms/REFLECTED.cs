﻿using System;
using System.Collections.Generic;
using Free.Core;

namespace Free.Crypto.CRCChecksums.RocksoftTMModelCRCAlgorithms
{
    /// <summary>
    /// Implements the REFLECTED algorithm as described in
    /// "A Painless Guide to CRC Error Detection Algorithms" (see crc_v3.txt) by Ross Williams.
    /// </summary>
    /// <remarks>
    /// The functions in this class represent an implementation that could be defined via the
    /// "Rocksoft^tm Model CRC Algorithm" parameter:
    /// <list type="table">
    ///  <listheader><term>Parameter</term><description>Value</description></listheader>
    ///  <item><term>Name</term><description>Not needed.</description></item>
    ///  <item><term>Width</term><description>1-64 (depending on the type of polynomial argument)</description></item>
    ///  <item><term>Poly</term><description>Any 1-64 bit polynomial.</description></item>
    ///  <item><term>Init</term><description>0</description></item>
    ///  <item><term>RefIn</term><description>true</description></item>
    ///  <item><term>RefOut</term><description>true</description></item>
    ///  <item><term>XorOut</term><description>0</description></item>
    ///  <item><term>Check</term><description>Depends on the polynomial.</description></item>
    /// </list>
    /// </remarks>
    /// <threadsafety static="true" instance="true"/>
    [CLSCompliant(false)]
    [Obsolete("This class is for educational purposes only. You should use the high-performance implementations in the class CRC.")]
    public static class REFLECTED
    {
        #region Generate Table
        /// <summary>
        /// Generates the table for 1-8 bit (w/o the leading 1) polynomials.
        /// </summary>
        /// <param name="polynomial">The polynomial the table is to generate for. Bits must be in the most significant bits.</param>
        /// <returns>The table.</returns>
        public static byte[] GenTable(byte polynomial)
        {
            polynomial = BitOrder.Reflect(polynomial);

            byte[] ret = new byte[256];

            byte i = 255;
            do
            {
                byte register = i;

                for (int a = 7; a >= 0; a--)
                {
                    bool pop = (register & 0x1) != 0;
                    register = (byte)(register >> 1);
                    if (pop)
                    {
                        register ^= polynomial;
                    }
                }

                ret[i] = register;
            } while ((i--) != 0);

            return ret;
        }

        /// <summary>
        /// Generates the table for 1-16 bit (w/o the leading 1) polynomials.
        /// </summary>
        /// <param name="polynomial">The polynomial the table is to generate for. Bits must be in the most significant bits.</param>
        /// <returns>The table.</returns>
        public static ushort[] GenTable(ushort polynomial)
        {
            polynomial = BitOrder.Reflect(polynomial);

            ushort[] ret = new ushort[256];

            ushort i = 255;
            do
            {
                ushort register = i;

                for (int a = 7; a >= 0; a--)
                {
                    bool pop = (register & 0x1) != 0;
                    register = (ushort)(register >> 1);
                    if (pop)
                    {
                        register ^= polynomial;
                    }
                }

                ret[i] = register;
            } while ((i--) != 0);

            return ret;
        }

        /// <summary>
        /// Generates the table for 1-32 bit (w/o the leading 1) polynomials.
        /// </summary>
        /// <param name="polynomial">The polynomial the table is to generate for. Bits must be in the most significant bits.</param>
        /// <returns>The table.</returns>
        public static uint[] GenTable(uint polynomial)
        {
            polynomial = BitOrder.Reflect(polynomial);

            uint[] ret = new uint[256];

            uint i = 255;
            do
            {
                uint register = i;

                for (int a = 7; a >= 0; a--)
                {
                    bool pop = (register & 0x1) != 0;
                    register = register >> 1;
                    if (pop)
                    {
                        register ^= polynomial;
                    }
                }

                ret[i] = register;
            } while ((i--) != 0);

            return ret;
        }

        /// <summary>
        /// Generates the table for 1-64 bit (w/o the leading 1) polynomials.
        /// </summary>
        /// <param name="polynomial">The polynomial the table is to generate for. Bits must be in the most significant bits.</param>
        /// <returns>The table.</returns>
        public static ulong[] GenTable(ulong polynomial)
        {
            polynomial = BitOrder.Reflect(polynomial);

            ulong[] ret = new ulong[256];

            uint i = 255;
            do
            {
                ulong register = i;

                for (int a = 7; a >= 0; a--)
                {
                    bool pop = (register & 0x1) != 0;
                    register = register >> 1;
                    if (pop)
                    {
                        register ^= polynomial;
                    }
                }

                ret[i] = register;
            } while ((i--) != 0);

            return ret;
        }
        #endregion

        /// <summary>
        /// Calculates the CRC of bits stored in a <see cref="Byte"/>[].
        /// The "'Reflected' Table-Driven Implementations". (see chapter 11 for explantion)
        /// </summary>
        /// <param name="polynomial">The polynomial to use. Bits must be in the most significant bits. (unreflected)</param>
        /// <param name="data">The bits. (reflected; Least significant bit of the first byte first (starting at <paramref name="offset"/>.)</param>
        /// <param name="offset">Location in the array where to start in bytes.</param>
        /// <param name="count">Number of bytes.</param>
        /// <returns>The CRC value, filled reflected in the least significant bits.</returns>
        public static byte Get(byte polynomial, IList<byte> data, int offset = 0, int count = 0)
        {
            if (polynomial == 0)
            {
                throw new ArgumentOutOfRangeException("polynomial", "Must not be 0.");
            }

            return Get(GenTable(polynomial), data, offset, count);
        }

        /// <summary>
        /// Calculates the CRC of bits stored in a <see cref="Byte"/>[].
        /// The "'Reflected' Table-Driven Implementations". (see chapter 11 for explantion)
        /// </summary>
        /// <param name="reflectedTable">The table generated from the polynomial to use.</param>
        /// <param name="data">The bits. (reflected; Least significant bit of the first byte first (starting at <paramref name="offset"/>.)</param>
        /// <param name="offset">Location in the array where to start in bytes.</param>
        /// <param name="count">Number of bytes.</param>
        /// <returns>The CRC value, filled reflected in the least significant bits.</returns>
        public static byte Get(byte[] reflectedTable, IList<byte> data, int offset = 0, int count = 0)
        {
            if (reflectedTable == null)
            {
                throw new ArgumentNullException("reflectedTable");
            }

            if (reflectedTable.Length < 256)
            {
                throw new ArgumentOutOfRangeException("reflectedTable", "Must have at least 256 elements.");
            }

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
                    return 0;
                }
            }

            int endOfByteStream = offset + count;

            byte register = 0;

            while (offset < endOfByteStream)
            {
                register = reflectedTable[(register ^ data[offset++]) & 0xFF];
            }

            return register;
        }

        /// <summary>
        /// Calculates the CRC of bits stored in a <see cref="Byte"/>[].
        /// The "'Reflected' Table-Driven Implementations". (see chapter 11 for explantion)
        /// </summary>
        /// <param name="polynomial">The polynomial to use. Bits must be in the most significant bits. (unreflected)</param>
        /// <param name="data">The bits. (reflected; Least significant bit of the first byte first (starting at <paramref name="offset"/>.)</param>
        /// <param name="offset">Location in the array where to start in bytes.</param>
        /// <param name="count">Number of bytes.</param>
        /// <returns>The CRC value, filled reflected in the least significant bits.</returns>
        public static ushort Get(ushort polynomial, IList<byte> data, int offset = 0, int count = 0)
        {
            if (polynomial == 0)
            {
                throw new ArgumentOutOfRangeException("polynomial", "Must not be 0.");
            }

            return Get(GenTable(polynomial), data, offset, count);
        }

        /// <summary>
        /// Calculates the CRC of bits stored in a <see cref="Byte"/>[].
        /// The "'Reflected' Table-Driven Implementations". (see chapter 11 for explantion)
        /// </summary>
        /// <param name="reflectedTable">The table generated from the polynomial to use.</param>
        /// <param name="data">The bits. (reflected; Least significant bit of the first byte first (starting at <paramref name="offset"/>.)</param>
        /// <param name="offset">Location in the array where to start in bytes.</param>
        /// <param name="count">Number of bytes.</param>
        /// <returns>The CRC value, filled reflected in the least significant bits.</returns>
        public static ushort Get(ushort[] reflectedTable, IList<byte> data, int offset = 0, int count = 0)
        {
            if (reflectedTable == null)
            {
                throw new ArgumentNullException("reflectedTable");
            }

            if (reflectedTable.Length < 256)
            {
                throw new ArgumentOutOfRangeException("reflectedTable", "Must have at least 256 elements.");
            }

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
                    return 0;
                }
            }

            int endOfByteStream = offset + count;

            ushort register = 0;

            while (offset < endOfByteStream)
            {
                register = (ushort)((register >> 8) ^ reflectedTable[(register ^ data[offset++]) & 0xFF]);
            }

            return register;
        }

        /// <summary>
        /// Calculates the CRC of bits stored in a <see cref="Byte"/>[].
        /// The "'Reflected' Table-Driven Implementations". (see chapter 11 for explantion)
        /// </summary>
        /// <param name="polynomial">The polynomial to use. Bits must be in the most significant bits. (unreflected)</param>
        /// <param name="data">The bits. (reflected; Least significant bit of the first byte first (starting at <paramref name="offset"/>.)</param>
        /// <param name="offset">Location in the array where to start in bytes.</param>
        /// <param name="count">Number of bytes.</param>
        /// <returns>The CRC value, filled reflected in the least significant bits.</returns>
        public static uint Get(uint polynomial, IList<byte> data, int offset = 0, int count = 0)
        {
            if (polynomial == 0)
            {
                throw new ArgumentOutOfRangeException("polynomial", "Must not be 0.");
            }

            return Get(GenTable(polynomial), data, offset, count);
        }

        /// <summary>
        /// Calculates the CRC of bits stored in a <see cref="Byte"/>[].
        /// The "'Reflected' Table-Driven Implementations". (see chapter 11 for explantion)
        /// </summary>
        /// <param name="reflectedTable">The table generated from the polynomial to use.</param>
        /// <param name="data">The bits. (reflected; Least significant bit of the first byte first (starting at <paramref name="offset"/>.)</param>
        /// <param name="offset">Location in the array where to start in bytes.</param>
        /// <param name="count">Number of bytes.</param>
        /// <returns>The CRC value, filled reflected in the least significant bits.</returns>
        public static uint Get(uint[] reflectedTable, IList<byte> data, int offset = 0, int count = 0)
        {
            if (reflectedTable == null)
            {
                throw new ArgumentNullException("reflectedTable");
            }

            if (reflectedTable.Length < 256)
            {
                throw new ArgumentOutOfRangeException("reflectedTable", "Must have at least 256 elements.");
            }

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
                    return 0;
                }
            }

            int endOfByteStream = offset + count;

            uint register = 0;

            while (offset < endOfByteStream)
            {
                register = (register >> 8) ^ reflectedTable[(register ^ data[offset++]) & 0xFF];
            }

            return register;
        }

        /// <summary>
        /// Calculates the CRC of bits stored in a <see cref="Byte"/>[].
        /// The "'Reflected' Table-Driven Implementations". (see chapter 11 for explantion)
        /// </summary>
        /// <param name="polynomial">The polynomial to use. Bits must be in the most significant bits. (unreflected)</param>
        /// <param name="data">The bits. (reflected; Least significant bit of the first byte first (starting at <paramref name="offset"/>.)</param>
        /// <param name="offset">Location in the array where to start in bytes.</param>
        /// <param name="count">Number of bytes.</param>
        /// <returns>The CRC value, filled reflected in the least significant bits.</returns>
        public static ulong Get(ulong polynomial, IList<byte> data, int offset = 0, int count = 0)
        {
            if (polynomial == 0)
            {
                throw new ArgumentOutOfRangeException("polynomial", "Must not be 0.");
            }

            return Get(GenTable(polynomial), data, offset, count);
        }

        /// <summary>
        /// Calculates the CRC of bits stored in a <see cref="Byte"/>[].
        /// The "'Reflected' Table-Driven Implementations". (see chapter 11 for explantion)
        /// </summary>
        /// <param name="reflectedTable">The table generated from the polynomial to use.</param>
        /// <param name="data">The bits. (reflected; Least significant bit of the first byte first (starting at <paramref name="offset"/>.)</param>
        /// <param name="offset">Location in the array where to start in bytes.</param>
        /// <param name="count">Number of bytes.</param>
        /// <returns>The CRC value, filled reflected in the least significant bits.</returns>
        public static ulong Get(ulong[] reflectedTable, IList<byte> data, int offset = 0, int count = 0)
        {
            if (reflectedTable == null)
            {
                throw new ArgumentNullException("reflectedTable");
            }

            if (reflectedTable.Length < 256)
            {
                throw new ArgumentOutOfRangeException("reflectedTable", "Must have at least 256 elements.");
            }

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
                    return 0;
                }
            }

            int endOfByteStream = offset + count;

            ulong register = 0;

            while (offset < endOfByteStream)
            {
                register = (register >> 8) ^ reflectedTable[(register ^ data[offset++]) & 0xFF];
            }

            return register;
        }
    }
}
