// pdfras_tool  error.cpp

///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) 2017 TWAIN Working Group
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

#include <string>
#include <iostream>

#include <pdfrasread.h>

#include "error.h"

using std::ostream;

error::error() {
	m_line = 0;
	m_pdfrt_error_code = OK;
	m_ReadErrorCode = READ_OK;
}

error::error(pdfrt_error_code err, const char *function, const char *file, int line) {
	m_pdfrt_error_code = err;
	m_ReadErrorCode = READ_OK;

	m_function = function;
	m_file = file;
	m_line = line;
}

error::error(ReadErrorCode err, const char *function, const char *file, int line) {
	m_pdfrt_error_code = OK;
	m_ReadErrorCode = err;

	m_function = function;
	m_file = file;
	m_line = line;
}

bool error::operator () (void) const {  // get
	return ((m_ReadErrorCode != READ_OK) || (m_pdfrt_error_code != OK)) ? true : false;
}

const char *error::get_error_string(void) const {
	const char *str;

	switch (m_pdfrt_error_code) {
	case OK: str = ""; break;
	case CLI_ARGS_INVALID: str = "command line argument(s) not valid"; break;
	case FILE_NOT_READABLE: str = "input file does not exist or is not readable"; break;
	case FILE_OPEN_APPEND_FAIL: str = "failed to open file for appending"; break;
	case PDFRAS_READER_CREATE_FAIL: str = "unable to create pdfras_reader handle"; break;
	case FILE_NOT_PDF_RASTER: str = "input file is not a PDF/raster file"; break;
	case PDFRAS_READER_OPEN_FAIL: str = "fail to open pdfras_reader handle"; break;
	case PDFRAS_READER_CLOSE_FAIL: str = "fail to close pdfras_reader handle"; break;
	case PDFRAS_READER_PAGE_COUNT_FAIL: str = "fail to parse page count in PDF/raster file"; break;
	case PDFRAS_READER_PAGE_OPTION_TOO_BIG: str = "-p option greater than the number of pages in PDF/raster file"; break;
	case PDFRAS_READER_PAGE_PIXEL_FORMAT_FAIL: str = "fail to parse page pixel format in PDF/raster file"; break;
	case FILE_CLOSE_FAIL: str = "failed to close file"; break;
	case PDFRAS_READER_PAGE_BITS_PER_COMPONENT_FAIL: str = "page bits per component invalid"; break;
	case PDFRAS_READER_PAGE_WIDTH_FAIL: str = "page width invalid"; break;
	case PDFRAS_READER_PAGE_HEIGHT_FAIL: str = "page height invalid"; break;
	case PDFRAS_READER_PAGE_STRIP_COUNT_FAIL: str = "page strip count invalid"; break;
	case PDFRAS_READER_PAGE_MAX_STRIP_SIZE_FAIL: str = "page maximum strip size invalid"; break;
	case PDFRAS_READER_PAGE_COMPRESSION_FAIL: str = "page compression invalid"; break;
	case FILE_WRITE_FAIL: str = "failed writing to file"; break;
	case FILE_OPEN_READ_FAIL: str = "failed to open file for reading"; break;
	case FILE_OPEN_WRITE_FAIL: str = "failed to open file for writing"; break;
	default: str = "unknown error"; break;
	}

	return str;
}

ostream& operator << (ostream &os, const error &err) {
	os << err.get_error_string();
	return os;
}