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
using System.Globalization;
using System.IO;
using System.Text;

namespace HotChai.Serialization.Json
{
    [Flags]
    public enum JsonFormatOptions
    {
        None = 0,
        MemberNewlines = 1,
    }

    public sealed class JsonObjectWriter : ObjectWriter
    {
        private readonly InspectorStream _stream;
        private readonly StreamWriter _writer;

        public JsonObjectWriter(
            Stream stream,
            JsonFormatOptions formatOptions = JsonFormatOptions.None)
        {
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            this._stream = new InspectorStream(stream);
            this._writer = new StreamWriter(
                this._stream,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true));
            this.FormatOptions = formatOptions;
        }

        public JsonFormatOptions FormatOptions
        {
            get;
            set;
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
            Write('{');
        }

        protected override void WriteStartMemberToken(
            int memberKey)
        {
            if (this.FormatOptions.HasFlag(JsonFormatOptions.MemberNewlines))
            {
                Write("\r\n");
            }
            Write('"');
            Write(memberKey.ToString(CultureInfo.InvariantCulture));
            Write("\":");
        }

        protected override void WriteEndMemberToken()
        {
        }

        protected override void WriteEndObjectToken()
        {
            Write('}');
        }

        protected override void WriteStartArrayToken()
        {
            Write('[');
        }

        protected override void WriteEndArrayToken()
        {
            Write(']');
        }

        protected override void WritePrimitiveNullValue()
        {
            Write("null");
        }

        protected override void WritePrimitiveValue(
            bool value)
        {
            if (value)
            {
                Write("true");
            }
            else
            {
                Write("false");
            }
        }

        protected override void WritePrimitiveValue(
            int value)
        {
            Write(value.ToString(CultureInfo.InvariantCulture));
        }

        protected override void WritePrimitiveValue(
            uint value)
        {
            Write(value.ToString(CultureInfo.InvariantCulture));
        }

        protected override void WritePrimitiveValue(
            long value)
        {
            Write(value.ToString(CultureInfo.InvariantCulture));
        }

        protected override void WritePrimitiveValue(
            ulong value)
        {
            Write(value.ToString(CultureInfo.InvariantCulture));
        }

        protected override void WritePrimitiveValue(
            float value)
        {
            Write(value.ToString("R"));
        }

        protected override void WritePrimitiveValue(
            double value)
        {
            Write(value.ToString("R"));
        }

        protected override void WritePrimitiveValue(
            byte[] value)
        {
            if (null == value)
            {
                WritePrimitiveNullValue();
            }
            else
            {
                // TODO: Efficient Base64 stream encoding directly to BinaryWriter
                string encodedString = Convert.ToBase64String(value);

                Write('"');
                Write(encodedString);
                Write('"');
            }
        }

#if NET5_0_OR_GREATER
        protected override void WritePrimitiveValue(
            ReadOnlySpan<byte> value)
        {
            if (null == value)
            {
                WritePrimitiveNullValue();
            }
            else
            {
                // TODO: Efficient Base64 stream encoding directly to BinaryWriter
                string encodedString = Convert.ToBase64String(value);

                Write('"');
                Write(encodedString);
                Write('"');
            }
        }
#endif

        protected override void WritePrimitiveValue(
            string value)
        {
            if (null == value)
            {
                WritePrimitiveNullValue();
            }
            else
            {
                Write('"');
                foreach (char c in value)
                {
                    if (c == '\\')
                    {
                        Write(@"\\");
                    }
                    else if (c == '"')
                    {
                        Write("\\\"");
                    }
                    else if (c == '\b')
                    {
                        Write(@"\b");
                    }
                    else if (c == '\f')
                    {
                        Write(@"\f");
                    }
                    else if (c == '\n')
                    {
                        Write(@"\n");
                    }
                    else if (c == '\r')
                    {
                        Write(@"\r");
                    }
                    else if (c == '\t')
                    {
                        Write(@"\t");
                    }
                    else if (Char.IsControl(c))
                    {
                        Write(@"\u");
                        Write(((ushort)c).ToString("x4"));
                    }
                    else
                    {
                        Write(c);
                    }
                }
                Write('"');
            }
        }

        public override void Flush()
        {
            this._writer.Flush();
        }

        protected override void WriteArrayValueSeparator()
        {
            Write(',');
        }

        protected override void WriteMemberSeparator()
        {
            Write(',');
        }

        private void Write(
            char value)
        {
            this._writer.Write(value);
        }

        private void Write(
            string value)
        {
            this._writer.Write(value);
        }
    }
}
