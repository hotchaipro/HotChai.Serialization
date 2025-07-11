﻿#region License
// Copyright (c) 2014, David Taylor
//
// Permission to use, copy, modify, and/or distribute this software for any 
// purpose with or without fee is hereby granted, provided that the above 
// copyright notice and this permission notice appear in all copies, unless 
// such copies are solely in the form of machine-executable object code 
// generated by a source language processor.
//
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES 
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR 
// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES 
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN 
// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF 
// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
#endregion License
#nullable enable
using System;
#if NET5_0_OR_GREATER
using System.Buffers.Binary;
#endif
using System.IO;
using System.Text;

namespace HotChai.Serialization.PortableBinary
{
    public sealed class PortableBinaryObjectWriter : ObjectWriter
    {
        private readonly InspectorStream _stream;
        private readonly BinaryWriter _writer;

        public PortableBinaryObjectWriter(
            Stream stream)
        {
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            this._stream = new InspectorStream(stream);
            this._writer = new BinaryWriter(this._stream);
        }

        public override ISerializationInspector? Inspector
        {
            get
            {
                return this._stream.Inspector;
            }

            set
            {
                this._stream.Inspector = value;
            }
        }

        protected override void WriteStartObjectToken()
        {
            WriteToken(PortableBinaryToken.StartObjectToken);
        }

        protected override void WriteStartMemberToken(
            int memberKey)
        {
            if (memberKey <= 0)
            {
                throw new ArgumentOutOfRangeException("memberKey", "Member key must be a positive integer.");
            }

            WritePackedInt(memberKey);
        }

        protected override void WriteEndMemberToken()
        {
            // No end member in this encoding
        }

        protected override void WriteEndObjectToken()
        {
            WriteToken(PortableBinaryToken.EndObjectToken);
        }

        protected override void WriteStartArrayToken()
        {
            WriteToken(PortableBinaryToken.StartArrayToken);
        }

        protected override void WriteEndArrayToken()
        {
            WriteToken(PortableBinaryToken.EndArrayToken);
        }

        protected override void WritePrimitiveNullValue()
        {
            WriteToken(PortableBinaryToken.NullValueToken);
        }

        protected override void WritePrimitiveValue(
            bool value)
        {
            if (value)
            {
                WriteToken(PortableBinaryToken.TrueValueToken);
            }
            else
            {
                WriteToken(PortableBinaryToken.FalseValueToken);
            }
        }

