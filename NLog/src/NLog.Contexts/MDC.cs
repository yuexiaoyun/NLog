// 
// Copyright (c) 2004 Jaroslaw Kowalski <jaak@polbox.com>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of the Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System;
using System.Collections;

namespace NLog.Contexts
{
    public sealed class MDC
    {
        private MDC() { }

        public static void Set(string item, string value) {
            IDictionary dict = GetThreadDictionary();

            dict[item] = value;
        }
        
        public static string Get(string item) {
            IDictionary dict = GetThreadDictionary();

            string s = (string)dict[item];
            if (s == null)
                return String.Empty;
            else
                return s;
        }

        public static bool Contains(string item) {
            IDictionary dict = GetThreadDictionary();

            return dict.Contains(item);
        }

        public static void Remove(string item) {
            IDictionary dict = GetThreadDictionary();

            dict.Remove(item);
        }

        public static void Clear() {
            IDictionary dict = GetThreadDictionary();

            dict.Clear();
        }

        private static IDictionary GetThreadDictionary() {
            IDictionary threadDictionary = (IDictionary)System.Threading.Thread.GetData(_dataSlot);
            
            if (threadDictionary == null) {
                threadDictionary = new Hashtable();
                System.Threading.Thread.SetData(_dataSlot, threadDictionary);
            }

            return threadDictionary;
        }

		private static LocalDataStoreSlot _dataSlot = System.Threading.Thread.AllocateDataSlot();
    }
}