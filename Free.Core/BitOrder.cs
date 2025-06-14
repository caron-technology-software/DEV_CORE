﻿using System;

namespace Free.Core
{
    /// <summary>
    /// Class for changing the bit-order of values.
    /// </summary>
    /// <threadsafety static="true" instance="true"/>
    public static class BitOrder
    {
        #region ReflectedBytes
        /// <summary>
        /// LookUpTable of reflected bytes. Each element is the reflected value of its array index.
        /// </summary>
        public static readonly byte[] ReflectedBytes = {
                0x00, 0x80, 0x40, 0xc0, 0x20, 0xa0, 0x60, 0xe0,
                0x10, 0x90, 0x50, 0xd0, 0x30, 0xb0, 0x70, 0xf0,
                0x08, 0x88, 0x48, 0xc8, 0x28, 0xa8, 0x68, 0xe8,
                0x18, 0x98, 0x58, 0xd8, 0x38, 0xb8, 0x78, 0xf8,
                0x04, 0x84, 0x44, 0xc4, 0x24, 0xa4, 0x64, 0xe4,
                0x14, 0x94, 0x54, 0xd4, 0x34, 0xb4, 0x74, 0xf4,
                0x0c, 0x8c, 0x4c, 0xcc, 0x2c, 0xac, 0x6c, 0xec,
                0x1c, 0x9c, 0x5c, 0xdc, 0x3c, 0xbc, 0x7c, 0xfc,
                0x02, 0x82, 0x42, 0xc2, 0x22, 0xa2, 0x62, 0xe2,
                0x12, 0x92, 0x52, 0xd2, 0x32, 0xb2, 0x72, 0xf2,
                0x0a, 0x8a, 0x4a, 0xca, 0x2a, 0xaa, 0x6a, 0xea,
                0x1a, 0x9a, 0x5a, 0xda, 0x3a, 0xba, 0x7a, 0xfa,
                0x06, 0x86, 0x46, 0xc6, 0x26, 0xa6, 0x66, 0xe6,
                0x16, 0x96, 0x56, 0xd6, 0x36, 0xb6, 0x76, 0xf6,
                0x0e, 0x8e, 0x4e, 0xce, 0x2e, 0xae, 0x6e, 0xee,
                0x1e, 0x9e, 0x5e, 0xde, 0x3e, 0xbe, 0x7e, 0xfe,
                0x01, 0x81, 0x41, 0xc1, 0x21, 0xa1, 0x61, 0xe1,
                0x11, 0x91, 0x51, 0xd1, 0x31, 0xb1, 0x71, 0xf1,
                0x09, 0x89, 0x49, 0xc9, 0x29, 0xa9, 0x69, 0xe9,
                0x19, 0x99, 0x59, 0xd9, 0x39, 0xb9, 0x79, 0xf9,
                0x05, 0x85, 0x45, 0xc5, 0x25, 0xa5, 0x65, 0xe5,
                0x15, 0x95, 0x55, 0xd5, 0x35, 0xb5, 0x75, 0xf5,
                0x0d, 0x8d, 0x4d, 0xcd, 0x2d, 0xad, 0x6d, 0xed,
                0x1d, 0x9d, 0x5d, 0xdd, 0x3d, 0xbd, 0x7d, 0xfd,
                0x03, 0x83, 0x43, 0xc3, 0x23, 0xa3, 0x63, 0xe3,
                0x13, 0x93, 0x53, 0xd3, 0x33, 0xb3, 0x73, 0xf3,
                0x0b, 0x8b, 0x4b, 0xcb, 0x2b, 0xab, 0x6b, 0xeb,
                0x1b, 0x9b, 0x5b, 0xdb, 0x3b, 0xbb, 0x7b, 0xfb,
                0x07, 0x87, 0x47, 0xc7, 0x27, 0xa7, 0x67, 0xe7,
                0x17, 0x97, 0x57, 0xd7, 0x37, 0xb7, 0x77, 0xf7,
                0x0f, 0x8f, 0x4f, 0xcf, 0x2f, 0xaf, 0x6f, 0xef,
                0x1f, 0x9f, 0x5f, 0xdf, 0x3f, 0xbf, 0x7f, 0xff
            };
        #endregion

        #region Reflect for values
        /// <summary>
        /// Reverses the bit-order of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <returns>The bit-order reversed value.</returns>
        public static byte Reflect(byte value)
        {
            return ReflectedBytes[value];
        }

        /// <summary>
        /// Reverses the bit-order of a value, including the sign-bit.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static sbyte Reflect(sbyte value)
        {
            return (sbyte)Reflect((byte)value);
        }

        /// <summary>
        /// Reverses the bit-order of a value, including the sign-bit.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <returns>The bit-order reversed value.</returns>
        public static short Reflect(short value)
        {
            return (short)Reflect((ushort)value);
        }

