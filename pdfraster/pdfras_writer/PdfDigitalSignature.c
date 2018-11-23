// PdfDigitalSignature.c - function for digital siging of PDF/R

#include <assert.h>
#include <string.h>
#include <stdio.h>

#include "PdfDigitalSignature.h"
#include "PdfOS.h"
#include "PdfAlloc.h"
#include "PdfRaster.h"
#include "PdfStreaming.h"
#include "PdfDict.h"
#include "PdfArray.h"
#include "PdfString.h"
#include "PdfXrefTable.h"
#include "PdfDate.h"

#include "pdfras_digitalsignature.h"

// Need this to make C# happy...
typedef void X509_STORE;
struct t_digitalsignature {
	struct t_signer* signer;
	X509_STORE* cert_store;
};

#define BUFFER_SIZE 8192

typedef struct {
    pduint8* buffer;
    pduint32 bufferSize;
    pduint32 written;
} t_writer_data;

// data structure for digital signature
struct t_pdfdigitalsignature {
    t_pdmempool* pool;

    t_pdfrasencoder* encoder;
    fOutputWriter userWriter;
    void* userCookie;

    // writer data
    t_writer_data* data;

    // data used by pdfras_digitalsignature module
    t_digitalsignature* ds;

    // page containing signature
    t_pdvalue page;
    
    // name, location, reason and contact info
    char* name;
    char* location;
    char* reason;
    char* contact_info;

    // written to output
    pdbool written;

    pduint32 digsig_objnum;
};

// Initialization
t_pdfdigitalsignature* digitalsignature_create(t_pdfrasencoder* encoder, const char* pfx_file, const char* password) {
    assert(encoder);

    t_pdmempool* pool = pdfr_encoder_mempool(encoder);
    t_pdfdigitalsignature* digitalSignature = (t_pdfdigitalsignature*) pd_alloc(pool, sizeof(t_pdfdigitalsignature));
    if (!digitalSignature)
        return NULL;

    digitalSignature->ds = pdfr_init_digitalsignature();
    if (digitalSignature->ds == NULL) {
        pd_free(digitalSignature);
        return NULL;
    }

    pdbool result = pdfr_digitalsignature_create_signer(digitalSignature->ds, pfx_file, password);
    if (!result) {
        pdfr_exit_digitalsignature(digitalSignature->ds);
        pd_free(digitalSignature);
        return NULL;
    }

    digitalSignature->data = (t_writer_data*) pd_alloc(pool, sizeof(t_writer_data));
    
    digitalSignature->data->buffer = (pduint8*)pd_alloc(pool, sizeof(pduint8) * BUFFER_SIZE);
    if (!digitalSignature->data->buffer)
        return NULL;

    digitalSignature->data->bufferSize = BUFFER_SIZE;
    digitalSignature->data->written = 0;
    
    digitalSignature->pool = pool;
    digitalSignature->encoder = encoder;
    digitalSignature->userWriter = pdfr_encoder_set_outputwriter(encoder, digitalsignature_writer);
    digitalSignature->userCookie = pdfr_encoder_set_cookie(encoder, (void*) digitalSignature);

    digitalSignature->name = NULL;
    digitalSignature->location = NULL;
    digitalSignature->reason = NULL;
    digitalSignature->contact_info = NULL;

    digitalSignature->written = PD_FALSE;

    digitalSignature->digsig_objnum = 0;
    
    return digitalSignature;
}

static pduint8* find_string_in_binary(pduint8* data, const pduint32 data_len, const char* what) {
    size_t idx = 0;
    size_t what_len = strlen(what);

    while ((idx < (data_len - what_len))) {
        if (memcmp(data + idx, what, what_len) == 0) {
            return data + idx;
        }

        ++idx;
    }

    return NULL;
}

