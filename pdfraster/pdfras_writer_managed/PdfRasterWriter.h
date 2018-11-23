// PdfRasterWriter.h

#pragma once

using namespace System;
using namespace System::Collections::Generic;

namespace PdfRasterWriter {

	public ref class Writer
	{
		///////////////////////////////////////////////////////////////////////////////
		// Public Definitions: PdfRasterWriter
		///////////////////////////////////////////////////////////////////////////////
#pragma region Public Definitions: PdfRasterWriter
	public:
		value struct PdfRasterConst
		{
			literal int PDFRASWR_API_LEVEL = PDFRAS_API_LEVEL;
			literal String^ PDFRASWR_LIBRARY_VERSION = PDFRAS_LIBRARY_VERSION;
		};

		// Pixel Formats
		enum struct PdfRasterPixelFormat
		{
			PDFRASWR_BITONAL = PDFRAS_BITONAL,				//  1-bit per pixel, 0=black
			PDFRASWR_GRAYSCALE = PDFRAS_GRAY8,				//  8-bit per pixel, 0=black
			PDFRASWR_GRAYSCALE16 = PDFRAS_GRAY16,			// 16-bit per pixel, 0=black
			PDFRASWR_RGB = PDFRAS_RGB24,					// 24-bit per pixel, sRGB
			PDFRASWR_RGB48 = PDFRAS_RGB48,					// 48-bit per pixel
		};

		// Compression Modes
		enum struct PdfRasterCompression
		{
			PDFRASWR_UNCOMPRESSED = PDFRAS_UNCOMPRESSED,	// uncompressed (/Filter null)
			PDFRASWR_JPEG = PDFRAS_JPEG,					// JPEG baseline (DCTDecode)
			PDFRASWR_CCITTG4 = PDFRAS_CCITTG4,				// CCITT Group 4 (CCITTFaxDecode)
		};

		// Permissions
		enum struct PdfRasterPermissions
		{
			PDFRASWR_PERM_PRINT_DOCUMENT = PDFRAS_PERM_PRINT_DOCUMENT,
			PDFRASWR_PERM_MODIFY_DOCUMENT = PDFRAS_PERM_MODIFY_DOCUMENT,
			PDFRASWR_PERM_COPY_FROM_DOCUMENT = PDFRAS_PERM_COPY_FROM_DOCUMENT,
			PDFRASWR_PERM_EDIT_ANNOTS = PDFRAS_PERM_EDIT_ANNOTS,
			PDFRASWR_PERM_FILL_FORMS = PDFRAS_PERM_FILL_FORMS,
			PDFRASWR_PERM_ACCESSIBILITY = PDFRAS_PERM_ACCESSIBILITY,
			PDFRASWR_PERM_ASSEMBLE_DOCUMENT = PDFRAS_PERM_ASSEMBLE_DOCUMENT,
			PDFRASWR_PERM_HIGH_PRINT = PDFRAS_PERM_HIGH_PRINT,
            PDFRASWR_PERM_ALL = PDFRAS_PERM_ALL
		};

        value struct PdfRasterPubSecRecipient
        {
            String^ public_key;
            PdfRasterPermissions perms;

        };
#pragma endregion Public Definitions for PdfRasterWriter

        ///////////////////////////////////////////////////////////////////////////////
        // Public Methods: PdfRasterWriter
        ///////////////////////////////////////////////////////////////////////////////
#pragma region Public Methods: PdfRasterWriter
	public:
		int  encoder_create(int apiLevel, String^ pdfFileName);
        int  encoder_create_digitally_signed(int apiLevel, String^ pdfFileName, String^ pfxFile, String^ password);
        void encoder_set_RC4_40_encrypter(int idx, String^ user_password, String^ owner_password, PdfRasterPermissions perms, pdbool metadata);
        void encoder_set_RC4_128_encrypter(int idx, String^ user_password, String^ owner_password, PdfRasterPermissions perms, pdbool metadata);
        void encoder_set_AES128_encrypter(int idx, String^ user_password, String^ owner_password, PdfRasterPermissions perms, pdbool metadata);
        void encoder_set_AES256_encrypter(int idx, String^ user_password, String^ owner_password, PdfRasterPermissions perms, pdbool metadata);
        void encoder_set_pubsec_RC4_128_encrypter(int idx, List<PdfRasterPubSecRecipient>^ recipients, pdbool metadata);
        void encoder_set_pubsec_AES128_encrypter(int idx, List<PdfRasterPubSecRecipient>^ recipients, pdbool metadata);
        void encoder_set_pubsec_AES256_encrypter(int idx, List<PdfRasterPubSecRecipient>^ recipients, pdbool metadata);
		void encoder_set_creator(int enc, String^ creator);
		void encoder_set_resolution(int enc, double xdpi, double ydpi);
		void encoder_set_pixelformat(int enc, PdfRasterPixelFormat format);
		void encoder_set_compression(int enc, PdfRasterCompression compression);
		void encoder_start_page(int enc, int width);
		void encoder_write_page_xmp(int idx, String^ xmpdata);
		void encoder_write_document_xmp(int idx, String^ xmpdata);
		void encoder_write_strip(int enc, int rows, array<unsigned char>^ buf, unsigned offset, unsigned len);
		void encoder_end_page(int enc);
		void encoder_end_document(int enc);
		void digital_signature_set_name(int enc, String^ name);
        void digital_signature_set_reason(int enc, String^ reason);
        void digital_signature_set_location(int enc, String^ location);
        void digital_signature_set_contactinfo(int enc, String^ contact);
        void encoder_destroy(int enc);

#pragma endregion Public Methods for PdfRasterWriter
	};
}
