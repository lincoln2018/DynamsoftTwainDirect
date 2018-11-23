///////////////////////////////////////////////////////////////////////////////////////
//
//  TwainWorkingGroup.TWAIN
//
//  These are the definitions for TWAIN.  They're essentially the C/C++
//  TWAIN.H file contents translated to C#, with modifications that
//  recognize the differences between Windows, Linux and Mac OS X.
//
///////////////////////////////////////////////////////////////////////////////////////
//  Author          Date            TWAIN       Comment
//  M.McLaughlin    13-Nov-2015     2.4.0.0     Updated to latest spec
//  M.McLaughlin    13-Sep-2015     2.3.1.2     DsmMem bug fixes
//  M.McLaughlin    26-Aug-2015     2.3.1.1     Log fix and sync with TWAIN Direct
//  M.McLaughlin    13-Mar-2015     2.3.1.0     Numerous fixes
//  M.McLaughlin    13-Oct-2014     2.3.0.4     Added logging
//  M.McLaughlin    24-Jun-2014     2.3.0.3     Stability fixes
//  M.McLaughlin    21-May-2014     2.3.0.2     64-Bit Linux
//  M.McLaughlin    27-Feb-2014     2.3.0.1     AnyCPU support
//  M.McLaughlin    21-Oct-2013     2.3.0.0     Initial Release
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) 2013-2018 Kodak Alaris Inc.
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//  DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace TWAINWorkingGroup
{
    /// <summary>
    /// This file contains content gleaned from version 2.4 of the C/C++ TWAIN.H
    /// header file released by the TWAIN Working Group.  It's organized like that
    /// file to make it easier to maintain.
    /// 
    /// Please do not add any code to this module, save for the minimum needed to
    /// maintain a particular definition (such as TW_STR32)...
    /// </summary>
    public partial class TWAIN
    {
        ///////////////////////////////////////////////////////////////////////////////
        // Type Definitions...
        ///////////////////////////////////////////////////////////////////////////////
        #region Type Definitions...

        // Follow these rules
        /******************************************************************************

        TW_HANDLE...............IntPtr
        TW_MEMREF...............IntPtr
        TW_UINTPTR..............UIntPtr
        
        TW_INT8.................char
        TW_INT16................short
        TW_INT32................int (was long on Linux 64-bit)
        
        TW_UINT8................byte
        TW_UINT16...............ushort
        TW_UINT32...............uint (was ulong on Linux 64-bit)
        TW_BOOL.................ushort
        
        ******************************************************************************/

        /// <summary>
        /// Our supported platforms...
        /// </summary>
        public enum Platform
        {
            /// <summary>
            /// </summary>
            UNKNOWN,
            /// <summary>
            /// </summary>
            WINDOWS,
            /// <summary>
            /// </summary>
            LINUX,
            /// <summary>
            /// </summary>
            MACOSX
        };

        /// <summary>
        /// Used for strings that go up to 32-bytes...
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_STR32
        {
            /// <summary>
            /// We're stuck with this, because marshalling with packed alignment
            /// can't handle arrays...
            /// </summary>
            private ushort sz32Item_00;
            /// <summary>
            /// </summary>
            private ushort sz32Item_01;
            /// <summary>
            /// </summary>
            private ushort sz32Item_02;
            /// <summary>
            /// </summary>
            private ushort sz32Item_03;
            /// <summary>
            /// </summary>
            private ushort sz32Item_04;
            /// <summary>
            /// </summary>
            private ushort sz32Item_05;
            /// <summary>
            /// </summary>
            private ushort sz32Item_06;
            /// <summary>
            /// </summary>
            private ushort sz32Item_07;
            /// <summary>
            /// </summary>
            private ushort sz32Item_08;
            /// <summary>
            /// </summary>
            private ushort sz32Item_09;
            /// <summary>
            /// </summary>
            private ushort sz32Item_10;
            /// <summary>
            /// </summary>
            private ushort sz32Item_11;
            /// <summary>
            /// </summary>
            private ushort sz32Item_12;
            /// <summary>
            /// </summary>
            private ushort sz32Item_13;
            /// <summary>
            /// </summary>
            private ushort sz32Item_14;
            /// <summary>
            /// </summary>
            private ushort sz32Item_15;
            /// <summary>
            /// </summary>
            private ushort sz32Item_16;

            /// <summary>
            /// The normal get...
            /// </summary>
            /// <returns></returns>
            public string Get()
            {
                return (GetValue(true));
            }

            /// <summary>
            /// Use this on Mac OS X if you have a call that uses a string
            /// that doesn't include the prefix byte...
            /// </summary>
            /// <returns></returns>
            public string GetNoPrefix()
            {
                return (GetValue(false));
            }

            /// <summary>
            /// Get our value...
            /// </summary>
            /// <returns></returns>
            private string GetValue(bool a_blMayHavePrefix)
            {
                // Unpack what we have into a string...
                string sz =
                    Convert.ToChar(sz32Item_00 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_00 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_01 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_01 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_02 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_02 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_03 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_03 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_04 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_04 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_05 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_05 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_06 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_06 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_07 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_07 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_08 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_08 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_09 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_09 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_10 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_10 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_11 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_11 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_12 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_12 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_13 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_13 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_14 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_14 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_15 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_15 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_16 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_16 >> 8) & 0xFF).ToString();

                // If the first character is a NUL, then return the empty string...
                while ((sz.Length > 0) && (sz[0] == '\0'))
                {
                    sz = sz.Remove(0, 1);
                }

                // We have an emptry string...
                if (sz.Length == 0)
                {
                    return ("");
                }

                // If we're running on a Mac, take off the prefix 'byte'...
                if (a_blMayHavePrefix && (TWAIN.GetPlatform() == Platform.MACOSX))
                {
                    sz = sz.Remove(0, 1);
                }

                // If we detect a NUL, then split around it...
                if (sz.IndexOf('\0') >= 0)
                {
                    sz = sz.Split(new char[] { '\0' })[0];
                }

                // All done...
                return (sz);
            }

            /// <summary>
            /// The normal set...
            /// </summary>
            /// <returns></returns>
            public void Set(string a_sz)
            {
                SetValue(a_sz, true);
            }

            /// <summary>
            /// Use this on Mac OS X if you have a call that uses a string
            /// that doesn't include the prefix byte...
            /// </summary>
            /// <returns></returns>
            public void SetNoPrefix(string a_sz)
            {
                SetValue(a_sz, false);
            }

            /// <summary>
            /// Set our value...
            /// </summary>
            /// <param name="a_sz"></param>
            /// <param name="a_blMayHavePrefix"></param>
            private void SetValue(string a_sz, bool a_blMayHavePrefix)
            {
                // If we're running on a Mac, tack on the prefix 'byte'...
                if (a_sz == null)
                {
                    a_sz = "";
                }
                else if (a_blMayHavePrefix && (TWAIN.GetPlatform() == Platform.MACOSX))
                {
                    a_sz = (char)a_sz.Length + a_sz;
                }

                // Make sure that we're NUL padded...
                string sz = a_sz +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0";
                if (sz.Length > 34)
                {
                    sz = sz.Remove(34);
                }

                // Pack the data...
                sz32Item_00 = (ushort)((sz[1] << 8) + sz[0]);
                sz32Item_01 = (ushort)((sz[3] << 8) + sz[2]);
                sz32Item_02 = (ushort)((sz[5] << 8) + sz[4]);
                sz32Item_03 = (ushort)((sz[7] << 8) + sz[6]);
                sz32Item_04 = (ushort)((sz[9] << 8) + sz[8]);
                sz32Item_05 = (ushort)((sz[11] << 8) + sz[10]);
                sz32Item_06 = (ushort)((sz[13] << 8) + sz[12]);
                sz32Item_07 = (ushort)((sz[15] << 8) + sz[14]);
                sz32Item_08 = (ushort)((sz[17] << 8) + sz[16]);
                sz32Item_09 = (ushort)((sz[19] << 8) + sz[18]);
                sz32Item_10 = (ushort)((sz[21] << 8) + sz[20]);
                sz32Item_11 = (ushort)((sz[23] << 8) + sz[22]);
                sz32Item_12 = (ushort)((sz[25] << 8) + sz[24]);
                sz32Item_13 = (ushort)((sz[27] << 8) + sz[26]);
                sz32Item_14 = (ushort)((sz[29] << 8) + sz[28]);
                sz32Item_15 = (ushort)((sz[31] << 8) + sz[30]);
                sz32Item_16 = (ushort)((sz[33] << 8) + sz[32]);
            }
        }

        /// <summary>
        /// Used for strings that go up to 64-bytes...
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_STR64
        {
            /// <summary>
            /// We're stuck with this, because marshalling with packed alignment
            /// can't handle arrays...
            /// </summary>
            public ushort sz32Item_000;
            /// <summary></summary>
            public ushort sz32Item_001;
            /// <summary></summary>
            public ushort sz32Item_002;
            /// <summary></summary>
            public ushort sz32Item_003;
            /// <summary></summary>
            public ushort sz32Item_004;
            /// <summary></summary>
            public ushort sz32Item_005;
            /// <summary></summary>
            public ushort sz32Item_006;
            /// <summary></summary>
            public ushort sz32Item_007;
            /// <summary></summary>
            public ushort sz32Item_008;
            /// <summary></summary>
            public ushort sz32Item_009;
            /// <summary></summary>
            public ushort sz32Item_010;
            /// <summary></summary>
            public ushort sz32Item_011;
            /// <summary></summary>
            public ushort sz32Item_012;
            /// <summary></summary>
            public ushort sz32Item_013;
            /// <summary></summary>
            public ushort sz32Item_014;
            /// <summary></summary>
            public ushort sz32Item_015;
            /// <summary></summary>
            public ushort sz32Item_016;
            /// <summary></summary>
            public ushort sz32Item_017;
            /// <summary></summary>
            public ushort sz32Item_018;
            /// <summary></summary>
            public ushort sz32Item_019;
            /// <summary></summary>
            public ushort sz32Item_020;
            /// <summary></summary>
            public ushort sz32Item_021;
            /// <summary></summary>
            public ushort sz32Item_022;
            /// <summary></summary>
            public ushort sz32Item_023;
            /// <summary></summary>
            public ushort sz32Item_024;
            /// <summary></summary>
            public ushort sz32Item_025;
            /// <summary></summary>
            public ushort sz32Item_026;
            /// <summary></summary>
            public ushort sz32Item_027;
            /// <summary></summary>
            public ushort sz32Item_028;
            /// <summary></summary>
            public ushort sz32Item_029;
            /// <summary></summary>
            public ushort sz32Item_030;
            /// <summary></summary>
            public ushort sz32Item_031;
            /// <summary></summary>
            public ushort sz32Item_032;

            /// <summary>
            /// The normal get...
            /// </summary>
            /// <returns></returns>
            public string Get()
            {
                return (GetValue(true));
            }

            /// <summary>
            /// Use this on Mac OS X if you have a call that uses a string
            /// that doesn't include the prefix byte...
            /// </summary>
            /// <returns></returns>
            public string GetNoPrefix()
            {
                return (GetValue(false));
            }

            /// <summary>
            /// Get our value...
            /// </summary>
            /// <returns></returns>
            private string GetValue(bool a_blMayHavePrefix)
            {
                string sz =
                    Convert.ToChar(sz32Item_000 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_000 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_001 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_001 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_002 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_002 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_003 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_003 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_004 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_004 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_005 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_005 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_006 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_006 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_007 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_007 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_008 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_008 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_009 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_009 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_010 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_010 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_011 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_011 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_012 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_012 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_013 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_013 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_014 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_014 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_015 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_015 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_016 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_016 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_017 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_017 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_018 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_018 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_019 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_019 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_020 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_020 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_021 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_021 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_022 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_022 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_023 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_023 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_024 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_024 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_025 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_025 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_026 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_026 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_027 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_027 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_028 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_028 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_029 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_029 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_030 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_030 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_031 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_031 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_032 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_032 >> 8) & 0xFF).ToString();

                // If the first character is a NUL, then return the empty string...
                if (sz[0] == '\0')
                {
                    return ("");
                }

                // If we're running on a Mac, take off the prefix 'byte'...
                if (a_blMayHavePrefix && (TWAIN.GetPlatform() == Platform.MACOSX))
                {
                    sz = sz.Remove(0, 1);
                }

                // If we detect a NUL, then split around it...
                if (sz.IndexOf('\0') >= 0)
                {
                    sz = sz.Split(new char[] { '\0' })[0];
                }

                // All done...
                return (sz);
            }

            /// <summary>
            /// The normal set...
            /// </summary>
            /// <returns></returns>
            public void Set(string a_sz)
            {
                SetValue(a_sz, true);
            }

            /// <summary>
            /// Use this on Mac OS X if you have a call that uses a string
            /// that doesn't include the prefix byte...
            /// </summary>
            /// <returns></returns>
            public void SetNoPrefix(string a_sz)
            {
                SetValue(a_sz, false);
            }

            /// <summary>
            /// Set our value...
            /// </summary>
            /// <param name="a_sz"></param>
            /// <param name="a_blMayHavePrefix"></param>
            private void SetValue(string a_sz, bool a_blMayHavePrefix)
            {
                // If we're running on a Mac, tack on the prefix 'byte'...
                if (a_sz == null)
                {
                    a_sz = "";
                }
                else if (a_blMayHavePrefix && (TWAIN.GetPlatform() == Platform.MACOSX))
                {
                    a_sz = (char)a_sz.Length + a_sz;
                }

                // Make sure that we're NUL padded...
                string sz =
                    a_sz +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
                if (sz.Length > 64)
                {
                    sz = sz.Remove(64);
                }

                // Pack the data...
                sz32Item_000 = (ushort)((sz[1] << 8) + sz[0]);
                sz32Item_001 = (ushort)((sz[3] << 8) + sz[2]);
                sz32Item_002 = (ushort)((sz[5] << 8) + sz[4]);
                sz32Item_003 = (ushort)((sz[7] << 8) + sz[6]);
                sz32Item_004 = (ushort)((sz[9] << 8) + sz[8]);
                sz32Item_005 = (ushort)((sz[11] << 8) + sz[10]);
                sz32Item_006 = (ushort)((sz[13] << 8) + sz[12]);
                sz32Item_007 = (ushort)((sz[15] << 8) + sz[14]);
                sz32Item_008 = (ushort)((sz[17] << 8) + sz[16]);
                sz32Item_009 = (ushort)((sz[19] << 8) + sz[18]);
                sz32Item_010 = (ushort)((sz[21] << 8) + sz[20]);
                sz32Item_011 = (ushort)((sz[23] << 8) + sz[22]);
                sz32Item_012 = (ushort)((sz[25] << 8) + sz[24]);
                sz32Item_013 = (ushort)((sz[27] << 8) + sz[26]);
                sz32Item_014 = (ushort)((sz[29] << 8) + sz[28]);
                sz32Item_015 = (ushort)((sz[31] << 8) + sz[30]);
                sz32Item_016 = (ushort)((sz[33] << 8) + sz[32]);
                sz32Item_017 = (ushort)((sz[35] << 8) + sz[34]);
                sz32Item_018 = (ushort)((sz[37] << 8) + sz[36]);
                sz32Item_019 = (ushort)((sz[39] << 8) + sz[38]);
                sz32Item_020 = (ushort)((sz[41] << 8) + sz[40]);
                sz32Item_021 = (ushort)((sz[43] << 8) + sz[42]);
                sz32Item_022 = (ushort)((sz[45] << 8) + sz[44]);
                sz32Item_023 = (ushort)((sz[47] << 8) + sz[46]);
                sz32Item_024 = (ushort)((sz[49] << 8) + sz[48]);
                sz32Item_025 = (ushort)((sz[51] << 8) + sz[50]);
                sz32Item_026 = (ushort)((sz[53] << 8) + sz[52]);
                sz32Item_027 = (ushort)((sz[55] << 8) + sz[54]);
                sz32Item_028 = (ushort)((sz[57] << 8) + sz[56]);
                sz32Item_029 = (ushort)((sz[59] << 8) + sz[58]);
                sz32Item_030 = (ushort)((sz[61] << 8) + sz[60]);
                sz32Item_031 = (ushort)((sz[63] << 8) + sz[62]);
                sz32Item_032 = (ushort)((sz[64] << 8) + sz[63]);
            }
        }

        /// <summary>
        /// Used for strings that go up to 128-bytes...
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_STR128
        {
            /// <summary>
            /// We're stuck with this, because marshalling with packed alignment
            /// can't handle arrays...
            /// </summary>
            public ushort sz32Item_000;
            /// <summary></summary>
            public ushort sz32Item_001;
            /// <summary></summary>
            public ushort sz32Item_002;
            /// <summary></summary>
            public ushort sz32Item_003;
            /// <summary></summary>
            public ushort sz32Item_004;
            /// <summary></summary>
            public ushort sz32Item_005;
            /// <summary></summary>
            public ushort sz32Item_006;
            /// <summary></summary>
            public ushort sz32Item_007;
            /// <summary></summary>
            public ushort sz32Item_008;
            /// <summary></summary>
            public ushort sz32Item_009;
            /// <summary></summary>
            public ushort sz32Item_010;
            /// <summary></summary>
            public ushort sz32Item_011;
            /// <summary></summary>
            public ushort sz32Item_012;
            /// <summary></summary>
            public ushort sz32Item_013;
            /// <summary></summary>
            public ushort sz32Item_014;
            /// <summary></summary>
            public ushort sz32Item_015;
            /// <summary></summary>
            public ushort sz32Item_016;
            /// <summary></summary>
            public ushort sz32Item_017;
            /// <summary></summary>
            public ushort sz32Item_018;
            /// <summary></summary>
            public ushort sz32Item_019;
            /// <summary></summary>
            public ushort sz32Item_020;
            /// <summary></summary>
            public ushort sz32Item_021;
            /// <summary></summary>
            public ushort sz32Item_022;
            /// <summary></summary>
            public ushort sz32Item_023;
            /// <summary></summary>
            public ushort sz32Item_024;
            /// <summary></summary>
            public ushort sz32Item_025;
            /// <summary></summary>
            public ushort sz32Item_026;
            /// <summary></summary>
            public ushort sz32Item_027;
            /// <summary></summary>
            public ushort sz32Item_028;
            /// <summary></summary>
            public ushort sz32Item_029;
            /// <summary></summary>
            public ushort sz32Item_030;
            /// <summary></summary>
            public ushort sz32Item_031;
            /// <summary></summary>
            public ushort sz32Item_032;
            /// <summary></summary>
            public ushort sz32Item_033;
            /// <summary></summary>
            public ushort sz32Item_034;
            /// <summary></summary>
            public ushort sz32Item_035;
            /// <summary></summary>
            public ushort sz32Item_036;
            /// <summary></summary>
            public ushort sz32Item_037;
            /// <summary></summary>
            public ushort sz32Item_038;
            /// <summary></summary>
            public ushort sz32Item_039;
            /// <summary></summary>
            public ushort sz32Item_040;
            /// <summary></summary>
            public ushort sz32Item_041;
            /// <summary></summary>
            public ushort sz32Item_042;
            /// <summary></summary>
            public ushort sz32Item_043;
            /// <summary></summary>
            public ushort sz32Item_044;
            /// <summary></summary>
            public ushort sz32Item_045;
            /// <summary></summary>
            public ushort sz32Item_046;
            /// <summary></summary>
            public ushort sz32Item_047;
            /// <summary></summary>
            public ushort sz32Item_048;
            /// <summary></summary>
            public ushort sz32Item_049;
            /// <summary></summary>
            public ushort sz32Item_050;
            /// <summary></summary>
            public ushort sz32Item_051;
            /// <summary></summary>
            public ushort sz32Item_052;
            /// <summary></summary>
            public ushort sz32Item_053;
            /// <summary></summary>
            public ushort sz32Item_054;
            /// <summary></summary>
            public ushort sz32Item_055;
            /// <summary></summary>
            public ushort sz32Item_056;
            /// <summary></summary>
            public ushort sz32Item_057;
            /// <summary></summary>
            public ushort sz32Item_058;
            /// <summary></summary>
            public ushort sz32Item_059;
            /// <summary></summary>
            public ushort sz32Item_060;
            /// <summary></summary>
            public ushort sz32Item_061;
            /// <summary></summary>
            public ushort sz32Item_062;
            /// <summary></summary>
            public ushort sz32Item_063;
            /// <summary></summary>
            public ushort sz32Item_064;

            /// <summary>
            /// The normal get...
            /// </summary>
            /// <returns></returns>
            public string Get()
            {
                return (GetValue(true));
            }

            /// <summary>
            /// Use this on Mac OS X if you have a call that uses a string
            /// that doesn't include the prefix byte...
            /// </summary>
            /// <returns></returns>
            public string GetNoPrefix()
            {
                return (GetValue(false));
            }

            /// <summary>
            /// Get our value...
            /// </summary>
            /// <returns></returns>
            private string GetValue(bool a_blMayHavePrefix)
            {
                string sz =
                    Convert.ToChar(sz32Item_000 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_000 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_001 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_001 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_002 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_002 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_003 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_003 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_004 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_004 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_005 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_005 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_006 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_006 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_007 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_007 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_008 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_008 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_009 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_009 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_010 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_010 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_011 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_011 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_012 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_012 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_013 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_013 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_014 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_014 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_015 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_015 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_016 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_016 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_017 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_017 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_018 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_018 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_019 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_019 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_020 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_020 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_021 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_021 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_022 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_022 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_023 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_023 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_024 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_024 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_025 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_025 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_026 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_026 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_027 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_027 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_028 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_028 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_029 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_029 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_030 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_030 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_031 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_031 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_032 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_032 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_033 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_033 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_034 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_034 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_035 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_035 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_036 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_036 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_037 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_037 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_038 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_038 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_039 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_039 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_040 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_040 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_041 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_041 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_042 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_042 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_043 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_043 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_044 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_044 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_045 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_045 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_046 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_046 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_047 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_047 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_048 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_048 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_049 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_049 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_050 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_050 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_051 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_051 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_052 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_052 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_053 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_053 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_054 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_054 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_055 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_055 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_056 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_056 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_057 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_057 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_058 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_058 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_059 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_059 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_060 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_060 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_061 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_061 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_062 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_062 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_063 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_063 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_064 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_064 >> 8) & 0xFF).ToString();

                // If the first character is a NUL, then return the empty string...
                if (sz[0] == '\0')
                {
                    return ("");
                }

                // If we're running on a Mac, take off the prefix 'byte'...
                if (a_blMayHavePrefix && (TWAIN.GetPlatform() == Platform.MACOSX))
                {
                    sz = sz.Remove(0, 1);
                }

                // If we detect a NUL, then split around it...
                if (sz.IndexOf('\0') >= 0)
                {
                    sz = sz.Split(new char[] { '\0' })[0];
                }

                // All done...
                return (sz);
            }

            /// <summary>
            /// The normal set...
            /// </summary>
            /// <returns></returns>
            public void Set(string a_sz)
            {
                SetValue(a_sz, true);
            }

            /// <summary>
            /// Use this on Mac OS X if you have a call that uses a string
            /// that doesn't include the prefix byte...
            /// </summary>
            /// <returns></returns>
            public void SetNoPrefix(string a_sz)
            {
                SetValue(a_sz, false);
            }

            /// <summary>
            /// Set our value...
            /// </summary>
            /// <param name="a_sz"></param>
            /// <param name="a_blMayHavePrefix"></param>
            private void SetValue(string a_sz, bool a_blMayHavePrefix)
            {
                // If we're running on a Mac, tack on the prefix 'byte'...
                if (a_sz == null)
                {
                    a_sz = "";
                }
                else if (a_blMayHavePrefix && (TWAIN.GetPlatform() == Platform.MACOSX))
                {
                    a_sz = (char)a_sz.Length + a_sz;
                }

                // Make sure that we're NUL padded...
                string sz =
                    a_sz +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0";
                if (sz.Length > 130)
                {
                    sz = sz.Remove(130);
                }

                // Pack the data...
                sz32Item_000 = (ushort)((sz[1] << 8) + sz[0]);
                sz32Item_001 = (ushort)((sz[3] << 8) + sz[2]);
                sz32Item_002 = (ushort)((sz[5] << 8) + sz[4]);
                sz32Item_003 = (ushort)((sz[7] << 8) + sz[6]);
                sz32Item_004 = (ushort)((sz[9] << 8) + sz[8]);
                sz32Item_005 = (ushort)((sz[11] << 8) + sz[10]);
                sz32Item_006 = (ushort)((sz[13] << 8) + sz[12]);
                sz32Item_007 = (ushort)((sz[15] << 8) + sz[14]);
                sz32Item_008 = (ushort)((sz[17] << 8) + sz[16]);
                sz32Item_009 = (ushort)((sz[19] << 8) + sz[18]);
                sz32Item_010 = (ushort)((sz[21] << 8) + sz[20]);
                sz32Item_011 = (ushort)((sz[23] << 8) + sz[22]);
                sz32Item_012 = (ushort)((sz[25] << 8) + sz[24]);
                sz32Item_013 = (ushort)((sz[27] << 8) + sz[26]);
                sz32Item_014 = (ushort)((sz[29] << 8) + sz[28]);
                sz32Item_015 = (ushort)((sz[31] << 8) + sz[30]);
                sz32Item_016 = (ushort)((sz[33] << 8) + sz[32]);
                sz32Item_017 = (ushort)((sz[35] << 8) + sz[34]);
                sz32Item_018 = (ushort)((sz[37] << 8) + sz[36]);
                sz32Item_019 = (ushort)((sz[39] << 8) + sz[38]);
                sz32Item_020 = (ushort)((sz[41] << 8) + sz[40]);
                sz32Item_021 = (ushort)((sz[43] << 8) + sz[42]);
                sz32Item_022 = (ushort)((sz[45] << 8) + sz[44]);
                sz32Item_023 = (ushort)((sz[47] << 8) + sz[46]);
                sz32Item_024 = (ushort)((sz[49] << 8) + sz[48]);
                sz32Item_025 = (ushort)((sz[51] << 8) + sz[50]);
                sz32Item_026 = (ushort)((sz[53] << 8) + sz[52]);
                sz32Item_027 = (ushort)((sz[55] << 8) + sz[54]);
                sz32Item_028 = (ushort)((sz[57] << 8) + sz[56]);
                sz32Item_029 = (ushort)((sz[59] << 8) + sz[58]);
                sz32Item_030 = (ushort)((sz[61] << 8) + sz[60]);
                sz32Item_031 = (ushort)((sz[63] << 8) + sz[62]);
                sz32Item_032 = (ushort)((sz[65] << 8) + sz[64]);
                sz32Item_033 = (ushort)((sz[67] << 8) + sz[66]);
                sz32Item_034 = (ushort)((sz[69] << 8) + sz[68]);
                sz32Item_035 = (ushort)((sz[71] << 8) + sz[70]);
                sz32Item_036 = (ushort)((sz[73] << 8) + sz[72]);
                sz32Item_037 = (ushort)((sz[75] << 8) + sz[74]);
                sz32Item_038 = (ushort)((sz[77] << 8) + sz[76]);
                sz32Item_039 = (ushort)((sz[79] << 8) + sz[78]);
                sz32Item_040 = (ushort)((sz[81] << 8) + sz[80]);
                sz32Item_041 = (ushort)((sz[83] << 8) + sz[82]);
                sz32Item_042 = (ushort)((sz[85] << 8) + sz[84]);
                sz32Item_043 = (ushort)((sz[87] << 8) + sz[86]);
                sz32Item_044 = (ushort)((sz[89] << 8) + sz[88]);
                sz32Item_045 = (ushort)((sz[91] << 8) + sz[90]);
                sz32Item_046 = (ushort)((sz[93] << 8) + sz[92]);
                sz32Item_047 = (ushort)((sz[95] << 8) + sz[94]);
                sz32Item_048 = (ushort)((sz[97] << 8) + sz[96]);
                sz32Item_049 = (ushort)((sz[99] << 8) + sz[98]);
                sz32Item_050 = (ushort)((sz[101] << 8) + sz[100]);
                sz32Item_051 = (ushort)((sz[103] << 8) + sz[102]);
                sz32Item_052 = (ushort)((sz[105] << 8) + sz[104]);
                sz32Item_053 = (ushort)((sz[107] << 8) + sz[106]);
                sz32Item_054 = (ushort)((sz[109] << 8) + sz[108]);
                sz32Item_055 = (ushort)((sz[111] << 8) + sz[110]);
                sz32Item_056 = (ushort)((sz[113] << 8) + sz[112]);
                sz32Item_057 = (ushort)((sz[115] << 8) + sz[114]);
                sz32Item_058 = (ushort)((sz[117] << 8) + sz[116]);
                sz32Item_059 = (ushort)((sz[119] << 8) + sz[118]);
                sz32Item_060 = (ushort)((sz[121] << 8) + sz[120]);
                sz32Item_061 = (ushort)((sz[123] << 8) + sz[122]);
                sz32Item_062 = (ushort)((sz[125] << 8) + sz[124]);
                sz32Item_063 = (ushort)((sz[127] << 8) + sz[126]);
                sz32Item_064 = (ushort)((sz[129] << 8) + sz[128]);
            }
        }

        /// <summary>
        /// Used for strings that go up to 256-bytes...
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_STR255
        {
            /// <summary>
            /// We're stuck with this, because marshalling with packed alignment
            /// can't handle arrays...
            /// </summary>
            public ushort sz32Item_000;
            /// <summary>
            /// </summary>
            public ushort sz32Item_001;
            /// <summary>
            /// </summary>
            public ushort sz32Item_002;
            /// <summary>
            /// </summary>
            public ushort sz32Item_003;
            /// <summary>
            /// </summary>
            public ushort sz32Item_004;
            /// <summary>
            /// </summary>
            public ushort sz32Item_005;
            /// <summary>
            /// </summary>
            public ushort sz32Item_006;
            /// <summary>
            /// </summary>
            public ushort sz32Item_007;
            /// <summary>
            /// </summary>
            public ushort sz32Item_008;
            /// <summary>
            /// </summary>
            public ushort sz32Item_009;
            /// <summary>
            /// </summary>
            public ushort sz32Item_010;
            /// <summary>
            /// </summary>
            public ushort sz32Item_011;
            /// <summary>
            /// </summary>
            public ushort sz32Item_012;
            /// <summary>
            /// </summary>
            public ushort sz32Item_013;
            /// <summary>
            /// </summary>
            public ushort sz32Item_014;
            /// <summary>
            /// </summary>
            public ushort sz32Item_015;
            /// <summary>
            /// </summary>
            public ushort sz32Item_016;
            /// <summary>
            /// </summary>
            public ushort sz32Item_017;
            /// <summary>
            /// </summary>
            public ushort sz32Item_018;
            /// <summary>
            /// </summary>
            public ushort sz32Item_019;
            /// <summary>
            /// </summary>
            public ushort sz32Item_020;
            /// <summary>
            /// </summary>
            public ushort sz32Item_021;
            /// <summary>
            /// </summary>
            public ushort sz32Item_022;
            /// <summary>
            /// </summary>
            public ushort sz32Item_023;
            /// <summary>
            /// </summary>
            public ushort sz32Item_024;
            /// <summary>
            /// </summary>
            public ushort sz32Item_025;
            /// <summary>
            /// </summary>
            public ushort sz32Item_026;
            /// <summary>
            /// </summary>
            public ushort sz32Item_027;
            /// <summary>
            /// </summary>
            public ushort sz32Item_028;
            /// <summary>
            /// </summary>
            public ushort sz32Item_029;
            /// <summary>
            /// </summary>
            public ushort sz32Item_030;
            /// <summary>
            /// </summary>
            public ushort sz32Item_031;
            /// <summary>
            /// </summary>
            public ushort sz32Item_032;
            /// <summary>
            /// </summary>
            public ushort sz32Item_033;
            /// <summary>
            /// </summary>
            public ushort sz32Item_034;
            /// <summary>
            /// </summary>
            public ushort sz32Item_035;
            /// <summary>
            /// </summary>
            public ushort sz32Item_036;
            /// <summary>
            /// </summary>
            public ushort sz32Item_037;
            /// <summary>
            /// </summary>
            public ushort sz32Item_038;
            /// <summary>
            /// </summary>
            public ushort sz32Item_039;
            /// <summary>
            /// </summary>
            public ushort sz32Item_040;
            /// <summary>
            /// </summary>
            public ushort sz32Item_041;
            /// <summary>
            /// </summary>
            public ushort sz32Item_042;
            /// <summary>
            /// </summary>
            public ushort sz32Item_043;
            /// <summary>
            /// </summary>
            public ushort sz32Item_044;
            /// <summary>
            /// </summary>
            public ushort sz32Item_045;
            /// <summary>
            /// </summary>
            public ushort sz32Item_046;
            /// <summary>
            /// </summary>
            public ushort sz32Item_047;
            /// <summary>
            /// </summary>
            public ushort sz32Item_048;
            /// <summary>
            /// </summary>
            public ushort sz32Item_049;
            /// <summary>
            /// </summary>
            public ushort sz32Item_050;
            /// <summary>
            /// </summary>
            public ushort sz32Item_051;
            /// <summary>
            /// </summary>
            public ushort sz32Item_052;
            /// <summary>
            /// </summary>
            public ushort sz32Item_053;
            /// <summary>
            /// </summary>
            public ushort sz32Item_054;
            /// <summary>
            /// </summary>
            public ushort sz32Item_055;
            /// <summary>
            /// </summary>
            public ushort sz32Item_056;
            /// <summary>
            /// </summary>
            public ushort sz32Item_057;
            /// <summary>
            /// </summary>
            public ushort sz32Item_058;
            /// <summary>
            /// </summary>
            public ushort sz32Item_059;
            /// <summary>
            /// </summary>
            public ushort sz32Item_060;
            /// <summary>
            /// </summary>
            public ushort sz32Item_061;
            /// <summary>
            /// </summary>
            public ushort sz32Item_062;
            /// <summary>
            /// </summary>
            public ushort sz32Item_063;
            /// <summary>
            /// </summary>
            public ushort sz32Item_064;
            /// <summary>
            /// </summary>
            public ushort sz32Item_065;
            /// <summary>
            /// </summary>
            public ushort sz32Item_066;
            /// <summary>
            /// </summary>
            public ushort sz32Item_067;
            /// <summary>
            /// </summary>
            public ushort sz32Item_068;
            /// <summary>
            /// </summary>
            public ushort sz32Item_069;
            /// <summary>
            /// </summary>
            public ushort sz32Item_070;
            /// <summary>
            /// </summary>
            public ushort sz32Item_071;
            /// <summary>
            /// </summary>
            public ushort sz32Item_072;
            /// <summary>
            /// </summary>
            public ushort sz32Item_073;
            /// <summary>
            /// </summary>
            public ushort sz32Item_074;
            /// <summary>
            /// </summary>
            public ushort sz32Item_075;
            /// <summary>
            /// </summary>
            public ushort sz32Item_076;
            /// <summary>
            /// </summary>
            public ushort sz32Item_077;
            /// <summary>
            /// </summary>
            public ushort sz32Item_078;
            /// <summary>
            /// </summary>
            public ushort sz32Item_079;
            /// <summary>
            /// </summary>
            public ushort sz32Item_080;
            /// <summary>
            /// </summary>
            public ushort sz32Item_081;
            /// <summary>
            /// </summary>
            public ushort sz32Item_082;
            /// <summary>
            /// </summary>
            public ushort sz32Item_083;
            /// <summary>
            /// </summary>
            public ushort sz32Item_084;
            /// <summary>
            /// </summary>
            public ushort sz32Item_085;
            /// <summary>
            /// </summary>
            public ushort sz32Item_086;
            /// <summary>
            /// </summary>
            public ushort sz32Item_087;
            /// <summary>
            /// </summary>
            public ushort sz32Item_088;
            /// <summary>
            /// </summary>
            public ushort sz32Item_089;
            /// <summary>
            /// </summary>
            public ushort sz32Item_090;
            /// <summary>
            /// </summary>
            public ushort sz32Item_091;
            /// <summary>
            /// </summary>
            public ushort sz32Item_092;
            /// <summary>
            /// </summary>
            public ushort sz32Item_093;
            /// <summary>
            /// </summary>
            public ushort sz32Item_094;
            /// <summary>
            /// </summary>
            public ushort sz32Item_095;
            /// <summary>
            /// </summary>
            public ushort sz32Item_096;
            /// <summary>
            /// </summary>
            public ushort sz32Item_097;
            /// <summary>
            /// </summary>
            public ushort sz32Item_098;
            /// <summary>
            /// </summary>
            public ushort sz32Item_099;
            /// <summary>
            /// </summary>
            public ushort sz32Item_100;
            /// <summary>
            /// </summary>
            public ushort sz32Item_101;
            /// <summary>
            /// </summary>
            public ushort sz32Item_102;
            /// <summary>
            /// </summary>
            public ushort sz32Item_103;
            /// <summary>
            /// </summary>
            public ushort sz32Item_104;
            /// <summary>
            /// </summary>
            public ushort sz32Item_105;
            /// <summary>
            /// </summary>
            public ushort sz32Item_106;
            /// <summary>
            /// </summary>
            public ushort sz32Item_107;
            /// <summary>
            /// </summary>
            public ushort sz32Item_108;
            /// <summary>
            /// </summary>
            public ushort sz32Item_109;
            /// <summary>
            /// </summary>
            public ushort sz32Item_110;
            /// <summary>
            /// </summary>
            public ushort sz32Item_111;
            /// <summary>
            /// </summary>
            public ushort sz32Item_112;
            /// <summary>
            /// </summary>
            public ushort sz32Item_113;
            /// <summary>
            /// </summary>
            public ushort sz32Item_114;
            /// <summary>
            /// </summary>
            public ushort sz32Item_115;
            /// <summary>
            /// </summary>
            public ushort sz32Item_116;
            /// <summary>
            /// </summary>
            public ushort sz32Item_117;
            /// <summary>
            /// </summary>
            public ushort sz32Item_118;
            /// <summary>
            /// </summary>
            public ushort sz32Item_119;
            /// <summary>
            /// </summary>
            public ushort sz32Item_120;
            /// <summary>
            /// </summary>
            public ushort sz32Item_121;
            /// <summary>
            /// </summary>
            public ushort sz32Item_122;
            /// <summary>
            /// </summary>
            public ushort sz32Item_123;
            /// <summary>
            /// </summary>
            public ushort sz32Item_124;
            /// <summary>
            /// </summary>
            public ushort sz32Item_125;
            /// <summary>
            /// </summary>
            public ushort sz32Item_126;
            /// <summary>
            /// </summary>
            public ushort sz32Item_127;

            /// <summary>
            /// The normal get...
            /// </summary>
            /// <returns></returns>
            public string Get()
            {
                return (GetValue(true));
            }

            /// <summary>
            /// Use this on Mac OS X if you have a call that uses a string
            /// that doesn't include the prefix byte...
            /// </summary>
            /// <returns></returns>
            public string GetNoPrefix()
            {
                return (GetValue(false));
            }

            /// <summary>
            /// Get our value...
            /// </summary>
            /// <returns></returns>
            private string GetValue(bool a_blMayHavePrefix)
            {
                string sz =
                    Convert.ToChar(sz32Item_000 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_000 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_001 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_001 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_002 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_002 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_003 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_003 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_004 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_004 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_005 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_005 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_006 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_006 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_007 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_007 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_008 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_008 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_009 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_009 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_010 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_010 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_011 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_011 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_012 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_012 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_013 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_013 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_014 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_014 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_015 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_015 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_016 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_016 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_017 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_017 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_018 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_018 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_019 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_019 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_020 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_020 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_021 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_021 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_022 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_022 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_023 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_023 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_024 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_024 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_025 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_025 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_026 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_026 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_027 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_027 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_028 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_028 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_029 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_029 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_030 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_030 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_031 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_031 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_032 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_032 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_033 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_033 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_034 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_034 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_035 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_035 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_036 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_036 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_037 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_037 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_038 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_038 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_039 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_039 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_040 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_040 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_041 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_041 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_042 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_042 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_043 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_043 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_044 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_044 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_045 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_045 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_046 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_046 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_047 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_047 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_048 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_048 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_049 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_049 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_050 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_050 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_051 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_051 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_052 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_052 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_053 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_053 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_054 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_054 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_055 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_055 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_056 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_056 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_057 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_057 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_058 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_058 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_059 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_059 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_060 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_060 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_061 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_061 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_062 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_062 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_063 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_063 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_064 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_064 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_065 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_065 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_066 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_066 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_067 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_067 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_068 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_068 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_069 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_069 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_070 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_070 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_071 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_071 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_072 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_072 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_073 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_073 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_074 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_074 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_075 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_075 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_076 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_076 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_077 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_077 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_078 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_078 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_079 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_079 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_080 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_080 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_081 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_081 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_082 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_082 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_083 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_083 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_084 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_084 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_085 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_085 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_086 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_086 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_087 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_087 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_088 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_088 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_089 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_089 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_090 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_090 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_091 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_091 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_092 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_092 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_093 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_093 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_094 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_094 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_095 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_095 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_096 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_096 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_097 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_097 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_098 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_098 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_099 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_099 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_100 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_100 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_101 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_101 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_102 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_102 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_103 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_103 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_104 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_104 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_105 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_105 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_106 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_106 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_107 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_107 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_108 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_108 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_109 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_109 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_110 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_110 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_111 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_111 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_112 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_112 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_113 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_113 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_114 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_114 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_115 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_115 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_116 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_116 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_117 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_117 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_118 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_118 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_119 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_119 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_120 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_120 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_121 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_121 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_122 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_122 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_123 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_123 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_124 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_124 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_125 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_125 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_126 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_126 >> 8) & 0xFF).ToString() +
                    Convert.ToChar(sz32Item_127 & 0xFF).ToString() +
                    Convert.ToChar((sz32Item_127 >> 8) & 0xFF).ToString();

                // If the first character is a NUL, then return the empty string...
                if (sz[0] == '\0')
                {
                    return ("");
                }

                // If we're running on a Mac, take off the prefix 'byte'...
                if (a_blMayHavePrefix && (TWAIN.GetPlatform() == Platform.MACOSX))
                {
                    sz = sz.Remove(0, 1);
                }

                // If we detect a NUL, then split around it...
                if (sz.IndexOf('\0') >= 0)
                {
                    sz = sz.Split(new char[] { '\0' })[0];
                }

                // All done...
                return (sz);
            }

            /// <summary>
            /// The normal set...
            /// </summary>
            /// <returns></returns>
            public void Set(string a_sz)
            {
                SetValue(a_sz, true);
            }

            /// <summary>
            /// Use this on Mac OS X if you have a call that uses a string
            /// that doesn't include the prefix byte...
            /// </summary>
            /// <returns></returns>
            public void SetNoPrefix(string a_sz)
            {
                SetValue(a_sz, false);
            }

            /// <summary>
            /// Set our value...
            /// </summary>
            /// <param name="a_sz"></param>
            /// <param name="a_blMayHavePrefix"></param>
            private void SetValue(string a_sz, bool a_blMayHavePrefix)
            {
                // If we're running on a Mac, tack on the prefix 'byte'...
                if (a_sz == null)
                {
                    a_sz = "";
                }
                else if (a_blMayHavePrefix && (TWAIN.GetPlatform() == Platform.MACOSX))
                {
                    a_sz = (char)a_sz.Length + a_sz;
                }

                // Make sure that we're NUL padded...
                string sz =
                    a_sz +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" +
                    "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
                if (sz.Length > 256)
                {
                    sz = sz.Remove(256);
                }

                // Pack the data...
                sz32Item_000 = (ushort)((sz[1] << 8) + sz[0]);
                sz32Item_001 = (ushort)((sz[3] << 8) + sz[2]);
                sz32Item_002 = (ushort)((sz[5] << 8) + sz[4]);
                sz32Item_003 = (ushort)((sz[7] << 8) + sz[6]);
                sz32Item_004 = (ushort)((sz[9] << 8) + sz[8]);
                sz32Item_005 = (ushort)((sz[11] << 8) + sz[10]);
                sz32Item_006 = (ushort)((sz[13] << 8) + sz[12]);
                sz32Item_007 = (ushort)((sz[15] << 8) + sz[14]);
                sz32Item_008 = (ushort)((sz[17] << 8) + sz[16]);
                sz32Item_009 = (ushort)((sz[19] << 8) + sz[18]);
                sz32Item_010 = (ushort)((sz[21] << 8) + sz[20]);
                sz32Item_011 = (ushort)((sz[23] << 8) + sz[22]);
                sz32Item_012 = (ushort)((sz[25] << 8) + sz[24]);
                sz32Item_013 = (ushort)((sz[27] << 8) + sz[26]);
                sz32Item_014 = (ushort)((sz[29] << 8) + sz[28]);
                sz32Item_015 = (ushort)((sz[31] << 8) + sz[30]);
                sz32Item_016 = (ushort)((sz[33] << 8) + sz[32]);
                sz32Item_017 = (ushort)((sz[35] << 8) + sz[34]);
                sz32Item_018 = (ushort)((sz[37] << 8) + sz[36]);
                sz32Item_019 = (ushort)((sz[39] << 8) + sz[38]);
                sz32Item_020 = (ushort)((sz[41] << 8) + sz[40]);
                sz32Item_021 = (ushort)((sz[43] << 8) + sz[42]);
                sz32Item_022 = (ushort)((sz[45] << 8) + sz[44]);
                sz32Item_023 = (ushort)((sz[47] << 8) + sz[46]);
                sz32Item_024 = (ushort)((sz[49] << 8) + sz[48]);
                sz32Item_025 = (ushort)((sz[51] << 8) + sz[50]);
                sz32Item_026 = (ushort)((sz[53] << 8) + sz[52]);
                sz32Item_027 = (ushort)((sz[55] << 8) + sz[54]);
                sz32Item_028 = (ushort)((sz[57] << 8) + sz[56]);
                sz32Item_029 = (ushort)((sz[59] << 8) + sz[58]);
                sz32Item_030 = (ushort)((sz[61] << 8) + sz[60]);
                sz32Item_031 = (ushort)((sz[63] << 8) + sz[62]);
                sz32Item_032 = (ushort)((sz[65] << 8) + sz[64]);
                sz32Item_033 = (ushort)((sz[67] << 8) + sz[66]);
                sz32Item_034 = (ushort)((sz[69] << 8) + sz[68]);
                sz32Item_035 = (ushort)((sz[71] << 8) + sz[70]);
                sz32Item_036 = (ushort)((sz[73] << 8) + sz[72]);
                sz32Item_037 = (ushort)((sz[75] << 8) + sz[74]);
                sz32Item_038 = (ushort)((sz[77] << 8) + sz[76]);
                sz32Item_039 = (ushort)((sz[79] << 8) + sz[78]);
                sz32Item_040 = (ushort)((sz[81] << 8) + sz[80]);
                sz32Item_041 = (ushort)((sz[83] << 8) + sz[82]);
                sz32Item_042 = (ushort)((sz[85] << 8) + sz[84]);
                sz32Item_043 = (ushort)((sz[87] << 8) + sz[86]);
                sz32Item_044 = (ushort)((sz[89] << 8) + sz[88]);
                sz32Item_045 = (ushort)((sz[91] << 8) + sz[90]);
                sz32Item_046 = (ushort)((sz[93] << 8) + sz[92]);
                sz32Item_047 = (ushort)((sz[95] << 8) + sz[94]);
                sz32Item_048 = (ushort)((sz[97] << 8) + sz[96]);
                sz32Item_049 = (ushort)((sz[99] << 8) + sz[98]);
                sz32Item_050 = (ushort)((sz[101] << 8) + sz[100]);
                sz32Item_051 = (ushort)((sz[103] << 8) + sz[102]);
                sz32Item_052 = (ushort)((sz[105] << 8) + sz[104]);
                sz32Item_053 = (ushort)((sz[107] << 8) + sz[106]);
                sz32Item_054 = (ushort)((sz[109] << 8) + sz[108]);
                sz32Item_055 = (ushort)((sz[111] << 8) + sz[110]);
                sz32Item_056 = (ushort)((sz[113] << 8) + sz[112]);
                sz32Item_057 = (ushort)((sz[115] << 8) + sz[114]);
                sz32Item_058 = (ushort)((sz[117] << 8) + sz[116]);
                sz32Item_059 = (ushort)((sz[119] << 8) + sz[118]);
                sz32Item_060 = (ushort)((sz[121] << 8) + sz[120]);
                sz32Item_061 = (ushort)((sz[123] << 8) + sz[122]);
                sz32Item_062 = (ushort)((sz[125] << 8) + sz[124]);
                sz32Item_063 = (ushort)((sz[127] << 8) + sz[126]);
                sz32Item_064 = (ushort)((sz[129] << 8) + sz[128]);
                sz32Item_065 = (ushort)((sz[131] << 8) + sz[130]);
                sz32Item_066 = (ushort)((sz[133] << 8) + sz[132]);
                sz32Item_067 = (ushort)((sz[135] << 8) + sz[134]);
                sz32Item_068 = (ushort)((sz[137] << 8) + sz[136]);
                sz32Item_069 = (ushort)((sz[139] << 8) + sz[138]);
                sz32Item_070 = (ushort)((sz[141] << 8) + sz[140]);
                sz32Item_071 = (ushort)((sz[143] << 8) + sz[142]);
                sz32Item_072 = (ushort)((sz[145] << 8) + sz[144]);
                sz32Item_073 = (ushort)((sz[147] << 8) + sz[146]);
                sz32Item_074 = (ushort)((sz[149] << 8) + sz[148]);
                sz32Item_075 = (ushort)((sz[151] << 8) + sz[150]);
                sz32Item_076 = (ushort)((sz[153] << 8) + sz[152]);
                sz32Item_077 = (ushort)((sz[155] << 8) + sz[154]);
                sz32Item_078 = (ushort)((sz[157] << 8) + sz[156]);
                sz32Item_079 = (ushort)((sz[159] << 8) + sz[158]);
                sz32Item_080 = (ushort)((sz[161] << 8) + sz[160]);
                sz32Item_081 = (ushort)((sz[163] << 8) + sz[162]);
                sz32Item_082 = (ushort)((sz[165] << 8) + sz[164]);
                sz32Item_083 = (ushort)((sz[167] << 8) + sz[166]);
                sz32Item_084 = (ushort)((sz[169] << 8) + sz[168]);
                sz32Item_085 = (ushort)((sz[171] << 8) + sz[170]);
                sz32Item_086 = (ushort)((sz[173] << 8) + sz[172]);
                sz32Item_087 = (ushort)((sz[175] << 8) + sz[174]);
                sz32Item_088 = (ushort)((sz[177] << 8) + sz[176]);
                sz32Item_089 = (ushort)((sz[179] << 8) + sz[178]);
                sz32Item_090 = (ushort)((sz[181] << 8) + sz[180]);
                sz32Item_091 = (ushort)((sz[183] << 8) + sz[182]);
                sz32Item_092 = (ushort)((sz[185] << 8) + sz[184]);
                sz32Item_093 = (ushort)((sz[187] << 8) + sz[186]);
                sz32Item_094 = (ushort)((sz[189] << 8) + sz[188]);
                sz32Item_095 = (ushort)((sz[191] << 8) + sz[190]);
                sz32Item_096 = (ushort)((sz[193] << 8) + sz[192]);
                sz32Item_097 = (ushort)((sz[195] << 8) + sz[194]);
                sz32Item_098 = (ushort)((sz[197] << 8) + sz[196]);
                sz32Item_099 = (ushort)((sz[199] << 8) + sz[198]);
                sz32Item_100 = (ushort)((sz[201] << 8) + sz[200]);
                sz32Item_101 = (ushort)((sz[203] << 8) + sz[202]);
                sz32Item_102 = (ushort)((sz[205] << 8) + sz[204]);
                sz32Item_103 = (ushort)((sz[207] << 8) + sz[206]);
                sz32Item_104 = (ushort)((sz[209] << 8) + sz[208]);
                sz32Item_105 = (ushort)((sz[211] << 8) + sz[210]);
                sz32Item_106 = (ushort)((sz[213] << 8) + sz[212]);
                sz32Item_107 = (ushort)((sz[215] << 8) + sz[214]);
                sz32Item_108 = (ushort)((sz[217] << 8) + sz[216]);
                sz32Item_109 = (ushort)((sz[219] << 8) + sz[218]);
                sz32Item_110 = (ushort)((sz[221] << 8) + sz[220]);
                sz32Item_111 = (ushort)((sz[223] << 8) + sz[222]);
                sz32Item_112 = (ushort)((sz[225] << 8) + sz[224]);
                sz32Item_113 = (ushort)((sz[227] << 8) + sz[226]);
                sz32Item_114 = (ushort)((sz[229] << 8) + sz[228]);
                sz32Item_115 = (ushort)((sz[231] << 8) + sz[230]);
                sz32Item_116 = (ushort)((sz[233] << 8) + sz[232]);
                sz32Item_117 = (ushort)((sz[235] << 8) + sz[234]);
                sz32Item_118 = (ushort)((sz[237] << 8) + sz[236]);
                sz32Item_119 = (ushort)((sz[239] << 8) + sz[238]);
                sz32Item_120 = (ushort)((sz[241] << 8) + sz[240]);
                sz32Item_121 = (ushort)((sz[243] << 8) + sz[242]);
                sz32Item_122 = (ushort)((sz[245] << 8) + sz[244]);
                sz32Item_123 = (ushort)((sz[247] << 8) + sz[246]);
                sz32Item_124 = (ushort)((sz[249] << 8) + sz[248]);
                sz32Item_125 = (ushort)((sz[251] << 8) + sz[250]);
                sz32Item_126 = (ushort)((sz[253] << 8) + sz[252]);
                sz32Item_127 = (ushort)((sz[255] << 8) + sz[254]);
            }
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Structure Definitions...
        ///////////////////////////////////////////////////////////////////////////////
        #region Structure Definitions..

        /// <summary>
        /// Fixed point structure type.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_FIX32
        {
            /// <summary>
            /// 
            /// </summary>
            public short Whole;
            /// <summary>
            /// 
            /// </summary>
            public ushort Frac;
        }

        /// <summary>
        /// Defines a frame rectangle in ICAP_UNITS coordinates.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_FRAME
        {
            /// <summary>
            /// </summary>
            public TW_FIX32 Left;
            /// <summary>
            /// </summary>
            public TW_FIX32 Top;
            /// <summary>
            /// </summary>
            public TW_FIX32 Right;
            /// <summary>
            /// </summary>
            public TW_FIX32 Bottom;
        }

        /// <summary>
        /// Defines the parameters used for channel-specific transformation.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_DECODEFUNCTION
        {
            /// <summary>
            /// </summary>
            public TW_FIX32 StartIn;
            /// <summary>
            /// </summary>
            public TW_FIX32 BreakIn;
            /// <summary>
            /// </summary>
            public TW_FIX32 EndIn;
            /// <summary>
            /// </summary>
            public TW_FIX32 StartOut;
            /// <summary>
            /// </summary>
            public TW_FIX32 BreakOut;
            /// <summary>
            /// </summary>
            public TW_FIX32 EndOut;
            /// <summary>
            /// </summary>
            public TW_FIX32 Gamma;
            /// <summary>
            /// </summary>
            public TW_FIX32 SampleCount;
        }

        /// <summary>
        /// Stores a Fixed point number in two parts, a whole and a fractional part.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_TRANSFORMSTAGE
        {
            /// <summary>
            /// </summary>
            public TW_DECODEFUNCTION Decode_0;
            /// <summary>
            /// </summary>
            public TW_DECODEFUNCTION Decode_1;
            /// <summary>
            /// </summary>
            public TW_DECODEFUNCTION Decode_2;
            /// <summary>
            /// </summary>
            public TW_FIX32 Mix_0_0;
            /// <summary>
            /// </summary>
            public TW_FIX32 Mix_0_1;
            /// <summary>
            /// </summary>
            public TW_FIX32 Mix_0_2;
            /// <summary>
            /// </summary>
            public TW_FIX32 Mix_1_0;
            /// <summary>
            /// </summary>
            public TW_FIX32 Mix_1_1;
            /// <summary>
            /// </summary>
            public TW_FIX32 Mix_1_2;
            /// <summary>
            /// </summary>
            public TW_FIX32 Mix_2_0;
            /// <summary>
            /// </summary>
            public TW_FIX32 Mix_2_1;
            /// <summary>
            /// </summary>
            public TW_FIX32 Mix_2_2;
        }

        /// <summary>
        /// Stores a list of values for a capability, the ItemList is commented
        /// out so that the caller can collect information about it with a
        /// marshalling call...
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_ARRAY
        {
            /// <summary>
            /// </summary>
            public TWTY ItemType;
            /// <summary>
            /// </summary>
            public uint NumItems;
            //public byte[] ItemList;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_ARRAY_MACOSX
        {
            /// <summary>
            /// </summary>
            public uint ItemType;
            /// <summary>
            /// </summary>
            public uint NumItems;
            //public byte[] ItemList;
        }

        /// <summary>
        /// Information about audio data.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_AUDIOINFO
        {
            /// <summary>
            /// </summary>
            public TW_STR255 Name;
            /// <summary>
            /// </summary>
            public uint Reserved;
        }

        /// <summary>
        /// Used to register callbacks.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_CALLBACK
        {
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr CallBackProc;
            /// <summary>
            /// </summary>
            public uint RefCon;
            /// <summary>
            /// </summary>
            public ushort Message;
        }

        /// <summary>
        /// Used to register callbacks.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_CALLBACK2
        {
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr CallBackProc;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public UIntPtr RefCon;
            /// <summary>
            /// </summary>
            public ushort Message;
        }

        /// <summary>
        /// Used by application to get/set capability from/in a data source.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_CAPABILITY
        {
            /// <summary>
            /// </summary>
            public CAP Cap;
            /// <summary>
            /// </summary>
            public TWON ConType;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr hContainer;
        }

        /// <summary>
        /// Defines a CIE XYZ space tri-stimulus value.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_CIEPOINT
        {
            /// <summary>
            /// </summary>
            public TW_FIX32 X;
            /// <summary>
            /// </summary>
            public TW_FIX32 Y;
            /// <summary>
            /// </summary>
            public TW_FIX32 Z;
        }

        /// <summary>
        /// Defines the mapping from an RGB color space device into CIE 1931 (XYZ) color space.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_CIECOLOR
        {
            /// <summary>
            /// </summary>
            public ushort ColorSpace;
            /// <summary>
            /// </summary>
            public short LowEndian;
            /// <summary>
            /// </summary>
            public short DeviceDependent;
            /// <summary>
            /// </summary>
            public int VersionNumber;
            /// <summary>
            /// </summary>
            public TW_TRANSFORMSTAGE StageABC;
            /// <summary>
            /// </summary>
            public TW_TRANSFORMSTAGE StageLNM;
            /// <summary>
            /// </summary>
            public TW_CIEPOINT WhitePoint;
            /// <summary>
            /// </summary>
            public TW_CIEPOINT BlackPoint;
            /// <summary>
            /// </summary>
            public TW_CIEPOINT WhitePaper;
            /// <summary>
            /// </summary>
            public TW_CIEPOINT BlackInk;
            /// <summary>
            /// </summary>
            public TW_FIX32 Samples;
        }

        /// <summary>
        /// Allows for a data source and application to pass custom data to each other.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_CUSTOMDSDATA
        {
            /// <summary>
            /// </summary>
            public uint InfoLength;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr hData;
        }

        /// <summary>
        /// Provides information about the Event that was raised by the Source.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_DEVICEEVENT
        {
            /// <summary>
            /// </summary>
            public uint Event;
            /// <summary>
            /// </summary>
            public TW_STR255 DeviceName;
            /// <summary>
            /// </summary>
            public uint BatteryMinutes;
            /// <summary>
            /// </summary>
            public short BatteryPercentage;
            /// <summary>
            /// </summary>
            public int PowerSupply;
            /// <summary>
            /// </summary>
            public TW_FIX32 XResolution;
            /// <summary>
            /// </summary>
            public TW_FIX32 YResolution;
            /// <summary>
            /// </summary>
            public uint FlashUsed2;
            /// <summary>
            /// </summary>
            public uint AutomaticCapture;
            /// <summary>
            /// </summary>
            public uint TimeBeforeFirstCapture;
            /// <summary>
            /// </summary>
            public uint TimeBetweenCaptures;
        }

        /// <summary>
        /// This structure holds the tri-stimulus color palette information for TW_PALETTE8 structures.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_ELEMENT8
        {
            /// <summary>
            /// </summary>
            public byte Index;
            /// <summary>
            /// </summary>
            public byte Channel1;
            /// <summary>
            /// </summary>
            public byte Channel2;
            /// <summary>
            /// </summary>
            public byte Channel3;
        }

        /// <summary>
        /// DAT_ENTRYPOINT. returns essential entry points.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_ENTRYPOINT
        {
            /// <summary>
            /// </summary>
            public UInt32 Size;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_Entry;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_MemAllocate;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_MemFree;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_MemLock;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_MemUnlock;
        }

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_ENTRYPOINT_LINUX64
        {
            /// <summary>
            /// </summary>
            public long Size;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_Entry;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_MemAllocate;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_MemFree;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_MemLock;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_MemUnlock;
        }
        /// <summary>
        /// </summary>
        public struct TW_ENTRYPOINT_DELEGATES
        {
            /// <summary>
            /// </summary>
            public UInt32 Size;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr DSM_Entry;
            /// <summary>
            /// </summary>
            public DSM_MEMALLOC DSM_MemAllocate;
            /// <summary>
            /// </summary>
            public DSM_MEMFREE DSM_MemFree;
            /// <summary>
            /// </summary>
            public DSM_MEMLOCK DSM_MemLock;
            /// <summary>
            /// </summary>
            public DSM_MEMUNLOCK DSM_MemUnlock;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public delegate IntPtr DSM_MEMALLOC(uint size);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        public delegate void DSM_MEMFREE(IntPtr handle);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public delegate IntPtr DSM_MEMLOCK(IntPtr handle);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        public delegate void DSM_MEMUNLOCK(IntPtr handle);

        /// <summary>
        /// Stores a group of enumerated values for a capability, the ItemList is
        /// commented out so that the caller can collect information about it with
        /// a marshalling call...
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_ENUMERATION
        {
            /// <summary>
            /// </summary>
            public TWTY ItemType;
            /// <summary>
            /// </summary>
            public uint NumItems;
            /// <summary>
            /// </summary>
            public uint CurrentIndex;
            /// <summary>
            /// </summary>
            public uint DefaultIndex;
            //public byte[] ItemList;
        }
        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_ENUMERATION_LINUX64
        {
            /// <summary>
            /// </summary>
            public TWTY ItemType;
            /// <summary>
            /// </summary>
            public ulong NumItems;
            /// <summary>
            /// </summary>
            public ulong CurrentIndex;
            /// <summary>
            /// </summary>
            public ulong DefaultIndex;
            //public byte[] ItemList;
        }
        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct TW_ENUMERATION_MACOSX
        {
            /// <summary>
            /// </summary>
            public uint ItemType;
            /// <summary>
            /// </summary>
            public uint NumItems;
            /// <summary>
            /// </summary>
            public uint CurrentIndex;
            /// <summary>
            /// </summary>
            public uint DefaultIndex;
            //public byte[] ItemList;
        }

        /// <summary>
        /// Used to pass application events/messages from the application to the Source.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_EVENT
        {
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr pEvent;
            /// <summary>
            /// </summary>
            public ushort TWMessage;
        }

        /// <summary>
        /// DAT_FILTER...
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_FILTER_DESCRIPTOR
        {
            /// <summary>
            /// </summary>
            public UInt32 Size;
            /// <summary>
            /// </summary>
            public UInt32 HueStart;
            /// <summary>
            /// </summary>
            public UInt32 HueEnd;
            /// <summary>
            /// </summary>
            public UInt32 SaturationStart;
            /// <summary>
            /// </summary>
            public UInt32 SaturationEnd;
            /// <summary>
            /// </summary>
            public UInt32 ValueStart;
            /// <summary>
            /// </summary>
            public UInt32 ValueEnd;
            /// <summary>
            /// </summary>
            public UInt32 Replacement;
        }

        /// <summary>
        /// DAT_FILTER...
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_FILTER
        {
            /// <summary>
            /// </summary>
            public UInt32 Size;
            /// <summary>
            /// </summary>
            public UInt32 DescriptorCount;
            /// <summary>
            /// </summary>
            public UInt32 MaxDescriptorCount;
            /// <summary>
            /// </summary>
            public UInt32 Condition;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr hDescriptors;
        }

        /// <summary>
        /// This structure is used to pass specific information between the data source and the application.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_INFO
        {
            /// <summary>
            /// </summary>
            public ushort InfoId;
            /// <summary>
            /// </summary>
            public ushort ItemType;
            /// <summary>
            /// </summary>
            public ushort NumItems;
            /// <summary>
            /// </summary>
            public ushort ReturnCode;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public UIntPtr Item;
        }

        /// <summary>
        /// This structure is used to pass specific information between the data source and the application.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_EXTIMAGEINFO
        {
            /// <summary>
            /// </summary>
            public uint NumInfos;
            /// <summary>
            /// </summary>
            public TW_INFO Info_000;
            /// <summary>
            /// </summary>
            public TW_INFO Info_001;
            /// <summary>
            /// </summary>
            public TW_INFO Info_002;
            /// <summary>
            /// </summary>
            public TW_INFO Info_003;
            /// <summary>
            /// </summary>
            public TW_INFO Info_004;
            /// <summary>
            /// </summary>
            public TW_INFO Info_005;
            /// <summary>
            /// </summary>
            public TW_INFO Info_006;
            /// <summary>
            /// </summary>
            public TW_INFO Info_007;
            /// <summary>
            /// </summary>
            public TW_INFO Info_008;
            /// <summary>
            /// </summary>
            public TW_INFO Info_009;
            /// <summary>
            /// </summary>
            public TW_INFO Info_010;
            /// <summary>
            /// </summary>
            public TW_INFO Info_011;
            /// <summary>
            /// </summary>
            public TW_INFO Info_012;
            /// <summary>
            /// </summary>
            public TW_INFO Info_013;
            /// <summary>
            /// </summary>
            public TW_INFO Info_014;
            /// <summary>
            /// </summary>
            public TW_INFO Info_015;
            /// <summary>
            /// </summary>
            public TW_INFO Info_016;
            /// <summary>
            /// </summary>
            public TW_INFO Info_017;
            /// <summary>
            /// </summary>
            public TW_INFO Info_018;
            /// <summary>
            /// </summary>
            public TW_INFO Info_019;
            /// <summary>
            /// </summary>
            public TW_INFO Info_020;
            /// <summary>
            /// </summary>
            public TW_INFO Info_021;
            /// <summary>
            /// </summary>
            public TW_INFO Info_022;
            /// <summary>
            /// </summary>
            public TW_INFO Info_023;
            /// <summary>
            /// </summary>
            public TW_INFO Info_024;
            /// <summary>
            /// </summary>
            public TW_INFO Info_025;
            /// <summary>
            /// </summary>
            public TW_INFO Info_026;
            /// <summary>
            /// </summary>
            public TW_INFO Info_027;
            /// <summary>
            /// </summary>
            public TW_INFO Info_028;
            /// <summary>
            /// </summary>
            public TW_INFO Info_029;
            /// <summary>
            /// </summary>
            public TW_INFO Info_030;
            /// <summary>
            /// </summary>
            public TW_INFO Info_031;
            /// <summary>
            /// </summary>
            public TW_INFO Info_032;
            /// <summary>
            /// </summary>
            public TW_INFO Info_033;
            /// <summary>
            /// </summary>
            public TW_INFO Info_034;
            /// <summary>
            /// </summary>
            public TW_INFO Info_035;
            /// <summary>
            /// </summary>
            public TW_INFO Info_036;
            /// <summary>
            /// </summary>
            public TW_INFO Info_037;
            /// <summary>
            /// </summary>
            public TW_INFO Info_038;
            /// <summary>
            /// </summary>
            public TW_INFO Info_039;
            /// <summary>
            /// </summary>
            public TW_INFO Info_040;
            /// <summary>
            /// </summary>
            public TW_INFO Info_041;
            /// <summary>
            /// </summary>
            public TW_INFO Info_042;
            /// <summary>
            /// </summary>
            public TW_INFO Info_043;
            /// <summary>
            /// </summary>
            public TW_INFO Info_044;
            /// <summary>
            /// </summary>
            public TW_INFO Info_045;
            /// <summary>
            /// </summary>
            public TW_INFO Info_046;
            /// <summary>
            /// </summary>
            public TW_INFO Info_047;
            /// <summary>
            /// </summary>
            public TW_INFO Info_048;
            /// <summary>
            /// </summary>
            public TW_INFO Info_049;
            /// <summary>
            /// </summary>
            public TW_INFO Info_050;
            /// <summary>
            /// </summary>
            public TW_INFO Info_051;
            /// <summary>
            /// </summary>
            public TW_INFO Info_052;
            /// <summary>
            /// </summary>
            public TW_INFO Info_053;
            /// <summary>
            /// </summary>
            public TW_INFO Info_054;
            /// <summary>
            /// </summary>
            public TW_INFO Info_055;
            /// <summary>
            /// </summary>
            public TW_INFO Info_056;
            /// <summary>
            /// </summary>
            public TW_INFO Info_057;
            /// <summary>
            /// </summary>
            public TW_INFO Info_058;
            /// <summary>
            /// </summary>
            public TW_INFO Info_059;
            /// <summary>
            /// </summary>
            public TW_INFO Info_060;
            /// <summary>
            /// </summary>
            public TW_INFO Info_061;
            /// <summary>
            /// </summary>
            public TW_INFO Info_062;
            /// <summary>
            /// </summary>
            public TW_INFO Info_063;
            /// <summary>
            /// </summary>
            public TW_INFO Info_064;
            /// <summary>
            /// </summary>
            public TW_INFO Info_065;
            /// <summary>
            /// </summary>
            public TW_INFO Info_066;
            /// <summary>
            /// </summary>
            public TW_INFO Info_067;
            /// <summary>
            /// </summary>
            public TW_INFO Info_068;
            /// <summary>
            /// </summary>
            public TW_INFO Info_069;
            /// <summary>
            /// </summary>
            public TW_INFO Info_070;
            /// <summary>
            /// </summary>
            public TW_INFO Info_071;
            /// <summary>
            /// </summary>
            public TW_INFO Info_072;
            /// <summary>
            /// </summary>
            public TW_INFO Info_073;
            /// <summary>
            /// </summary>
            public TW_INFO Info_074;
            /// <summary>
            /// </summary>
            public TW_INFO Info_075;
            /// <summary>
            /// </summary>
            public TW_INFO Info_076;
            /// <summary>
            /// </summary>
            public TW_INFO Info_077;
            /// <summary>
            /// </summary>
            public TW_INFO Info_078;
            /// <summary>
            /// </summary>
            public TW_INFO Info_079;
            /// <summary>
            /// </summary>
            public TW_INFO Info_080;
            /// <summary>
            /// </summary>
            public TW_INFO Info_081;
            /// <summary>
            /// </summary>
            public TW_INFO Info_082;
            /// <summary>
            /// </summary>
            public TW_INFO Info_083;
            /// <summary>
            /// </summary>
            public TW_INFO Info_084;
            /// <summary>
            /// </summary>
            public TW_INFO Info_085;
            /// <summary>
            /// </summary>
            public TW_INFO Info_086;
            /// <summary>
            /// </summary>
            public TW_INFO Info_087;
            /// <summary>
            /// </summary>
            public TW_INFO Info_088;
            /// <summary>
            /// </summary>
            public TW_INFO Info_089;
            /// <summary>
            /// </summary>
            public TW_INFO Info_090;
            /// <summary>
            /// </summary>
            public TW_INFO Info_091;
            /// <summary>
            /// </summary>
            public TW_INFO Info_092;
            /// <summary>
            /// </summary>
            public TW_INFO Info_093;
            /// <summary>
            /// </summary>
            public TW_INFO Info_094;
            /// <summary>
            /// </summary>
            public TW_INFO Info_095;
            /// <summary>
            /// </summary>
            public TW_INFO Info_096;
            /// <summary>
            /// </summary>
            public TW_INFO Info_097;
            /// <summary>
            /// </summary>
            public TW_INFO Info_098;
            /// <summary>
            /// </summary>
            public TW_INFO Info_099;
            /// <summary>
            /// </summary>
            public TW_INFO Info_100;
            /// <summary>
            /// </summary>
            public TW_INFO Info_101;
            /// <summary>
            /// </summary>
            public TW_INFO Info_102;
            /// <summary>
            /// </summary>
            public TW_INFO Info_103;
            /// <summary>
            /// </summary>
            public TW_INFO Info_104;
            /// <summary>
            /// </summary>
            public TW_INFO Info_105;
            /// <summary>
            /// </summary>
            public TW_INFO Info_106;
            /// <summary>
            /// </summary>
            public TW_INFO Info_107;
            /// <summary>
            /// </summary>
            public TW_INFO Info_108;
            /// <summary>
            /// </summary>
            public TW_INFO Info_109;
            /// <summary>
            /// </summary>
            public TW_INFO Info_110;
            /// <summary>
            /// </summary>
            public TW_INFO Info_111;
            /// <summary>
            /// </summary>
            public TW_INFO Info_112;
            /// <summary>
            /// </summary>
            public TW_INFO Info_113;
            /// <summary>
            /// </summary>
            public TW_INFO Info_114;
            /// <summary>
            /// </summary>
            public TW_INFO Info_115;
            /// <summary>
            /// </summary>
            public TW_INFO Info_116;
            /// <summary>
            /// </summary>
            public TW_INFO Info_117;
            /// <summary>
            /// </summary>
            public TW_INFO Info_118;
            /// <summary>
            /// </summary>
            public TW_INFO Info_119;
            /// <summary>
            /// </summary>
            public TW_INFO Info_120;
            /// <summary>
            /// </summary>
            public TW_INFO Info_121;
            /// <summary>
            /// </summary>
            public TW_INFO Info_122;
            /// <summary>
            /// </summary>
            public TW_INFO Info_123;
            /// <summary>
            /// </summary>
            public TW_INFO Info_124;
            /// <summary>
            /// </summary>
            public TW_INFO Info_125;
            /// <summary>
            /// </summary>
            public TW_INFO Info_126;
            /// <summary>
            /// </summary>
            public TW_INFO Info_127;
            /// <summary>
            /// </summary>
            public TW_INFO Info_128;
            /// <summary>
            /// </summary>
            public TW_INFO Info_129;
            /// <summary>
            /// </summary>
            public TW_INFO Info_130;
            /// <summary>
            /// </summary>
            public TW_INFO Info_131;
            /// <summary>
            /// </summary>
            public TW_INFO Info_132;
            /// <summary></summary>
            public TW_INFO Info_133;
            /// <summary>
            /// </summary>
            public TW_INFO Info_134;
            /// <summary>
            /// </summary>
            public TW_INFO Info_135;
            /// <summary>
            /// </summary>
            public TW_INFO Info_136;
            /// <summary>
            /// </summary>
            public TW_INFO Info_137;
            /// <summary>
            /// </summary>
            public TW_INFO Info_138;
            /// <summary>
            /// </summary>
            public TW_INFO Info_139;
            /// <summary>
            /// </summary>
            public TW_INFO Info_140;
            /// <summary>
            /// </summary>
            public TW_INFO Info_141;
            /// <summary>
            /// </summary>
            public TW_INFO Info_142;
            /// <summary>
            /// </summary>
            public TW_INFO Info_143;
            /// <summary>
            /// </summary>
            public TW_INFO Info_144;
            /// <summary>
            /// </summary>
            public TW_INFO Info_145;
            /// <summary>
            /// </summary>
            public TW_INFO Info_146;
            /// <summary>
            /// </summary>
            public TW_INFO Info_147;
            /// <summary>
            /// </summary>
            public TW_INFO Info_148;
            /// <summary>
            /// </summary>
            public TW_INFO Info_149;
            /// <summary>
            /// </summary>
            public TW_INFO Info_150;
            /// <summary>
            /// </summary>
            public TW_INFO Info_151;
            /// <summary>
            /// </summary>
            public TW_INFO Info_152;
            /// <summary>
            /// </summary>
            public TW_INFO Info_153;
            /// <summary>
            /// </summary>
            public TW_INFO Info_154;
            /// <summary>
            /// </summary>
            public TW_INFO Info_155;
            /// <summary>
            /// </summary>
            public TW_INFO Info_156;
            /// <summary>
            /// </summary>
            public TW_INFO Info_157;
            /// <summary>
            /// </summary>
            public TW_INFO Info_158;
            /// <summary>
            /// </summary>
            public TW_INFO Info_159;
            /// <summary>
            /// </summary>
            public TW_INFO Info_160;
            /// <summary>
            /// </summary>
            public TW_INFO Info_161;
            /// <summary>
            /// </summary>
            public TW_INFO Info_162;
            /// <summary>
            /// </summary>
            public TW_INFO Info_163;
            /// <summary>
            /// </summary>
            public TW_INFO Info_164;
            /// <summary>
            /// </summary>
            public TW_INFO Info_165;
            /// <summary>
            /// </summary>
            public TW_INFO Info_166;
            /// <summary>
            /// </summary>
            public TW_INFO Info_167;
            /// <summary>
            /// </summary>
            public TW_INFO Info_168;
            /// <summary>
            /// </summary>
            public TW_INFO Info_169;
            /// <summary>
            /// </summary>
            public TW_INFO Info_170;
            /// <summary>
            /// </summary>
            public TW_INFO Info_171;
            /// <summary>
            /// </summary>
            public TW_INFO Info_172;
            /// <summary>
            /// </summary>
            public TW_INFO Info_173;
            /// <summary>
            /// </summary>
            public TW_INFO Info_174;
            /// <summary>
            /// </summary>
            public TW_INFO Info_175;
            /// <summary>
            /// </summary>
            public TW_INFO Info_176;
            /// <summary>
            /// </summary>
            public TW_INFO Info_177;
            /// <summary>
            /// </summary>
            public TW_INFO Info_178;
            /// <summary>
            /// </summary>
            public TW_INFO Info_179;
            /// <summary>
            /// </summary>
            public TW_INFO Info_180;
            /// <summary>
            /// </summary>
            public TW_INFO Info_181;
            /// <summary>
            /// </summary>
            public TW_INFO Info_182;
            /// <summary>
            /// </summary>
            public TW_INFO Info_183;
            /// <summary>
            /// </summary>
            public TW_INFO Info_184;
            /// <summary>
            /// </summary>
            public TW_INFO Info_185;
            /// <summary>
            /// </summary>
            public TW_INFO Info_186;
            /// <summary>
            /// </summary>
            public TW_INFO Info_187;
            /// <summary>
            /// </summary>
            public TW_INFO Info_188;
            /// <summary>
            /// </summary>
            public TW_INFO Info_189;
            /// <summary>
            /// </summary>
            public TW_INFO Info_190;
            /// <summary>
            /// </summary>
            public TW_INFO Info_191;
            /// <summary>
            /// </summary>
            public TW_INFO Info_192;
            /// <summary>
            /// </summary>
            public TW_INFO Info_193;
            /// <summary>
            /// </summary>
            public TW_INFO Info_194;
            /// <summary>
            /// </summary>
            public TW_INFO Info_195;
            /// <summary>
            /// </summary>
            public TW_INFO Info_196;
            /// <summary>
            /// </summary>
            public TW_INFO Info_197;
            /// <summary>
            /// </summary>
            public TW_INFO Info_198;
            /// <summary>
            /// </summary>
            public TW_INFO Info_199;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="a_uIndex"></param>
            /// <param name="a_twinfo"></param>
            public void Get(uint a_uIndex, ref TW_INFO a_twinfo)
            {
                switch (a_uIndex)
                {
                    default: return;
                    case 0: a_twinfo = Info_000; return;
                    case 1: a_twinfo = Info_001; return;
                    case 2: a_twinfo = Info_002; return;
                    case 3: a_twinfo = Info_003; return;
                    case 4: a_twinfo = Info_004; return;
                    case 5: a_twinfo = Info_005; return;
                    case 6: a_twinfo = Info_006; return;
                    case 7: a_twinfo = Info_007; return;
                    case 8: a_twinfo = Info_008; return;
                    case 9: a_twinfo = Info_009; return;
                    case 10: a_twinfo = Info_010; return;
                    case 11: a_twinfo = Info_011; return;
                    case 12: a_twinfo = Info_012; return;
                    case 13: a_twinfo = Info_013; return;
                    case 14: a_twinfo = Info_014; return;
                    case 15: a_twinfo = Info_015; return;
                    case 16: a_twinfo = Info_016; return;
                    case 17: a_twinfo = Info_017; return;
                    case 18: a_twinfo = Info_018; return;
                    case 19: a_twinfo = Info_019; return;
                    case 20: a_twinfo = Info_020; return;
                    case 21: a_twinfo = Info_021; return;
                    case 22: a_twinfo = Info_022; return;
                    case 23: a_twinfo = Info_023; return;
                    case 24: a_twinfo = Info_024; return;
                    case 25: a_twinfo = Info_025; return;
                    case 26: a_twinfo = Info_026; return;
                    case 27: a_twinfo = Info_027; return;
                    case 28: a_twinfo = Info_028; return;
                    case 29: a_twinfo = Info_029; return;
                    case 30: a_twinfo = Info_030; return;
                    case 31: a_twinfo = Info_031; return;
                    case 32: a_twinfo = Info_032; return;
                    case 33: a_twinfo = Info_033; return;
                    case 34: a_twinfo = Info_034; return;
                    case 35: a_twinfo = Info_035; return;
                    case 36: a_twinfo = Info_036; return;
                    case 37: a_twinfo = Info_037; return;
                    case 38: a_twinfo = Info_038; return;
                    case 39: a_twinfo = Info_039; return;
                    case 40: a_twinfo = Info_040; return;
                    case 41: a_twinfo = Info_041; return;
                    case 42: a_twinfo = Info_042; return;
                    case 43: a_twinfo = Info_043; return;
                    case 44: a_twinfo = Info_044; return;
                    case 45: a_twinfo = Info_045; return;
                    case 46: a_twinfo = Info_046; return;
                    case 47: a_twinfo = Info_047; return;
                    case 48: a_twinfo = Info_048; return;
                    case 49: a_twinfo = Info_049; return;
                    case 50: a_twinfo = Info_050; return;
                    case 51: a_twinfo = Info_051; return;
                    case 52: a_twinfo = Info_052; return;
                    case 53: a_twinfo = Info_053; return;
                    case 54: a_twinfo = Info_054; return;
                    case 55: a_twinfo = Info_055; return;
                    case 56: a_twinfo = Info_056; return;
                    case 57: a_twinfo = Info_057; return;
                    case 58: a_twinfo = Info_058; return;
                    case 59: a_twinfo = Info_059; return;
                    case 60: a_twinfo = Info_060; return;
                    case 61: a_twinfo = Info_061; return;
                    case 62: a_twinfo = Info_062; return;
                    case 63: a_twinfo = Info_063; return;
                    case 64: a_twinfo = Info_064; return;
                    case 65: a_twinfo = Info_065; return;
                    case 66: a_twinfo = Info_066; return;
                    case 67: a_twinfo = Info_067; return;
                    case 68: a_twinfo = Info_068; return;
                    case 69: a_twinfo = Info_069; return;
                    case 70: a_twinfo = Info_070; return;
                    case 71: a_twinfo = Info_071; return;
                    case 72: a_twinfo = Info_072; return;
                    case 73: a_twinfo = Info_073; return;
                    case 74: a_twinfo = Info_074; return;
                    case 75: a_twinfo = Info_075; return;
                    case 76: a_twinfo = Info_076; return;
                    case 77: a_twinfo = Info_077; return;
                    case 78: a_twinfo = Info_078; return;
                    case 79: a_twinfo = Info_079; return;
                    case 80: a_twinfo = Info_080; return;
                    case 81: a_twinfo = Info_081; return;
                    case 82: a_twinfo = Info_082; return;
                    case 83: a_twinfo = Info_083; return;
                    case 84: a_twinfo = Info_084; return;
                    case 85: a_twinfo = Info_085; return;
                    case 86: a_twinfo = Info_086; return;
                    case 87: a_twinfo = Info_087; return;
                    case 88: a_twinfo = Info_088; return;
                    case 89: a_twinfo = Info_089; return;
                    case 90: a_twinfo = Info_090; return;
                    case 91: a_twinfo = Info_091; return;
                    case 92: a_twinfo = Info_092; return;
                    case 93: a_twinfo = Info_093; return;
                    case 94: a_twinfo = Info_094; return;
                    case 95: a_twinfo = Info_095; return;
                    case 96: a_twinfo = Info_096; return;
                    case 97: a_twinfo = Info_097; return;
                    case 98: a_twinfo = Info_098; return;
                    case 99: a_twinfo = Info_099; return;
                    case 100: a_twinfo = Info_100; return;
                    case 101: a_twinfo = Info_101; return;
                    case 102: a_twinfo = Info_102; return;
                    case 103: a_twinfo = Info_103; return;
                    case 104: a_twinfo = Info_104; return;
                    case 105: a_twinfo = Info_105; return;
                    case 106: a_twinfo = Info_106; return;
                    case 107: a_twinfo = Info_107; return;
                    case 108: a_twinfo = Info_108; return;
                    case 109: a_twinfo = Info_109; return;
                    case 110: a_twinfo = Info_110; return;
                    case 111: a_twinfo = Info_111; return;
                    case 112: a_twinfo = Info_112; return;
                    case 113: a_twinfo = Info_113; return;
                    case 114: a_twinfo = Info_114; return;
                    case 115: a_twinfo = Info_115; return;
                    case 116: a_twinfo = Info_116; return;
                    case 117: a_twinfo = Info_117; return;
                    case 118: a_twinfo = Info_118; return;
                    case 119: a_twinfo = Info_119; return;
                    case 120: a_twinfo = Info_120; return;
                    case 121: a_twinfo = Info_121; return;
                    case 122: a_twinfo = Info_122; return;
                    case 123: a_twinfo = Info_123; return;
                    case 124: a_twinfo = Info_124; return;
                    case 125: a_twinfo = Info_125; return;
                    case 126: a_twinfo = Info_126; return;
                    case 127: a_twinfo = Info_127; return;
                    case 128: a_twinfo = Info_128; return;
                    case 129: a_twinfo = Info_129; return;
                    case 130: a_twinfo = Info_130; return;
                    case 131: a_twinfo = Info_131; return;
                    case 132: a_twinfo = Info_132; return;
                    case 133: a_twinfo = Info_133; return;
                    case 134: a_twinfo = Info_134; return;
                    case 135: a_twinfo = Info_135; return;
                    case 136: a_twinfo = Info_136; return;
                    case 137: a_twinfo = Info_137; return;
                    case 138: a_twinfo = Info_138; return;
                    case 139: a_twinfo = Info_139; return;
                    case 140: a_twinfo = Info_140; return;
                    case 141: a_twinfo = Info_141; return;
                    case 142: a_twinfo = Info_142; return;
                    case 143: a_twinfo = Info_143; return;
                    case 144: a_twinfo = Info_144; return;
                    case 145: a_twinfo = Info_145; return;
                    case 146: a_twinfo = Info_146; return;
                    case 147: a_twinfo = Info_147; return;
                    case 148: a_twinfo = Info_148; return;
                    case 149: a_twinfo = Info_149; return;
                    case 150: a_twinfo = Info_150; return;
                    case 151: a_twinfo = Info_151; return;
                    case 152: a_twinfo = Info_152; return;
                    case 153: a_twinfo = Info_153; return;
                    case 154: a_twinfo = Info_154; return;
                    case 155: a_twinfo = Info_155; return;
                    case 156: a_twinfo = Info_156; return;
                    case 157: a_twinfo = Info_157; return;
                    case 158: a_twinfo = Info_158; return;
                    case 159: a_twinfo = Info_159; return;
                    case 160: a_twinfo = Info_160; return;
                    case 161: a_twinfo = Info_161; return;
                    case 162: a_twinfo = Info_162; return;
                    case 163: a_twinfo = Info_163; return;
                    case 164: a_twinfo = Info_164; return;
                    case 165: a_twinfo = Info_165; return;
                    case 166: a_twinfo = Info_166; return;
                    case 167: a_twinfo = Info_167; return;
                    case 168: a_twinfo = Info_168; return;
                    case 169: a_twinfo = Info_169; return;
                    case 170: a_twinfo = Info_170; return;
                    case 171: a_twinfo = Info_171; return;
                    case 172: a_twinfo = Info_172; return;
                    case 173: a_twinfo = Info_173; return;
                    case 174: a_twinfo = Info_174; return;
                    case 175: a_twinfo = Info_175; return;
                    case 176: a_twinfo = Info_176; return;
                    case 177: a_twinfo = Info_177; return;
                    case 178: a_twinfo = Info_178; return;
                    case 179: a_twinfo = Info_179; return;
                    case 180: a_twinfo = Info_180; return;
                    case 181: a_twinfo = Info_181; return;
                    case 182: a_twinfo = Info_182; return;
                    case 183: a_twinfo = Info_183; return;
                    case 184: a_twinfo = Info_184; return;
                    case 185: a_twinfo = Info_185; return;
                    case 186: a_twinfo = Info_186; return;
                    case 187: a_twinfo = Info_187; return;
                    case 188: a_twinfo = Info_188; return;
                    case 189: a_twinfo = Info_189; return;
                    case 190: a_twinfo = Info_190; return;
                    case 191: a_twinfo = Info_191; return;
                    case 192: a_twinfo = Info_192; return;
                    case 193: a_twinfo = Info_193; return;
                    case 194: a_twinfo = Info_194; return;
                    case 195: a_twinfo = Info_195; return;
                    case 196: a_twinfo = Info_196; return;
                    case 197: a_twinfo = Info_197; return;
                    case 198: a_twinfo = Info_198; return;
                    case 199: a_twinfo = Info_199; return;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="a_uIndex"></param>
            /// <param name="a_twinfo"></param>
            public void Set(uint a_uIndex, ref TW_INFO a_twinfo)
            {
                switch (a_uIndex)
                {
                    default: return;
                    case 0: Info_000 = a_twinfo; return;
                    case 1: Info_001 = a_twinfo; return;
                    case 2: Info_002 = a_twinfo; return;
                    case 3: Info_003 = a_twinfo; return;
                    case 4: Info_004 = a_twinfo; return;
                    case 5: Info_005 = a_twinfo; return;
                    case 6: Info_006 = a_twinfo; return;
                    case 7: Info_007 = a_twinfo; return;
                    case 8: Info_008 = a_twinfo; return;
                    case 9: Info_009 = a_twinfo; return;
                    case 10: Info_010 = a_twinfo; return;
                    case 11: Info_011 = a_twinfo; return;
                    case 12: Info_012 = a_twinfo; return;
                    case 13: Info_013 = a_twinfo; return;
                    case 14: Info_014 = a_twinfo; return;
                    case 15: Info_015 = a_twinfo; return;
                    case 16: Info_016 = a_twinfo; return;
                    case 17: Info_017 = a_twinfo; return;
                    case 18: Info_018 = a_twinfo; return;
                    case 19: Info_019 = a_twinfo; return;
                    case 20: Info_020 = a_twinfo; return;
                    case 21: Info_021 = a_twinfo; return;
                    case 22: Info_022 = a_twinfo; return;
                    case 23: Info_023 = a_twinfo; return;
                    case 24: Info_024 = a_twinfo; return;
                    case 25: Info_025 = a_twinfo; return;
                    case 26: Info_026 = a_twinfo; return;
                    case 27: Info_027 = a_twinfo; return;
                    case 28: Info_028 = a_twinfo; return;
                    case 29: Info_029 = a_twinfo; return;
                    case 30: Info_030 = a_twinfo; return;
                    case 31: Info_031 = a_twinfo; return;
                    case 32: Info_032 = a_twinfo; return;
                    case 33: Info_033 = a_twinfo; return;
                    case 34: Info_034 = a_twinfo; return;
                    case 35: Info_035 = a_twinfo; return;
                    case 36: Info_036 = a_twinfo; return;
                    case 37: Info_037 = a_twinfo; return;
                    case 38: Info_038 = a_twinfo; return;
                    case 39: Info_039 = a_twinfo; return;
                    case 40: Info_040 = a_twinfo; return;
                    case 41: Info_041 = a_twinfo; return;
                    case 42: Info_042 = a_twinfo; return;
                    case 43: Info_043 = a_twinfo; return;
                    case 44: Info_044 = a_twinfo; return;
                    case 45: Info_045 = a_twinfo; return;
                    case 46: Info_046 = a_twinfo; return;
                    case 47: Info_047 = a_twinfo; return;
                    case 48: Info_048 = a_twinfo; return;
                    case 49: Info_049 = a_twinfo; return;
                    case 50: Info_050 = a_twinfo; return;
                    case 51: Info_051 = a_twinfo; return;
                    case 52: Info_052 = a_twinfo; return;
                    case 53: Info_053 = a_twinfo; return;
                    case 54: Info_054 = a_twinfo; return;
                    case 55: Info_055 = a_twinfo; return;
                    case 56: Info_056 = a_twinfo; return;
                    case 57: Info_057 = a_twinfo; return;
                    case 58: Info_058 = a_twinfo; return;
                    case 59: Info_059 = a_twinfo; return;
                    case 60: Info_060 = a_twinfo; return;
                    case 61: Info_061 = a_twinfo; return;
                    case 62: Info_062 = a_twinfo; return;
                    case 63: Info_063 = a_twinfo; return;
                    case 64: Info_064 = a_twinfo; return;
                    case 65: Info_065 = a_twinfo; return;
                    case 66: Info_066 = a_twinfo; return;
                    case 67: Info_067 = a_twinfo; return;
                    case 68: Info_068 = a_twinfo; return;
                    case 69: Info_069 = a_twinfo; return;
                    case 70: Info_070 = a_twinfo; return;
                    case 71: Info_071 = a_twinfo; return;
                    case 72: Info_072 = a_twinfo; return;
                    case 73: Info_073 = a_twinfo; return;
                    case 74: Info_074 = a_twinfo; return;
                    case 75: Info_075 = a_twinfo; return;
                    case 76: Info_076 = a_twinfo; return;
                    case 77: Info_077 = a_twinfo; return;
                    case 78: Info_078 = a_twinfo; return;
                    case 79: Info_079 = a_twinfo; return;
                    case 80: Info_080 = a_twinfo; return;
                    case 81: Info_081 = a_twinfo; return;
                    case 82: Info_082 = a_twinfo; return;
                    case 83: Info_083 = a_twinfo; return;
                    case 84: Info_084 = a_twinfo; return;
                    case 85: Info_085 = a_twinfo; return;
                    case 86: Info_086 = a_twinfo; return;
                    case 87: Info_087 = a_twinfo; return;
                    case 88: Info_088 = a_twinfo; return;
                    case 89: Info_089 = a_twinfo; return;
                    case 90: Info_090 = a_twinfo; return;
                    case 91: Info_091 = a_twinfo; return;
                    case 92: Info_092 = a_twinfo; return;
                    case 93: Info_093 = a_twinfo; return;
                    case 94: Info_094 = a_twinfo; return;
                    case 95: Info_095 = a_twinfo; return;
                    case 96: Info_096 = a_twinfo; return;
                    case 97: Info_097 = a_twinfo; return;
                    case 98: Info_098 = a_twinfo; return;
                    case 99: Info_099 = a_twinfo; return;
                    case 100: Info_100 = a_twinfo; return;
                    case 101: Info_101 = a_twinfo; return;
                    case 102: Info_102 = a_twinfo; return;
                    case 103: Info_103 = a_twinfo; return;
                    case 104: Info_104 = a_twinfo; return;
                    case 105: Info_105 = a_twinfo; return;
                    case 106: Info_106 = a_twinfo; return;
                    case 107: Info_107 = a_twinfo; return;
                    case 108: Info_108 = a_twinfo; return;
                    case 109: Info_109 = a_twinfo; return;
                    case 110: Info_110 = a_twinfo; return;
                    case 111: Info_111 = a_twinfo; return;
                    case 112: Info_112 = a_twinfo; return;
                    case 113: Info_113 = a_twinfo; return;
                    case 114: Info_114 = a_twinfo; return;
                    case 115: Info_115 = a_twinfo; return;
                    case 116: Info_116 = a_twinfo; return;
                    case 117: Info_117 = a_twinfo; return;
                    case 118: Info_118 = a_twinfo; return;
                    case 119: Info_119 = a_twinfo; return;
                    case 120: Info_120 = a_twinfo; return;
                    case 121: Info_121 = a_twinfo; return;
                    case 122: Info_122 = a_twinfo; return;
                    case 123: Info_123 = a_twinfo; return;
                    case 124: Info_124 = a_twinfo; return;
                    case 125: Info_125 = a_twinfo; return;
                    case 126: Info_126 = a_twinfo; return;
                    case 127: Info_127 = a_twinfo; return;
                    case 128: Info_128 = a_twinfo; return;
                    case 129: Info_129 = a_twinfo; return;
                    case 130: Info_130 = a_twinfo; return;
                    case 131: Info_131 = a_twinfo; return;
                    case 132: Info_132 = a_twinfo; return;
                    case 133: Info_133 = a_twinfo; return;
                    case 134: Info_134 = a_twinfo; return;
                    case 135: Info_135 = a_twinfo; return;
                    case 136: Info_136 = a_twinfo; return;
                    case 137: Info_137 = a_twinfo; return;
                    case 138: Info_138 = a_twinfo; return;
                    case 139: Info_139 = a_twinfo; return;
                    case 140: Info_140 = a_twinfo; return;
                    case 141: Info_141 = a_twinfo; return;
                    case 142: Info_142 = a_twinfo; return;
                    case 143: Info_143 = a_twinfo; return;
                    case 144: Info_144 = a_twinfo; return;
                    case 145: Info_145 = a_twinfo; return;
                    case 146: Info_146 = a_twinfo; return;
                    case 147: Info_147 = a_twinfo; return;
                    case 148: Info_148 = a_twinfo; return;
                    case 149: Info_149 = a_twinfo; return;
                    case 150: Info_150 = a_twinfo; return;
                    case 151: Info_151 = a_twinfo; return;
                    case 152: Info_152 = a_twinfo; return;
                    case 153: Info_153 = a_twinfo; return;
                    case 154: Info_154 = a_twinfo; return;
                    case 155: Info_155 = a_twinfo; return;
                    case 156: Info_156 = a_twinfo; return;
                    case 157: Info_157 = a_twinfo; return;
                    case 158: Info_158 = a_twinfo; return;
                    case 159: Info_159 = a_twinfo; return;
                    case 160: Info_160 = a_twinfo; return;
                    case 161: Info_161 = a_twinfo; return;
                    case 162: Info_162 = a_twinfo; return;
                    case 163: Info_163 = a_twinfo; return;
                    case 164: Info_164 = a_twinfo; return;
                    case 165: Info_165 = a_twinfo; return;
                    case 166: Info_166 = a_twinfo; return;
                    case 167: Info_167 = a_twinfo; return;
                    case 168: Info_168 = a_twinfo; return;
                    case 169: Info_169 = a_twinfo; return;
                    case 170: Info_170 = a_twinfo; return;
                    case 171: Info_171 = a_twinfo; return;
                    case 172: Info_172 = a_twinfo; return;
                    case 173: Info_173 = a_twinfo; return;
                    case 174: Info_174 = a_twinfo; return;
                    case 175: Info_175 = a_twinfo; return;
                    case 176: Info_176 = a_twinfo; return;
                    case 177: Info_177 = a_twinfo; return;
                    case 178: Info_178 = a_twinfo; return;
                    case 179: Info_179 = a_twinfo; return;
                    case 180: Info_180 = a_twinfo; return;
                    case 181: Info_181 = a_twinfo; return;
                    case 182: Info_182 = a_twinfo; return;
                    case 183: Info_183 = a_twinfo; return;
                    case 184: Info_184 = a_twinfo; return;
                    case 185: Info_185 = a_twinfo; return;
                    case 186: Info_186 = a_twinfo; return;
                    case 187: Info_187 = a_twinfo; return;
                    case 188: Info_188 = a_twinfo; return;
                    case 189: Info_189 = a_twinfo; return;
                    case 190: Info_190 = a_twinfo; return;
                    case 191: Info_191 = a_twinfo; return;
                    case 192: Info_192 = a_twinfo; return;
                    case 193: Info_193 = a_twinfo; return;
                    case 194: Info_194 = a_twinfo; return;
                    case 195: Info_195 = a_twinfo; return;
                    case 196: Info_196 = a_twinfo; return;
                    case 197: Info_197 = a_twinfo; return;
                    case 198: Info_198 = a_twinfo; return;
                    case 199: Info_199 = a_twinfo; return;
                }
            }
        }

        /// <summary>
        /// Provides information about the currently selected device.
        /// TBD -- need a 32/64 bit solution for this mess
        /// </summary>
        [SuppressMessage("Microsoft.Portability", "CA1900:ValueTypeFieldsShouldBePortable", MessageId = "ModifiedTimeDate")]
        [SuppressMessage("Microsoft.Portability", "CA1900:ValueTypeFieldsShouldBePortable", MessageId = "CreateTimeDate")]
        [StructLayout(LayoutKind.Explicit, Pack = 2)]
        public struct TW_FILESYSTEM
        {
            /// <summary>
            /// </summary>
            [FieldOffset(0)]
            public TW_STR255 InputName;

            /// <summary>
            /// </summary>
            [FieldOffset(256)]
            public TW_STR255 OutputName;

            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            [FieldOffset(512)]
            public IntPtr Context;

            /// <summary>
            /// </summary>
            [FieldOffset(520)]
            public Int32 Recursive;

            /// <summary>
            /// </summary>
            [FieldOffset(520)]
            public UInt16 Subdirectories;

            /// <summary>
            /// </summary>
            [FieldOffset(524)]
            public Int32 FileType;

            /// <summary>
            /// </summary>
            [FieldOffset(524)]
            public UInt32 FileSystemType;

            /// <summary>
            /// </summary>
            [FieldOffset(528)]
            public UInt32 Size;

            /// <summary>
            /// </summary>
            [FieldOffset(532)]
            public TW_STR32 CreateTimeDate;

            /// <summary>
            /// </summary>
            [FieldOffset(566)]
            public TW_STR32 ModifiedTimeDate;

            /// <summary>
            /// </summary>
            [FieldOffset(600)]
            public UInt32 FreeSpace;

            /// <summary>
            /// </summary>
            [FieldOffset(604)]
            public UInt32 NewImageSize;

            /// <summary>
            /// </summary>
            [FieldOffset(608)]
            public UInt32 NumberOfFiles;

            /// <summary>
            /// </summary>
            [FieldOffset(612)]
            public UInt32 NumberOfSnippets;

            /// <summary>
            /// </summary>
            [FieldOffset(616)]
            public UInt32 DeviceGroupMask;

            /// <summary>
            /// </summary>
            [FieldOffset(620)]
            public byte Reserved;

            /// <summary>
            /// </summary>
            [FieldOffset(1127)] // 620 + 508 - 1
            private byte ReservedEnd;
        }

        /// <summary>
        /// </summary>
        [SuppressMessage("Microsoft.Portability", "CA1900:ValueTypeFieldsShouldBePortable", MessageId = "ModifiedTimeDate")]
        [StructLayout(LayoutKind.Explicit, Pack = 2)]
        public struct TW_FILESYSTEM_LEGACY
        {
            /// <summary>
            /// </summary>
            [FieldOffset(0)]
            public TW_STR255 InputName;

            /// <summary>
            /// </summary>
            [FieldOffset(256)]
            public TW_STR255 OutputName;

            /// <summary>
            /// </summary>
            [FieldOffset(512)]
            public UInt32 Context;

            /// <summary>
            /// </summary>
            [FieldOffset(516)]
            public Int32 Recursive;

            /// <summary>
            /// </summary>
            [FieldOffset(516)]
            public UInt16 Subdirectories;

            /// <summary>
            /// </summary>
            [FieldOffset(520)]
            public Int32 FileType;

            /// <summary>
            /// </summary>
            [FieldOffset(520)]
            public UInt32 FileSystemType;

            /// <summary>
            /// </summary>
            [FieldOffset(524)]
            public UInt32 Size;

            /// <summary>
            /// </summary>
            [FieldOffset(528)]
            public TW_STR32 CreateTimeDate;

            /// <summary>
            /// </summary>
            [FieldOffset(562)]
            public TW_STR32 ModifiedTimeDate;

            /// <summary>
            /// </summary>
            [FieldOffset(596)]
            public UInt32 FreeSpace;

            /// <summary>
            /// </summary>
            [FieldOffset(600)]
            public UInt32 NewImageSize;

            /// <summary>
            /// </summary>
            [FieldOffset(604)]
            public UInt32 NumberOfFiles;

            /// <summary>
            /// </summary>
            [FieldOffset(608)]
            public UInt32 NumberOfSnippets;

            /// <summary>
            /// </summary>
            [FieldOffset(612)]
            public UInt32 DeviceGroupMask;

            /// <summary>
            /// </summary>
            [FieldOffset(616)]
            public byte Reserved;

            /// <summary>
            /// </summary>
            [FieldOffset(1123)] // 616 + 508 - 1
            private byte ReservedEnd;
        }

        /// <summary>
        /// This structure is used by the application to specify a set of mapping values to be applied to grayscale data.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_GRAYRESPONSE
        {
            /// <summary>
            /// </summary>
            public TW_ELEMENT8 Response_00;
        }

        /// <summary>
        /// A general way to describe the version of software that is running.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_VERSION
        {
            /// <summary>
            /// </summary>
            public ushort MajorNum;
            /// <summary>
            /// </summary>
            public ushort MinorNum;
            /// <summary>
            /// </summary>
            public TWLG Language;
            /// <summary>
            /// </summary>
            public TWCY Country;
            /// <summary>
            /// </summary>
            public TW_STR32 Info;
        }

        /// <summary>
        /// Provides identification information about a TWAIN entity.
        /// The use of Padding is there to allow us to use the structure
        /// with Linux 64-bit systems where the TW_INT32 and TW_UINT32
        /// types were long, and therefore 64-bits in size.  This should
        /// have no impact with well-behaved systems that have these types
        /// as 32-bit, but should prevent memory corruption in all other
        /// situations...
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_IDENTITY
        {
            /// <summary>
            /// </summary>
            public ulong Id;
            /// <summary>
            /// </summary>
            public TW_VERSION Version;
            /// <summary>
            /// </summary>
            public ushort ProtocolMajor;
            /// <summary>
            /// </summary>
            public ushort ProtocolMinor;
            /// <summary>
            /// </summary>
            public uint SupportedGroups;
            /// <summary>
            /// </summary>
            public TW_STR32 Manufacturer;
            /// <summary>
            /// </summary>
            public TW_STR32 ProductFamily;
            /// <summary>
            /// </summary>
            public TW_STR32 ProductName;
        }

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_IDENTITY_LEGACY
        {
            /// <summary>
            /// </summary>
            public uint Id;
            /// <summary>
            /// </summary>
            public TW_VERSION Version;
            /// <summary>
            /// </summary>
            public ushort ProtocolMajor;
            /// <summary>
            /// </summary>
            public ushort ProtocolMinor;
            /// <summary>
            /// </summary>
            public uint SupportedGroups;
            /// <summary>
            /// </summary>
            public TW_STR32 Manufacturer;
            /// <summary>
            /// </summary>
            public TW_STR32 ProductFamily;
            /// <summary>
            /// </summary>
            public TW_STR32 ProductName;
            private UInt64 Padding; // accounts for Id and SupportedGroups
        }

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_IDENTITY_LINUX64
        {
            /// <summary>
            /// </summary>
            public ulong Id;
            /// <summary>
            /// </summary>
            public TW_VERSION Version;
            /// <summary>
            /// </summary>
            public ushort ProtocolMajor;
            /// <summary>
            /// </summary>
            public ushort ProtocolMinor;
            /// <summary>
            /// </summary>
            public ulong SupportedGroups;
            /// <summary>
            /// </summary>
            public TW_STR32 Manufacturer;
            /// <summary>
            /// </summary>
            public TW_STR32 ProductFamily;
            /// <summary>
            /// </summary>
            public TW_STR32 ProductName;
        }

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_IDENTITY_MACOSX
        {
            /// <summary>
            /// </summary>
            public uint Id;
            /// <summary>
            /// </summary>
            public TW_VERSION Version;
            /// <summary>
            /// </summary>
            public ushort ProtocolMajor;
            /// <summary>
            /// </summary>
            public ushort ProtocolMinor;
            /// <summary>
            /// </summary>
            private ushort padding;
            /// <summary>
            /// </summary>
            public uint SupportedGroups;
            /// <summary>
            /// </summary>
            public TW_STR32 Manufacturer;
            /// <summary>
            /// </summary>
            public TW_STR32 ProductFamily;
            /// <summary>
            /// </summary>
            public TW_STR32 ProductName;
        }

        /// <summary>
        /// Describes the “real” image data, that is, the complete image being transferred between the Source and application.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_IMAGEINFO
        {
            /// <summary>
            /// </summary>
            public TW_FIX32 XResolution;
            /// <summary>
            /// </summary>
            public TW_FIX32 YResolution;
            /// <summary>
            /// </summary>
            public int ImageWidth;
            /// <summary>
            /// </summary>
            public int ImageLength;
            /// <summary>
            /// </summary>
            public short SamplesPerPixel;
            /// <summary>
            /// </summary>
            public short BitsPerSample_0;
            /// <summary>
            /// </summary>
            public short BitsPerSample_1;
            /// <summary>
            /// </summary>
            public short BitsPerSample_2;
            /// <summary>
            /// </summary>
            public short BitsPerSample_3;
            /// <summary>
            /// </summary>
            public short BitsPerSample_4;
            /// <summary>
            /// </summary>
            public short BitsPerSample_5;
            /// <summary>
            /// </summary>
            public short BitsPerSample_6;
            /// <summary>
            /// </summary>
            public short BitsPerSample_7;
            /// <summary>
            /// </summary>
            public short BitsPerPixel;
            /// <summary>
            /// </summary>
            public ushort Planar;
            /// <summary>
            /// </summary>
            public short PixelType;
            /// <summary>
            /// </summary>
            public ushort Compression;
        }

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_IMAGEINFO_LINUX64
        {
            /// <summary>
            /// </summary>
            public TW_FIX32 XResolution;
            /// <summary>
            /// </summary>
            public TW_FIX32 YResolution;
            /// <summary>
            /// </summary>
            public int ImageWidth;
            /// <summary>
            /// </summary>
            public int ImageLength;
            /// <summary>
            /// </summary>
            public short SamplesPerPixel;
            /// <summary>
            /// </summary>
            public short BitsPerSample_0;
            /// <summary>
            /// </summary>
            public short BitsPerSample_1;
            /// <summary>
            /// </summary>
            public short BitsPerSample_2;
            /// <summary>
            /// </summary>
            public short BitsPerSample_3;
            /// <summary>
            /// </summary>
            public short BitsPerSample_4;
            /// <summary>
            /// </summary>
            public short BitsPerSample_5;
            /// <summary>
            /// </summary>
            public short BitsPerSample_6;
            /// <summary>
            /// </summary>
            public short BitsPerSample_7;
            /// <summary>
            /// </summary>
            public short BitsPerPixel;
            /// <summary>
            /// </summary>
            public ushort Planar;
            /// <summary>
            /// </summary>
            public short PixelType;
            /// <summary>
            /// </summary>
            public ushort Compression;
        }

        /// <summary>
        /// Involves information about the original size of the acquired image.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_IMAGELAYOUT
        {
            /// <summary>
            /// </summary>
            public TW_FRAME Frame;
            /// <summary>
            /// </summary>
            public uint DocumentNumber;
            /// <summary>
            /// </summary>
            public uint PageNumber;
            /// <summary>
            /// </summary>
            public uint FrameNumber;
        }

        /// <summary>
        /// Provides information for managing memory buffers.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_MEMORY
        {
            /// <summary>
            /// </summary>
            public uint Flags;
            /// <summary>
            /// </summary>
            public uint Length;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr TheMem;
        }

        /// <summary>
        /// Describes the form of the acquired data being passed from the Source to the application.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_IMAGEMEMXFER
        {
            /// <summary>
            /// </summary>
            public ushort Compression;
            /// <summary>
            /// </summary>
            public uint BytesPerRow;
            /// <summary>
            /// </summary>
            public uint Columns;
            /// <summary>
            /// </summary>
            public uint Rows;
            /// <summary>
            /// </summary>
            public uint XOffset;
            /// <summary>
            /// </summary>
            public uint YOffset;
            /// <summary>
            /// </summary>
            public uint BytesWritten;
            /// <summary>
            /// </summary>
            public TW_MEMORY Memory;
        }
        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_IMAGEMEMXFER_LINUX64
        {
            /// <summary>
            /// </summary>
            public ushort Compression;
            /// <summary>
            /// </summary>
            public UInt64 BytesPerRow;
            /// <summary>
            /// </summary>
            public UInt64 Columns;
            /// <summary>
            /// </summary>
            public UInt64 Rows;
            /// <summary>
            /// </summary>
            public UInt64 XOffset;
            /// <summary>
            /// </summary>
            public UInt64 YOffset;
            /// <summary>
            /// </summary>
            public UInt64 BytesWritten;
            /// <summary>
            /// </summary>
            public UInt64 MemoryFlags;
            /// <summary>
            /// </summary>
            public UInt64 MemoryLength;

            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr MemoryTheMem;
        }

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_IMAGEMEMXFER_MACOSX
        {
            /// <summary>
            /// </summary>
            public uint Compression;
            /// <summary>
            /// </summary>
            public uint BytesPerRow;
            /// <summary>
            /// </summary>
            public uint Columns;
            /// <summary>
            /// </summary>
            public uint Rows;
            /// <summary>
            /// </summary>
            public uint XOffset;
            /// <summary>
            /// </summary>
            public uint YOffset;
            /// <summary>
            /// </summary>
            public uint BytesWritten;
            /// <summary>
            /// </summary>
            public TW_MEMORY Memory;
        }

        /// <summary>
        /// Describes the information necessary to transfer a JPEG-compressed image.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_JPEGCOMPRESSION
        {
            /// <summary>
            /// </summary>
            public ushort ColorSpace;
            /// <summary>
            /// </summary>
            public uint SubSampling;
            /// <summary>
            /// </summary>
            public ushort NumComponents;
            /// <summary>
            /// </summary>
            public ushort QuantMap_0;
            /// <summary>
            /// </summary>
            public ushort QuantMap_1;
            /// <summary>
            /// </summary>
            public ushort QuantMap_2;
            /// <summary>
            /// </summary>
            public ushort QuantMap_3;
            /// <summary>
            /// </summary>
            public TW_MEMORY QuantTable_0;
            /// <summary>
            /// </summary>
            public TW_MEMORY QuantTable_1;
            /// <summary>
            /// </summary>
            public TW_MEMORY QuantTable_2;
            /// <summary>
            /// </summary>
            public TW_MEMORY QuantTable_3;
            /// <summary>
            /// </summary>
            public ushort HuffmanMap_0;
            /// <summary>
            /// </summary>
            public ushort HuffmanMap_1;
            /// <summary>
            /// </summary>
            public ushort HuffmanMap_2;
            /// <summary>
            /// </summary>
            public ushort HuffmanMap_3;
            /// <summary>
            /// </summary>
            public TW_MEMORY HuffmanDC_0;
            /// <summary>
            /// </summary>
            public TW_MEMORY HuffmanDC_1;
            /// <summary>
            /// </summary>
            public TW_MEMORY HuffmanAC_0;
            /// <summary>
            /// </summary>
            public TW_MEMORY HuffmanAC_2;
        }

        /// <summary>
        /// Stores a single value (item) which describes a capability.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_ONEVALUE
        {
            /// <summary>
            /// </summary>
            public TWTY ItemType;
            // public uint Item;
        }
        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct TW_ONEVALUE_MACOSX
        {
            /// <summary>
            /// </summary>
            public uint ItemType;
            // public uint Item;
        }

        /// <summary>
        /// This structure holds the color palette information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_PALETTE8
        {
            /// <summary>
            /// </summary>
            public ushort Flags;
            /// <summary>
            /// </summary>
            public ushort Length;
            /// <summary>
            /// </summary>
            public TW_ELEMENT8 Colors_000;
        }

        /// <summary>
        /// Used to bypass the TWAIN protocol when communicating with a device.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_PASSTHRU
        {
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr pCommand;
            /// <summary>
            /// </summary>
            public uint CommandBytes;
            /// <summary>
            /// </summary>
            public int Direction;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr pData;
            /// <summary>
            /// </summary>
            public uint DataBytes;
            /// <summary>
            /// </summary>
            public uint DataBytesXfered;
        }

        /// <summary>
        /// This structure tells the application how many more complete transfers the Source currently has available.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_PENDINGXFERS
        {
            /// <summary>
            /// </summary>
            public ushort Count;
            /// <summary>
            /// </summary>
            public uint EOJ;
        }

        /// <summary>
        /// Stores a range of individual values describing a capability.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_RANGE
        {
            /// <summary>
            /// </summary>
            public TWTY ItemType;
            /// <summary>
            /// </summary>
            public uint MinValue;
            /// <summary>
            /// </summary>
            public uint MaxValue;
            /// <summary>
            /// </summary>
            public uint StepSize;
            /// <summary>
            /// </summary>
            public uint DefaultValue;
            /// <summary>
            /// </summary>
            public uint CurrentValue;
        }

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_RANGE_LINUX64
        {
            /// <summary>
            /// </summary>
            public TWTY ItemType;
            /// <summary>
            /// </summary>
            public ulong MinValue;
            /// <summary>
            /// </summary>
            public ulong MaxValue;
            /// <summary>
            /// </summary>
            public ulong StepSize;
            /// <summary>
            /// </summary>
            public ulong DefaultValue;
            /// <summary>
            /// </summary>
            public ulong CurrentValue;
        }

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct TW_RANGE_MACOSX
        {
            /// <summary>
            /// </summary>
            public uint ItemType;
            /// <summary>
            /// </summary>
            public uint MinValue;
            /// <summary>
            /// </summary>
            public uint MaxValue;
            /// <summary>
            /// </summary>
            public uint StepSize;
            /// <summary>
            /// </summary>
            public uint DefaultValue;
            /// <summary>
            /// </summary>
            public uint CurrentValue;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        private struct TW_RANGE_FIX32
        {
            public TWTY ItemType;
            public TW_FIX32 MinValue;
            public TW_FIX32 MaxValue;
            public TW_FIX32 StepSize;
            public TW_FIX32 DefaultValue;
            public TW_FIX32 CurrentValue;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct TW_RANGE_FIX32_MACOSX
        {
            public uint ItemType;
            public TW_FIX32 MinValue;
            public TW_FIX32 MaxValue;
            public TW_FIX32 StepSize;
            public TW_FIX32 DefaultValue;
            public TW_FIX32 CurrentValue;
        }

        /// <summary>
        /// This structure is used by the application to specify a set of mapping values to be applied to RGB color data.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_RGBRESPONSE
        {
            /// <summary>
            /// </summary>
            public TW_ELEMENT8 Response_00;
        }

        /// <summary>
        /// Describes the file format and file specification information for a transfer through a disk file.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_SETUPFILEXFER
        {
            /// <summary>
            /// </summary>
            public TW_STR255 FileName;
            /// <summary>
            /// </summary>
            public TWFF Format;
            /// <summary>
            /// </summary>
            public short VRefNum;
        }

        /// <summary>
        /// Provides the application information about the Source’s requirements and preferences regarding allocation of transfer buffer(s).
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_SETUPMEMXFER
        {

            /// <summary>
            /// </summary>
            public uint MinBufSize;
            /// <summary>
            /// </summary>
            public uint MaxBufSize;
            /// <summary>
            /// </summary>
            public uint Preferred;
        }

        /// <summary>
        /// Describes the status of a source.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_STATUS
        {
            /// <summary>
            /// </summary>
            public ushort ConditionCode;
            /// <summary>
            /// </summary>
            public ushort Data;
        }

        /// <summary>
        /// Translates the contents of Status into a localized UTF8string.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TW_STATUSUTF8
        {
            /// <summary>
            /// </summary>
            public TW_STATUS Status;
            /// <summary>
            /// </summary>
            public uint Size;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr UTF8string;
        }

        /// <summary>
        /// Passthru for TWAIN Direct tasks.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_TWAINDIRECT
        {
            /// <summary>
            /// </summary>
            public uint SizeOf;
            /// <summary>
            /// </summary>
            public ushort CommunicationManager;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr Send;
            /// <summary>
            /// </summary>
            public uint SendSize;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr Receive;
            /// <summary>
            /// </summary>
            public uint ReceiveSize;
        }

        /// <summary>
        /// This structure is used to handle the user interface coordination between an application and a Source.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        public struct TW_USERINTERFACE
        {
            /// <summary>
            /// </summary>
            public ushort ShowUI;
            /// <summary>
            /// </summary>
            public ushort ModalUI;
            /// <summary>
            /// </summary>
            [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
            public IntPtr hParent;
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Generic Constants...
        ///////////////////////////////////////////////////////////////////////////////
        #region Generic Constants...

        /// <summary>
        /// Container Types...
        /// </summary>
        public enum TWON : ushort
        {
            /// <summary></summary>
            ARRAY = 3,
            /// <summary></summary>
            ENUMERATION = 4,
            /// <summary></summary>
            ONEVALUE = 5,
            /// <summary></summary>
            RANGE = 6,

            /// <summary></summary>
            ICONID = 962,
            /// <summary></summary>
            DSMID = 461,
            /// <summary></summary>
            DSMCODEID = 63
        }

        /// <summary>
        /// Don't care values...
        /// </summary>
        public const byte TWON_DONTCARE8 = 0xff;
        /// <summary></summary>
        public const ushort TWON_DONTCARE16 = 0xff;
        /// <summary></summary>
        public const uint TWON_DONTCARE32 = 0xffffffff;

        /// <summary>
        /// Flags used in TW_MEMORY structure.
        /// </summary>
        public enum TWMF : ushort
        {
            /// <summary></summary>
            APPOWNS = 0x0001,
            /// <summary></summary>
            DSMOWNS = 0x0002,
            /// <summary></summary>
            DSOWNS = 0x0004,
            /// <summary></summary>
            POINTER = 0x0008,
            /// <summary></summary>
            HANDLE = 0x0010
        }

        /// <summary>
        /// Type values...
        /// </summary>
        public enum TWTY : ushort
        {
            /// <summary></summary>
            INT8 = 0x0000,
            /// <summary></summary>
            INT16 = 0x0001,
            /// <summary></summary>
            INT32 = 0x0002,

            /// <summary></summary>
            UINT8 = 0x0003,
            /// <summary></summary>
            UINT16 = 0x0004,
            /// <summary></summary>
            UINT32 = 0x0005,

            /// <summary></summary>
            BOOL = 0x0006,

            /// <summary></summary>
            FIX32 = 0x0007,

            /// <summary></summary>
            FRAME = 0x0008,

            /// <summary></summary>
            STR32 = 0x0009,
            /// <summary></summary>
            STR64 = 0x000a,
            /// <summary></summary>
            STR128 = 0x000b,
            /// <summary></summary>
            STR255 = 0x000c,
            /// <summary></summary>
            HANDLE = 0x000f
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Capability Constants...
        ///////////////////////////////////////////////////////////////////////////////
        #region Capability Constants...

        /// <summary>
        /// CAP_ALARMS values
        /// </summary>
        public enum TWAL : ushort
        {
            /// <summary>
            /// </summary>
            ALARM = 0,
            /// <summary>
            /// </summary>
            FEEDERERROR = 1,
            /// <summary>
            /// </summary>
            FEEDERWARNING = 2,
            /// <summary>
            /// </summary>
            BARCODE = 3,
            /// <summary>
            /// </summary>
            DOUBLEFEED = 4,
            /// <summary>
            /// </summary>
            JAM = 5,
            /// <summary>
            /// </summary>
            PATCHCODE = 6,
            /// <summary>
            /// </summary>
            POWER = 7,
            /// <summary>
            /// </summary>
            SKEW = 8
        }

        /// <summary>
        /// ICAP_AUTOSIZE values
        /// </summary>
        public enum TWAS : ushort
        {
            /// <summary>
            /// </summary>
            NONE = 0,
            /// <summary>
            /// </summary>
            AUTO = 1,
            /// <summary>
            /// </summary>
            CURRENT = 2
        }

        /// <summary>
        /// TWEI_BARCODEROTATION values
        /// </summary>
        public enum TWBCOR : ushort
        {
            /// <summary>
            /// </summary>
            ROT0 = 0,
            /// <summary>
            /// </summary>
            ROT90 = 1,
            /// <summary>
            /// </summary>
            ROT180 = 2,
            /// <summary>
            /// </summary>
            ROT270 = 3,
            /// <summary>
            /// </summary>
            ROTX = 4
        }

        /// <summary>
        /// ICAP_BARCODESEARCHMODE values
        /// </summary>
        public enum TWBD : ushort
        {
            /// <summary>
            /// </summary>
            HORZ = 0,
            /// <summary>
            /// </summary>
            VERT = 1,
            /// <summary>
            /// </summary>
            HORZVERT = 2,
            /// <summary>
            /// </summary>
            VERTHORZ = 3
        }

        /// <summary>
        /// ICAP_BITORDER values
        /// </summary>
        public enum TWBO : ushort
        {
            /// <summary>
            /// </summary>
            LSBFIRST = 0,
            /// <summary>
            /// </summary>
            MSBFIRST = 1
        }

        /// <summary>
        /// ICAP_AUTODISCARDBLANKPAGES values
        /// </summary>
        public enum TWBP : short
        {
            /// <summary>
            /// </summary>
            DISABLE = -2,
            /// <summary>
            /// </summary>
            AUTO = -1
        }

        /// <summary>
        /// ICAP_BITDEPTHREDUCTION values
        /// </summary>
        public enum TWBR : ushort
        {
            /// <summary>
            /// </summary>
            THRESHOLD = 0,
            /// <summary>
            /// </summary>
            HALFTONE = 1,
            /// <summary>
            /// </summary>
            CUSTHALFTONE = 2,
            /// <summary>
            /// </summary>
            DIFFUSION = 3,
            /// <summary></summary>
            DYNAMICTHRESHOLD = 4
        }

        /// <summary>
        /// ICAP_SUPPORTEDBARCODETYPES and TWEI_BARCODETYPE values
        /// </summary>
        public enum TWBT : ushort
        {
            /// <summary></summary>
            X3OF9 = 0, // 3OF9 in TWAIN.H
            /// <summary></summary>
            X2OF5INTERLEAVED = 1, // 2OF5INTERLEAVED in TWAIN.H
            /// <summary></summary>
            X2OF5NONINTERLEAVED = 2, // 2OF5NONINTERLEAVED in TWAIN.H
            /// <summary></summary>
            CODE93 = 3,
            /// <summary></summary>
            CODE128 = 4,
            /// <summary></summary>
            UCC128 = 5,
            /// <summary></summary>
            CODABAR = 6,
            /// <summary></summary>
            UPCA = 7,
            /// <summary></summary>
            UPCE = 8,
            /// <summary></summary>
            EAN8 = 9,
            /// <summary></summary>
            EAN13 = 10,
            /// <summary></summary>
            POSTNET = 11,
            /// <summary></summary>
            PDF417 = 12,
            /// <summary></summary>
            X2OF5INDUSTRIAL = 13, // 2OF5INDUSTRIAL in TWAIN.H
            /// <summary></summary>
            X2OF5MATRIX = 14, // 2OF5MATRIX in TWAIN.H
            /// <summary></summary>
            X2OF5DATALOGIC = 15, // 2OF5DATALOGIC in TWAIN.H
            /// <summary></summary>
            X2OF5IATA = 16, // 2OF5IATA in TWAIN.H
            /// <summary></summary>
            X3OF9FULLASCII = 17, // 3OF9FULLASCII in TWAIN.H
            /// <summary></summary>
            CODABARWITHSTARTSTOP = 18,
            /// <summary></summary>
            MAXICODE = 19,
            /// <summary></summary>
            QRCODE = 20
        }

        /// <summary>
        /// ICAP_COMPRESSION values
        /// </summary>
        public enum TWCP : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            PACKBITS = 1,
            /// <summary></summary>
            GROUP31D = 2,
            /// <summary></summary>
            GROUP31DEOL = 3,
            /// <summary></summary>
            GROUP32D = 4,
            /// <summary></summary>
            GROUP4 = 5,
            /// <summary></summary>
            JPEG = 6,
            /// <summary></summary>
            LZW = 7,
            /// <summary></summary>
            JBIG = 8,
            /// <summary></summary>
            PNG = 9,
            /// <summary></summary>
            RLE4 = 10,
            /// <summary></summary>
            RLE8 = 11,
            /// <summary></summary>
            BITFIELDS = 12,
            /// <summary></summary>
            ZIP = 13,
            /// <summary></summary>
            JPEG2000 = 14
        }

        /// <summary>
        /// CAP_CAMERASIDE and TWEI_PAGESIDE values
        /// </summary>
        public enum TWCS : ushort
        {
            /// <summary></summary>
            BOTH = 0,
            /// <summary></summary>
            TOP = 1,
            /// <summary></summary>
            BOTTOM = 2
        }

        /// <summary>
        /// CAP_CLEARBUFFERS values
        /// </summary>
        public enum TWCB : ushort
        {
            /// <summary></summary>
            AUTO = 0,
            /// <summary></summary>
            CLEAR = 1,
            /// <summary></summary>
            NOCLEAR = 2
        }

        /// <summary>
        /// CAP_DEVICEEVENT values
        /// </summary>
        public enum TWDE : ushort
        {
            /// <summary></summary>
            CUSTOMEVENTS = 0x8000,
            /// <summary></summary>
            CHECKAUTOMATICCAPTURE = 0,
            /// <summary></summary>
            CHECKBATTERY = 1,
            /// <summary></summary>
            CHECKDEVICEONLINE = 2,
            /// <summary></summary>
            CHECKFLASH = 3,
            /// <summary></summary>
            CHECKPOWERSUPPLY = 4,
            /// <summary></summary>
            CHECKRESOLUTION = 5,
            /// <summary></summary>
            DEVICEADDED = 6,
            /// <summary></summary>
            DEVICEOFFLINE = 7,
            /// <summary></summary>
            DEVICEREADY = 8,
            /// <summary></summary>
            DEVICEREMOVED = 9,
            /// <summary></summary>
            IMAGECAPTURED = 10,
            /// <summary></summary>
            IMAGEDELETED = 11,
            /// <summary></summary>
            PAPERDOUBLEFEED = 12,
            /// <summary></summary>
            PAPERJAM = 13,
            /// <summary></summary>
            LAMPFAILURE = 14,
            /// <summary></summary>
            POWERSAVE = 15,
            /// <summary></summary>
            POWERSAVENOTIFY = 16
        }

        /// <summary>
        /// TW_PASSTHRU.Direction values
        /// </summary>
        public enum TWDR : ushort
        {
            /// <summary></summary>
            GET = 1,
            /// <summary></summary>
            SET = 2
        }

        /// <summary>
        /// TWEI_DESKEWSTATUS values
        /// </summary>
        public enum TWDSK : ushort
        {
            /// <summary></summary>
            SUCCESS = 0,
            /// <summary></summary>
            REPORTONLY = 1,
            /// <summary></summary>
            FAIL = 2,
            /// <summary></summary>
            DISABLED = 3
        }

        /// <summary>
        /// CAP_DUPLEX values
        /// </summary>
        public enum TWDX : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            X1PASSDUPLEX = 1, // 1PASSDUPLEX in TWAIN.H
            /// <summary></summary>
            X2PASSDUPLEX = 2  // 2PASSDUPLEX in TWAIN.H
        }

        /// <summary>
        /// CAP_FEEDERALIGNMENT values
        /// </summary>
        public enum TWFA : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            LEFT = 1,
            /// <summary></summary>
            CENTER = 2,
            /// <summary></summary>
            RIGHT = 3
        }

        /// <summary>
        /// ICAP_FEEDERTYPE values
        /// </summary>
        public enum TWFE : ushort
        {
            /// <summary></summary>
            GENERAL = 0,
            /// <summary></summary>
            PHOTO = 1
        }

        /// <summary>
        /// ICAP_IMAGEFILEFORMAT values
        /// </summary>
        public enum TWFF : ushort
        {
            /// <summary></summary>
            TIFF = 0,
            /// <summary></summary>
            PICT = 1,
            /// <summary></summary>
            BMP = 2,
            /// <summary></summary>
            XBM = 3,
            /// <summary></summary>
            JFIF = 4,
            /// <summary></summary>
            FPX = 5,
            /// <summary></summary>
            TIFFMULTI = 6,
            /// <summary></summary>
            PNG = 7,
            /// <summary></summary>
            SPIFF = 8,
            /// <summary></summary>
            EXIF = 9,
            /// <summary></summary>
            PDF = 10,
            /// <summary></summary>
            JP2 = 11,
            /// <summary></summary>
            JPX = 13,
            /// <summary></summary>
            DEJAVU = 14,
            /// <summary></summary>
            PDFA = 15,
            /// <summary></summary>
            PDFA2 = 16,
            /// <summary></summary>
            PDFRASTER = 17
        }

        /// <summary>
        /// ICAP_FLASHUSED2 values
        /// </summary>
        public enum TWFL : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            OFF = 1,
            /// <summary></summary>
            ON = 2,
            /// <summary></summary>
            AUTO = 3,
            /// <summary></summary>
            REDEYE = 4
        }

        /// <summary>
        /// CAP_FEEDERORDER values
        /// </summary>
        public enum TWFO : ushort
        {
            /// <summary></summary>
            FIRSTPAGEFIRST = 0,
            /// <summary></summary>
            LASTPAGEFIRST = 1
        }

        /// <summary>
        /// CAP_FEEDERPOCKET values
        /// </summary>
        public enum TWFP : ushort
        {
            /// <summary></summary>
            POCKETERROR = 0,
            /// <summary></summary>
            POCKET1 = 1,
            /// <summary></summary>
            POCKET2 = 2,
            /// <summary></summary>
            POCKET3 = 3,
            /// <summary></summary>
            POCKET4 = 4,
            /// <summary></summary>
            POCKET5 = 5,
            /// <summary></summary>
            POCKET6 = 6,
            /// <summary></summary>
            POCKET7 = 7,
            /// <summary></summary>
            POCKET8 = 8,
            /// <summary></summary>
            POCKET9 = 9,
            /// <summary></summary>
            POCKET10 = 10,
            /// <summary></summary>
            POCKET11 = 11,
            /// <summary></summary>
            POCKET12 = 12,
            /// <summary></summary>
            POCKET13 = 13,
            /// <summary></summary>
            POCKET14 = 14,
            /// <summary></summary>
            POCKET15 = 15,
            /// <summary></summary>
            POCKET16 = 16
        }

        /// <summary>
        /// ICAP_FLIPROTATION values
        /// </summary>
        public enum TWFR : ushort
        {
            /// <summary></summary>
            BOOK = 0,
            /// <summary></summary>
            FANFOLD = 1
        }

        /// <summary>
        /// ICAP_FILTER values
        /// </summary>
        public enum TWFT : ushort
        {
            /// <summary></summary>
            RED = 0,
            /// <summary></summary>
            GREEN = 1,
            /// <summary></summary>
            BLUE = 2,
            /// <summary></summary>
            NONE = 3,
            /// <summary></summary>
            WHITE = 4,
            /// <summary></summary>
            CYAN = 5,
            /// <summary></summary>
            MAGENTA = 6,
            /// <summary></summary>
            YELLOW = 7,
            /// <summary></summary>
            BLACK = 8
        }

        /// <summary>
        /// TW_FILESYSTEM.FileType values
        /// </summary>
        public enum TWFY : ushort
        {
            /// <summary></summary>
            CAMERA = 0,
            /// <summary></summary>
            CAMERATOP = 1,
            /// <summary></summary>
            CAMERABOTTOM = 2,
            /// <summary></summary>
            CAMERAPREVIEW = 3,
            /// <summary></summary>
            DOMAIN = 4,
            /// <summary></summary>
            HOST = 5,
            /// <summary></summary>
            DIRECTORY = 6,
            /// <summary></summary>
            IMAGE = 7,
            /// <summary></summary>
            UNKNOWN = 8
        }

        /// <summary>
        /// ICAP_ICCPROFILE values
        /// </summary>
        public enum TWIC : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            LINK = 1,
            /// <summary></summary>
            EMBED = 2
        }

        /// <summary>
        /// ICAP_IMAGEFILTER values
        /// </summary>
        public enum TWIF : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            AUTO = 1,
            /// <summary></summary>
            LOWPASS = 2,
            /// <summary></summary>
            BANDPASS = 3,
            /// <summary></summary>
            HIGHPASS = 4,
            /// <summary></summary>
            TEXT = BANDPASS,
            /// <summary></summary>
            FINELINE = HIGHPASS
        }

        /// <summary>
        /// ICAP_IMAGEMERGE values
        /// </summary>
        public enum TWIM : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            FRONTONTOP = 1,
            /// <summary></summary>
            FRONTONBOTTOM = 2,
            /// <summary></summary>
            FRONTONLEFT = 3,
            /// <summary></summary>
            FRONTONRIGHT = 4
        }

        /// <summary>
        /// CAP_JOBCONTROL values
        /// </summary>
        public enum TWJC : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            JSIC = 1,
            /// <summary></summary>
            JSIS = 2,
            /// <summary></summary>
            JSXC = 3,
            /// <summary></summary>
            JSXS = 4
        }

        /// <summary>
        /// ICAP_JPEGQUALITY values
        /// </summary>
        public enum TWJQ : short
        {
            /// <summary></summary>
            UNKNOWN = -4,
            /// <summary></summary>
            LOW = -3,
            /// <summary></summary>
            MEDIUM = -2,
            /// <summary></summary>
            HIGH = -1
        }

        /// <summary>
        /// ICAP_LIGHTPATH values
        /// </summary>
        public enum TWLP : ushort
        {
            /// <summary></summary>
            REFLECTIVE = 0,
            /// <summary></summary>
            TRANSMISSIVE = 1
        }

        /// <summary>
        /// ICAP_LIGHTSOURCE values
        /// </summary>
        public enum TWLS : ushort
        {
            /// <summary></summary>
            RED = 0,
            /// <summary></summary>
            GREEN = 1,
            /// <summary></summary>
            BLUE = 2,
            /// <summary></summary>
            NONE = 3,
            /// <summary></summary>
            WHITE = 4,
            /// <summary></summary>
            UV = 5,
            /// <summary></summary>
            IR = 6
        }

        /// <summary>
        /// TWEI_MAGTYPE values
        /// </summary>
        public enum TWMD : ushort
        {
            /// <summary></summary>
            MICR = 0,
            /// <summary></summary>
            RAW = 1,
            /// <summary></summary>
            INVALID = 2
        }

        /// <summary>
        /// ICAP_NOISEFILTER values
        /// </summary>
        public enum TWNF : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            AUTO = 1,
            /// <summary></summary>
            LONEPIXEL = 2,
            /// <summary></summary>
            MAJORITYRULE = 3
        }

        /// <summary>
        /// ICAP_ORIENTATION values
        /// </summary>
        public enum TWOR : ushort
        {
            /// <summary></summary>
            ROT0 = 0,
            /// <summary></summary>
            ROT90 = 1,
            /// <summary></summary>
            ROT180 = 2,
            /// <summary></summary>
            ROT270 = 3,
            /// <summary></summary>
            PORTRAIT = ROT0,
            /// <summary></summary>
            LANDSCAPE = ROT270,
            /// <summary></summary>
            AUTO = 4,
            /// <summary></summary>
            AUTOTEXT = 5,
            /// <summary></summary>
            AUTOPICTURE = 6
        }

        /// <summary>
        /// ICAP_OVERSCAN values
        /// </summary>
        public enum TWOV : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            AUTO = 1,
            /// <summary></summary>
            TOPBOTTOM = 2,
            /// <summary></summary>
            LEFTRIGHT = 3,
            /// <summary></summary>
            ALL = 4
        }

        /// <summary>
        /// Palette types for TW_PALETTE8
        /// </summary>
        public enum TWPA : ushort
        {
            /// <summary></summary>
            RGB = 0,
            /// <summary></summary>
            GRAY = 1,
            /// <summary></summary>
            CMY = 2
        }

        /// <summary>
        /// ICAP_PLANARCHUNKY values
        /// </summary>
        public enum TWPC : ushort
        {
            /// <summary></summary>
            CHUNKY = 0,
            /// <summary></summary>
            PLANAR = 1
        }

        /// <summary>
        /// TWEI_PATCHCODE values
        /// </summary>
        public enum TWPCH : ushort
        {
            /// <summary></summary>
            PATCH1 = 0,
            /// <summary></summary>
            PATCH2 = 1,
            /// <summary></summary>
            PATCH3 = 2,
            /// <summary></summary>
            PATCH4 = 3,
            /// <summary></summary>
            PATCH6 = 4,
            /// <summary></summary>
            PATCHT = 5
        }

        /// <summary>
        /// ICAP_PIXELFLAVOR values
        /// </summary>
        public enum TWPF : ushort
        {
            /// <summary></summary>
            CHOCOLATE = 0,
            /// <summary></summary>
            VANILLA = 1
        }

        /// <summary>
        /// CAP_PRINTERMODE values
        /// </summary>
        public enum TWPM : ushort
        {
            /// <summary></summary>
            SINGLESTRING = 0,
            /// <summary></summary>
            MULTISTRING = 1,
            /// <summary></summary>
            COMPOUNDSTRING = 2
        }

        /// <summary>
        /// CAP_PRINTER values
        /// </summary>
        public enum TWPR : ushort
        {
            /// <summary></summary>
            IMPRINTERTOPBEFORE = 0,
            /// <summary></summary>
            IMPRINTERTOPAFTER = 1,
            /// <summary></summary>
            IMPRINTERBOTTOMBEFORE = 2,
            /// <summary></summary>
            IMPRINTERBOTTOMAFTER = 3,
            /// <summary></summary>
            ENDORSERTOPBEFORE = 4,
            /// <summary></summary>
            ENDORSERTOPAFTER = 5,
            /// <summary></summary>
            ENDORSERBOTTOMBEFORE = 6,
            /// <summary></summary>
            ENDORSERBOTTOMAFTER = 7
        }

        /// <summary>
        /// CAP_PRINTERFONTSTYLE Added 2.3 (TWPF in TWAIN.H)
        /// </summary>
        public enum TWPFS : ushort
        {
            /// <summary></summary>
            NORMAL = 0,
            /// <summary></summary>
            BOLD = 1,
            /// <summary></summary>
            ITALIC = 2,
            /// <summary></summary>
            LARGESIZE = 3,
            /// <summary></summary>
            SMALLSIZE = 4
        }

        /// <summary>
        /// CAP_PRINTERINDEXTRIGGER Added 2.3
        /// </summary>
        public enum TWCT : ushort
        {
            /// <summary></summary>
            PAGE = 0,
            /// <summary></summary>
            PATCH1 = 1,
            /// <summary></summary>
            PATCH2 = 2,
            /// <summary></summary>
            PATCH3 = 3,
            /// <summary></summary>
            PATCH4 = 4,
            /// <summary></summary>
            PATCHT = 5,
            /// <summary></summary>
            PATCH6 = 6
        }

        /// <summary>
        /// CAP_POWERSUPPLY values
        /// </summary>
        public enum TWPS : ushort
        {
            /// <summary></summary>
            EXTERNAL = 0,
            /// <summary></summary>
            BATTERY = 1
        }

        /// <summary>
        /// ICAP_PIXELTYPE values (PT_ means Pixel Type)
        /// </summary>
        public enum TWPT : ushort
        {
            /// <summary></summary>
            BW = 0,
            /// <summary></summary>
            GRAY = 1,
            /// <summary></summary>
            RGB = 2,
            /// <summary></summary>
            PALETTE = 3,
            /// <summary></summary>
            CMY = 4,
            /// <summary></summary>
            CMYK = 5,
            /// <summary></summary>
            YUV = 6,
            /// <summary></summary>
            YUVK = 7,
            /// <summary></summary>
            CIEXYZ = 8,
            /// <summary></summary>
            LAB = 9,
            /// <summary></summary>
            SRGB = 10,
            /// <summary></summary>
            SCRGB = 11,
            /// <summary></summary>
            INFRARED = 16
        }

        /// <summary>
        /// CAP_SEGMENTED values
        /// </summary>
        public enum TWSG : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            AUTO = 1,
            /// <summary></summary>
            MANUAL = 2
        }

        /// <summary>
        /// ICAP_FILMTYPE values
        /// </summary>
        public enum TWFM : ushort
        {
            /// <summary></summary>
            POSITIVE = 0,
            /// <summary></summary>
            NEGATIVE = 1
        }

        /// <summary>
        /// CAP_DOUBLEFEEDDETECTION values
        /// </summary>
        public enum TWDF : ushort
        {
            /// <summary></summary>
            ULTRASONIC = 0,
            /// <summary></summary>
            BYLENGTH = 1,
            /// <summary></summary>
            INFRARED = 2
        }

        /// <summary>
        /// CAP_DOUBLEFEEDDETECTIONSENSITIVITY values
        /// </summary>
        public enum TWUS : ushort
        {
            /// <summary></summary>
            LOW = 0,
            /// <summary></summary>
            MEDIUM = 1,
            /// <summary></summary>
            HIGH = 2
        }

        /// <summary>
        /// CAP_DOUBLEFEEDDETECTIONRESPONSE values
        /// </summary>
        public enum TWDP : ushort
        {
            /// <summary></summary>
            STOP = 0,
            /// <summary></summary>
            STOPANDWAIT = 1,
            /// <summary></summary>
            SOUND = 2,
            /// <summary></summary>
            DONOTIMPRINT = 3
        }

        /// <summary>
        /// ICAP_MIRROR values
        /// </summary>
        public enum TWMR : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            VERTICAL = 1,
            /// <summary></summary>
            HORIZONTAL = 2
        }

        /// <summary>
        /// ICAP_JPEGSUBSAMPLING values
        /// </summary>
        public enum TWJS : ushort
        {
            /// <summary></summary>
            X444YCBCR = 0, // 444YCBCR in TWAIN.H
            /// <summary></summary>
            X444RGB = 1, // 444RGB in TWAIN.H
            /// <summary></summary>
            X422 = 2, // 422 in TWAIN.H
            /// <summary></summary>
            X421 = 3, // 421 in TWAIN.H
            /// <summary></summary>
            X411 = 4, // 411 in TWAIN.H
            /// <summary></summary>
            X420 = 5, // 420 in TWAIN.H
            /// <summary></summary>
            X410 = 6, // 410 in TWAIN.H
            /// <summary></summary>
            X311 = 7  // 311 in TWAIN.H
        }

        /// <summary>
        /// CAP_PAPERHANDLING values
        /// </summary>
        public enum TWPH : ushort
        {
            /// <summary></summary>
            NORMAL = 0,
            /// <summary></summary>
            FRAGILE = 1,
            /// <summary></summary>
            THICK = 2,
            /// <summary></summary>
            TRIFOLD = 3,
            /// <summary></summary>
            PHOTOGRAPH = 4
        }

        /// <summary>
        /// CAP_INDICATORSMODE values
        /// </summary>
        public enum TWCI : ushort
        {
            /// <summary></summary>
            INFO = 0,
            /// <summary></summary>
            WARNING = 1,
            /// <summary></summary>
            ERROR = 2,
            /// <summary></summary>
            WARMUP = 3
        }

        /// <summary>
        /// ICAP_SUPPORTEDSIZES values (SS_ means Supported Sizes)
        /// </summary>
        public enum TWSS : ushort
        {
            /// <summary></summary>
            NONE = 0,
            /// <summary></summary>
            A4 = 1,
            /// <summary></summary>
            JISB5 = 2,
            /// <summary></summary>
            USLETTER = 3,
            /// <summary></summary>
            USLEGAL = 4,
            /// <summary></summary>
            A5 = 5,
            /// <summary></summary>
            ISOB4 = 6,
            /// <summary></summary>
            ISOB6 = 7,
            /// <summary></summary>
            USLEDGER = 9,
            /// <summary></summary>
            USEXECUTIVE = 10,
            /// <summary></summary>
            A3 = 11,
            /// <summary></summary>
            ISOB3 = 12,
            /// <summary></summary>
            A6 = 13,
            /// <summary></summary>
            C4 = 14,
            /// <summary></summary>
            C5 = 15,
            /// <summary></summary>
            C6 = 16,
            /// <summary></summary>
            X4A0 = 17, // 4A0 in TWAIN.H
            /// <summary></summary>
            X2A0 = 18, // 2A0 in TWAIN.H
            /// <summary></summary>
            A0 = 19,
            /// <summary></summary>
            A1 = 20,
            /// <summary></summary>
            A2 = 21,
            /// <summary></summary>
            A7 = 22,
            /// <summary></summary>
            A8 = 23,
            /// <summary></summary>
            A9 = 24,
            /// <summary></summary>
            A10 = 25,
            /// <summary></summary>
            ISOB0 = 26,
            /// <summary></summary>
            ISOB1 = 27,
            /// <summary></summary>
            ISOB2 = 28,
            /// <summary></summary>
            ISOB5 = 29,
            /// <summary></summary>
            ISOB7 = 30,
            /// <summary></summary>
            ISOB8 = 31,
            /// <summary></summary>
            ISOB9 = 32,
            /// <summary></summary>
            ISOB10 = 33,
            /// <summary></summary>
            JISB0 = 34,
            /// <summary></summary>
            JISB1 = 35,
            /// <summary></summary>
            JISB2 = 36,
            /// <summary></summary>
            JISB3 = 37,
            /// <summary></summary>
            JISB4 = 38,
            /// <summary></summary>
            JISB6 = 39,
            /// <summary></summary>
            JISB7 = 40,
            /// <summary></summary>
            JISB8 = 41,
            /// <summary></summary>
            JISB9 = 42,
            /// <summary></summary>
            JISB10 = 43,
            /// <summary></summary>
            C0 = 44,
            /// <summary></summary>
            C1 = 45,
            /// <summary></summary>
            C2 = 46,
            /// <summary></summary>
            C3 = 47,
            /// <summary></summary>
            C7 = 48,
            /// <summary></summary>
            C8 = 49,
            /// <summary></summary>
            C9 = 50,
            /// <summary></summary>
            C10 = 51,
            /// <summary></summary>
            USSTATEMENT = 52,
            /// <summary></summary>
            BUSINESSCARD = 53,
            /// <summary></summary>
            MAXSIZE = 54
        }

        /// <summary>
        /// ICAP_XFERMECH values (SX_ means Setup XFer)
        /// </summary>
        public enum TWSX : ushort
        {
            /// <summary></summary>
            NATIVE = 0,
            /// <summary></summary>
            FILE = 1,
            /// <summary></summary>
            MEMORY = 2,
            /// <summary></summary>
            MEMFILE = 4
        }

        /// <summary>
        /// ICAP_UNITS values (UN_ means UNits)
        /// </summary>
        public enum TWUN : ushort
        {
            /// <summary></summary>
            INCHES = 0,
            /// <summary></summary>
            CENTIMETERS = 1,
            /// <summary></summary>
            PICAS = 2,
            /// <summary></summary>
            POINTS = 3,
            /// <summary></summary>
            TWIPS = 4,
            /// <summary></summary>
            PIXELS = 5,
            /// <summary></summary>
            MILLIMETERS = 6
        }

        /// <summary>
        /// Country Constants
        /// </summary>
        public enum TWCY : ushort
        {
            /// <summary></summary>
            AFGHANISTAN = 1001,
            /// <summary></summary>
            ALGERIA = 213,
            /// <summary></summary>
            AMERICANSAMOA = 684,
            /// <summary></summary>
            ANDORRA = 33,
            /// <summary></summary>
            ANGOLA = 1002,
            /// <summary></summary>
            ANGUILLA = 8090,
            /// <summary></summary>
            ANTIGUA = 8091,
            /// <summary></summary>
            ARGENTINA = 54,
            /// <summary></summary>
            ARUBA = 297,
            /// <summary></summary>
            ASCENSIONI = 247,
            /// <summary></summary>
            AUSTRALIA = 61,
            /// <summary></summary>
            AUSTRIA = 43,
            /// <summary></summary>
            BAHAMAS = 8092,
            /// <summary></summary>
            BAHRAIN = 973,
            /// <summary></summary>
            BANGLADESH = 880,
            /// <summary></summary>
            BARBADOS = 8093,
            /// <summary></summary>
            BELGIUM = 32,
            /// <summary></summary>
            BELIZE = 501,
            /// <summary></summary>
            BENIN = 229,
            /// <summary></summary>
            BERMUDA = 8094,
            /// <summary></summary>
            BHUTAN = 1003,
            /// <summary></summary>
            BOLIVIA = 591,
            /// <summary></summary>
            BOTSWANA = 267,
            /// <summary></summary>
            BRITAIN = 6,
            /// <summary></summary>
            BRITVIRGINIS = 8095,
            /// <summary></summary>
            BRAZIL = 55,
            /// <summary></summary>
            BRUNEI = 673,
            /// <summary></summary>
            BULGARIA = 359,
            /// <summary></summary>
            BURKINAFASO = 1004,
            /// <summary></summary>
            BURMA = 1005,
            /// <summary></summary>
            BURUNDI = 1006,
            /// <summary></summary>
            CAMAROON = 237,
            /// <summary></summary>
            CANADA = 2,
            /// <summary></summary>
            CAPEVERDEIS = 238,
            /// <summary></summary>
            CAYMANIS = 8096,
            /// <summary></summary>
            CENTRALAFREP = 1007,
            /// <summary></summary>
            CHAD = 1008,
            /// <summary></summary>
            CHILE = 56,
            /// <summary></summary>
            CHINA = 86,
            /// <summary></summary>
            CHRISTMASIS = 1009,
            /// <summary></summary>
            COCOSIS = 1009,
            /// <summary></summary>
            COLOMBIA = 57,
            /// <summary></summary>
            COMOROS = 1010,
            /// <summary></summary>
            CONGO = 1011,
            /// <summary></summary>
            COOKIS = 1012,
            /// <summary></summary>
            COSTARICA = 506,
            /// <summary></summary>
            CUBA = 5,
            /// <summary></summary>
            CYPRUS = 357,
            /// <summary></summary>
            CZECHOSLOVAKIA = 42,
            /// <summary></summary>
            DENMARK = 45,
            /// <summary></summary>
            DJIBOUTI = 1013,
            /// <summary></summary>
            DOMINICA = 8097,
            /// <summary></summary>
            DOMINCANREP = 8098,
            /// <summary></summary>
            EASTERIS = 1014,
            /// <summary></summary>
            ECUADOR = 593,
            /// <summary></summary>
            EGYPT = 20,
            /// <summary></summary>
            ELSALVADOR = 503,
            /// <summary></summary>
            EQGUINEA = 1015,
            /// <summary></summary>
            ETHIOPIA = 251,
            /// <summary></summary>
            FALKLANDIS = 1016,
            /// <summary></summary>
            FAEROEIS = 298,
            /// <summary></summary>
            FIJIISLANDS = 679,
            /// <summary></summary>
            FINLAND = 358,
            /// <summary></summary>
            FRANCE = 33,
            /// <summary></summary>
            FRANTILLES = 596,
            /// <summary></summary>
            FRGUIANA = 594,
            /// <summary></summary>
            FRPOLYNEISA = 689,
            /// <summary></summary>
            FUTANAIS = 1043,
            /// <summary></summary>
            GABON = 241,
            /// <summary></summary>
            GAMBIA = 220,
            /// <summary></summary>
            GERMANY = 49,
            /// <summary></summary>
            GHANA = 233,
            /// <summary></summary>
            GIBRALTER = 350,
            /// <summary></summary>
            GREECE = 30,
            /// <summary></summary>
            GREENLAND = 299,
            /// <summary></summary>
            GRENADA = 8099,
            /// <summary></summary>
            GRENEDINES = 8015,
            /// <summary></summary>
            GUADELOUPE = 590,
            /// <summary></summary>
            GUAM = 671,
            /// <summary></summary>
            GUANTANAMOBAY = 5399,
            /// <summary></summary>
            GUATEMALA = 502,
            /// <summary></summary>
            GUINEA = 224,
            /// <summary></summary>
            GUINEABISSAU = 1017,
            /// <summary></summary>
            GUYANA = 592,
            /// <summary></summary>
            HAITI = 509,
            /// <summary></summary>
            HONDURAS = 504,
            /// <summary></summary>
            HONGKONG = 852,
            /// <summary></summary>
            HUNGARY = 36,
            /// <summary></summary>
            ICELAND = 354,
            /// <summary></summary>
            INDIA = 91,
            /// <summary></summary>
            INDONESIA = 62,
            /// <summary></summary>
            IRAN = 98,
            /// <summary></summary>
            IRAQ = 964,
            /// <summary></summary>
            IRELAND = 353,
            /// <summary></summary>
            ISRAEL = 972,
            /// <summary></summary>
            ITALY = 39,
            /// <summary></summary>
            IVORYCOAST = 225,
            /// <summary></summary>
            JAMAICA = 8010,
            /// <summary></summary>
            JAPAN = 81,
            /// <summary></summary>
            JORDAN = 962,
            /// <summary></summary>
            KENYA = 254,
            /// <summary></summary>
            KIRIBATI = 1018,
            /// <summary></summary>
            KOREA = 82,
            /// <summary></summary>
            KUWAIT = 965,
            /// <summary></summary>
            LAOS = 1019,
            /// <summary></summary>
            LEBANON = 1020,
            /// <summary></summary>
            LIBERIA = 231,
            /// <summary></summary>
            LIBYA = 218,
            /// <summary></summary>
            LIECHTENSTEIN = 41,
            /// <summary></summary>
            LUXENBOURG = 352,
            /// <summary></summary>
            MACAO = 853,
            /// <summary></summary>
            MADAGASCAR = 1021,
            /// <summary></summary>
            MALAWI = 265,
            /// <summary></summary>
            MALAYSIA = 60,
            /// <summary></summary>
            MALDIVES = 960,
            /// <summary></summary>
            MALI = 1022,
            /// <summary></summary>
            MALTA = 356,
            /// <summary></summary>
            MARSHALLIS = 692,
            /// <summary></summary>
            MAURITANIA = 1023,
            /// <summary></summary>
            MAURITIUS = 230,
            /// <summary></summary>
            MEXICO = 3,
            /// <summary></summary>
            MICRONESIA = 691,
            /// <summary></summary>
            MIQUELON = 508,
            /// <summary></summary>
            MONACO = 33,
            /// <summary></summary>
            MONGOLIA = 1024,
            /// <summary></summary>
            MONTSERRAT = 8011,
            /// <summary></summary>
            MOROCCO = 212,
            /// <summary></summary>
            MOZAMBIQUE = 1025,
            /// <summary></summary>
            NAMIBIA = 264,
            /// <summary></summary>
            NAURU = 1026,
            /// <summary></summary>
            NEPAL = 977,
            /// <summary></summary>
            NETHERLANDS = 31,
            /// <summary></summary>
            NETHANTILLES = 599,
            /// <summary></summary>
            NEVIS = 8012,
            /// <summary></summary>
            NEWCALEDONIA = 687,
            /// <summary></summary>
            NEWZEALAND = 64,
            /// <summary></summary>
            NICARAGUA = 505,
            /// <summary></summary>
            NIGER = 227,
            /// <summary></summary>
            NIGERIA = 234,
            /// <summary></summary>
            NIUE = 1027,
            /// <summary></summary>
            NORFOLKI = 1028,
            /// <summary></summary>
            NORWAY = 47,
            /// <summary></summary>
            OMAN = 968,
            /// <summary></summary>
            PAKISTAN = 92,
            /// <summary></summary>
            PALAU = 1029,
            /// <summary></summary>
            PANAMA = 507,
            /// <summary></summary>
            PARAGUAY = 595,
            /// <summary></summary>
            PERU = 51,
            /// <summary></summary>
            PHILLIPPINES = 63,
            /// <summary></summary>
            PITCAIRNIS = 1030,
            /// <summary></summary>
            PNEWGUINEA = 675,
            /// <summary></summary>
            POLAND = 48,
            /// <summary></summary>
            PORTUGAL = 351,
            /// <summary></summary>
            QATAR = 974,
            /// <summary></summary>
            REUNIONI = 1031,
            /// <summary></summary>
            ROMANIA = 40,
            /// <summary></summary>
            RWANDA = 250,
            /// <summary></summary>
            SAIPAN = 670,
            /// <summary></summary>
            SANMARINO = 39,
            /// <summary></summary>
            SAOTOME = 1033,
            /// <summary></summary>
            SAUDIARABIA = 966,
            /// <summary></summary>
            SENEGAL = 221,
            /// <summary></summary>
            SEYCHELLESIS = 1034,
            /// <summary></summary>
            SIERRALEONE = 1035,
            /// <summary></summary>
            SINGAPORE = 65,
            /// <summary></summary>
            SOLOMONIS = 1036,
            /// <summary></summary>
            SOMALI = 1037,
            /// <summary></summary>
            SOUTHAFRICA = 27,
            /// <summary></summary>
            SPAIN = 34,
            /// <summary></summary>
            SRILANKA = 94,
            /// <summary></summary>
            STHELENA = 1032,
            /// <summary></summary>
            STKITTS = 8013,
            /// <summary></summary>
            STLUCIA = 8014,
            /// <summary></summary>
            STPIERRE = 508,
            /// <summary></summary>
            STVINCENT = 8015,
            /// <summary></summary>
            SUDAN = 1038,
            /// <summary></summary>
            SURINAME = 597,
            /// <summary></summary>
            SWAZILAND = 268,
            /// <summary></summary>
            SWEDEN = 46,
            /// <summary></summary>
            SWITZERLAND = 41,
            /// <summary></summary>
            SYRIA = 1039,
            /// <summary></summary>
            TAIWAN = 886,
            /// <summary></summary>
            TANZANIA = 255,
            /// <summary></summary>
            THAILAND = 66,
            /// <summary></summary>
            TOBAGO = 8016,
            /// <summary></summary>
            TOGO = 228,
            /// <summary></summary>
            TONGAIS = 676,
            /// <summary></summary>
            TRINIDAD = 8016,
            /// <summary></summary>
            TUNISIA = 216,
            /// <summary></summary>
            TURKEY = 90,
            /// <summary></summary>
            TURKSCAICOS = 8017,
            /// <summary></summary>
            TUVALU = 1040,
            /// <summary></summary>
            UGANDA = 256,
            /// <summary></summary>
            USSR = 7,
            /// <summary></summary>
            UAEMIRATES = 971,
            /// <summary></summary>
            UNITEDKINGDOM = 44,
            /// <summary></summary>
            USA = 1,
            /// <summary></summary>
            URUGUAY = 598,
            /// <summary></summary>
            VANUATU = 1041,
            /// <summary></summary>
            VATICANCITY = 39,
            /// <summary></summary>
            VENEZUELA = 58,
            /// <summary></summary>
            WAKE = 1042,
            /// <summary></summary>
            WALLISIS = 1043,
            /// <summary></summary>
            WESTERNSAHARA = 1044,
            /// <summary></summary>
            WESTERNSAMOA = 1045,
            /// <summary></summary>
            YEMEN = 1046,
            /// <summary></summary>
            YUGOSLAVIA = 38,
            /// <summary></summary>
            ZAIRE = 243,
            /// <summary></summary>
            ZAMBIA = 260,
            /// <summary></summary>
            ZIMBABWE = 263,
            /// <summary></summary>
            ALBANIA = 355,
            /// <summary></summary>
            ARMENIA = 374,
            /// <summary></summary>
            AZERBAIJAN = 994,
            /// <summary></summary>
            BELARUS = 375,
            /// <summary></summary>
            BOSNIAHERZGO = 387,
            /// <summary></summary>
            CAMBODIA = 855,
            /// <summary></summary>
            CROATIA = 385,
            /// <summary></summary>
            CZECHREPUBLIC = 420,
            /// <summary></summary>
            DIEGOGARCIA = 246,
            /// <summary></summary>
            ERITREA = 291,
            /// <summary></summary>
            ESTONIA = 372,
            /// <summary></summary>
            GEORGIA = 995,
            /// <summary></summary>
            LATVIA = 371,
            /// <summary></summary>
            LESOTHO = 266,
            /// <summary></summary>
            LITHUANIA = 370,
            /// <summary></summary>
            MACEDONIA = 389,
            /// <summary></summary>
            MAYOTTEIS = 269,
            /// <summary></summary>
            MOLDOVA = 373,
            /// <summary></summary>
            MYANMAR = 95,
            /// <summary></summary>
            NORTHKOREA = 850,
            /// <summary></summary>
            PUERTORICO = 787,
            /// <summary></summary>
            RUSSIA = 7,
            /// <summary></summary>
            SERBIA = 381,
            /// <summary></summary>
            SLOVAKIA = 421,
            /// <summary></summary>
            SLOVENIA = 386,
            /// <summary></summary>
            SOUTHKOREA = 82,
            /// <summary></summary>
            UKRAINE = 380,
            /// <summary></summary>
            USVIRGINIS = 340,
            /// <summary></summary>
            VIETNAM = 84
        }

        /// <summary>
        /// Language Constants
        /// </summary>
        public enum TWLG : short
        {
            /// <summary></summary>
            USERLOCALE = -1,
            /// <summary></summary>
            DAN = 0,
            /// <summary></summary>
            DUT = 1,
            /// <summary></summary>
            ENG = 2,
            /// <summary></summary>
            FCF = 3,
            /// <summary></summary>
            FIN = 4,
            /// <summary></summary>
            FRN = 5,
            /// <summary></summary>
            GER = 6,
            /// <summary></summary>
            ICE = 7,
            /// <summary></summary>
            ITN = 8,
            /// <summary></summary>
            NOR = 9,
            /// <summary></summary>
            POR = 10,
            /// <summary></summary>
            SPA = 11,
            /// <summary></summary>
            SWE = 12,
            /// <summary></summary>
            USA = 13,
            /// <summary></summary>
            AFRIKAANS = 14,
            /// <summary></summary>
            ALBANIA = 15,
            /// <summary></summary>
            ARABIC = 16,
            /// <summary></summary>
            ARABIC_ALGERIA = 17,
            /// <summary></summary>
            ARABIC_BAHRAIN = 18,
            /// <summary></summary>
            ARABIC_EGYPT = 19,
            /// <summary></summary>
            ARABIC_IRAQ = 20,
            /// <summary></summary>
            ARABIC_JORDAN = 21,
            /// <summary></summary>
            ARABIC_KUWAIT = 22,
            /// <summary></summary>
            ARABIC_LEBANON = 23,
            /// <summary></summary>
            ARABIC_LIBYA = 24,
            /// <summary></summary>
            ARABIC_MOROCCO = 25,
            /// <summary></summary>
            ARABIC_OMAN = 26,
            /// <summary></summary>
            ARABIC_QATAR = 27,
            /// <summary></summary>
            ARABIC_SAUDIARABIA = 28,
            /// <summary></summary>
            ARABIC_SYRIA = 29,
            /// <summary></summary>
            ARABIC_TUNISIA = 30,
            /// <summary></summary>
            ARABIC_UAE = 31,
            /// <summary></summary>
            ARABIC_YEMEN = 32,
            /// <summary></summary>
            BASQUE = 33,
            /// <summary></summary>
            BYELORUSSIAN = 34,
            /// <summary></summary>
            BULGARIAN = 35,
            /// <summary></summary>
            CATALAN = 36,
            /// <summary></summary>
            CHINESE = 37,
            /// <summary></summary>
            CHINESE_HONGKONG = 38,
            /// <summary></summary>
            CHINESE_PRC = 39,
            /// <summary></summary>
            CHINESE_SINGAPORE = 40,
            /// <summary></summary>
            CHINESE_SIMPLIFIED = 41,
            /// <summary></summary>
            CHINESE_TAIWAN = 42,
            /// <summary></summary>
            CHINESE_TRADITIONAL = 43,
            /// <summary></summary>
            CROATIA = 44,
            /// <summary></summary>
            CZECH = 45,
            /// <summary></summary>
            DANISH = DAN,
            /// <summary></summary>
            DUTCH = DUT,
            /// <summary></summary>
            DUTCH_BELGIAN = 46,
            /// <summary></summary>
            ENGLISH = ENG,
            /// <summary></summary>
            ENGLISH_AUSTRALIAN = 47,
            /// <summary></summary>
            ENGLISH_CANADIAN = 48,
            /// <summary></summary>
            ENGLISH_IRELAND = 49,
            /// <summary></summary>
            ENGLISH_NEWZEALAND = 50,
            /// <summary></summary>
            ENGLISH_SOUTHAFRICA = 51,
            /// <summary></summary>
            ENGLISH_UK = 52,
            /// <summary></summary>
            ENGLISH_USA = USA,
            /// <summary></summary>
            ESTONIAN = 53,
            /// <summary></summary>
            FAEROESE = 54,
            /// <summary></summary>
            FARSI = 55,
            /// <summary></summary>
            FINNISH = FIN,
            /// <summary></summary>
            FRENCH = FRN,
            /// <summary></summary>
            FRENCH_BELGIAN = 56,
            /// <summary></summary>
            FRENCH_CANADIAN = FCF,
            /// <summary></summary>
            FRENCH_LUXEMBOURG = 57,
            /// <summary></summary>
            FRENCH_SWISS = 58,
            /// <summary></summary>
            GERMAN = GER,
            /// <summary></summary>
            GERMAN_AUSTRIAN = 59,
            /// <summary></summary>
            GERMAN_LUXEMBOURG = 60,
            /// <summary></summary>
            GERMAN_LIECHTENSTEIN = 61,
            /// <summary></summary>
            GERMAN_SWISS = 62,
            /// <summary></summary>
            GREEK = 63,
            /// <summary></summary>
            HEBREW = 64,
            /// <summary></summary>
            HUNGARIAN = 65,
            /// <summary></summary>
            ICELANDIC = ICE,
            /// <summary></summary>
            INDONESIAN = 66,
            /// <summary></summary>
            ITALIAN = ITN,
            /// <summary></summary>
            ITALIAN_SWISS = 67,
            /// <summary></summary>
            JAPANESE = 68,
            /// <summary></summary>
            KOREAN = 69,
            /// <summary></summary>
            KOREAN_JOHAB = 70,
            /// <summary></summary>
            LATVIAN = 71,
            /// <summary></summary>
            LITHUANIAN = 72,
            /// <summary></summary>
            NORWEGIAN = NOR,
            /// <summary></summary>
            NORWEGIAN_BOKMAL = 73,
            /// <summary></summary>
            NORWEGIAN_NYNORSK = 74,
            /// <summary></summary>
            POLISH = 75,
            /// <summary></summary>
            PORTUGUESE = POR,
            /// <summary></summary>
            PORTUGUESE_BRAZIL = 76,
            /// <summary></summary>
            ROMANIAN = 77,
            /// <summary></summary>
            RUSSIAN = 78,
            /// <summary></summary>
            SERBIAN_LATIN = 79,
            /// <summary></summary>
            SLOVAK = 80,
            /// <summary></summary>
            SLOVENIAN = 81,
            /// <summary></summary>
            SPANISH = TWLG.SPA,
            /// <summary></summary>
            SPANISH_MEXICAN = 82,
            /// <summary></summary>
            SPANISH_MODERN = 83,
            /// <summary></summary>
            SWEDISH = TWLG.SWE,
            /// <summary></summary>
            THAI = 84,
            /// <summary></summary>
            TURKISH = 85,
            /// <summary></summary>
            UKRANIAN = 86,
            /// <summary></summary>
            ASSAMESE = 87,
            /// <summary></summary>
            BENGALI = 88,
            /// <summary></summary>
            BIHARI = 89,
            /// <summary></summary>
            BODO = 90,
            /// <summary></summary>
            DOGRI = 91,
            /// <summary></summary>
            GUJARATI = 92,
            /// <summary></summary>
            HARYANVI = 93,
            /// <summary></summary>
            HINDI = 94,
            /// <summary></summary>
            KANNADA = 95,
            /// <summary></summary>
            KASHMIRI = 96,
            /// <summary></summary>
            MALAYALAM = 97,
            /// <summary></summary>
            MARATHI = 98,
            /// <summary></summary>
            MARWARI = 99,
            /// <summary></summary>
            MEGHALAYAN = 100,
            /// <summary></summary>
            MIZO = 101,
            /// <summary></summary>
            NAGA = 102,
            /// <summary></summary>
            ORISSI = 103,
            /// <summary></summary>
            PUNJABI = 104,
            /// <summary></summary>
            PUSHTU = 105,
            /// <summary></summary>
            SERBIAN_CYRILLIC = 106,
            /// <summary></summary>
            SIKKIMI = 107,
            /// <summary></summary>
            SWEDISH_FINLAND = 108,
            /// <summary></summary>
            TAMIL = 109,
            /// <summary></summary>
            TELUGU = 110,
            /// <summary></summary>
            TRIPURI = 111,
            /// <summary></summary>
            URDU = 112,
            /// <summary></summary>
            VIETNAMESE = 113
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Data Groups...
        ///////////////////////////////////////////////////////////////////////////////
        #region Data Groups...

        /// <summary>
        /// Data Groups...
        /// </summary>
        public enum DG : uint
        {
            /// <summary></summary>
            CONTROL = 0x1,
            /// <summary></summary>
            IMAGE = 0x2,
            /// <summary></summary>
            AUDIO = 0x4,

            // More Data Functionality may be added in the future.
            // These are for items that need to be determined before DS is opened.
            // NOTE: Supported Functionality constants must be powers of 2 as they are
            //       used as bitflags when Application asks DSM to present a list of DSs.
            //       to support backward capability the App and DS will not use the fields
            /// <summary></summary>
            DSM2 = 0x10000000,
            /// <summary></summary>
            APP2 = 0x20000000,
            /// <summary></summary>
            DS2 = 0x40000000,
            /// <summary></summary>
            MASK = 0xFFFF
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Data Argument Types...
        ///////////////////////////////////////////////////////////////////////////////
        #region Data Argument Types...

        /// <summary>
        /// Data Argument Types...
        /// </summary>
        public enum DAT : ushort
        {
            // NULL and Custom Base...
            /// <summary></summary>
            NULL = 0x0,
            /// <summary></summary>
            CUSTOM = 0x8000,

            // Data Argument Types for the DG_CONTROL Data Group.
            /// <summary></summary>
            CAPABILITY = 0x1,
            /// <summary></summary>
            EVENT = 0x2,
            /// <summary></summary>
            IDENTITY = 0x3,
            /// <summary></summary>
            PARENT = 0x4,
            /// <summary></summary>
            PENDINGXFERS = 0x5,
            /// <summary></summary>
            SETUPMEMXFER = 0x6,
            /// <summary></summary>
            SETUPFILEXFER = 0x7,
            /// <summary></summary>
            STATUS = 0x8,
            /// <summary></summary>
            USERINTERFACE = 0x9,
            /// <summary></summary>
            XFERGROUP = 0xa,
            /// <summary></summary>
            CUSTOMDSDATA = 0xc,
            /// <summary></summary>
            DEVICEEVENT = 0xd,
            /// <summary></summary>
            FILESYSTEM = 0xe,
            /// <summary></summary>
            PASSTHRU = 0xf,
            /// <summary></summary>
            CALLBACK = 0x10,
            /// <summary></summary>
            STATUSUTF8 = 0x11,
            /// <summary></summary>
            CALLBACK2 = 0x12,
            /// <summary></summary>
            METRICS = 0x13,
            /// <summary></summary>
            TWAINDIRECT = 0x14,

            // Data Argument Types for the DG_IMAGE Data Group.
            /// <summary></summary>
            IMAGEINFO = 0x0101,
            /// <summary></summary>
            IMAGELAYOUT = 0x0102,
            /// <summary></summary>
            IMAGEMEMXFER = 0x0103,
            /// <summary></summary>
            IMAGENATIVEXFER = 0x0104,
            /// <summary></summary>
            IMAGEFILEXFER = 0x105,
            /// <summary></summary>
            CIECOLOR = 0x106,
            /// <summary></summary>
            GRAYRESPONSE = 0x107,
            /// <summary></summary>
            RGBRESPONSE = 0x108,
            /// <summary></summary>
            JPEGCOMPRESSION = 0x109,
            /// <summary></summary>
            PALETTE8 = 0x10a,
            /// <summary></summary>
            EXTIMAGEINFO = 0x10b,
            /// <summary></summary>
            FILTER = 0x10c,

            /* Data Argument Types for the DG_AUDIO Data Group. */
            /// <summary></summary>
            AUDIOFILEXFER = 0x201,
            /// <summary></summary>
            AUDIOINFO = 0x202,
            /// <summary></summary>
            AUDIONATIVEXFER = 0x203,

            /* misplaced */
            /// <summary></summary>
            ICCPROFILE = 0x401,
            /// <summary></summary>
            IMAGEMEMFILEXFER = 0x402,
            /// <summary></summary>
            ENTRYPOINT = 0x403
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Messages...
        ///////////////////////////////////////////////////////////////////////////////
        #region Messages...

        /// <summary>
        /// All message constants are unique.
        /// Messages are grouped according to which DATs they are used with.
        /// </summary>
        public enum MSG : ushort
        {
            // Only used to clear fields...
            /// <summary></summary>
            NULL = 0x0,

            // Generic messages may be used with any of several DATs.
            /// <summary></summary>
            GET = 0x1,
            /// <summary></summary>
            GETCURRENT = 0x2,
            /// <summary></summary>
            GETDEFAULT = 0x3,
            /// <summary></summary>
            GETFIRST = 0x4,
            /// <summary></summary>
            GETNEXT = 0x5,
            /// <summary></summary>
            SET = 0x6,
            /// <summary></summary>
            RESET = 0x7,
            /// <summary></summary>
            QUERYSUPPORT = 0x8,
            /// <summary></summary>
            GETHELP = 0x9,
            /// <summary></summary>
            GETLABEL = 0xa,
            /// <summary></summary>
            GETLABELENUM = 0xb,
            /// <summary></summary>
            SETCONSTRAINT = 0xc,

            // Messages used with DAT_NULL.
            /// <summary></summary>
            XFERREADY = 0x101,
            /// <summary></summary>
            CLOSEDSREQ = 0x102,
            /// <summary></summary>
            CLOSEDSOK = 0x103,
            /// <summary></summary>
            DEVICEEVENT = 0x104,

            // Messages used with a pointer to DAT_PARENT data.
            /// <summary></summary>
            OPENDSM = 0x301,
            /// <summary></summary>
            CLOSEDSM = 0x302,

            // Messages used with a pointer to a DAT_IDENTITY structure.
            /// <summary></summary>
            OPENDS = 0x401,
            /// <summary></summary>
            CLOSEDS = 0x402,
            /// <summary></summary>
            USERSELECT = 0x403,

            // Messages used with a pointer to a DAT_USERINTERFACE structure.
            /// <summary></summary>
            DISABLEDS = 0x501,
            /// <summary></summary>
            ENABLEDS = 0x502,
            /// <summary></summary>
            ENABLEDSUIONLY = 0x503,

            // Messages used with a pointer to a DAT_EVENT structure.
            /// <summary></summary>
            PROCESSEVENT = 0x601,

            // Messages used with a pointer to a DAT_PENDINGXFERS structure
            /// <summary></summary>
            ENDXFER = 0x701,
            /// <summary></summary>
            STOPFEEDER = 0x702,

            // Messages used with a pointer to a DAT_FILESYSTEM structure
            /// <summary></summary>
            CHANGEDIRECTORY = 0x0801,
            /// <summary></summary>
            CREATEDIRECTORY = 0x0802,
            /// <summary></summary>
            DELETE = 0x0803,
            /// <summary></summary>
            FORMATMEDIA = 0x0804,
            /// <summary></summary>
            GETCLOSE = 0x0805,
            /// <summary></summary>
            GETFIRSTFILE = 0x0806,
            /// <summary></summary>
            GETINFO = 0x0807,
            /// <summary></summary>
            GETNEXTFILE = 0x0808,
            /// <summary></summary>
            RENAME = 0x0809,
            /// <summary></summary>
            COPY = 0x080A,
            /// <summary></summary>
            AUTOMATICCAPTUREDIRECTORY = 0x080B,

            // Messages used with a pointer to a DAT_PASSTHRU structure
            /// <summary></summary>
            PASSTHRU = 0x0901,

            // used with DAT_CALLBACK
            /// <summary></summary>
            REGISTER_CALLBACK = 0x0902,

            // used with DAT_CAPABILITY
            /// <summary></summary>
            RESETALL = 0x0A01,

            // used with DAT_TWAINDIRECT
            /// <summary></summary>
            SETTASK = 0x0B01
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Capabilities...
        ///////////////////////////////////////////////////////////////////////////////
        #region Capabilities...

        /// <summary>
        /// The naming convention is a little awkward, but it allows us to
        /// achieve a unified capability type...
        /// </summary>
        public enum CAP : ushort
        {
            // Base of custom capabilities.
            /// <summary></summary>
            CAP_CUSTOMBASE = 0x8000,

            /* all data sources are REQUIRED to support these caps */
            /// <summary></summary>
            CAP_XFERCOUNT = 0x0001,

            /* image data sources are REQUIRED to support these caps */
            /// <summary></summary>
            ICAP_COMPRESSION = 0x0100,
            /// <summary></summary>
            ICAP_PIXELTYPE = 0x0101,
            /// <summary></summary>
            ICAP_UNITS = 0x0102,
            /// <summary></summary>
            ICAP_XFERMECH = 0x0103,

            // all data sources MAY support these caps.
            /// <summary></summary>
            CAP_AUTHOR = 0x1000,
            /// <summary></summary>
            CAP_CAPTION = 0x1001,
            /// <summary></summary>
            CAP_FEEDERENABLED = 0x1002,
            /// <summary></summary>
            CAP_FEEDERLOADED = 0x1003,
            /// <summary></summary>
            CAP_TIMEDATE = 0x1004,
            /// <summary></summary>
            CAP_SUPPORTEDCAPS = 0x1005,
            /// <summary></summary>
            CAP_EXTENDEDCAPS = 0x1006,
            /// <summary></summary>
            CAP_AUTOFEED = 0x1007,
            /// <summary></summary>
            CAP_CLEARPAGE = 0x1008,
            /// <summary></summary>
            CAP_FEEDPAGE = 0x1009,
            /// <summary></summary>
            CAP_REWINDPAGE = 0x100a,
            /// <summary></summary>
            CAP_INDICATORS = 0x100b,
            /// <summary></summary>
            CAP_PAPERDETECTABLE = 0x100d,
            /// <summary></summary>
            CAP_UICONTROLLABLE = 0x100e,
            /// <summary></summary>
            CAP_DEVICEONLINE = 0x100f,
            /// <summary></summary>
            CAP_AUTOSCAN = 0x1010,
            /// <summary></summary>
            CAP_THUMBNAILSENABLED = 0x1011,
            /// <summary></summary>
            CAP_DUPLEX = 0x1012,
            /// <summary></summary>
            CAP_DUPLEXENABLED = 0x1013,
            /// <summary></summary>
            CAP_ENABLEDSUIONLY = 0x1014,
            /// <summary></summary>
            CAP_CUSTOMDSDATA = 0x1015,
            /// <summary></summary>
            CAP_ENDORSER = 0x1016,
            /// <summary></summary>
            CAP_JOBCONTROL = 0x1017,
            /// <summary></summary>
            CAP_ALARMS = 0x1018,
            /// <summary></summary>
            CAP_ALARMVOLUME = 0x1019,
            /// <summary></summary>
            CAP_AUTOMATICCAPTURE = 0x101a,
            /// <summary></summary>
            CAP_TIMEBEFOREFIRSTCAPTURE = 0x101b,
            /// <summary></summary>
            CAP_TIMEBETWEENCAPTURES = 0x101c,
            /// <summary></summary>
            CAP_CLEARBUFFERS = 0x101d,
            /// <summary></summary>
            CAP_MAXBATCHBUFFERS = 0x101e,
            /// <summary></summary>
            CAP_DEVICETIMEDATE = 0x101f,
            /// <summary></summary>
            CAP_POWERSUPPLY = 0x1020,
            /// <summary></summary>
            CAP_CAMERAPREVIEWUI = 0x1021,
            /// <summary></summary>
            CAP_DEVICEEVENT = 0x1022,
            /// <summary></summary>
            CAP_SERIALNUMBER = 0x1024,
            /// <summary></summary>
            CAP_PRINTER = 0x1026,
            /// <summary></summary>
            CAP_PRINTERENABLED = 0x1027,
            /// <summary></summary>
            CAP_PRINTERINDEX = 0x1028,
            /// <summary></summary>
            CAP_PRINTERMODE = 0x1029,
            /// <summary></summary>
            CAP_PRINTERSTRING = 0x102a,
            /// <summary></summary>
            CAP_PRINTERSUFFIX = 0x102b,
            /// <summary></summary>
            CAP_LANGUAGE = 0x102c,
            /// <summary></summary>
            CAP_FEEDERALIGNMENT = 0x102d,
            /// <summary></summary>
            CAP_FEEDERORDER = 0x102e,
            /// <summary></summary>
            CAP_REACQUIREALLOWED = 0x1030,
            /// <summary></summary>
            CAP_BATTERYMINUTES = 0x1032,
            /// <summary></summary>
            CAP_BATTERYPERCENTAGE = 0x1033,
            /// <summary></summary>
            CAP_CAMERASIDE = 0x1034,
            /// <summary></summary>
            CAP_SEGMENTED = 0x1035,
            /// <summary></summary>
            CAP_CAMERAENABLED = 0x1036,
            /// <summary></summary>
            CAP_CAMERAORDER = 0x1037,
            /// <summary></summary>
            CAP_MICRENABLED = 0x1038,
            /// <summary></summary>
            CAP_FEEDERPREP = 0x1039,
            /// <summary></summary>
            CAP_FEEDERPOCKET = 0x103a,
            /// <summary></summary>
            CAP_AUTOMATICSENSEMEDIUM = 0x103b,
            /// <summary></summary>
            CAP_CUSTOMINTERFACEGUID = 0x103c,
            /// <summary></summary>
            CAP_SUPPORTEDCAPSSEGMENTUNIQUE = 0x103d,
            /// <summary></summary>
            CAP_SUPPORTEDDATS = 0x103e,
            /// <summary></summary>
            CAP_DOUBLEFEEDDETECTION = 0x103f,
            /// <summary></summary>
            CAP_DOUBLEFEEDDETECTIONLENGTH = 0x1040,
            /// <summary></summary>
            CAP_DOUBLEFEEDDETECTIONSENSITIVITY = 0x1041,
            /// <summary></summary>
            CAP_DOUBLEFEEDDETECTIONRESPONSE = 0x1042,
            /// <summary></summary>
            CAP_PAPERHANDLING = 0x1043,
            /// <summary></summary>
            CAP_INDICATORSMODE = 0x1044,
            /// <summary></summary>
            CAP_PRINTERVERTICALOFFSET = 0x1045,
            /// <summary></summary>
            CAP_POWERSAVETIME = 0x1046,
            /// <summary></summary>
            CAP_PRINTERCHARROTATION = 0x1047,
            /// <summary></summary>
            CAP_PRINTERFONTSTYLE = 0x1048,
            /// <summary></summary>
            CAP_PRINTERINDEXLEADCHAR = 0x1049,
            /// <summary></summary>
            CAP_PRINTERINDEXMAXVALUE = 0x104A,
            /// <summary></summary>
            CAP_PRINTERINDEXNUMDIGITS = 0x104B,
            /// <summary></summary>
            CAP_PRINTERINDEXSTEP = 0x104C,
            /// <summary></summary>
            CAP_PRINTERINDEXTRIGGER = 0x104D,
            /// <summary></summary>
            CAP_PRINTERSTRINGPREVIEW = 0x104E,
            /// <summary></summary>
            CAP_SHEETCOUNT = 0x104F,

            // image data sources MAY support these caps.
            /// <summary></summary>
            ICAP_AUTOBRIGHT = 0x1100,
            /// <summary></summary>
            ICAP_BRIGHTNESS = 0x1101,
            /// <summary></summary>
            ICAP_CONTRAST = 0x1103,
            /// <summary></summary>
            ICAP_CUSTHALFTONE = 0x1104,
            /// <summary></summary>
            ICAP_EXPOSURETIME = 0x1105,
            /// <summary></summary>
            ICAP_FILTER = 0x1106,
            /// <summary></summary>
            ICAP_FLASHUSED = 0x1107,
            /// <summary></summary>
            ICAP_GAMMA = 0x1108,
            /// <summary></summary>
            ICAP_HALFTONES = 0x1109,
            /// <summary></summary>
            ICAP_HIGHLIGHT = 0x110a,
            /// <summary></summary>
            ICAP_IMAGEFILEFORMAT = 0x110c,
            /// <summary></summary>
            ICAP_LAMPSTATE = 0x110d,
            /// <summary></summary>
            ICAP_LIGHTSOURCE = 0x110e,
            /// <summary></summary>
            ICAP_ORIENTATION = 0x1110,
            /// <summary></summary>
            ICAP_PHYSICALWIDTH = 0x1111,
            /// <summary></summary>
            ICAP_PHYSICALHEIGHT = 0x1112,
            /// <summary></summary>
            ICAP_SHADOW = 0x1113,
            /// <summary></summary>
            ICAP_FRAMES = 0x1114,
            /// <summary></summary>
            ICAP_XNATIVERESOLUTION = 0x1116,
            /// <summary></summary>
            ICAP_YNATIVERESOLUTION = 0x1117,
            /// <summary></summary>
            ICAP_XRESOLUTION = 0x1118,
            /// <summary></summary>
            ICAP_YRESOLUTION = 0x1119,
            /// <summary></summary>
            ICAP_MAXFRAMES = 0x111a,
            /// <summary></summary>
            ICAP_TILES = 0x111b,
            /// <summary></summary>
            ICAP_BITORDER = 0x111c,
            /// <summary></summary>
            ICAP_CCITTKFACTOR = 0x111d,
            /// <summary></summary>
            ICAP_LIGHTPATH = 0x111e,
            /// <summary></summary>
            ICAP_PIXELFLAVOR = 0x111f,
            /// <summary></summary>
            ICAP_PLANARCHUNKY = 0x1120,
            /// <summary></summary>
            ICAP_ROTATION = 0x1121,
            /// <summary></summary>
            ICAP_SUPPORTEDSIZES = 0x1122,
            /// <summary></summary>
            ICAP_THRESHOLD = 0x1123,
            /// <summary></summary>
            ICAP_XSCALING = 0x1124,
            /// <summary></summary>
            ICAP_YSCALING = 0x1125,
            /// <summary></summary>
            ICAP_BITORDERCODES = 0x1126,
            /// <summary></summary>
            ICAP_PIXELFLAVORCODES = 0x1127,
            /// <summary></summary>
            ICAP_JPEGPIXELTYPE = 0x1128,
            /// <summary></summary>
            ICAP_TIMEFILL = 0x112a,
            /// <summary></summary>
            ICAP_BITDEPTH = 0x112b,
            /// <summary></summary>
            ICAP_BITDEPTHREDUCTION = 0x112c,
            /// <summary></summary>
            ICAP_UNDEFINEDIMAGESIZE = 0x112d,
            /// <summary></summary>
            ICAP_IMAGEDATASET = 0x112e,
            /// <summary></summary>
            ICAP_EXTIMAGEINFO = 0x112f,
            /// <summary></summary>
            ICAP_MINIMUMHEIGHT = 0x1130,
            /// <summary></summary>
            ICAP_MINIMUMWIDTH = 0x1131,
            /// <summary></summary>
            ICAP_AUTODISCARDBLANKPAGES = 0x1134,
            /// <summary></summary>
            ICAP_FLIPROTATION = 0x1136,
            /// <summary></summary>
            ICAP_BARCODEDETECTIONENABLED = 0x1137,
            /// <summary></summary>
            ICAP_SUPPORTEDBARCODETYPES = 0x1138,
            /// <summary></summary>
            ICAP_BARCODEMAXSEARCHPRIORITIES = 0x1139,
            /// <summary></summary>
            ICAP_BARCODESEARCHPRIORITIES = 0x113a,
            /// <summary></summary>
            ICAP_BARCODESEARCHMODE = 0x113b,
            /// <summary></summary>
            ICAP_BARCODEMAXRETRIES = 0x113c,
            /// <summary></summary>
            ICAP_BARCODETIMEOUT = 0x113d,
            /// <summary></summary>
            ICAP_ZOOMFACTOR = 0x113e,
            /// <summary></summary>
            ICAP_PATCHCODEDETECTIONENABLED = 0x113f,
            /// <summary></summary>
            ICAP_SUPPORTEDPATCHCODETYPES = 0x1140,
            /// <summary></summary>
            ICAP_PATCHCODEMAXSEARCHPRIORITIES = 0x1141,
            /// <summary></summary>
            ICAP_PATCHCODESEARCHPRIORITIES = 0x1142,
            /// <summary></summary>
            ICAP_PATCHCODESEARCHMODE = 0x1143,
            /// <summary></summary>
            ICAP_PATCHCODEMAXRETRIES = 0x1144,
            /// <summary></summary>
            ICAP_PATCHCODETIMEOUT = 0x1145,
            /// <summary></summary>
            ICAP_FLASHUSED2 = 0x1146,
            /// <summary></summary>
            ICAP_IMAGEFILTER = 0x1147,
            /// <summary></summary>
            ICAP_NOISEFILTER = 0x1148,
            /// <summary></summary>
            ICAP_OVERSCAN = 0x1149,
            /// <summary></summary>
            ICAP_AUTOMATICBORDERDETECTION = 0x1150,
            /// <summary></summary>
            ICAP_AUTOMATICDESKEW = 0x1151,
            /// <summary></summary>
            ICAP_AUTOMATICROTATE = 0x1152,
            /// <summary></summary>
            ICAP_JPEGQUALITY = 0x1153,
            /// <summary></summary>
            ICAP_FEEDERTYPE = 0x1154,
            /// <summary></summary>
            ICAP_ICCPROFILE = 0x1155,
            /// <summary></summary>
            ICAP_AUTOSIZE = 0x1156,
            /// <summary></summary>
            ICAP_AUTOMATICCROPUSESFRAME = 0x1157,
            /// <summary></summary>
            ICAP_AUTOMATICLENGTHDETECTION = 0x1158,
            /// <summary></summary>
            ICAP_AUTOMATICCOLORENABLED = 0x1159,
            /// <summary></summary>
            ICAP_AUTOMATICCOLORNONCOLORPIXELTYPE = 0x115a,
            /// <summary></summary>
            ICAP_COLORMANAGEMENTENABLED = 0x115b,
            /// <summary></summary>
            ICAP_IMAGEMERGE = 0x115c,
            /// <summary></summary>
            ICAP_IMAGEMERGEHEIGHTTHRESHOLD = 0x115d,
            /// <summary></summary>
            ICAP_SUPPORTEDEXTIMAGEINFO = 0x115e,
            /// <summary></summary>
            ICAP_FILMTYPE = 0x115f,
            /// <summary></summary>
            ICAP_MIRROR = 0x1160,
            /// <summary></summary>
            ICAP_JPEGSUBSAMPLING = 0x1161,

            // image data sources MAY support these audio caps.
            /// <summary></summary>
            ACAP_XFERMECH = 0x1202
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Extended Image Info Attributes section  Added 1.7...
        ///////////////////////////////////////////////////////////////////////////////
        #region Extended Image Info Attributes section  Added 1.7...

        /// <summary>
        /// Extended Image Info Attributes...
        /// </summary>
        public enum TWEI : ushort
        {
            /// <summary></summary>
            BARCODEX = 0x1200,
            /// <summary></summary>
            BARCODEY = 0x1201,
            /// <summary></summary>
            BARCODETEXT = 0x1202,
            /// <summary></summary>
            BARCODETYPE = 0x1203,
            /// <summary></summary>
            DESHADETOP = 0x1204,
            /// <summary></summary>
            DESHADELEFT = 0x1205,
            /// <summary></summary>
            DESHADEHEIGHT = 0x1206,
            /// <summary></summary>
            DESHADEWIDTH = 0x1207,
            /// <summary></summary>
            DESHADESIZE = 0x1208,
            /// <summary></summary>
            SPECKLESREMOVED = 0x1209,
            /// <summary></summary>
            HORZLINEXCOORD = 0x120A,
            /// <summary></summary>
            HORZLINEYCOORD = 0x120B,
            /// <summary></summary>
            HORZLINELENGTH = 0x120C,
            /// <summary></summary>
            HORZLINETHICKNESS = 0x120D,
            /// <summary></summary>
            VERTLINEXCOORD = 0x120E,
            /// <summary></summary>
            VERTLINEYCOORD = 0x120F,
            /// <summary></summary>
            VERTLINELENGTH = 0x1210,
            /// <summary></summary>
            VERTLINETHICKNESS = 0x1211,
            /// <summary></summary>
            PATCHCODE = 0x1212,
            /// <summary></summary>
            ENDORSEDTEXT = 0x1213,
            /// <summary></summary>
            FORMCONFIDENCE = 0x1214,
            /// <summary></summary>
            FORMTEMPLATEMATCH = 0x1215,
            /// <summary></summary>
            FORMTEMPLATEPAGEMATCH = 0x1216,
            /// <summary></summary>
            FORMHORZDOCOFFSET = 0x1217,
            /// <summary></summary>
            FORMVERTDOCOFFSET = 0x1218,
            /// <summary></summary>
            BARCODECOUNT = 0x1219,
            /// <summary></summary>
            BARCODECONFIDENCE = 0x121A,
            /// <summary></summary>
            BARCODEROTATION = 0x121B,
            /// <summary></summary>
            BARCODETEXTLENGTH = 0x121C,
            /// <summary></summary>
            DESHADECOUNT = 0x121D,
            /// <summary></summary>
            DESHADEBLACKCOUNTOLD = 0x121E,
            /// <summary></summary>
            DESHADEBLACKCOUNTNEW = 0x121F,
            /// <summary></summary>
            DESHADEBLACKRLMIN = 0x1220,
            /// <summary></summary>
            DESHADEBLACKRLMAX = 0x1221,
            /// <summary></summary>
            DESHADEWHITECOUNTOLD = 0x1222,
            /// <summary></summary>
            DESHADEWHITECOUNTNEW = 0x1223,
            /// <summary></summary>
            DESHADEWHITERLMIN = 0x1224,
            /// <summary></summary>
            DESHADEWHITERLAVE = 0x1225,
            /// <summary></summary>
            DESHADEWHITERLMAX = 0x1226,
            /// <summary></summary>
            BLACKSPECKLESREMOVED = 0x1227,
            /// <summary></summary>
            WHITESPECKLESREMOVED = 0x1228,
            /// <summary></summary>
            HORZLINECOUNT = 0x1229,
            /// <summary></summary>
            VERTLINECOUNT = 0x122A,
            /// <summary></summary>
            DESKEWSTATUS = 0x122B,
            /// <summary></summary>
            SKEWORIGINALANGLE = 0x122C,
            /// <summary></summary>
            SKEWFINALANGLE = 0x122D,
            /// <summary></summary>
            SKEWCONFIDENCE = 0x122E,
            /// <summary></summary>
            SKEWWINDOWX1 = 0x122F,
            /// <summary></summary>
            SKEWWINDOWY1 = 0x1230,
            /// <summary></summary>
            SKEWWINDOWX2 = 0x1231,
            /// <summary></summary>
            SKEWWINDOWY2 = 0x1232,
            /// <summary></summary>
            SKEWWINDOWX3 = 0x1233,
            /// <summary></summary>
            SKEWWINDOWY3 = 0x1234,
            /// <summary></summary>
            SKEWWINDOWX4 = 0x1235,
            /// <summary></summary>
            SKEWWINDOWY4 = 0x1236,
            /// <summary></summary>
            BOOKNAME = 0x1238,
            /// <summary></summary>
            CHAPTERNUMBER = 0x1239,
            /// <summary></summary>
            DOCUMENTNUMBER = 0x123A,
            /// <summary></summary>
            PAGENUMBER = 0x123B,
            /// <summary></summary>
            CAMERA = 0x123C,
            /// <summary></summary>
            FRAMENUMBER = 0x123D,
            /// <summary></summary>
            FRAME = 0x123E,
            /// <summary></summary>
            PIXELFLAVOR = 0x123F,
            /// <summary></summary>
            ICCPROFILE = 0x1240,
            /// <summary></summary>
            LASTSEGMENT = 0x1241,
            /// <summary></summary>
            SEGMENTNUMBER = 0x1242,
            /// <summary></summary>
            MAGDATA = 0x1243,
            /// <summary></summary>
            MAGTYPE = 0x1244,
            /// <summary></summary>
            PAGESIDE = 0x1245,
            /// <summary></summary>
            FILESYSTEMSOURCE = 0x1246,
            /// <summary></summary>
            IMAGEMERGED = 0x1247,
            /// <summary></summary>
            MAGDATALENGTH = 0x1248,
            /// <summary></summary>
            PAPERCOUNT = 0x1249,
            /// <summary></summary>
            PRINTERTEXT = 0x124A,
            /// <summary></summary>
            TWAINDIRECTMETADATA = 0x124B
        }

        /// <summary></summary>
        public enum TWEJ : ushort
        {
            /// <summary></summary>
            NONE = 0x0000,
            /// <summary></summary>
            MIDSEPARATOR = 0x0001,
            /// <summary></summary>
            PATCH1 = 0x0002,
            /// <summary></summary>
            PATCH2 = 0x0003,
            /// <summary></summary>
            PATCH3 = 0x0004,
            /// <summary></summary>
            PATCH4 = 0x0005,
            /// <summary></summary>
            PATCH6 = 0x0006,
            /// <summary></summary>
            PATCHT = 0x0007
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Return Codes and Condition Codes section...
        ///////////////////////////////////////////////////////////////////////////////
        #region Return Codes and Condition Codes section...

        /// <summary>
        /// We're departing from a strict translation of TWAIN.H so that
        /// we can achieve a unified status return type.  
        /// </summary>
        public const int STSCC = 0x10000; // get us past the custom space
        /// <summary>
        /// 
        /// </summary>
        public enum STS
        {
            // Custom base (same for TWRC and TWCC)...
            /// <summary>
            /// </summary>
            CUSTOMBASE = 0x8000,

            // Return codes...
            /// <summary>
            /// </summary>
            SUCCESS = 0,
            /// <summary>
            /// </summary>
            FAILURE = 1,
            /// <summary>
            /// </summary>
            CHECKSTATUS = 2,
            /// <summary>
            /// </summary>
            CANCEL = 3,
            /// <summary>
            /// </summary>
            DSEVENT = 4,
            /// <summary>
            /// </summary>
            NOTDSEVENT = 5,
            /// <summary>
            /// </summary>
            XFERDONE = 6,
            /// <summary>
            /// </summary>
            ENDOFLIST = 7,
            /// <summary>
            /// </summary>
            INFONOTSUPPORTED = 8,
            /// <summary>
            /// </summary>
            DATANOTAVAILABLE = 9,
            /// <summary>
            /// </summary>
            BUSY = 10,
            /// <summary>
            /// </summary>
            SCANNERLOCKED = 11,

            // Condition codes (always associated with TWRC_FAILURE)...
            /// <summary>
            /// </summary>
            BUMMER = STSCC + 1,
            /// <summary>
            /// </summary>
            LOWMEMORY = STSCC + 2,
            /// <summary>
            /// </summary>
            NODS = STSCC + 3,
            /// <summary>
            /// </summary>
            MAXCONNECTIONS = STSCC + 4,
            /// <summary>
            /// </summary>
            OPERATIONERROR = STSCC + 5,
            /// <summary>
            /// </summary>
            BADCAP = STSCC + 6,
            /// <summary>
            /// </summary>
            BADPROTOCOL = STSCC + 9,
            /// <summary>
            /// </summary>
            BADVALUE = STSCC + 10,
            /// <summary>
            /// </summary>
            SEQERROR = STSCC + 11,
            /// <summary>
            /// </summary>
            BADDEST = STSCC + 12,
            /// <summary>
            /// </summary>
            CAPUNSUPPORTED = STSCC + 13,
            /// <summary>
            /// </summary>
            CAPBADOPERATION = STSCC + 14,
            /// <summary>
            /// </summary>
            CAPSEQERROR = STSCC + 15,
            /// <summary>
            /// </summary>
            DENIED = STSCC + 16,
            /// <summary>
            /// </summary>
            FILEEXISTS = STSCC + 17,
            /// <summary>
            /// 
            /// </summary>
            FILENOTFOUND = STSCC + 18,
            /// <summary>
            /// 
            /// </summary>
            NOTEMPTY = STSCC + 19,
            /// <summary>
            /// 
            /// </summary>
            PAPERJAM = STSCC + 20,
            /// <summary>
            /// 
            /// </summary>
            PAPERDOUBLEFEED = STSCC + 21,
            /// <summary>
            /// 
            /// </summary>
            FILEWRITEERROR = STSCC + 22,
            /// <summary>
            /// 
            /// </summary>
            CHECKDEVICEONLINE = STSCC + 23,
            /// <summary>
            /// 
            /// </summary>
            INTERLOCK = STSCC + 24,
            /// <summary>
            /// 
            /// </summary>
            DAMAGEDCORNER = STSCC + 25,
            /// <summary>
            /// 
            /// </summary>
            FOCUSERROR = STSCC + 26,
            /// <summary>
            /// 
            /// </summary>
            DOCTOOLIGHT = STSCC + 27,
            /// <summary>
            /// 
            /// </summary>
            DOCTOODARK = STSCC + 28,
            /// <summary>
            /// 
            /// </summary>
            NOMEDIA = STSCC + 29
        }

        /// <summary>
        /// bit patterns: for query the operation that are supported by the data source on a capability
        /// Application gets these through DG_CONTROL/DAT_CAPABILITY/MSG_QUERYSUPPORT
        /// </summary>
        public enum TWQC : ushort
        {
            /// <summary>
            /// 
            /// </summary>
            GET = 0x0001,
            /// <summary>
            /// 
            /// </summary>
            SET = 0x0002,
            /// <summary>
            /// 
            /// </summary>
            GETDEFAULT = 0x0004,
            /// <summary>
            /// 
            /// </summary>
            GETCURRENT = 0x0008,
            /// <summary>
            /// 
            /// </summary>
            RESET = 0x0010,
            /// <summary>
            /// 
            /// </summary>
            SETCONSTRAINT = 0x0020,
            /// <summary>
            /// 
            /// </summary>
            CONSTRAINABLE = 0x0040
        }

        /// <summary>
        /// The TWAIN States...
        /// </summary>
        public enum STATE
        {
            /// <summary>
            /// 
            /// </summary>
            S1, // Nothing loaded or open
            /// <summary>
            /// 
            /// </summary>
            S2, // DSM loaded
            /// <summary>
            /// 
            /// </summary>
            S3, // DSM open
            /// <summary>
            /// 
            /// </summary>
            S4, // Data Source open, programmatic mode (no GUI)
            /// <summary>
            /// 
            /// </summary>
            S5, // GUI up or waiting to transfer first image
            /// <summary>
            /// 
            /// </summary>
            S6, // ready to start transferring image
            /// <summary>
            /// 
            /// </summary>
            S7  // transferring image or transfer done
        }

        #endregion

    }


    /// <summary>
    /// All of our DllImports live here...
    /// </summary>
    internal sealed class NativeMethods
    {
        ///////////////////////////////////////////////////////////////////////////////
        // Windows
        ///////////////////////////////////////////////////////////////////////////////
        #region Windows

        /// <summary>
        /// Get the ID for the current thread...
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        internal static extern uint GetCurrentThreadId();

        /// <summary>
        /// Allocate a handle to memory...
        /// </summary>
        /// <param name="uFlags"></param>
        /// <param name="dwBytes"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        /// <summary>
        /// Free a memory handle...
        /// </summary>
        /// <param name="hMem"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalFree(IntPtr hMem);

        /// <summary>
        /// Lock a memory handle...
        /// </summary>
        /// <param name="hMem"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalLock(IntPtr hMem);

        /// <summary>
        /// Unlock a memory handle...
        /// </summary>
        /// <param name="hMem"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GlobalUnlock(IntPtr hMem);

        #endregion


        // We're supporting every DSM that we can...

        /// <summary>
        /// Use this entry for generic access to the DSM where the
        /// destination must be IntPtr.Zero (null)...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="zero"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="memref"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryNullDest
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            IntPtr zero,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="zero"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="memref"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryNullDest
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            IntPtr zero,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="zero"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="memref"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryNullDest
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            IntPtr zero,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="zero"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="memref"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryNullDest
        (
            ref TWAIN.TW_IDENTITY origin,
            IntPtr zero,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="zero"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="memref"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryNullDest
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            IntPtr zero,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="zero"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="memref"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryNullDest
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            IntPtr zero,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );


        /// <summary>
        /// Use for generic access to the DSM where the destination must
        /// reference a data source...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="memref"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntry
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntry
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntry
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntry
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntry
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntry
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );


        /// <summary>
        /// Use this for DG_AUDIO / DAT.AUDIOFILEXFER / MSG.GET calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="memref"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryAudioAudiofilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryAudioAudiofilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryAudioAudiofilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryAudioAudiofilexfer
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryAudioAudiofilexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryAudioAudiofilexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr memref
        );

        /// <summary>
        /// Use this for DG_AUDIO / DAT.AUDIOINFO / MSG.GET calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twaudioinfo"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryAudioAudioinfo
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_AUDIOINFO twaudioinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryAudioAudioinfo
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_AUDIOINFO twaudioinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryAudioAudioinfo
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_AUDIOINFO twaudioinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryAudioAudioinfo
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_AUDIOINFO twaudioinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryAudioAudioinfo
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_AUDIOINFO twaudioinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryAudioAudioinfo
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_AUDIOINFO twaudioinfo
        );

        /// <summary>
        /// Use this for DG_AUDIO / DAT.AUDIONATIVEXFER / MSG.GET...
        /// *** We'll add this later...maybe***
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twcallback"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryCallback
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK twcallback
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryCallback
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK twcallback
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryCallback
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK twcallback
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryCallback
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK twcallback
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryCallback
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK twcallback
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryCallback
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK twcallback
        );
        public delegate UInt16 WindowsDsmEntryCallbackDelegate
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twnull
        );
        public delegate UInt16 LinuxDsmEntryCallbackDelegate
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twnull
        );
        public delegate UInt16 Linux020302Dsm64bitEntryCallbackDelegate
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twnull
        );
        public delegate UInt16 MacosxDsmEntryCallbackDelegate
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twnull
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.CALLBACK2 / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twcallback2"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryCallback2
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK2 twcallback2
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twcallback2"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryCallback2
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK2 twcallback2
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryCallback2
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK2 twcallback2
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryCallback2
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK2 twcallback2
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryCallback2
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK2 twcallback
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryCallback2
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY des,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CALLBACK2 twcallback
        );
        private delegate UInt16 WindowsDsmEntryCallback2Delegate
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twnull
        );
        private delegate UInt16 LinuxDsmEntryCallback2Delegate
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twnull
        );
        private delegate UInt16 Linux020302Dsm64bitEntryCallback2Delegate
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twnull
        );
        private delegate UInt16 MacosxDsmEntryCallback2Delegate
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twnull
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.CAPABILITY / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twcapability"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryCapability
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CAPABILITY twcapability
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryCapability
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CAPABILITY twcapability
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryCapability
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CAPABILITY twcapability
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryCapability
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CAPABILITY twcapability
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryCapability
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CAPABILITY twcapability
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryCapability
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CAPABILITY twcapability
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.CUSTOMDSDATA / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twcustomedsdata"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryCustomdsdata
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CUSTOMDSDATA twcustomedsdata
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twcustomdsdata"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryCustomdsdata
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CUSTOMDSDATA twcustomdsdata
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twcustomdsdata"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryCustomdsdata
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CUSTOMDSDATA twcustomdsdata
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryCustomdsdata
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CUSTOMDSDATA twcustomdsdata
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryCustomdsdata
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CUSTOMDSDATA twcustomedsdata
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryCustomdsdata
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CUSTOMDSDATA twcustomedsdata
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.DEVICEEVENT / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twdeviceevent"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryDeviceevent
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_DEVICEEVENT twdeviceevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryDeviceevent
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_DEVICEEVENT twdeviceevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryDeviceevent
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_DEVICEEVENT twdeviceevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryDeviceevent
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_DEVICEEVENT twdeviceevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryDeviceevent
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_DEVICEEVENT twdeviceevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryDeviceevent
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_DEVICEEVENT twdeviceevent
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.EVENT / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twevent"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryEvent
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EVENT twevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryEvent
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EVENT twevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryEvent
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EVENT twevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryEvent
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EVENT twevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryEvent
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EVENT twevent
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryEvent
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EVENT twevent
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.ENTRYPOINT / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twentrypoint"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryEntrypoint
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_ENTRYPOINT twentrypoint
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryEntrypoint
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_ENTRYPOINT twentrypoint
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryEntrypoint
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_ENTRYPOINT twentrypoint
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryEntrypoint
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_ENTRYPOINT_LINUX64 twentrypoint
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryEntrypoint
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_ENTRYPOINT twentrypoint
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryEntrypoint
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_ENTRYPOINT twentrypoint
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.FILESYSTEM / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twfilesystem"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryFilesystem
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILESYSTEM twfilesystem
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twfilesystem"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryFilesystem
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILESYSTEM twfilesystem
        );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twfilesystem"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryFilesystem
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILESYSTEM twfilesystem
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryFilesystem
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILESYSTEM twfilesystem
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryFilesystem
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILESYSTEM twfilesystem
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryFilesystem
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILESYSTEM twfilesystem
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.IDENTITY / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twidentity"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryIdentity
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IDENTITY_LEGACY twidentity
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryIdentity
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IDENTITY_LEGACY twidentity
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryIdentity
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IDENTITY_LEGACY twidentity
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryIdentity
        (
            ref TWAIN.TW_IDENTITY_LINUX64 origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IDENTITY_LINUX64 twidentity
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryIdentity
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IDENTITY_MACOSX twidentity
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryIdentity
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IDENTITY_MACOSX twidentity
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.NULL / MSG.* calls...
        /// ***Only needed for drivers, so we don't have it***
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryParent
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr hwnd
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryParent
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr hwnd
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryParent
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr hwnd
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryParent
        (
            ref TWAIN.TW_IDENTITY origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr hwnd
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryParent
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr hwnd
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryParent
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            IntPtr dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr hwnd
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.PASSTHRU / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twpassthru"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryPassthru
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PASSTHRU twpassthru
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryPassthru
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PASSTHRU twpassthru
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryPassthru
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PASSTHRU twpassthru
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryPassthru
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PASSTHRU twpassthru
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryPassthru
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PASSTHRU twpassthru
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryPassthru
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PASSTHRU twpassthru
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.PENDINGXFERS / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twpendingxfers"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryPendingxfers
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PENDINGXFERS twpendingxfers
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryPendingxfers
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PENDINGXFERS twpendingxfers
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryPendingxfers
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PENDINGXFERS twpendingxfers
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryPendingxfers
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PENDINGXFERS twpendingxfers
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryPendingxfers
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PENDINGXFERS twpendingxfers
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryPendingxfers
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PENDINGXFERS twpendingxfers
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.SETUPFILEXFER / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twsetupfilexfer"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntrySetupfilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPFILEXFER twsetupfilexfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntrySetupfilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPFILEXFER twsetupfilexfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntrySetupfilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPFILEXFER twsetupfilexfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntrySetupfilexfer
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPFILEXFER twsetupfilexfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntrySetupfilexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPFILEXFER twsetupfilexfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntrySetupfilexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPFILEXFER twsetupfilexfer
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.SETUPMEMXFER / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twsetupmemxfer"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntrySetupmemxfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPMEMXFER twsetupmemxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntrySetupmemxfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPMEMXFER twsetupmemxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntrySetupmemxfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPMEMXFER twsetupmemxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntrySetupmemxfer
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPMEMXFER twsetupmemxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntrySetupmemxfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPMEMXFER twsetupmemxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntrySetupmemxfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_SETUPMEMXFER twsetupmemxfer
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.STATUS / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twstatus"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryStatus
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUS twstatus
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryStatus
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUS twstatus
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryStatus
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUS twstatus
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryStatus
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUS twstatus
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryStatus
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUS twstatus
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryStatus
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUS twstatus
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.STATUSUTF8 / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twstatusutf8"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryStatusutf8
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUSUTF8 twstatusutf8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryStatusutf8
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUSUTF8 twstatusutf8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryStatusutf8
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUSUTF8 twstatusutf8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryStatusutf8
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUSUTF8 twstatusutf8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryStatusutf8
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUSUTF8 twstatusutf8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryStatusutf8
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_STATUSUTF8 twstatusutf8
        );

        /// <summary>
        /// Use this for DG.CONTROL / DAT.TWAINDIRECT / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twtwaindirect"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryTwaindirect
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_TWAINDIRECT twtwaindirect
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryTwaindirect
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_TWAINDIRECT twtwaindirect
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryTwaindirect
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_TWAINDIRECT twtwaindirect
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryTwaindirect
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_TWAINDIRECT twtwaindirect
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryTwaindirect
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_TWAINDIRECT twtwaindirect
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryTwaindirect
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_TWAINDIRECT twtwaindirect
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.USERINTERFACE / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twuserinterface"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryUserinterface
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_USERINTERFACE twuserinterface
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryUserinterface
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_USERINTERFACE twuserinterface
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryUserinterface
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_USERINTERFACE twuserinterface
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryUserinterface
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_USERINTERFACE twuserinterface
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryUserinterface
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_USERINTERFACE twuserinterface
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryUserinterface
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_USERINTERFACE twuserinterface
        );

        /// <summary>
        /// Use this for DG_CONTROL / DAT.XFERGROUP / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twuint32"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryXfergroup
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref UInt32 twuint32
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryXfergroup
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref UInt32 twuint32
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryXfergroup
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref UInt32 twuint32
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryXfergroup
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref UInt32 twuint32
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryXfergroup
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref UInt32 twuint32
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryXfergroup
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref UInt32 twuint32
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.CIECOLOR / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twciecolor"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryCiecolor
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CIECOLOR twciecolor
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryCiecolor
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CIECOLOR twciecolor
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryCiecolor
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CIECOLOR twciecolor
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryCiecolor
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CIECOLOR twciecolor
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryCiecolor
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CIECOLOR twciecolor
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryCiecolor
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_CIECOLOR twciecolor
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.EXTIMAGEINFO / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twextimageinfo"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryExtimageinfo
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EXTIMAGEINFO twextimageinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryExtimageinfo
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EXTIMAGEINFO twextimageinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryExtimageinfo
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EXTIMAGEINFO twextimageinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryExtimageinfo
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EXTIMAGEINFO twextimageinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryExtimageinfo
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EXTIMAGEINFO twextimageinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryExtimageinfo
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_EXTIMAGEINFO twextimageinfo
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.FILTER / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twfilter"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryFilter
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILTER twfilter
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryFilter
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILTER twfilter
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryFilter
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILTER twfilter
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryFilter
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILTER twfilter
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryFilter
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILTER twfilter
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryFilter
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_FILTER twfilter
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.GRAYRESPONSE / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twgrayresponse"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryGrayresponse
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_GRAYRESPONSE twgrayresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryGrayresponse
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_GRAYRESPONSE twgrayresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryGrayresponse
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_GRAYRESPONSE twgrayresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryGrayresponse
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_GRAYRESPONSE twgrayresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryGrayresponse
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_GRAYRESPONSE twgrayresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryGrayresponse
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_GRAYRESPONSE twgrayresponse
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.ICCPROFILE / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twmemory"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryIccprofile
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_MEMORY twmemory
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryIccprofile
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_MEMORY twmemory
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryIccprofile
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_MEMORY twmemory
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryIccprofile
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_MEMORY twmemory
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryIccprofile
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_MEMORY twmemory
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryIccprofile
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_MEMORY twmemory
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.IMAGEFILEXFER / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twmemref"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryImagefilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twmemref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryImagefilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twmemref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryImagefilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twmemref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryImagefilexfer
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twmemref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryImagefilexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twmemref
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryImagefilexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            IntPtr twmemref
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.IMAGEINFO / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twimageinfo"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryImageinfo
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEINFO twimageinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryImageinfo
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEINFO twimageinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryImageinfo
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEINFO twimageinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryImageinfo
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEINFO_LINUX64 twimageinfolinux64
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryImageinfo
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEINFO twimageinfo
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryImageinfo
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEINFO twimageinfo
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.IMAGELAYOUT / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twimagelayout"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryImagelayout
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGELAYOUT twimagelayout
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryImagelayout
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGELAYOUT twimagelayout
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryImagelayout
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGELAYOUT twimagelayout
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryImagelayout
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGELAYOUT twimagelayout
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryImagelayout
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGELAYOUT twimagelayout
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryImagelayout
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGELAYOUT twimagelayout
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.IMAGEMEMFILEXFER / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twimagememxfer"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryImagememfilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER twimagememxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryImagememfilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER twimagememxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryImagememfilexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER twimagememxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryImagememfilexfer
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER_LINUX64 twimagememxferlinux64
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryImagememfilexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER_MACOSX twimagememxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryImagememfilexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER_MACOSX twimagememxfer
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.IMAGEMEMXFER / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twimagememxfer"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryImagememxfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER twimagememxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryImagememxfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER twimagememxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryImagememxfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER twimagememxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryImagememxfer
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER_LINUX64 twimagememxferlinux64
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryImagememxfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER_MACOSX twimagememxfer
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryImagememxfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_IMAGEMEMXFER_MACOSX twimagememxfer
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.IMAGENATIVEXFER / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="intptrBitmap"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryImagenativexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr intptrBitmap
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryImagenativexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr intptrBitmap
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryImagenativexfer
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr intptrBitmap
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryImagenativexfer
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr intptrBitmap
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryImagenativexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr intptrBitmap
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryImagenativexfer
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref IntPtr intptrBitmap
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.JPEGCOMPRESSION / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twjpegcompression"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryJpegcompression
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_JPEGCOMPRESSION twjpegcompression
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryJpegcompression
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_JPEGCOMPRESSION twjpegcompression
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryJpegcompression
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_JPEGCOMPRESSION twjpegcompression
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryJpegcompression
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_JPEGCOMPRESSION twjpegcompression
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryJpegcompression
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_JPEGCOMPRESSION twjpegcompression
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryJpegcompression
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_JPEGCOMPRESSION twjpegcompression
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.PALETTE8 / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twpalette8"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryPalette8
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PALETTE8 twpalette8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryPalette8
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PALETTE8 twpalette8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryPalette8
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PALETTE8 twpalette8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryPalette8
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PALETTE8 twpalette8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryPalette8
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PALETTE8 twpalette8
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryPalette8
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_PALETTE8 twpalette8
        );

        /// <summary>
        /// Use this for DG_IMAGE / DAT.RGBRESPONSE / MSG.* calls...
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="dg"></param>
        /// <param name="dat"></param>
        /// <param name="msg"></param>
        /// <param name="twrgbresponse"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwain32DsmEntryRgbresponse
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_RGBRESPONSE twrgbresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 WindowsTwaindsmDsmEntryRgbresponse
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_RGBRESPONSE twrgbresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 LinuxDsmEntryRgbresponse
        (
            ref TWAIN.TW_IDENTITY_LEGACY origin,
            ref TWAIN.TW_IDENTITY_LEGACY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_RGBRESPONSE twrgbresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/usr/local/lib/libtwaindsm.so.2.3.2", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 Linux020302Dsm64bitEntryRgbresponse
        (
            ref TWAIN.TW_IDENTITY origin,
            ref TWAIN.TW_IDENTITY dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_RGBRESPONSE twrgbresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/System/Library/Frameworks/TWAIN.framework/TWAIN", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwainDsmEntryRgbresponse
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_RGBRESPONSE twrgbresponse
        );
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("/Library/Frameworks/TWAINDSM.framework/TWAINDSM", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern UInt16 MacosxTwaindsmDsmEntryRgbresponse
        (
            ref TWAIN.TW_IDENTITY_MACOSX origin,
            ref TWAIN.TW_IDENTITY_MACOSX dest,
            TWAIN.DG dg,
            TWAIN.DAT dat,
            TWAIN.MSG msg,
            ref TWAIN.TW_RGBRESPONSE twrgbresponse
        );

    }
}