void digitalsignature_finish(t_pdfdigitalsignature* signature) {
    pduint32 offset1 = 0;
    pduint32 length1 = 0;
    pduint32 offset2 = 0;
    pduint32 length2 = 0;

    pduint8* contents = find_string_in_binary(signature->data->buffer, signature->data->written, "/Contents <");
    if (!contents)
        return;

    length1 = (pduint32)(contents - signature->data->buffer + 10);

    pduint8* p = signature->data->buffer + length1;
    offset2 = length1;
    while (*p != '>') {
        ++offset2;
        ++p;
    }

    offset2 += 1; 
    length2 = signature->data->written - offset2;

    p = find_string_in_binary(signature->data->buffer, signature->data->written, "/ByteRange [");
    if (!p)
        return;

    sprintf((char*) (p + 12), "%d %d %d %d", offset1, length1, offset2, length2);

    p += 12;
    while (*p != '\0') {
        ++p;
    }
    *p = ' ';
    while (*p != ']') {
        *p = ' ';
        ++p;
    }

    pduint8* buffer_to_sign = (pduint8*)pd_alloc(signature->pool, sizeof(pduint8) * (length1 + length2));
    memcpy(buffer_to_sign, signature->data->buffer + offset1, length1);
    memcpy(buffer_to_sign + length1, signature->data->buffer + offset2, length2);

    pduint32 signed_len_in_hex = offset2 - length1 - 2;
    pduint32 signed_len_in_bytes = 0;
    pduint8* signed_data = (pduint8*)pd_alloc(signature->pool, sizeof(pduint8) * signed_len_in_hex);
    pduint8* signed_data_hex = (pduint8*)pd_alloc(signature->pool, sizeof(pduint8) * (signed_len_in_hex + 1));
    memset(signed_data, 0, sizeof(pduint8) * signed_len_in_hex);
    memset(signed_data_hex, 0, sizeof(pduint8) * (signed_len_in_hex + 1));

    signed_len_in_bytes = pdfr_digsig_sign_data(signature->ds, buffer_to_sign, length1 + length2, signed_data, signed_len_in_hex);
    if (signed_len_in_bytes > 0) {
		pduint32 i;
        // converting to hex
        for (i = 0; i < signed_len_in_bytes; ++i) {
            sprintf((char*)&signed_data_hex[i * 2], "%02X", signed_data[i]);
        }

        p = signature->data->buffer + offset1 + length1 + 1;
        sprintf((char*)p, "%s", signed_data_hex);
        while (*p != '\0') {
            ++p;
        }
        *p = '0';
        while (*p != '>') {
            *p = '0';
            ++p;
        }
    }

    pd_free(buffer_to_sign);

    signature->userWriter(signature->data->buffer, 0, signature->data->written, signature->userCookie);
}

// Destroy
void digitalsignature_destroy(t_pdfdigitalsignature* signature) {
    assert(signature);

    pdfr_exit_digitalsignature(signature->ds);

    // restore user defined write callback and cookie
    pdfr_encoder_set_outputwriter(signature->encoder, signature->userWriter);
    pdfr_encoder_set_cookie(signature->encoder, signature->userCookie);

    // free allocated memmory
    if (signature->name != NULL)
        pd_free(signature->name);
    if (signature->location != NULL)
        pd_free(signature->location);
    if (signature->reason != NULL)
        pd_free(signature->reason);
    if (signature->contact_info != NULL)
        pd_free(signature->contact_info);
    if (signature->data->buffer != NULL)
        pd_free(signature->data->buffer);

    pd_free(signature);

    signature = NULL;
}

// Set Name of signer. Owner of buffer with name becomes library
void pdfr_digitalsignature_set_name(t_pdfdigitalsignature* signature, const char* name) {
    assert(signature);

    pdint32 len = pdstrlen(name);
    if (signature->name != NULL)
        pd_free(signature->name);

    if (len == 0)
        signature->name = NULL;

    signature->name = (char*) pd_alloc(signature->pool, sizeof(char) * (len + 1));
    memcpy(signature->name, name, len);
    signature->name[len] = '\0';
}

// Set reason for the signing.
void pdfr_digitalsignature_set_reason(t_pdfdigitalsignature* signature, const char* reason) {
    assert(signature);

    pdint32 len = pdstrlen(reason);
    if (signature->reason != NULL)
        pd_free(signature->reason);

    if (len == 0)
        signature->reason = NULL;

    signature->reason = (char*)pd_alloc(signature->pool, sizeof(char) * (len + 1));
    memcpy(signature->reason, reason, len);
    signature->reason[len] = '\0';
}

