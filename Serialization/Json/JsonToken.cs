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

namespace HotChai.Serialization.Json
{
    internal enum JsonToken
    {
        StartObject = 1,
        EndObject = 2,
        StartArray = 3,
        EndArray = 4,
        Colon = 5,
        Comma = 6,
        String = 7,
        Number = 8,
        Null = 9,
        True = 10,
        False = 11,
    }
}
