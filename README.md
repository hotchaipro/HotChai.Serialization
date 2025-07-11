# HotChai.Serialization

A cross-platform .NET library for efficiently serializing objects into a variety of formats.

## Features

* Simple and effective support for backward compatibility when adding new serialized object members.

* Stream-based API doesn't require the entire object to reside in memory.

* Supports a variety of serialization formats, including [JSON](https://json.org), [XML](https://en.wikipedia.org/wiki/XML), [Bencode](https://en.wikipedia.org/wiki/Bencode), and [PBON](https://pbon.info), with a single API.

* Visual Studio Shared Project support for easy inclusion of source code in your project.

* Supports transmitting objects between platforms (regardless of processor endianness, for example).

* Supports inspection of the underlying byte stream, which enables scenarios such as digital signing.

* Suitable for extending into higher-level abstractions, for example, a messaging protocol.

## For more information

See the [Project Wiki](https://github.com/hotchaipro/HotChai.Serialization/wiki)
