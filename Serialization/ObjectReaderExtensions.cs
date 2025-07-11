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
using System.Collections.Generic;

namespace HotChai.Serialization
{
    public static class ObjectReaderExtensions
    {
        #region Object members enumerator

        public sealed class ObjectReaderExtension
        {
            private readonly IObjectReader _reader;

            internal ObjectReaderExtension(
                IObjectReader reader)
            {
                if (null == reader)
                {
                    throw new ArgumentNullException("reader");
                }

                this._reader = reader;
            }

            public IEnumerable<IObjectReader> Members
            {
                get
                {
                    return new ObjectMemberCollection(this._reader);
                }
            }
        }

        public static ObjectReaderExtension? GetObject(
            this IObjectReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (!reader.ReadStartObject())
            {
                return null;
            }

            return new ObjectReaderExtension(reader);
        }

        internal sealed class ObjectMemberCollection : IEnumerable<IObjectReader>, IEnumerator<IObjectReader>
        {
            private readonly IObjectReader _reader;

            internal ObjectMemberCollection(
                IObjectReader reader)
            {
                if (null == reader)
                {
                    throw new ArgumentNullException("reader");
                }

                this._reader = reader;
            }

            public IEnumerator<IObjectReader> GetEnumerator()
            {
                return this;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this;
            }

            public IObjectReader Current
            {
                get { return this._reader; }
            }

            public void Dispose()
            {
            }

            object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                if (this._reader.MoveToNextMember())
                {
                    return true;
                }

                this._reader.ReadEndObject();

                return false;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        #endregion Object members enumerator

        #region Array enumerators