        protected override void WritePrimitiveValue(
            int value)
        {
            // The most significant bit is used for the sign

            byte sign = 0;
            if (value < 0)
            {
                sign = 0x80;
                value = ~value;
            }

            // Remainder of value is the base 256 encoded absolute value (big-endian)

            if (value <= 0x7f)
            {
                WriteLength(1);
                this._writer.Write((byte)(value | sign));
            }
            else if (value <= 0x7fff)
            {
                WriteLength(2);
                this._writer.Write((byte)((value >> 8) | sign));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0x7fffff)
            {
                WriteLength(3);
                this._writer.Write((byte)((value >> 16) | sign));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else
            {
                WriteLength(4);
                this._writer.Write((byte)((value >> 24) | sign));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
        }

        protected override void WritePrimitiveValue(
            uint value)
        {
            // Base 256 encoded value (big-endian)

            if (value <= 0xff)
            {
                WriteLength(1);
                this._writer.Write((byte)(value));
            }
            else if (value <= 0xffff)
            {
                WriteLength(2);
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0xffffff)
            {
                WriteLength(3);
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else
            {
                WriteLength(4);
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
        }

        protected override void WritePrimitiveValue(
            long value)
        {
            // The most significant bit is used for the sign

            byte sign = 0;
            if (value < 0)
            {
                sign = 0x80;
                value = ~value;
            }

            // Remainder of value is the base 256 encoded absolute value (big-endian)

            if (value <= 0x7f)
            {
                WriteLength(1);
                this._writer.Write((byte)(value | sign));
            }
            else if (value <= 0x7fff)
            {
                WriteLength(2);
                this._writer.Write((byte)((value >> 8) | sign));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0x7fffff)
            {
                WriteLength(3);
                this._writer.Write((byte)((value >> 16) | sign));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0x7fffffff)
            {
                WriteLength(4);
                this._writer.Write((byte)((value >> 24) | sign));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0x7fffffffff)
            {
                WriteLength(5);
                this._writer.Write((byte)((value >> 32) | sign));
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0x7fffffffffff)
            {
                WriteLength(6);
                this._writer.Write((byte)((value >> 40) | sign));
                this._writer.Write((byte)(value >> 32));
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0x7fffffffffffff)
            {
                WriteLength(7);
                this._writer.Write((byte)((value >> 48) | sign));
                this._writer.Write((byte)(value >> 40));
                this._writer.Write((byte)(value >> 32));
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else
            {
                WriteLength(8);
                this._writer.Write((byte)((value >> 56) | sign));
                this._writer.Write((byte)(value >> 48));
                this._writer.Write((byte)(value >> 40));
                this._writer.Write((byte)(value >> 32));
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
        }

        protected override void WritePrimitiveValue(
            ulong value)
        {
            // Base 256 encoded value (big-endian)

            if (value <= 0xff)
            {
                WriteLength(1);
                this._writer.Write((byte)(value));
            }
            else if (value <= 0xffff)
            {
                WriteLength(2);
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0xffffff)
            {
                WriteLength(3);
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0xffffffff)
            {
                WriteLength(4);
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0x7fffffffff)
            {
                WriteLength(5);
                this._writer.Write((byte)(value >> 32));
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0x7fffffffffff)
            {
                WriteLength(6);
                this._writer.Write((byte)(value >> 40));
                this._writer.Write((byte)(value >> 32));
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else if (value <= 0x7fffffffffffff)
            {
                WriteLength(7);
                this._writer.Write((byte)(value >> 48));
                this._writer.Write((byte)(value >> 40));
                this._writer.Write((byte)(value >> 32));
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
            else
            {
                WriteLength(8);
                this._writer.Write((byte)(value >> 56));
                this._writer.Write((byte)(value >> 48));
                this._writer.Write((byte)(value >> 40));
                this._writer.Write((byte)(value >> 32));
                this._writer.Write((byte)(value >> 24));
                this._writer.Write((byte)(value >> 16));
                this._writer.Write((byte)(value >> 8));
                this._writer.Write((byte)(value));
            }
        }

#if NET5_0_OR_GREATER
        protected override void WritePrimitiveValue(
            float value)
        {
            Span<byte> bytes = stackalloc byte[4];
            BinaryPrimitives.TryWriteSingleBigEndian(bytes, value);
            WriteLength(bytes.Length);
            if (bytes.Length > 0)
            {
                this._writer.Write(bytes);
            }
        }

        protected override void WritePrimitiveValue(
            double value)
        {
            Span<byte> bytes = stackalloc byte[8];
            BinaryPrimitives.TryWriteDoubleBigEndian(bytes, value);
            WriteLength(bytes.Length);
            if (bytes.Length > 0)
            {
                this._writer.Write(bytes);
            }
        }
#else
        protected override void WritePrimitiveValue(
            float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                // Convert to network order (big-endian)
                Array.Reverse(bytes);
            }
            WriteLength(bytes.Length);
            if (bytes.Length > 0)
            {
                this._writer.Write(bytes, 0, bytes.Length);
            }
        }

        protected override void WritePrimitiveValue(
            double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                // Convert to network order (big-endian)
                Array.Reverse(bytes);
            }
            WriteLength(bytes.Length);
            if (bytes.Length > 0)
            {
                this._writer.Write(bytes, 0, bytes.Length);
            }
        }
#endif

        protected override void WritePrimitiveValue(
            byte[] value)
        {
            if (null == value)
            {
                WriteToken(PortableBinaryToken.NullValueToken);
            }
            else
            {
                WriteLength(value.Length);
                if (value.Length > 0)
                {
                    this._writer.Write(value, 0, value.Length);
                }
            }
        }

#if NET5_0_OR_GREATER
        protected override void WritePrimitiveValue(
            ReadOnlySpan<byte> value)
        {
            WriteLength(value.Length);
            if (value.Length > 0)
            {
                this._writer.Write(value);
            }
        }
#endif

        protected override void WritePrimitiveValue(
            string value)
        {
            if (null == value)
            {
                WriteToken(PortableBinaryToken.NullValueToken);
            }
            else
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                WriteLength(bytes.Length);
                if (bytes.Length > 0)
                {
                    this._writer.Write(bytes, 0, bytes.Length);
                }
            }
        }

        public override void Flush()
        {
            this._writer.Flush();
        }

        private void WriteToken(
            int token)
        {
            if (token >= 0)
            {
                throw new ArgumentOutOfRangeException("token", "Token value must be a negative integer.");
            }

            WritePackedInt(token);
        }

        private void WriteLength(
            int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Length must be a non-negative integer.");
            }

            WritePackedInt(length);
        }

        /// <summary>
        /// Big-endian variable-length quantity (VLQ) encoding
        /// </summary>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Variable-length_quantity.
        /// </remarks>
        private void WritePackedInt(
            int value)
        {
            byte sign = 0;

            // Bit 6 contains the sign
            if (value < 0)
            {
                // Set the sign bit
                sign = 0x40;

                // Negate the value (so it is non-negative)
                value = ~value;
            }

            // Remainder of value is the base 128 encoded absolute value (big-endian),
            // with the MSB set as a continuation bit.

            if (value <= 0x3f)
            {
                this._writer.Write((byte)(value | sign));
            }
            else if (value <= 0x1fff)
            {
                this._writer.Write((byte)(0x80 | (value >> 7) | sign));
                this._writer.Write((byte)(value & 0x7f));
            }
            else if (value <= 0xfffff)
            {
                this._writer.Write((byte)(0x80 | (value >> 14) | sign));
                this._writer.Write((byte)(0x80 | value >> 7));
                this._writer.Write((byte)(value & 0x7f));
            }
            else if (value <= 0x7ffffff)
            {
                this._writer.Write((byte)(0x80 | (value >> 21) | sign));
                this._writer.Write((byte)(0x80 | value >> 14));
                this._writer.Write((byte)(0x80 | value >> 7));
                this._writer.Write((byte)(value & 0x7f));
            }
            else
            {
                this._writer.Write((byte)(0x80 | (value >> 28) | sign));
                this._writer.Write((byte)(0x80 | value >> 21));
                this._writer.Write((byte)(0x80 | value >> 14));
                this._writer.Write((byte)(0x80 | value >> 7));
                this._writer.Write((byte)(value & 0x7f));
            }
        }
    }
}
