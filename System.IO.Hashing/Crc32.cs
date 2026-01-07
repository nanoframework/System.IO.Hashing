//
// Copyright (c) .NET Foundation and Contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.
//

using System.Runtime.CompilerServices;

namespace System.IO.Hashing
{
    /// <summary>
    ///   Provides an implementation of the CRC-32 algorithm, as used in
    ///   ITU-T V.42 and IEEE 802.3.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     If the platform or target doesn't support Hardware Crc32 computation it will offer a software implementation.
    ///   </para>
    ///   <para>
    ///     For methods that return byte arrays or that write into spans of bytes, this implementation
    ///     emits the answer in the Little Endian byte order so that the CRC residue relationship
    ///     (CRC(message concat CRC(message))) is a fixed value) holds.
    ///     For CRC-32 this stable output is the byte sequence <c>{ 0x1C, 0xDF, 0x44, 0x21 }</c>,
    ///     the Little Endian representation of <c>0x2144DF1C</c>.
    ///   </para>
    ///   <para>
    ///     There are multiple, incompatible, definitions of a 32-bit cyclic redundancy
    ///     check (CRC) algorithm. When interoperating with another system, ensure that you
    ///     are using the same definition. The definition used by this implementation is not
    ///     compatible with the cyclic redundancy check described in ITU-T I.363.5.
    ///   </para>
    /// </remarks>
    public class Crc32
    {
        private const uint InitialState = 0xFFFF_FFFF;
        private uint _crc = InitialState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Crc32"/> class.
        /// </summary>
        public Crc32()
        {
        }

        /// <summary>
        /// Appends the contents of <paramref name="source"/> to the data already processed for the current hash computation.
        /// </summary>
        /// <param name="source">The data to process.</param>
        public void Append(Span<byte> source) => _crc = ComputeHash(
            _crc,
            source);

        /// <summary>
        /// Appends the contents of <paramref name="source"/> to the data already processed for the current hash computation.
        /// </summary>
        /// <param name="source">The data to process.</param>
        public void Append(byte[] source) => _crc = ComputeHash(
            _crc,
            new Span<byte>(source));

        /// <summary>
        ///   Appends <paramref name="value"/> to the data already processed for the current hash computation.
        /// </summary>
        /// <param name="value">The <see langword="byte"/> to process.</param>
        public void Append(byte value) => _crc = ComputeHash(
            _crc,
            new Span<byte>([value]));

        /// <summary>
        ///   Resets the hash computation to the initial state.
        /// </summary>
        public void Reset() => _crc = InitialState;

        /// <summary>Gets the current computed hash value without modifying accumulated state.</summary>
        /// <returns>The hash value for the data already provided.</returns>
        public uint GetCurrentHashAsUInt32() => _crc ^ InitialState;

        /// <summary>Computes the CRC-32 hash of the provided data.</summary>
        /// <param name="source">The data to hash.</param>
        /// <returns>The computed CRC-32 hash.</returns>
        public static uint HashToUInt32(Span<byte> source) => ComputeHash(InitialState, source) ^ InitialState;

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern private static uint ComputeHash(
            uint crc,
            Span<byte> source);
    }
}