// Set location of signing
void pdfr_digitalsignature_set_location(t_pdfdigitalsignature* signature, const char* location) {
    assert(signature);

    pdint32 len = pdstrlen(location);
    if (signature->location != NULL)
        pd_free(signature->location);

    if (len == 0)
        signature->location = NULL;

    signature->location = (char*)pd_alloc(signature->pool, sizeof(char) * (len + 1));
    memcpy(signature->location, location, len);
    signature->location[len] = '\0';
}

// Set contact info
void pdfr_digitalsignature_set_contactinfo(t_pdfdigitalsignature* signature, const char* contactinfo) {
    assert(signature);

    pdint32 len = pdstrlen(contactinfo);
    if (signature->contact_info != NULL)
        pd_free(signature->contact_info);

    if (len == 0)
        signature->contact_info = NULL;

    signature->contact_info = (char*)pd_alloc(signature->pool, sizeof(char) * (len + 1));
    memcpy(signature->contact_info, contactinfo, len);
    signature->contact_info[len] = '\0';
}

// set page containing signature
void digitalsignature_set_page(t_pdfdigitalsignature* signature, t_pdvalue page) {
    assert(signature);
    
    if (!IS_NULL(page))
        signature->page = page;
}

// callback for writing data during digital signing
int digitalsignature_writer(const pduint8* data, pduint32 offset, pduint32 len, void* cookie) {
    assert(cookie);

    if (!data || !len)
        return 0;

    data += offset;

    t_pdfdigitalsignature* digitalSignature = (t_pdfdigitalsignature*) cookie;
    
    if ((digitalSignature->data->written + len) > digitalSignature->data->bufferSize) {
        pduint32 size = digitalSignature->data->written + len;
        pduint8* buf = (pduint8*)pd_alloc(digitalSignature->pool, sizeof(pduint8) * size);

        if (!buf)
            return 0;

        memcpy(buf, digitalSignature->data->buffer, digitalSignature->data->written);
        pd_free(digitalSignature->data->buffer);
        digitalSignature->data->buffer = buf;
        digitalSignature->data->bufferSize = size;
    }

    memcpy(digitalSignature->data->buffer + digitalSignature->data->written, data, len);
    digitalSignature->data->written += len;

    return len;
}