        /// <summary>
        /// Reverses the bit-order of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static ushort Reflect(ushort value)
        {
            return (ushort)(ReflectedBytes[value & 0xff] << 8 | (ReflectedBytes[(value >> 8) & 0xff]));
        }

        /// <summary>
        /// Reverses the bit-order of a value, including the sign-bit.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <returns>The bit-order reversed value.</returns>
        public static int Reflect(int value)
        {
            return (int)Reflect((uint)value);
        }

        /// <summary>
        /// Reverses the bit-order of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static uint Reflect(uint value)
        {
            return (uint)(ReflectedBytes[value & 0xff] << 24 | (ReflectedBytes[(value >> 8) & 0xff] << 16) |
                (ReflectedBytes[(value >> 16) & 0xff] << 8) | (ReflectedBytes[(value >> 24) & 0xff]));
        }

        /// <summary>
        /// Reverses the bit-order of a value, including the sign-bit.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <returns>The bit-order reversed value.</returns>
        public static long Reflect(long value)
        {
            return (long)Reflect((ulong)value);
        }

        /// <summary>
        /// Reverses the bit-order of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static ulong Reflect(ulong value)
        {
            uint val = (uint)value;
            uint a = (uint)(ReflectedBytes[val & 0xff] << 24 | (ReflectedBytes[(val >> 8) & 0xff] << 16) |
                (ReflectedBytes[(val >> 16) & 0xff] << 8) | (ReflectedBytes[(val >> 24) & 0xff]));

            val = (uint)(value >> 32);
            return (ulong)a << 32 | (uint)(ReflectedBytes[val & 0xff] << 24 | (ReflectedBytes[(val >> 8) & 0xff] << 16) |
                (ReflectedBytes[(val >> 16) & 0xff] << 8) | (ReflectedBytes[(val >> 24) & 0xff]));
        }

        /// <summary>
        /// Reverses the bit-order of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static UInt128 Reflect(UInt128 value)
        {
            return new UInt128(Reflect(value.Low), Reflect(value.High));
        }
        #endregion

        #region Reflect for parts of values.
        /// <summary>
        /// Reverses the bit-order of a part or all of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <param name="bits">The number of bits to reverse the bit-order of.</param>
        /// <param name="offset">The position of the least-significant-bit.</param>
        /// <returns>The bit-order reversed value.</returns>
        public static byte Reflect(byte value, int bits, int offset = 0)
        {
            if (bits < 0 || bits > 8)
            {
                throw new ArgumentOutOfRangeException("bits", "Must be greater than or equal to zero and less than 9.");
            }

            if (offset < 0 || offset > 7)
            {
                throw new ArgumentOutOfRangeException("offset", "Must be greater than or equal to zero and less than 8.");
            }

            if (offset + bits > 8)
            {
                throw new ArgumentOutOfRangeException("bits+offset", "The sum of bits and offset must be less than 9.");
            }

            if (bits == 0 || bits == 1)
            {
                return value;
            }

            byte val = (byte)(value >> offset);
            byte mask = (byte)(1 << (bits + offset - 1));
            for (int i = 0; i < bits; i++)
            {
                if ((val & 0x1) != 0)
                {
                    value |= mask;
                }
                else
                {
                    value &= (byte)(~mask);
                }

                val >>= 1; mask >>= 1;
            }
            return value;
        }

        /// <summary>
        /// Reverses the bit-order of a part or all of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <param name="bits">The number of bits to reverse the bit-order of.</param>
        /// <param name="offset">The position of the least-significant-bit.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static sbyte Reflect(sbyte value, int bits, int offset = 0)
        {
            return (sbyte)Reflect((byte)value, bits, offset);
        }

        /// <summary>
        /// Reverses the bit-order of a part or all of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <param name="bits">The number of bits to reverse the bit-order of.</param>
        /// <param name="offset">The position of the least-significant-bit.</param>
        /// <returns>The bit-order reversed value.</returns>
        public static short Reflect(short value, int bits, int offset = 0)
        {
            return (short)Reflect((ushort)value, bits, offset);
        }

        /// <summary>
        /// Reverses the bit-order of a part or all of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <param name="bits">The number of bits to reverse the bit-order of.</param>
        /// <param name="offset">The position of the least-significant-bit.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static ushort Reflect(ushort value, int bits, int offset = 0)
        {
            if (bits < 0 || bits > 16)
            {
                throw new ArgumentOutOfRangeException("bits", "Must be greater than or equal to zero and less than 17.");
            }

            if (offset < 0 || offset > 15)
            {
                throw new ArgumentOutOfRangeException("offset", "Must be greater than or equal to zero and less than 16.");
            }

            if (offset + bits > 16)
            {
                throw new ArgumentOutOfRangeException("bits+offset", "The sum of bits and offset must be less than 17.");
            }

            if (bits == 0 || bits == 1)
            {
                return value;
            }

            ushort val = (ushort)(value >> offset);
            ushort mask = (ushort)(1 << (bits + offset - 1));
            for (int i = 0; i < bits; i++)
            {
                if ((val & 0x1) != 0)
                {
                    value |= mask;
                }
                else
                {
                    value &= (ushort)(~mask);
                }

                val >>= 1; mask >>= 1;
            }
            return value;
        }

