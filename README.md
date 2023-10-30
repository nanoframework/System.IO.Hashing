[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_System.IO.Hashing&metric=alert_status)](https://sonarcloud.io/dashboard?id=nanoframework_System.IO.Hashing) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_System.IO.Hashing&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=nanoframework_System.IO.Hashing) [![NuGet](https://img.shields.io/nuget/dt/nanoFramework.System.IO.Hashing.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.System.IO.Hashing/) [![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://github.com/nanoframework/Home/blob/main/CONTRIBUTING.md) [![Discord](https://img.shields.io/discord/478725473862549535.svg?logo=discord&logoColor=white&label=Discord&color=7289DA)](https://discord.gg/gCyBu8T)

![nanoFramework logo](https://raw.githubusercontent.com/nanoframework/Home/main/resources/logo/nanoFramework-repo-logo.png)

-----

# Welcome to the .NET **nanoFramework** System.IO.Hashing Library repository

This repository contains the nanoFramework System.IO.Hashing class library.

## Build status

| Component | Build Status | NuGet Package |
|:-|---|---|
| System.IO.Hashing | [![Build Status](https://dev.azure.com/nanoframework/System.IO.Hashing/_apis/build/status%2Fnanoframework.System.IO.Hashing?branchName=main)](https://dev.azure.com/nanoframework/System.IO.Hashing/_build/latest?definitionId=102&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.System.IO.Hashing.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.System.IO.Hashing/) |

## System.IO.Hashing usage

### CRC32 example

This implementation emits the answer in the Little Endian byte order so that the CRC residue relationship (CRC(message concat CRC(message)) is a fixed value). For CRC-32, this stable output is the byte sequence { 0x1C, 0xDF, 0x44, 0x21 }, the Little Endian representation of 0x2144DF1C.

> [!WARNING] There are multiple, incompatible, definitions of a 32-bit cyclic redundancy check (CRC) algorithm. When interoperating with another system, ensure that you are using the same definition. The definition used by this implementation is not compatible with the cyclic redundancy check described in ITU-T I.363.5.

#### Computing a CRC32 for a byte array

```csharp

var testData = new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x26, 0x39, 0xF4, 0xCB };

Crc32 crc32 = new Crc32();
crc32.Append(testData);

ConsoleWriteline($"CRC32 for test data is: {crc32.GetCurrentHashAsUInt32()}");
```

An alternative (static) API could be used instead.

```csharp

var testData = new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x26, 0x39, 0xF4, 0xCB };

var computedHash = Crc32.HashToUInt32(testData)

ConsoleWriteline($"CRC32 for test data is: {computedHash}");
```

#### Computing a CRC32 for several data chunks

```csharp

var testData1 = new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x26, 0x39, 0xF4, 0xCB };
var testData2 = new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0xD9, 0xC6, 0x0B, 0x34 },

Crc32 crc32 = new Crc32();
crc32.Append(testData1);
crc32.Append(testData2);

ConsoleWriteline($"CRC32 for test data is: {crc32.GetCurrentHashAsUInt32()}");
```

In case the `Crc32` object needs to be reused, it's a matter of resetting the hash calculator with a call to `Reset()`.

#### Computing a CRC32 for strings

This can easily be accomplished by extrating the byte representation of a string. Note that this requires adding a reference to `nanoFramework.System.Text`.

```csharp
var computedHash = Crc32.HashToUInt32(Encoding.UTF8.GetBytes("The quick brown fox jumps over the lazy dog"))

ConsoleWriteline($"CRC32 for the string is: {computedHash}");
```

## Feedback and documentation

For documentation, providing feedback, issues and finding out how to contribute please refer to the [Home repo](https://github.com/nanoframework/Home).

Join our Discord community [here](https://discord.gg/gCyBu8T).

## Credits

The list of contributors to this project can be found at [CONTRIBUTORS](https://github.com/nanoframework/Home/blob/main/CONTRIBUTORS.md).

## License

The **nanoFramework** Class Libraries are licensed under the [MIT license](LICENSE.md).

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behaviour in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

### .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