// Creates necessary dictionaries for signature
void digitalsignature_create_dictionaries(t_pdfdigitalsignature* signature) {
    pdbool succ;
    t_pdvalue* catalog = pdfr_encoder_catalog(signature->encoder);
    t_pdxref* xref = pdfr_encoder_xref(signature->encoder);

    // Create AcroForm entry in the Catalog
    t_pdvalue acro_form = pd_dict_get(*catalog, (t_pdatom)"AcroForm", &succ);
    if (!succ) {
        acro_form = pd_dict_new(signature->pool, 2);
        t_pdvalue acro_form_ref = pd_xref_makereference(xref, acro_form);
        pd_dict_put(*catalog, ((t_pdatom)"AcroForm"), acro_form_ref);
    }

    // Filling AcroForm
    pd_dict_put(acro_form, ((t_pdatom)"SigFlags"), pdintvalue(3));

    //Creating /Fields array
    t_pdvalue fields = pd_dict_get(*catalog, (t_pdatom)"Fields", &succ);
    if (!succ) {
        fields = pdarrayvalue(pd_array_new(signature->pool, 2));
        t_pdvalue fields_ref = pd_xref_makereference(xref, fields);
        pd_dict_put(acro_form, ((t_pdatom)"Fields"), fields_ref);
    }

    // Preparing Field and appending to the Fields array
    t_pdvalue signature_field = pd_dict_new(signature->pool, 5);
    t_pdvalue signature_field_ref = pd_xref_makereference(xref, signature_field);
    pd_array_add(fields.value.arrvalue, signature_field_ref);

    //Enter paramaters to the /Field
    char t[] = "Sig_1";
    pd_dict_put(signature_field, ((t_pdatom)"T"), pdstringvalue(pd_string_new(signature->pool, (pduint32) strlen(t), t)));
    pd_dict_put(signature_field, ((t_pdatom)"FT"), pdatomvalue((t_pdatom)"Sig"));
    pd_dict_put(signature_field, ((t_pdatom)"Type"), pdatomvalue((t_pdatom)"Annot"));
    pd_dict_put(signature_field, ((t_pdatom)"SubType"), pdatomvalue((t_pdatom)"Widget"));
    pd_dict_put(signature_field, ((t_pdatom)"F"), pdintvalue(132));

    t_pdarray *field_rect_arr = pd_array_new(signature->pool, 4);
    pd_array_add(field_rect_arr, pdintvalue(0));
    pd_array_add(field_rect_arr, pdintvalue(0));
    pd_array_add(field_rect_arr, pdintvalue(0));
    pd_array_add(field_rect_arr, pdintvalue(0));
    pd_dict_put(signature_field, ((t_pdatom)"Rect"), pdarrayvalue(field_rect_arr));

    pd_dict_put(signature_field, ((t_pdatom)"P"), signature->page);
  
    if (IS_REFERENCE(signature->page)) {
        t_pdvalue page_dict = pd_reference_get_value(signature->page);
        
        t_pdarray* annots_array = pd_array_new(signature->pool, 1);
        pd_array_add(annots_array, signature_field_ref);

        pd_dict_put(page_dict, ((t_pdatom) "Annots"), pdarrayvalue(annots_array));
    }
    
    // Preparing /V dictionary 
    char content[1024];
    memset(content, '0', 1024);

    // calculating lenght of the Content just to have accurate size 
    pdint32 signature_length = pdfr_digsig_signature_length(signature->ds);
    
    t_pdvalue v_dict = pd_dict_new(signature->pool, 3);
    pd_dict_put(v_dict, ((t_pdatom)"Contents"), pdstringvalue(pd_string_new_binary(signature->pool, signature_length, content)));
    pd_dict_put(v_dict, ((t_pdatom)"Type"), pdatomvalue((t_pdatom)"Sig"));
    pd_dict_put(v_dict, ((t_pdatom)"Filter"), pdatomvalue((t_pdatom)"Adobe.PPKLite"));
    pd_dict_put(v_dict, ((t_pdatom)"SubFilter"), pdatomvalue((t_pdatom)"adbe.pkcs7.detached"));

    if (signature->name)
        pd_dict_put(v_dict, ((t_pdatom) "Name"), pdstringvalue(pd_string_new(signature->pool, pdstrlen(signature->name), signature->name)));
    if (signature->reason)
        pd_dict_put(v_dict, ((t_pdatom) "Reason"), pdstringvalue(pd_string_new(signature->pool, pdstrlen(signature->reason), signature->reason)));
    if (signature->location)
        pd_dict_put(v_dict, ((t_pdatom) "Location"), pdstringvalue(pd_string_new(signature->pool, pdstrlen(signature->location), signature->location)));
    if (signature->contact_info)
        pd_dict_put(v_dict, ((t_pdatom) "ContactInfo"), pdstringvalue(pd_string_new(signature->pool, pdstrlen(signature->contact_info), signature->contact_info)));

    t_pdarray *byterange_arr = pd_array_new(signature->pool, 4);
    pd_array_add(byterange_arr, pdintvalue(2147483647));
    pd_array_add(byterange_arr, pdintvalue(2147483647));
    pd_array_add(byterange_arr, pdintvalue(2147483647));
    pd_array_add(byterange_arr, pdintvalue(2147483647));
    pd_dict_put(v_dict, ((t_pdatom)"ByteRange"), pdarrayvalue(byterange_arr));

    t_date* now = pd_date_create_current_localtime(signature->pool);
    char* m = pd_date_to_pdfstring(now);
    pd_dict_put(v_dict, ((t_pdatom)"M"), pdstringvalue(pd_string_new(signature->pool, pdstrlen(m), m)));
    pd_date_destroy(now);
    pd_free(m);

    // Insert V into the Filed dictionary
    t_pdvalue v_dict_ref = pd_xref_makereference(xref, v_dict);
    pd_dict_put(signature_field, ((t_pdatom)"V"), v_dict_ref);

    signature->digsig_objnum = pd_reference_object_number(v_dict_ref);

    signature->written = PD_TRUE;
}

pdbool digitalsignature_written(t_pdfdigitalsignature* signature) {
    return signature->written;
}

pduint32 pd_digitalsignature_digsig_objnum(t_pdfdigitalsignature* signature) {
    assert(signature);

    return signature->digsig_objnum;
}
