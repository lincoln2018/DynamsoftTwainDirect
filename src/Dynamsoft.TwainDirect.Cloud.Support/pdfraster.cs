﻿///////////////////////////////////////////////////////////////////////////////////////
//
// TwainDirectSupport.PdfRaster
//
// PDF/raster writer functions.  These are implemented as close as possible to the
// C modules written by Atalasoft.  The main deviation is in the callback functions.
// We're not trying to support a heap manager in this version of the code.
//
///////////////////////////////////////////////////////////////////////////////////////
//  Author          Date            Version     Comment
//  M.McLaughlin    19-Dec-2014     0.0.0.1     Initial Transcription from C
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) 2014-2016 Kodak Alaris Inc.
//  Copyright (C) 2014-2015 Atalasoft, Inc.
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

// Helpers...
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace Dynamsoft.TwainDirect.Cloud.Support
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PdfRaster
    {
        ///////////////////////////////////////////////////////////////////////////////
        // Public Methods: PdfRaster
        ///////////////////////////////////////////////////////////////////////////////
        #region Public Methods: PdfRaster

        /// <summary>
        /// See if we can load the PDF/raster binaries, use this to determine
        /// if we need to install the visual studio redistributables...
        /// </summary>
        /// <returns></returns>
        public bool HealthCheck()
        {
            try
            {
                PdfRasterReader.Reader pdfRasRd = null;
                pdfRasRd = new PdfRasterReader.Reader();
                
                pdfRasRd = null;
            }
            catch(SystemException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Install the redistributables for Visual Studio 2017...
        /// </summary>
        /// <returns>true if we launched it</returns>
        public bool InstallVisualStudioRedistributables()
        {
            string szVcRedistExe;
            Process process;

            // Get the path to the manager...
            szVcRedistExe = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "VC_redist.x86.exe");
            if (!File.Exists(szVcRedistExe))
            {
                return false;
            }

            // Launch it as admin...
            process = new Process();
            process.StartInfo.FileName = szVcRedistExe;
            process.StartInfo.UseShellExecute = true;
            if (System.Environment.OSVersion.Version.Major >= 6)
            {
                process.StartInfo.Verb = "runas";
            }
            try
            {
                process.Start();
            }
            catch
            {
                return false;
            }

            // Wait for it to finish...
            process.WaitForExit();
            process.Dispose();

            // Alld done...
            return true;
        }

        /// <summary>
        /// Add image header to the image data from a PDF/raster..
        /// </summary>
        /// <param name="a_abImage">the image data</param>
        /// <param name="a_abStripData"></param>
        /// <param name="a_rasterpixelformat">color, grayscale, black and white</param>
        /// <param name="a_rastercompression">none. group4, jpeg</param>
        /// <param name="a_lResolution">resolution in dots per inch</param>
        /// <param name="a_lWidth">width in pixels</param>
        /// <param name="a_lHeight">height in pixels</param>
        /// <returns>true on success</returns>
        public static bool AddImageHeader
        (
            out byte[] a_abImage,
            byte[] a_abStripData,
            PdfRasterReader.Reader.PdfRasterReaderPixelFormat a_rasterpixelformat,
            PdfRasterReader.Reader.PdfRasterReaderCompression a_rastercompression,
            long a_lResolution,
            long a_lWidth,
            long a_lHeight
        )
        {
            int iCount;
            int iHeader;
            IntPtr intptr;
            TiffBitonalUncompressed tiffbitonaluncompressed;
            TiffBitonalG4 tiffbitonalg4;
            TiffGrayscaleUncompressed tiffgrayscaleuncompressed;
            TiffColorUncompressed tiffcoloruncompressed;

            // Init stuff...
            a_abImage = null;

            // Add a header, if needed...
            switch (a_rasterpixelformat)
            {
                default:
                    return false;
                    
                case PdfRasterReader.Reader.PdfRasterReaderPixelFormat.PDFRASREAD_BITONAL:
                    switch (a_rastercompression)
                    {
                        default:
                            return false;
                            
                        case PdfRasterReader.Reader.PdfRasterReaderCompression.PDFRASREAD_UNCOMPRESSED:
                            iCount = (int)(((a_lWidth + 7) / 8) * a_lHeight); //it's packed
                            tiffbitonaluncompressed = new TiffBitonalUncompressed((uint)a_lWidth, (uint)a_lHeight, (uint)a_lResolution, (uint)iCount);
                            iHeader = Marshal.SizeOf(tiffbitonaluncompressed);
                            a_abImage = new byte[iHeader + iCount];
                            intptr = Marshal.AllocHGlobal(iHeader);
                            Marshal.StructureToPtr(tiffbitonaluncompressed, intptr, true);
                            Marshal.Copy(intptr, a_abImage, 0, iHeader);
                            Marshal.FreeHGlobal(intptr);
                            Buffer.BlockCopy(a_abStripData, 0, a_abImage, iHeader, iCount);
                            break;
                            
                        case PdfRasterReader.Reader.PdfRasterReaderCompression.PDFRASEARD_CCITTG4:
                            iCount = a_abStripData.Length;
                            tiffbitonalg4 = new TiffBitonalG4((uint)a_lWidth, (uint)a_lHeight, (uint)a_lResolution, (uint)iCount);
                            iHeader = Marshal.SizeOf(tiffbitonalg4);
                            a_abImage = new byte[iHeader + iCount];
                            intptr = Marshal.AllocHGlobal(iHeader);
                            Marshal.StructureToPtr(tiffbitonalg4, intptr, true);
                            Marshal.Copy(intptr, a_abImage, 0, iHeader);
                            Marshal.FreeHGlobal(intptr);
                            Buffer.BlockCopy(a_abStripData, 0, a_abImage, iHeader, iCount);
                            break;
                    }
                    break;
                    
                case PdfRasterReader.Reader.PdfRasterReaderPixelFormat.PDFRASREAD_GRAYSCALE:
                    switch (a_rastercompression)
                    {
                        default:
                            return false;
                            
                        case PdfRasterReader.Reader.PdfRasterReaderCompression.PDFRASREAD_UNCOMPRESSED:
                            iCount = (int)(a_lWidth * a_lHeight);
                            tiffgrayscaleuncompressed = new TiffGrayscaleUncompressed((uint)a_lWidth, (uint)a_lHeight, (uint)a_lResolution, (uint)iCount);
                            iHeader = Marshal.SizeOf(tiffgrayscaleuncompressed);
                            a_abImage = new byte[iHeader + iCount];
                            intptr = Marshal.AllocHGlobal(iHeader);
                            Marshal.StructureToPtr(tiffgrayscaleuncompressed, intptr, true);
                            Marshal.Copy(intptr, a_abImage, 0, iHeader);
                            Marshal.FreeHGlobal(intptr);
                            Buffer.BlockCopy(a_abStripData, 0, a_abImage, iHeader, iCount);
                            break;
                            
                        case PdfRasterReader.Reader.PdfRasterReaderCompression.PDFRASREAD_JPEG:
                            iCount = a_abStripData.Length;
                            iHeader = 0;
                            a_abImage = new byte[iCount];
                            Buffer.BlockCopy(a_abStripData, 0, a_abImage, iHeader, iCount);
                            break;
                    }
                    break;
                    
                case PdfRasterReader.Reader.PdfRasterReaderPixelFormat.PDFRASREAD_RGB:
                    switch (a_rastercompression)
                    {
                        default:
                            return false;
                            
                        case PdfRasterReader.Reader.PdfRasterReaderCompression.PDFRASREAD_UNCOMPRESSED:
                            iCount = (int)((a_lWidth * 3) * a_lHeight);
                            tiffcoloruncompressed = new TiffColorUncompressed((uint)a_lWidth, (uint)a_lHeight, (uint)a_lResolution, (uint)iCount);
                            iHeader = Marshal.SizeOf(tiffcoloruncompressed);
                            a_abImage = new byte[iHeader + iCount];
                            intptr = Marshal.AllocHGlobal(iHeader);
                            Marshal.StructureToPtr(tiffcoloruncompressed, intptr, true);
                            Marshal.Copy(intptr, a_abImage, 0, iHeader);
                            Marshal.FreeHGlobal(intptr);
                            Buffer.BlockCopy(a_abStripData, 0, a_abImage, iHeader, iCount);
                            break;
                            
                        case PdfRasterReader.Reader.PdfRasterReaderCompression.PDFRASREAD_JPEG:
                            iCount = a_abStripData.Length;
                            iHeader = 0;                       
                            a_abImage = new byte[iCount];
                            Buffer.BlockCopy(a_abStripData, 0, a_abImage, iHeader, iCount);
                            break;
                    }
                    break;
            }

            // All done...
            return true;
        }

        /// <summary>
        /// Get the security infor for a PDF/raster file...
        /// </summary>
        /// <param name="a_szImage">a PDF/raster file</param>
        /// <param name="a_securitytype">security for this file</param>
        /// <returns>true if we were able to get info</returns>
        public static bool GetSecurityType(string a_szImage, out SecurityType a_securitytype)
        {
            PdfRasterReader.Reader.PdfRasterReaderSecurityType rasterreadersecuritytype;
            PdfRasterReader.Reader pdfRasRd = null;

            // Check the argument...
            if (string.IsNullOrEmpty(a_szImage))
            {
                Log.Error("GetSecurityType: file not specified...");
                a_securitytype = SecurityType.Unknown;
                return false;
            }
            if (!File.Exists(a_szImage))
            {
                Log.Error("GetSecurityType: file not found - " + a_szImage);
                a_securitytype = SecurityType.Unknown;
                return false;
            }

            // Give us a PDF reader object...
            pdfRasRd = new PdfRasterReader.Reader();

            // Get the security type...
            try
            {
                rasterreadersecuritytype = pdfRasRd.decoder_get_security_type(a_szImage);
            }
            catch (Exception exception)
            {
                Log.Error("GetSecurityType: decoder_get_security_type for " + a_szImage + " - " + exception.Message);
                a_securitytype = SecurityType.Unknown;
                return false;
            }

            // Report the result...
            switch (rasterreadersecuritytype)
            {
                default:
                case PdfRasterReader.Reader.PdfRasterReaderSecurityType.PDFRASREAD_SECURITY_UNKNOWN:
                    a_securitytype = SecurityType.Unknown;
                    return true;
                case PdfRasterReader.Reader.PdfRasterReaderSecurityType.PDFRASREAD_UNENCRYPTED:
                    a_securitytype = SecurityType.None;
                    return true;
                case PdfRasterReader.Reader.PdfRasterReaderSecurityType.RASREAD_STANDARD_SECURITY:
                    a_securitytype = SecurityType.Password;
                    return true;
                case PdfRasterReader.Reader.PdfRasterReaderSecurityType.RASREAD_PUBLIC_KEY_SECURITY:
                    a_securitytype = SecurityType.Certificate;
                    return true;
            }
        }

        /// <summary>
        /// Convert a PDF/raster into something we can easily display with
        /// native .NET stuff...
        /// </summary>
        /// <param name="a_szImage">file to convert</param>
        /// <param name="a_szPassword">an optional password</param>
        /// <returns>a byte array we can turn into a bitmap</returns>
        public static byte[] ConvertPdfToTiffOrJpeg(string a_szImage, string a_szPassword = null)
        {
            int decoder = -1;
            long lWidth;
            long lHeight;
            long lResolution;
            byte[] abStripData;
            byte[] abImage = null;
            PdfRasterReader.Reader.PdfRasterReaderSecurityType rasterreadersecuritytype;
            PdfRasterReader.Reader.PdfRasterReaderPixelFormat rasterreaderpixelformat;
            PdfRasterReader.Reader.PdfRasterReaderCompression rasterreadercompression;
            PdfRasterReader.Reader pdfRasRd = null;

            // Do the conversion...
            try
            {
                // Give us a PDF reader object...
                pdfRasRd = new PdfRasterReader.Reader();

                // We can't handle displaying encrypted images yet...
                rasterreadersecuritytype = pdfRasRd.decoder_get_security_type(a_szImage);
                switch (rasterreadersecuritytype)
                {
                    default:
                    case PdfRasterReader.Reader.PdfRasterReaderSecurityType.PDFRASREAD_SECURITY_UNKNOWN:
                        Log.Error("ConvertPdfToTiffOrJpeg: unrecognized security type - " + rasterreadersecuritytype);
                        return (null);
                    case PdfRasterReader.Reader.PdfRasterReaderSecurityType.RASREAD_PUBLIC_KEY_SECURITY:
                        decoder = pdfRasRd.decoder_create(PdfRasterReader.Reader.PdfRasterConst.PDFRASREAD_API_LEVEL, a_szImage, a_szPassword);
                        break;
                    case PdfRasterReader.Reader.PdfRasterReaderSecurityType.PDFRASREAD_UNENCRYPTED:
                        decoder = pdfRasRd.decoder_create(PdfRasterReader.Reader.PdfRasterConst.PDFRASREAD_API_LEVEL, a_szImage);
                        break;
                    case PdfRasterReader.Reader.PdfRasterReaderSecurityType.RASREAD_STANDARD_SECURITY:
                        decoder = pdfRasRd.decoder_create(PdfRasterReader.Reader.PdfRasterConst.PDFRASREAD_API_LEVEL, a_szImage, a_szPassword);
                        break;
                }

                // Okay, let's do it...
                lWidth = pdfRasRd.decoder_get_width(decoder);
                lHeight = pdfRasRd.decoder_get_height(decoder);
                lResolution = (long)pdfRasRd.decoder_get_yresolution(decoder);
                rasterreaderpixelformat = pdfRasRd.decoder_get_pixelformat(decoder);
                rasterreadercompression = pdfRasRd.decoder_get_compression(decoder);
                abStripData = pdfRasRd.decoder_read_strips(decoder);
                pdfRasRd.decoder_destroy(decoder);
                decoder = -1;
                AddImageHeader(out abImage, abStripData, rasterreaderpixelformat, rasterreadercompression, lResolution, lWidth, lHeight);
            }
            catch (Exception exception)
            {
                Log.Error("ConvertPdfToTiffOrJpeg failed: " + exception.Message);
                if ((pdfRasRd != null) && (decoder != -1))
                {
                    pdfRasRd.decoder_destroy(decoder);
                }
                return (null);
            }

            // Spit back the result...
            return (abImage);
        }

        /// <summary>
        /// Create a PDF/raster from one of the following:
        /// - bitonal uncompressed
        /// - bitonal group4
        /// - grayscale uncompressed
        /// - grayscale jpeg
        /// - color uncompressed
        /// - color jpeg
        /// </summary>
        /// <param name="a_szPdfRasterFile">file to store the pdf/raster</param>
        /// <param name="a_szEncryptionProfileName">name of a profile or null</param>
        /// <param name="a_szPfxFile">pfx file to sign with</param>
        /// <param name="a_szPfxPassword">password for the pfx file</param>
        /// <param name="a_szMetadataJson">metadata in json->xml conversion format</param>
        /// <param name="a_abImage">raw image data to wrap</param>
        /// <param name="a_iImageOffset">byte offset into the image</param>
        /// <param name="a_szPixelFormat">bw1, gray8, rgb24</param>
        /// <param name="a_szCompression">none, group4, jpeg</param>
        /// <param name="a_i32Resolution">dots per inch</param>
        /// <param name="a_i32Width">width in pixels</param>
        /// <param name="a_i32Height">height in pixels</param>
        /// <returns></returns>
        public static bool CreatePdfRaster
        (
            string a_szPdfRasterFile,
            string a_szEncryptionProfileName,
            string a_szPfxFile,
            string a_szPfxPassword,
            string a_szMetadataJson,
            byte[] a_abImage,
            int a_iImageOffset,
            string a_szPixelFormat,
            string a_szCompression,
            int a_i32Resolution,
            int a_i32Width,
            int a_i32Height
        )
        {
            bool blSuccess = true;
            PdfRasterWriter.Writer.PdfRasterPixelFormat rasterpixelformat;
            PdfRasterWriter.Writer.PdfRasterCompression rastercompression;

            // Convert the pixel type...
            switch (a_szPixelFormat)
            {
                default:
                    Log.Error("Unsupported pixel type: " + a_szPixelFormat);
                    return false;
                case "bw1": rasterpixelformat = PdfRasterWriter.Writer.PdfRasterPixelFormat.PDFRASWR_BITONAL; break;
                case "gray8": rasterpixelformat = PdfRasterWriter.Writer.PdfRasterPixelFormat.PDFRASWR_GRAYSCALE; break;
                case "rgb24": rasterpixelformat = PdfRasterWriter.Writer.PdfRasterPixelFormat.PDFRASWR_RGB; break;
            }

            // Convert the compression...
            switch (a_szCompression)
            {
                default:
                    Log.Error("Unsupported compression: " + a_szCompression);
                    return false;
                case "none": rastercompression = PdfRasterWriter.Writer.PdfRasterCompression.PDFRASWR_UNCOMPRESSED; break;
                case "group4": rastercompression = PdfRasterWriter.Writer.PdfRasterCompression.PDFRASWR_CCITTG4; break;
                case "jpeg": rastercompression = PdfRasterWriter.Writer.PdfRasterCompression.PDFRASWR_JPEG; break;
            }

            // Create the file...
            try
            {
                int enc;
                bool blDigitallySigned = false;

                // Are we signing?  Note that we allow for an empty password...
                if (!string.IsNullOrEmpty(a_szPfxFile) && File.Exists(a_szPfxFile) && (a_szPfxPassword != null))
                {
                    blDigitallySigned = true;
                }

                // Construct a raster PDF encoder
                PdfRasterWriter.Writer pdfRasWr = new PdfRasterWriter.Writer();
                if (blDigitallySigned)
                {
                    enc = pdfRasWr.encoder_create_digitally_signed(PdfRasterWriter.Writer.PdfRasterConst.PDFRASWR_API_LEVEL, a_szPdfRasterFile, a_szPfxFile, a_szPfxPassword);
                }
                else
                {
                    enc = pdfRasWr.encoder_create(PdfRasterWriter.Writer.PdfRasterConst.PDFRASWR_API_LEVEL, a_szPdfRasterFile);
                }
                pdfRasWr.encoder_set_creator(enc, "TWAIN Direct on TWAIN v1.0");

                // Set info for the digital signature...
                if (blDigitallySigned)
                {
                    pdfRasWr.digital_signature_set_contactinfo(enc, "contact info");
                    pdfRasWr.digital_signature_set_location(enc, "PC");
                    pdfRasWr.digital_signature_set_name(enc, "TWAIN-Direct-on-TWAIN");
                    pdfRasWr.digital_signature_set_reason(enc, "Scanning");
                }

                // Request encryption...
                if (!string.IsNullOrEmpty(a_szEncryptionProfileName))
                {
                    bool blFound = false;
                    int iProfile = 0;

                    // Look for a match...
                    while (true)
                    {
                        // Get the next encryptionProfile...
                        string szProfileName = Config.Get("encryptionProfiles[" + iProfile + "].name", "");
                        if (string.IsNullOrEmpty(szProfileName))
                        {
                            break;
                        }

                        // Do we have a match?
                        if (a_szEncryptionProfileName == szProfileName)
                        {
                            blFound = true;
                            break;
                        }

                        // Next profile...
                        iProfile += 1;
                    }

                    // Found it...
                    if (blFound)
                    {
                        List<PdfRasterWriter.Writer.PdfRasterPubSecRecipient> listpdfrasterrubsecrecipient;
                        PdfRasterWriter.Writer.PdfRasterPubSecRecipient pdfrasterpubsecrecipient;
                        switch (Config.Get("encryptionProfiles[" + iProfile + "].type", ""))
                        {
                            default:
                                break;
                            case "password":
                                pdfRasWr.encoder_set_AES256_encrypter
                                (
                                    enc,
                                    Config.Get("encryptionProfiles[" + iProfile + "].passwordUser", ""),
                                    Config.Get("encryptionProfiles[" + iProfile + "].passwordOwner", ""),
                                    PdfRasterWriter.Writer.PdfRasterPermissions.PDFRASWR_PERM_FILL_FORMS | PdfRasterWriter.Writer.PdfRasterPermissions.PDFRASWR_PERM_PRINT_DOCUMENT,
                                    ((Config.Get("encryptMetadata", "true") == "true") ? (uint)1 : (uint)0)
                                );
                                break;
                            case "publicKey":
                                listpdfrasterrubsecrecipient = new List<PdfRasterWriter.Writer.PdfRasterPubSecRecipient>();
                                pdfrasterpubsecrecipient = new PdfRasterWriter.Writer.PdfRasterPubSecRecipient();
                                switch (Config.Get("encryptionProfiles[" + iProfile + "].publicKeyType", ""))
                                {
                                    default:
                                        Log.Error("Unsupported publicKeyType: <" + Config.Get("encryptionProfiles[" + iProfile + "].publicKeyType", "") + ">");
                                        break;
                                    case "aes256":
                                        pdfrasterpubsecrecipient.public_key = Config.Get("encryptionProfiles[" + iProfile + "].publicKey", "");
                                        pdfrasterpubsecrecipient.perms = PdfRasterWriter.Writer.PdfRasterPermissions.PDFRASWR_PERM_FILL_FORMS | PdfRasterWriter.Writer.PdfRasterPermissions.PDFRASWR_PERM_PRINT_DOCUMENT;
                                        listpdfrasterrubsecrecipient.Add(pdfrasterpubsecrecipient);
                                        pdfRasWr.encoder_set_pubsec_AES256_encrypter
                                        (
                                            enc,
                                            listpdfrasterrubsecrecipient,
                                            ((Config.Get("encryptMetadata", "true") == "true") ? (uint)1 : (uint)0)
                                        );
                                        break;
                                }
                                break;
                        }
                    }
                }

                // Create the page (we only ever have one)...
                pdfRasWr.encoder_set_resolution(enc, a_i32Resolution, a_i32Resolution);
                pdfRasWr.encoder_set_pixelformat(enc, rasterpixelformat);
                pdfRasWr.encoder_set_compression(enc, rastercompression);
                pdfRasWr.encoder_start_page(enc, (int)a_i32Width);

                // Data is compressed...
                if (rastercompression != PdfRasterWriter.Writer.PdfRasterCompression.PDFRASWR_UNCOMPRESSED)
                {
                    pdfRasWr.encoder_write_strip(enc, a_i32Height, a_abImage, (UInt32)a_iImageOffset, (UInt32)(a_abImage.Length - a_iImageOffset));
                }

                // If uncompressed, need to remove BMP EOL conditions, basically we're
                // packing the data on a byte boundary, instead of the DWORD boundary
                // that TWAIN requires...
                else
                {
                    // Work out the byte packing boundary...
                    UInt32 rowWidthInBytesNotBMP = 0;
                    switch (rasterpixelformat)
                    {
                        case PdfRasterWriter.Writer.PdfRasterPixelFormat.PDFRASWR_BITONAL: rowWidthInBytesNotBMP = (uint)((a_i32Width + 7) / 8); break;
                        case PdfRasterWriter.Writer.PdfRasterPixelFormat.PDFRASWR_GRAYSCALE: rowWidthInBytesNotBMP = (uint)a_i32Width; break;
                        case PdfRasterWriter.Writer.PdfRasterPixelFormat.PDFRASWR_RGB: rowWidthInBytesNotBMP = (uint)(a_i32Width * 3); break;
                    }

                    // Get the stride for the source by dividing the total number of bytes in the image
                    // by the height.  If this isn't an integer value, then we have a bad image size,
                    // and we'll find that out when we go to do the copy...
                    UInt32 rowWidthInBytesOfBMP = (UInt32)(a_abImage.Length / a_i32Height);

                    // There's a possibility that the two values match, in that case we shouldn't
                    // penalize the user by doing an unneeded copy.  So write what we have...
                    //
                    // Otherwise we need to pack the data before writing it.  We can do this in
                    // the same buffer, since the result is going to be smaller than the original.
                    // That'll save on memory.
                    //
                    // Note that we're not bothering with the offset, we know where the raster data
                    // is located, and that's good enough.
                    if (rowWidthInBytesOfBMP != rowWidthInBytesNotBMP)
                    {
                        UInt32 srcOffset = (UInt32)a_iImageOffset;
                        UInt32 dstOffset = (UInt32)a_iImageOffset;
                        for (UInt32 ii = 0; ii < a_i32Height; ii++)
                        {
                            Array.Copy(a_abImage, srcOffset, a_abImage, dstOffset, rowWidthInBytesNotBMP);
                            srcOffset += rowWidthInBytesOfBMP;
                            dstOffset += rowWidthInBytesNotBMP;
                        }                     
                    }

                    // Write the strip...
                    pdfRasWr.encoder_write_strip(enc, (int)a_i32Height, a_abImage, (UInt32)a_iImageOffset, (UInt32)(rowWidthInBytesNotBMP * a_i32Height));
                }

                // Add the metadata...
                if (!string.IsNullOrEmpty(a_szMetadataJson))
                {
                    string szXmp;

                    // Convert our metadata to a UTF-8 byte array...
                    byte[] abMetadataJson = Encoding.UTF8.GetBytes(a_szMetadataJson);

                    // Try to build an XMP string that makes some kind of sense...
                    szXmp =
                        "<?xpacket begin=\"?\" id=\"W5M0MpCehiHzreSzNTczkc9d\"?>" +
                        "<x:xmpdata xmlns:x=\"adobe:ns:meta/\">" +
                        "<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:twaindirect=\"http://www.twaindirect.org/twaindirect\">" +
                        "<rdf:Description rdf:about=\"http://www.twaindirect.org/twaindirect#metadata\">" +
                        "<twaindirect:metadata>" +
                        Convert.ToBase64String(abMetadataJson) +
                        "</twaindirect:metadata>" +
                        "</rdf:Description>" +
                        "</rdf:RDF>" +
                        "</x:xmpdata>" +
                        "<?xpacket end=\"w\"?>";

                    // Write it...
                    pdfRasWr.encoder_write_page_xmp(enc, szXmp);
                }

                // End of page...
                pdfRasWr.encoder_end_page(enc);

                // The document is complete
                pdfRasWr.encoder_end_document(enc);
 
                // clean up
                pdfRasWr.encoder_destroy(enc);
            }
            catch (Exception exception)
            {
                Log.Error("unable to open " + a_szPdfRasterFile + " for writing");
                Log.Error(exception.Message);
                Log.Error("file: <" + a_szPdfRasterFile + ">");
                Log.Error("image length: " + ((a_abImage == null) ? "-1" : a_abImage.Length.ToString()));
                Log.Error("image offset: " + a_iImageOffset);
                Log.Error("pixelFormat:  " + a_szPixelFormat);
                Log.Error("compression:  " + a_szCompression);
                Log.Error("resolution:   " + a_i32Resolution);
                Log.Error("width:        " + a_i32Width);
                Log.Error("height:       " + a_i32Height);
                blSuccess = false;
            }

            // All done...
            return (blSuccess);
        }

        /// <summary>
        /// Create a thumbnail from a PDF/raster file...
        /// </summary>
        /// <param name="a_szPdf">source</param>
        /// <param name="a_szThumbnailFile">destination</param>
        /// <param name="a_szPfxFile">pfx file to sign with</param>
        /// <param name="a_szPfxPassword">password for the pfx file</param>
        /// <returns>true on success</returns>
        public static bool CreatePdfRasterThumbnail
        (
            string a_szPdf,
            string a_szThumbnailFile,
            string a_szPfxFile,
            string a_szPfxPassword
        )
        {
            int hh;
            int ssww;
            int ddww;
            bool blSuccess;
            long lWidth;
            long lHeight;
            long lResolution;
            byte[] abImage;
            byte[] abStripData;
            Bitmap bitmap;
            BitmapData bitmapdata;

            // Convert the image to a thumbnail...
            PdfRasterReader.Reader.PdfRasterReaderPixelFormat rasterreaderpixelformat;
            PdfRasterReader.Reader.PdfRasterReaderCompression rasterreadercompression;
            PdfRasterReader.Reader pdfRasRd = new PdfRasterReader.Reader();
            int decoder = pdfRasRd.decoder_create(PdfRasterReader.Reader.PdfRasterConst.PDFRASREAD_API_LEVEL, a_szPdf);
            lWidth = pdfRasRd.decoder_get_width(decoder);
            lHeight = pdfRasRd.decoder_get_height(decoder);
            lResolution = (long)pdfRasRd.decoder_get_yresolution(decoder);
            rasterreaderpixelformat = pdfRasRd.decoder_get_pixelformat(decoder);
            rasterreadercompression = pdfRasRd.decoder_get_compression(decoder);
            abStripData = pdfRasRd.decoder_read_strips(decoder);
            pdfRasRd.decoder_destroy(decoder);
            PdfRaster.AddImageHeader(out abImage, abStripData, rasterreaderpixelformat, rasterreadercompression, lResolution, lWidth, lHeight);
            using (var memorystream = new MemoryStream(abImage))
            {
                // Get the thumbnail, fix so all thumbnails have the same height
                // we'd like to preserve the aspect ratio (that's the tricky bit)...
                bitmap = new Bitmap(memorystream);
                Image imageThumbnail = FixedSize(bitmap, 64, 64);
                bitmap = new Bitmap(imageThumbnail);

                // Convert it from 32bit rgb to 24bit rgb, this is a shame, because
                // it would be nice to keep an alpha channel, but so it goes...
                bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                byte[] abImageBgr = new byte[bitmapdata.Stride * bitmap.Height];
                Marshal.Copy(bitmapdata.Scan0, abImageBgr, 0, abImageBgr.Length);
                int iNewStride = (bitmapdata.Stride - (bitmapdata.Stride / 4)); // lose the A from BGRA
                iNewStride = (iNewStride + 3) & ~3; // align the new stride on a 4-byte boundary
                abImage = new byte[iNewStride * bitmapdata.Height];
                for (hh = 0; hh < bitmapdata.Height; hh++)
                {
                    long lSsRow = (hh * bitmapdata.Stride);
                    long lDdRow = (hh * iNewStride);
                    for (ssww = ddww = 0; ssww < bitmapdata.Stride; ddww += 3, ssww += 4)
                    {
                        abImage[lDdRow + ddww + 0] = (byte)abImageBgr[lSsRow + ssww + 2]; // R
                        abImage[lDdRow + ddww + 1] = (byte)abImageBgr[lSsRow + ssww + 1]; // G
                        abImage[lDdRow + ddww + 2] = (byte)abImageBgr[lSsRow + ssww + 0]; // B
                    }
                }

                // PDF/raster it...              
                blSuccess = PdfRaster.CreatePdfRaster(a_szThumbnailFile, null, a_szPfxFile, a_szPfxPassword, "", abImage, 0, "rgb24", "none", (int)bitmap.HorizontalResolution, bitmap.Width, bitmap.Height);
            }

            // All done...
            return (blSuccess);
        }
        private static bool ThumbnailCallback()
        {
            return false;
        }
        static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.LightGray);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage
            (
                imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel
            );

            grPhoto.Dispose();
            return bmPhoto;
        }

        /*
        /// <summary>
        /// Check if a file is a valid PDF/raster...
        /// </summary>
        /// <param name="a_szPdf">file to check</param>
        /// <param name="a_szError">text if we hit an error</param>
        /// <returns>true if it's a valid PDF/raster file</returns>
        public static bool ValidPdfRaster(string a_szPdf, out string a_szError)
        {
            int iDecoder;
            byte[] abStripData;
            PdfRasterReader.Reader pdfRasRd;
            string szFunction = "";
            string szXmpdata;
            string szXmpMetadata;
            byte[] abXmpMetadata;
            string szFileMetadata;

            // Hope for the best...
            a_szError = "";

            // The class throws errors, so get the mitt ready...
            try
            {
                szFunction = "new PdfRasterReader. Reader()";
                pdfRasRd = new PdfRasterReader. Reader();

                szFunction = "pdfRasRd.decoder_create()";
                iDecoder = pdfRasRd.decoder_create(PdfRasterReader.Reader.PdfRasterConst.PDFRASREAD_API_LEVEL, a_szPdf);

                szFunction = "pdfRasRd.decoder_get_compression()";
                pdfRasRd.decoder_get_compression(iDecoder);

                szFunction = "pdfRasRd.decoder_get_height()";
                pdfRasRd.decoder_get_height(iDecoder);

                szFunction = "pdfRasRd.decoder_get_page_count()";
                pdfRasRd.decoder_get_page_count(iDecoder);

                szFunction = "pdfRasRd.decoder_get_pixelformat()";
                pdfRasRd.decoder_get_pixelformat(iDecoder);

                szFunction = "pdfRasRd.decoder_get_width()";
                pdfRasRd.decoder_get_width(iDecoder);

                szFunction = "pdfRasRd.decoder_get_xresolution()";
                pdfRasRd.decoder_get_xresolution(iDecoder);

                szFunction = "pdfRasRd.decoder_get_yresolution()";
                pdfRasRd.decoder_get_yresolution(iDecoder);

                szFunction = "pdfRasRd.decoder_read_strips()";
                abStripData = pdfRasRd.decoder_read_strips(iDecoder);

                // Check Metadata
                #region Check Metadata

                szFunction = "pdfRasRd.decoder_page_metadata()";
                szXmpdata = pdfRasRd.decoder_page_metadata(iDecoder, 1);

                // Okay, now let's compare the data we found inside of the
                // XMP block with our .meta file.

                // TBD: We're doing some very simplistic checks here,
                // there's got to be a better way...
                if (!szXmpdata.StartsWith("<?xpacket begin=\"?\" id=\"W5M0MpCehiHzreSzNTczkc9d\"?>"))
                {
                    a_szError = szFunction + ": failed to find xpacket";
                    pdfRasRd.decoder_destroy(iDecoder);
                    return false;
                }
                if (!szXmpdata.Contains("<x:xmpdata xmlns:x=\"adobe:ns:meta/\">"))
                {
                    a_szError = szFunction + ": failed to find x:xmpdata";
                    pdfRasRd.decoder_destroy(iDecoder);
                    return false;
                }
                if (!szXmpdata.Contains("<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:twaindirect=\"http://www.twaindirect.org/twaindirect\">"))
                {
                    a_szError = szFunction + ": failed to find rdf:RDF";
                    pdfRasRd.decoder_destroy(iDecoder);
                    return false;
                }
                if (!szXmpdata.Contains("<rdf:Description rdf:about=\"http://www.twaindirect.org/twaindirect#metadata\">"))
                {
                    a_szError = szFunction + ": failed to find rdf:Description";
                    pdfRasRd.decoder_destroy(iDecoder);
                    return false;
                }

                // This next step confirms that we found twaindirect:metadata, and
                // that all the intervening tags are in the right order...
                XmlDocument xmldocument = new XmlDocument();
                xmldocument.LoadXml(szXmpdata);
                XmlNode xmlnode = xmldocument.SelectSingleNode("//*[name()='x:xmpdata']/*[name() = 'rdf:RDF']/*[name() = 'rdf:Description']//*[name() = 'twaindirect:metadata']");
                if ((xmlnode == null) || string.IsNullOrEmpty(xmlnode.InnerXml))
                {
                    a_szError = szFunction + ": failed to find twaindirect:metadata";
                    pdfRasRd.decoder_destroy(iDecoder);
                    return false;
                }

                // Convert the data from base64 to UTF8...
                abXmpMetadata = Convert.FromBase64String(xmlnode.InnerXml);
                szXmpMetadata = Encoding.UTF8.GetString(abXmpMetadata);

                // Get the data in the file...
                szFileMetadata = File.ReadAllText(Path.Combine(Path.GetDirectoryName(a_szPdf), Path.GetFileNameWithoutExtension(a_szPdf)) + ".meta");

                // Compare the two.  We're expecting them to be identical, if
                // this seems unreasonable, we'll have to compare the contents
                // field by field...
                if (szXmpMetadata != szFileMetadata)
                {
                    a_szError = szFunction + ": XMP metadata doesn't match the contents of the .meta file";
                    pdfRasRd.decoder_destroy(iDecoder);
                    return false;
                }

                #endregion

                // Check Digital Signature
                #region Check Digital Signature

                szFunction = "pdfRasRd.decoder_digital_signature_count()";
                int iDigitalSignatureCount = pdfRasRd.decoder_digital_signature_count(iDecoder);
                if (iDigitalSignatureCount <= 0)
                {
                    a_szError = szFunction + ": no digital signatures found";
                    pdfRasRd.decoder_destroy(iDecoder);
                    return false;
                }

                // Loopy on the signatures...
                for (int ii = 0; ii < iDigitalSignatureCount; ii++)
                {
                    string szValue;

                    szFunction = "pdfRasRd.decoder_digital_signature_contactinfo()";
                    szValue = pdfRasRd.decoder_digital_signature_contactinfo(iDecoder, ii);
                    if (string.IsNullOrEmpty(szValue))
                    {
                        a_szError = szFunction + ": no contact info";
                        pdfRasRd.decoder_destroy(iDecoder);
                        return false;
                    }

                    szFunction = "pdfRasRd.decoder_digital_signature_location()";
                    szValue = pdfRasRd.decoder_digital_signature_location(iDecoder, ii);
                    if (string.IsNullOrEmpty(szValue))
                    {
                        a_szError = szFunction + ": no location";
                        pdfRasRd.decoder_destroy(iDecoder);
                        return false;
                    }

                    szFunction = "pdfRasRd.decoder_digital_signature_name()";
                    szValue = pdfRasRd.decoder_digital_signature_name(iDecoder, ii);
                    if (string.IsNullOrEmpty(szValue))
                    {
                        a_szError = szFunction + ": no name";
                        pdfRasRd.decoder_destroy(iDecoder);
                        return false;
                    }

                    szFunction = "pdfRasRd.decoder_digital_signature_reason()";
                    szValue = pdfRasRd.decoder_digital_signature_reason(iDecoder, ii);
                    if (string.IsNullOrEmpty(szValue))
                    {
                        a_szError = szFunction + ": no reason";
                        pdfRasRd.decoder_destroy(iDecoder);
                        return false;
                    }

                    szFunction = "pdfRasRd.decoder_digital_signature_validate()";
                    int iValidate = pdfRasRd.decoder_digital_signature_validate(iDecoder, ii);
                    if (iValidate < 0)
                    {
                        a_szError = szFunction + ": error validating";
                        pdfRasRd.decoder_destroy(iDecoder);
                        return false;
                    }
                    if ((iValidate & 0x1) != 0x1 ) // DS_DOC_NOT_CHANGED
                    {
                        a_szError = szFunction + ": document has been modified since it was signed";
                        pdfRasRd.decoder_destroy(iDecoder);
                        return false;
                    }
                    if ((iValidate & 0x2) != 0x2 ) // DS_CERT_VERIFIED
                    {
                        a_szError = szFunction + ": we can't validate, is the certificate in the store?";
                        pdfRasRd.decoder_destroy(iDecoder);
                        return false;
                    }
                }

                #endregion

                szFunction = "pdfRasRd.decoder_destroy()";
                pdfRasRd.decoder_destroy(iDecoder);
            }
            catch (Exception exception)
            {
                a_szError = szFunction + ": " + exception.Message;
                return false;
            }

            // We almost made it...
            if (abStripData == null)
            {
                a_szError = "pdfRasRd.decoder_read_strips(): error reading strip data";
                return false;
            }

            // Success...
            return true;
        }

        */
        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Public Definitions...
        ///////////////////////////////////////////////////////////////////////////////
        #region Public Definitions...

        /// <summary>
        /// The kinds of security that can be given to a PDF/raster file...
        /// </summary>
        public enum SecurityType
        {
            /// <summary></summary>
            None,
            /// <summary></summary>
            Password,
            /// <summary></summary>
            Certificate,
            /// <summary></summary>
            Unknown
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Definitions...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Definitions...

        // A TIFF header is composed of tags...
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        private struct TiffTag
        {
            public TiffTag(ushort a_u16Tag, ushort a_u16Type, uint a_u32Count, uint a_u32Value)
            {
                u16Tag = a_u16Tag;
                u16Type = a_u16Type;
                u32Count = a_u32Count;
                u32Value = a_u32Value;
            }

            public ushort u16Tag;
            public ushort u16Type;
            public uint u32Count;
            public uint u32Value;
        }

        // TIFF header for Uncompressed BITONAL images...
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        private struct TiffBitonalUncompressed
        {
            // Constructor...
            public TiffBitonalUncompressed(uint a_u32Width, uint a_u32Height, uint a_u32Resolution, uint a_u32Size)
            {
                // Header...
                u16ByteOrder = 0x4949;
                u16Version = 42;
                u32OffsetFirstIFD = 8;

                // First IFD...
                u16IFD = 16;

                // Tags...
                tifftagNewSubFileType = new TiffTag(254, 4, 1, 0);
                tifftagSubFileType = new TiffTag(255, 3, 1, 1);
                tifftagImageWidth = new TiffTag(256, 4, 1, a_u32Width);
                tifftagImageLength = new TiffTag(257, 4, 1, a_u32Height);
                tifftagBitsPerSample = new TiffTag(258, 3, 1, 1);
                tifftagCompression = new TiffTag(259, 3, 1, 1);
                tifftagPhotometricInterpretation = new TiffTag(262, 3, 1, 1);
                tifftagFillOrder = new TiffTag(266, 3, 1, 1);
                tifftagStripOffsets = new TiffTag(273, 4, 1, 222);
                tifftagSamplesPerPixel = new TiffTag(277, 3, 1, 1);
                tifftagRowsPerStrip = new TiffTag(278, 4, 1, a_u32Height);
                tifftagStripByteCounts = new TiffTag(279, 4, 1, a_u32Size);
                tifftagXResolution = new TiffTag(282, 5, 1, 206);
                tifftagYResolution = new TiffTag(283, 5, 1, 214);
                tifftagT4T6Options = new TiffTag(292, 4, 1, 0);
                tifftagResolutionUnit = new TiffTag(296, 3, 1, 2);

                // Footer...
                u32NextIFD = 0;
                u64XResolution = (ulong)0x100000000 + (ulong)a_u32Resolution;
                u64YResolution = (ulong)0x100000000 + (ulong)a_u32Resolution;
            }

            // Header...
            public ushort u16ByteOrder;
            public ushort u16Version;
            public uint u32OffsetFirstIFD;

            // First IFD...
            public ushort u16IFD;

            // Tags...
            public TiffTag tifftagNewSubFileType;
            public TiffTag tifftagSubFileType;
            public TiffTag tifftagImageWidth;
            public TiffTag tifftagImageLength;
            public TiffTag tifftagBitsPerSample;
            public TiffTag tifftagCompression;
            public TiffTag tifftagPhotometricInterpretation;
            public TiffTag tifftagFillOrder;
            public TiffTag tifftagStripOffsets;
            public TiffTag tifftagSamplesPerPixel;
            public TiffTag tifftagRowsPerStrip;
            public TiffTag tifftagStripByteCounts;
            public TiffTag tifftagXResolution;
            public TiffTag tifftagYResolution;
            public TiffTag tifftagT4T6Options;
            public TiffTag tifftagResolutionUnit;

            // Footer...
            public uint u32NextIFD;
            public ulong u64XResolution;
            public ulong u64YResolution;
        }

        // TIFF header for Group4 BITONAL images...
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        private struct TiffBitonalG4
        {
            // Constructor...
            public TiffBitonalG4(uint a_u32Width, uint a_u32Height, uint a_u32Resolution, uint a_u32Size)
            {
                // Header...
                u16ByteOrder = 0x4949;
                u16Version = 42;
                u32OffsetFirstIFD = 8;

                // First IFD...
                u16IFD = 16;

                // Tags...
                tifftagNewSubFileType = new TiffTag(254, 4, 1, 0);
                tifftagSubFileType = new TiffTag(255, 3, 1, 1);
                tifftagImageWidth = new TiffTag(256, 4, 1, a_u32Width);
                tifftagImageLength = new TiffTag(257, 4, 1, a_u32Height);
                tifftagBitsPerSample = new TiffTag(258, 3, 1, 1);
                tifftagCompression = new TiffTag(259, 3, 1, 4);
                tifftagPhotometricInterpretation = new TiffTag(262, 3, 1, 0);
                tifftagFillOrder = new TiffTag(266, 3, 1, 1);
                tifftagStripOffsets = new TiffTag(273, 4, 1, 222);
                tifftagSamplesPerPixel = new TiffTag(277, 3, 1, 1);
                tifftagRowsPerStrip = new TiffTag(278, 4, 1, a_u32Height);
                tifftagStripByteCounts = new TiffTag(279, 4, 1, a_u32Size);
                tifftagXResolution = new TiffTag(282, 5, 1, 206);
                tifftagYResolution = new TiffTag(283, 5, 1, 214);
                tifftagT4T6Options = new TiffTag(293, 4, 1, 0);
                tifftagResolutionUnit = new TiffTag(296, 3, 1, 2);

                // Footer...
                u32NextIFD = 0;
                u64XResolution = (ulong)0x100000000 + (ulong)a_u32Resolution;
                u64YResolution = (ulong)0x100000000 + (ulong)a_u32Resolution;
            }

            // Header...
            public ushort u16ByteOrder;
            public ushort u16Version;
            public uint u32OffsetFirstIFD;

            // First IFD...
            public ushort u16IFD;

            // Tags...
            public TiffTag tifftagNewSubFileType;
            public TiffTag tifftagSubFileType;
            public TiffTag tifftagImageWidth;
            public TiffTag tifftagImageLength;
            public TiffTag tifftagBitsPerSample;
            public TiffTag tifftagCompression;
            public TiffTag tifftagPhotometricInterpretation;
            public TiffTag tifftagFillOrder;
            public TiffTag tifftagStripOffsets;
            public TiffTag tifftagSamplesPerPixel;
            public TiffTag tifftagRowsPerStrip;
            public TiffTag tifftagStripByteCounts;
            public TiffTag tifftagXResolution;
            public TiffTag tifftagYResolution;
            public TiffTag tifftagT4T6Options;
            public TiffTag tifftagResolutionUnit;

            // Footer...
            public uint u32NextIFD;
            public ulong u64XResolution;
            public ulong u64YResolution;
        }

        // TIFF header for Uncompressed GRAYSCALE images...
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        private struct TiffGrayscaleUncompressed
        {
            // Constructor...
            public TiffGrayscaleUncompressed(uint a_u32Width, uint a_u32Height, uint a_u32Resolution, uint a_u32Size)
            {
                // Header...
                u16ByteOrder = 0x4949;
                u16Version = 42;
                u32OffsetFirstIFD = 8;

                // First IFD...
                u16IFD = 14;

                // Tags...
                tifftagNewSubFileType = new TiffTag(254, 4, 1, 0);
                tifftagSubFileType = new TiffTag(255, 3, 1, 1);
                tifftagImageWidth = new TiffTag(256, 4, 1, a_u32Width);
                tifftagImageLength = new TiffTag(257, 4, 1, a_u32Height);
                tifftagBitsPerSample = new TiffTag(258, 3, 1, 8);
                tifftagCompression = new TiffTag(259, 3, 1, 1);
                tifftagPhotometricInterpretation = new TiffTag(262, 3, 1, 1);
                tifftagStripOffsets = new TiffTag(273, 4, 1, 198);
                tifftagSamplesPerPixel = new TiffTag(277, 3, 1, 1);
                tifftagRowsPerStrip = new TiffTag(278, 4, 1, a_u32Height);
                tifftagStripByteCounts = new TiffTag(279, 4, 1, a_u32Size);
                tifftagXResolution = new TiffTag(282, 5, 1, 182);
                tifftagYResolution = new TiffTag(283, 5, 1, 190);
                tifftagResolutionUnit = new TiffTag(296, 3, 1, 2);

                // Footer...
                u32NextIFD = 0;
                u64XResolution = (ulong)0x100000000 + (ulong)a_u32Resolution;
                u64YResolution = (ulong)0x100000000 + (ulong)a_u32Resolution;
            }

            // Header...
            public ushort u16ByteOrder;
            public ushort u16Version;
            public uint u32OffsetFirstIFD;

            // First IFD...
            public ushort u16IFD;

            // Tags...
            public TiffTag tifftagNewSubFileType;
            public TiffTag tifftagSubFileType;
            public TiffTag tifftagImageWidth;
            public TiffTag tifftagImageLength;
            public TiffTag tifftagBitsPerSample;
            public TiffTag tifftagCompression;
            public TiffTag tifftagPhotometricInterpretation;
            public TiffTag tifftagStripOffsets;
            public TiffTag tifftagSamplesPerPixel;
            public TiffTag tifftagRowsPerStrip;
            public TiffTag tifftagStripByteCounts;
            public TiffTag tifftagXResolution;
            public TiffTag tifftagYResolution;
            public TiffTag tifftagResolutionUnit;

            // Footer...
            public uint u32NextIFD;
            public ulong u64XResolution;
            public ulong u64YResolution;
        }

        // TIFF header for Uncompressed COLOR images...
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        private struct TiffColorUncompressed
        {
            // Constructor...
            public TiffColorUncompressed(uint a_u32Width, uint a_u32Height, uint a_u32Resolution, uint a_u32Size)
            {
                // Header...
                u16ByteOrder = 0x4949;
                u16Version = 42;
                u32OffsetFirstIFD = 8;

                // First IFD...
                u16IFD = 14;

                // Tags...
                tifftagNewSubFileType = new TiffTag(254, 4, 1, 0);
                tifftagSubFileType = new TiffTag(255, 3, 1, 1);
                tifftagImageWidth = new TiffTag(256, 4, 1, a_u32Width);
                tifftagImageLength = new TiffTag(257, 4, 1, a_u32Height);
                tifftagBitsPerSample = new TiffTag(258, 3, 3, 182);
                tifftagCompression = new TiffTag(259, 3, 1, 1);
                tifftagPhotometricInterpretation = new TiffTag(262, 3, 1, 2);
                tifftagStripOffsets = new TiffTag(273, 4, 1, 204);
                tifftagSamplesPerPixel = new TiffTag(277, 3, 1, 3);
                tifftagRowsPerStrip = new TiffTag(278, 4, 1, a_u32Height);
                tifftagStripByteCounts = new TiffTag(279, 4, 1, a_u32Size);
                tifftagXResolution = new TiffTag(282, 5, 1, 188);
                tifftagYResolution = new TiffTag(283, 5, 1, 196);
                tifftagResolutionUnit = new TiffTag(296, 3, 1, 2);

                // Footer...
                u32NextIFD = 0;
                u16XBitsPerSample1 = 8;
                u16XBitsPerSample2 = 8;
                u16XBitsPerSample3 = 8;
                u64XResolution = (ulong)0x100000000 + (ulong)a_u32Resolution;
                u64YResolution = (ulong)0x100000000 + (ulong)a_u32Resolution;
            }

            // Header...
            public ushort u16ByteOrder;
            public ushort u16Version;
            public uint u32OffsetFirstIFD;

            // First IFD...
            public ushort u16IFD;

            // Tags...
            public TiffTag tifftagNewSubFileType;
            public TiffTag tifftagSubFileType;
            public TiffTag tifftagImageWidth;
            public TiffTag tifftagImageLength;
            public TiffTag tifftagBitsPerSample;
            public TiffTag tifftagCompression;
            public TiffTag tifftagPhotometricInterpretation;
            public TiffTag tifftagStripOffsets;
            public TiffTag tifftagSamplesPerPixel;
            public TiffTag tifftagRowsPerStrip;
            public TiffTag tifftagStripByteCounts;
            public TiffTag tifftagXResolution;
            public TiffTag tifftagYResolution;
            public TiffTag tifftagResolutionUnit;

            // Footer...
            public uint u32NextIFD;
            public ushort u16XBitsPerSample1;
            public ushort u16XBitsPerSample2;
            public ushort u16XBitsPerSample3;
            public ulong u64XResolution;
            public ulong u64YResolution;
        }

        #endregion

    }
}