        internal abstract class ArrayValuesCollection<TValue>
            : IEnumerable<TValue>, IEnumerator<TValue>
        {
            private readonly IObjectReader _reader;

            protected ArrayValuesCollection(
                IObjectReader reader)
            {
                if (null == reader)
                {
                    throw new ArgumentNullException("reader");
                }

                this._reader = reader;
            }

            protected IObjectReader Reader
            {
                get { return this._reader; }
            }

            public IEnumerator<TValue> GetEnumerator()
            {
                return this;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this;
            }

            public abstract TValue Current
            {
                get;
            }

            public void Dispose()
            {
            }

            object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                if (this._reader.MoveToNextArrayValue())
                {
                    return true;
                }

                this._reader.ReadEndArray();

                return false;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        internal sealed class ArrayValuesAsBooleanCollection : ArrayValuesCollection<bool>
        {
            internal ArrayValuesAsBooleanCollection(
                IObjectReader reader)
                : base(reader)
            {
            }

            public override bool Current
            {
                get { return this.Reader.ReadValueAsBoolean(); }
            }
        }

        public static IEnumerable<bool>? GetBooleanArrayValues(
            this IObjectReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (!reader.ReadStartArray())
            {
                return null;
            }

            return new ArrayValuesAsBooleanCollection(reader);
        }

        internal sealed class ArrayValuesAsInt32Collection : ArrayValuesCollection<int>
        {
            internal ArrayValuesAsInt32Collection(
                IObjectReader reader)
                : base(reader)
            {
            }

            public override int Current
            {
                get { return this.Reader.ReadValueAsInt32(); }
            }
        }

        public static IEnumerable<int>? GetInt32ArrayValues(
            this IObjectReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (!reader.ReadStartArray())
            {
                return null;
            }

            return new ArrayValuesAsInt32Collection(reader);
        }

        internal sealed class ArrayValuesAsUInt32Collection : ArrayValuesCollection<uint>
        {
            internal ArrayValuesAsUInt32Collection(
                IObjectReader reader)
                : base(reader)
            {
            }

            public override uint Current
            {
                get { return this.Reader.ReadValueAsUInt32(); }
            }
        }

        public static IEnumerable<uint>? GetUInt32ArrayValues(
            this IObjectReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (!reader.ReadStartArray())
            {
                return null;
            }

            return new ArrayValuesAsUInt32Collection(reader);
        }

        internal sealed class ArrayValuesAsInt64Collection : ArrayValuesCollection<long>
        {
            internal ArrayValuesAsInt64Collection(
                IObjectReader reader)
                : base(reader)
            {
            }

            public override long Current
            {
                get { return this.Reader.ReadValueAsInt64(); }
            }
        }

        public static IEnumerable<long>? GetInt64ArrayValues(
            this IObjectReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (!reader.ReadStartArray())
            {
                return null;
            }

            return new ArrayValuesAsInt64Collection(reader);
        }

        internal sealed class ArrayValuesAsUInt64Collection : ArrayValuesCollection<ulong>
        {
            internal ArrayValuesAsUInt64Collection(
                IObjectReader reader)
                : base(reader)
            {
            }

            public override ulong Current
            {
                get { return this.Reader.ReadValueAsUInt64(); }
            }
        }

        public static IEnumerable<ulong>? GetUInt64ArrayValues(
            this IObjectReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (!reader.ReadStartArray())
            {
                return null;
            }

            return new ArrayValuesAsUInt64Collection(reader);
        }

        internal sealed class ArrayValuesAsSingleCollection : ArrayValuesCollection<float>
        {
            internal ArrayValuesAsSingleCollection(
                IObjectReader reader)
                : base(reader)
            {
            }

            public override float Current
            {
                get { return this.Reader.ReadValueAsSingle(); }
            }
        }

        public static IEnumerable<float>? GetSingleArrayValues(
            this IObjectReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (!reader.ReadStartArray())
            {
                return null;
            }

            return new ArrayValuesAsSingleCollection(reader);
        }

        internal sealed class ArrayValuesAsDoubleCollection : ArrayValuesCollection<double>
        {
            internal ArrayValuesAsDoubleCollection(
                IObjectReader reader)
                : base(reader)
            {
            }

            public override double Current
            {
                get { return this.Reader.ReadValueAsDouble(); }
            }
        }

        public static IEnumerable<double>? GetDoubleArrayValues(
            this IObjectReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (!reader.ReadStartArray())
            {
                return null;
            }

            return new ArrayValuesAsDoubleCollection(reader);
        }

        internal sealed class ArrayValuesAsStringCollection : ArrayValuesCollection<string>
        {
            private readonly int _itemQuota;

            internal ArrayValuesAsStringCollection(
                IObjectReader reader,
                int itemQuota)
                : base(reader)
            {
                this._itemQuota = itemQuota;
            }

            public override string Current
            {
                get { return this.Reader.ReadValueAsString(this._itemQuota); }
            }
        }

        public static IEnumerable<string>? GetStringArrayValues(
            this IObjectReader reader,
            int itemQuota)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (!reader.ReadStartArray())
            {
                return null;
            }

            return new ArrayValuesAsStringCollection(reader, itemQuota);
        }

        #endregion Array enumerators

        #region List readers

        public static List<bool>? ReadValueAsBooleanList(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            List<bool>? list = null;

            if (reader.ReadStartArray())
            {
                list = new List<bool>();

                while (reader.MoveToNextArrayValue())
                {
                    list.Add(reader.ReadValueAsBoolean());
                }

                reader.ReadEndArray();
            }

            return list;
        }

        public static List<int>? ReadValueAsInt32List(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            List<int>? list = null;

            if (reader.ReadStartArray())
            {
                list = new List<int>();

                while (reader.MoveToNextArrayValue())
                {
                    list.Add(reader.ReadValueAsInt32());
                }

                reader.ReadEndArray();
            }

            return list;
        }

        public static List<uint>? ReadValueAsUInt32List(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            List<uint>? list = null;

            if (reader.ReadStartArray())
            {
                list = new List<uint>();

                while (reader.MoveToNextArrayValue())
                {
                    list.Add(reader.ReadValueAsUInt32());
                }

                reader.ReadEndArray();
            }

            return list;
        }

        public static List<long>? ReadValueAsInt64List(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            List<long>? list = null;

            if (reader.ReadStartArray())
            {
                list = new List<long>();

                while (reader.MoveToNextArrayValue())
                {
                    list.Add(reader.ReadValueAsInt64());
                }

                reader.ReadEndArray();
            }

            return list;
        }

