//
// Copyright (c) .NET Foundation and Contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.
//

using nanoFramework.TestFramework;
using System;
using System.Collections;
using System.IO.Hashing;
using System.Text;

namespace Crc32Tests
{
    [TestClass]
    public class Crc32Tests
    {
        static ArrayList _testData = new()
        {
            new byte[] { },
            new byte[] { 0x01 },
            new byte[] { 0x00, 0x00, 0x00, 0x00 },
            new byte[] { 0xFF, 0xFF, 0xFF, 0xFF },
            new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x26, 0x39, 0xF4, 0xCB },
            new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0xD9, 0xC6, 0x0B, 0x34 },
            Encoding.UTF8.GetBytes("a"),
            Encoding.UTF8.GetBytes("abc"),
            Encoding.UTF8.GetBytes("The quick brown fox jumps over the lazy dog")
        };

        [DataRow("Empty", 0, 0u)]
        [DataRow("One", 1, 0xA505DF1Bu)]
        [DataRow("Zero-Residue", 2, 0x2144DF1Cu)]
        [DataRow("Zero-InverseResidue", 3, 0xFFFFFFFFu)]
        [DataRow("Self-test residue", 4, 0x2144DF1Cu)]
        [DataRow("Self-test inverse residue", 5, 0xFFFFFFFFu)]
        [DataRow("String a", 6, 0xE8B7BE43u)]
        [DataRow("String abc", 7, 0x352441C2u)]
        [DataRow("String message digest", 8, 0x414FA339u)]
        [TestMethod]
        public void TestByteArrayHashing_00(
            string testName,
            int dataIndex,
            uint hash)
        {
            OutputHelper.WriteLine($"Test: {testName}");

            var crc32 = new Crc32();
            var data = (byte[])_testData[dataIndex];

            crc32.Append(new Span<byte>(data));
            Assert.AreEqual(hash, crc32.GetCurrentHashAsUInt32());
        }

        [DataRow("Empty", 0, 0u)]
        [DataRow("One", 1, 0xA505DF1Bu)]
        [DataRow("Zero-Residue", 2, 0x2144DF1Cu)]
        [DataRow("Zero-InverseResidue", 3, 0xFFFFFFFFu)]
        [DataRow("Self-test residue", 4, 0x2144DF1Cu)]
        [DataRow("Self-test inverse residue", 5, 0xFFFFFFFFu)]
        [DataRow("String a", 6, 0xE8B7BE43u)]
        [DataRow("String abc", 7, 0x352441C2u)]
        [DataRow("String message digest", 8, 0x414FA339u)]
        [TestMethod]
        public void TestByteArrayHashing_01(
            string testName,
            int dataIndex,
            uint hash)
        {
            OutputHelper.WriteLine($"Test: {testName}");
            var data = (byte[])_testData[dataIndex];

            var computedHash = Crc32.HashToUInt32(new Span<byte>(data));
            Assert.AreEqual(hash, computedHash);
        }
    }
}