        /// <summary>
        /// Reverses the bit-order of a part or all of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <param name="bits">The number of bits to reverse the bit-order of.</param>
        /// <param name="offset">The position of the least-significant-bit.</param>
        /// <returns>The bit-order reversed value.</returns>
        public static int Reflect(int value, int bits, int offset = 0)
        {
            return (int)Reflect((uint)value, bits, offset);
        }

        /// <summary>
        /// Reverses the bit-order of a part or all of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <param name="bits">The number of bits to reverse the bit-order of.</param>
        /// <param name="offset">The position of the least-significant-bit.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static uint Reflect(uint value, int bits, int offset = 0)
        {
            if (bits < 0 || bits > 32)
            {
                throw new ArgumentOutOfRangeException("bits", "Must be greater than or equal to zero and less than 33.");
            }

            if (offset < 0 || offset > 31)
            {
                throw new ArgumentOutOfRangeException("offset", "Must be greater than or equal to zero and less than 32.");
            }

            if (offset + bits > 32)
            {
                throw new ArgumentOutOfRangeException("bits+offset", "The sum of bits and offset must be less than 33.");
            }

            if (bits == 0 || bits == 1)
            {
                return value;
            }

            uint val = value >> offset;
            uint mask = 1u << (bits + offset - 1);
            for (int i = 0; i < bits; i++)
            {
                if ((val & 0x1) != 0)
                {
                    value |= mask;
                }
                else
                {
                    value &= ~mask;
                }

                val >>= 1; mask >>= 1;
            }
            return value;
        }

        /// <summary>
        /// Reverses the bit-order of a part or all of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <param name="bits">The number of bits to reverse the bit-order of.</param>
        /// <param name="offset">The position of the least-significant-bit.</param>
        /// <returns>The bit-order reversed value.</returns>
        public static long Reflect(long value, int bits, int offset = 0)
        {
            return (long)Reflect((ulong)value, bits, offset);
        }

        /// <summary>
        /// Reverses the bit-order of a part or all of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <param name="bits">The number of bits to reverse the bit-order of.</param>
        /// <param name="offset">The position of the least-significant-bit.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static ulong Reflect(ulong value, int bits, int offset = 0)
        {
            if (bits < 0 || bits > 64)
            {
                throw new ArgumentOutOfRangeException("bits", "Must be greater than or equal to zero and less than 65.");
            }

            if (offset < 0 || offset > 63)
            {
                throw new ArgumentOutOfRangeException("offset", "Must be greater than or equal to zero and less than 64.");
            }

            if (offset + bits > 64)
            {
                throw new ArgumentOutOfRangeException("bits+offset", "The sum of bits and offset must be less than 65.");
            }

            if (bits == 0 || bits == 1)
            {
                return value;
            }

            ulong val = value >> offset;
            ulong mask = 1ul << (bits + offset - 1);
            for (int i = 0; i < bits; i++)
            {
                if ((val & 0x1) != 0)
                {
                    value |= mask;
                }
                else
                {
                    value &= ~mask;
                }

                val >>= 1; mask >>= 1;
            }
            return value;
        }

        /// <summary>
        /// Reverses the bit-order of a part or all of a value.
        /// </summary>
        /// <param name="value">The value which bit-order is to be reversed.</param>
        /// <param name="bits">The number of bits to reverse the bit-order of.</param>
        /// <param name="offset">The position of the least-significant-bit.</param>
        /// <returns>The bit-order reversed value.</returns>
        [CLSCompliant(false)]
        public static UInt128 Reflect(UInt128 value, int bits, int offset = 0)
        {
            if (bits < 0 || bits > 128)
            {
                throw new ArgumentOutOfRangeException("bits", "Must be greater than or equal to zero and less than 129.");
            }

            if (offset < 0 || offset > 127)
            {
                throw new ArgumentOutOfRangeException("offset", "Must be greater than or equal to zero and less than 128.");
            }

            if (offset + bits > 128)
            {
                throw new ArgumentOutOfRangeException("bits+offset", "The sum of bits and offset must be less than 129.");
            }

            if (bits == 0 || bits == 1)
            {
                return value;
            }

            UInt128 val = value >> offset;
            UInt128 mask = ((UInt128)1) << (bits + offset - 1);
            for (int i = 0; i < bits; i++)
            {
                if ((val & 0x1) != 0)
                {
                    value |= mask;
                }
                else
                {
                    value &= ~mask;
                }

                val >>= 1; mask >>= 1;
            }
            return value;
        }
        #endregion
    }
}
