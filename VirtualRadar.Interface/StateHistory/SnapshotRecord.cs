﻿// Copyright © 2020 onwards, Andrew Whewell
// All rights reserved.
//
// Redistribution and use of this software in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//    * Neither the name of the author nor the names of the program's contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHORS OF THE SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualRadar.Interface.StateHistory
{
    /// <summary>
    /// The base class for records that use SHA1 fingerprints to record a snapshot of
    /// standing data.
    /// </summary>
    public abstract class SnapshotRecord
    {
        /// <summary>
        /// Gets or sets the time that the fingerprint was established / snapshot was taken.
        /// </summary>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the fingerprint derived from the record's content.
        /// </summary>
        public byte[] Fingerprint { get; set; }

        /// <summary>
        /// Gets the fingerprint expressed as a hex string without a prefix.
        /// </summary>
        public string FingerprintHex => Sha1Fingerprint.ConvertToString(Fingerprint);

        /// <summary>
        /// Fills the <see cref="Fingerprint"/> property with the object's fingerprint and
        /// <see cref="CreatedUtc"/> with the current time.
        /// </summary>
        public void TakeFingerprint()
        {
            CreatedUtc = DateTime.UtcNow;
            Fingerprint = FingerprintProperties();
        }

        /// <summary>
        /// When overridden by the derivee this returns the fingerprint for all properties except
        /// for the ID and everything on the base.
        /// </summary>
        protected abstract byte[] FingerprintProperties();
    }
}