        public static List<ulong>? ReadValueAsUInt64List(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            List<ulong>? list = null;

            if (reader.ReadStartArray())
            {
                list = new List<ulong>();

                while (reader.MoveToNextArrayValue())
                {
                    list.Add(reader.ReadValueAsUInt64());
                }

                reader.ReadEndArray();
            }

            return list;
        }

        public static List<float>? ReadValueAsSingleList(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            List<float>? list = null;

            if (reader.ReadStartArray())
            {
                list = new List<float>();

                while (reader.MoveToNextArrayValue())
                {
                    list.Add(reader.ReadValueAsSingle());
                }

                reader.ReadEndArray();
            }

            return list;
        }

        public static List<double>? ReadValueAsDoubleList(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            List<double>? list = null;

            if (reader.ReadStartArray())
            {
                list = new List<double>();

                while (reader.MoveToNextArrayValue())
                {
                    list.Add(reader.ReadValueAsDouble());
                }

                reader.ReadEndArray();
            }

            return list;
        }

        public static List<string>? ReadValueAsStringList(
            this IObjectReader reader,
            int itemQuota)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            List<string>? list = null;

            if (reader.ReadStartArray())
            {
                list = new List<string>();

                while (reader.MoveToNextArrayValue())
                {
                    list.Add(reader.ReadValueAsString(itemQuota));
                }

                reader.ReadEndArray();
            }

            return list;
        }

        #endregion List readers

        #region Array readers

        public static bool[]? ReadValueAsBooleanArray(
            this IObjectReader reader)
        {
            List<bool>? list = ReadValueAsBooleanList(reader);

            if (list == null)
            {
                return null;
            }

            return list.ToArray();
        }

        public static int[]? ReadValueAsInt32Array(
            this IObjectReader reader)
        {
            List<int>? list = ReadValueAsInt32List(reader);

            if (list == null)
            {
                return null;
            }

            return list.ToArray();
        }

        public static uint[]? ReadValueAsUInt32Array(
            this IObjectReader reader)
        {
            List<uint>? list = ReadValueAsUInt32List(reader);

            if (list == null)
            {
                return null;
            }

            return list.ToArray();
        }

        public static long[]? ReadValueAsInt64Array(
            this IObjectReader reader)
        {
            List<long>? list = ReadValueAsInt64List(reader);

            if (list == null)
            {
                return null;
            }

            return list.ToArray();
        }

        public static ulong[]? ReadValueAsUInt64Array(
            this IObjectReader reader)
        {
            List<ulong>? list = ReadValueAsUInt64List(reader);

            if (list == null)
            {
                return null;
            }

            return list.ToArray();
        }

        public static float[]? ReadValueAsSingleArray(
            this IObjectReader reader)
        {
            List<float>? list = ReadValueAsSingleList(reader);

            if (list == null)
            {
                return null;
            }

            return list.ToArray();
        }

        public static double[]? ReadValueAsDoubleArray(
            this IObjectReader reader)
        {
            List<double>? list = ReadValueAsDoubleList(reader);

            if (list == null)
            {
                return null;
            }

            return list.ToArray();
        }

        public static string[]? ReadValueAsStringArray(
            this IObjectReader reader,
            int itemQuota)
        {
            List<string>? list = ReadValueAsStringList(reader, itemQuota);

            if (list == null)
            {
                return null;
            }

            return list.ToArray();
        }

        #endregion Array readers

        #region Extended types

        public static Guid ReadValueAsGuid(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            byte[]? guidBytes = reader.ReadValueAsBytes(16);
            return new Guid(guidBytes);
        }

        public static TimeSpan ReadValueAsTimeSpan(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            long ticks = reader.ReadValueAsInt64();
            return TimeSpan.FromTicks(ticks);
        }

        public static DateTime ReadValueAsDateTime(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            long ticks = reader.ReadValueAsInt64();
            return new DateTime(ticks, DateTimeKind.Utc);
        }

        public static DateTimeOffset ReadValueAsDateTimeOffset(
            this IObjectReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            long ticks = reader.ReadValueAsInt64();
            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }

        #endregion Extended types
    }
}
