namespace CSharpMiscLibrary.Services
{
    /// <summary>
    /// Common service for constants used in all projects.
    /// </summary>
    public class ConstantsService
    {
        /***********
         * Symbols
         ***********/
        /// <summary>
        /// Denmark country code.
        /// </summary>
        public const string SYM_COUNTRY_DENMARK = "DK";

        /// <summary>
        /// Logistics export symbol.
        /// </summary>
        public const string SYM_SERVICE_TYPE_EXPORT = "E";

        /// <summary>
        /// Logistics import symbol.
        /// </summary>
        public const string SYM_SERVICE_TYPE_IMPORT = "I";

        /// <summary>
        /// Logistics domestic symbol.
        /// </summary>
        public const string SYM_SERVICE_TYPE_DOMESTIC = "D";

        /***********
         * Datetime
         ***********/
        /// <summary>
        /// Local default inversed date format.
        /// </summary>
        public const string DATE_LOCAL_INV_DEFAULT = "yyyy-mm-dd";

        /// <summary>
        /// Local default date format.
        /// </summary>
        public const string DATE_LOCAL_DEFAULT = "DD-MM-YYYY";

        /// <summary>
        /// Local default time format.
        /// </summary>
        public const string TIME_LOCAL_DEFAULT = "HH:mm:ss";

        /// <summary>
        /// Simplified date time format.
        /// </summary>
        public const string DATE_TIME_FORMAT_SIMPLIFIED_WITHOUT_SECS = "dd-mm-yyyy hh:mm";

        /// <summary>
        /// Simplified inversed date time format.
        /// </summary>
        public const string DATE_TIME_INV_FORMAT_SIMPLIFIED_WITHOUT_SECS = "yyyy-mm-dd hh:mm";

        /***********
         * Currency
         ***********/
        /// <summary>
        /// Local default currency invoice. For DK: Danish crowns.
        /// </summary>
        public const string CURRENCY_INVOICE_LOCAL_DEFAULT = CURRENCY_CODE_DANISH_CROWNS;

        /// <summary>
        /// Danish crowns currency code.
        /// </summary>
        public const string CURRENCY_CODE_DANISH_CROWNS = "DKK";

        /// <summary>
        /// Euros currency code.
        /// </summary>
        public const string CURRENCY_CODE_EUROS = "EUR";

        /********************************************
         * Logistics Carrier Names and Abbreviations
         ********************************************/
        /// <summary>
        /// Carrier code for DHL.
        /// </summary>
        public const string CARRIER_CODE_DHL = "DHL";

        /// <summary>
        /// Carrier code for UPS.
        /// </summary>
        public const string CARRIER_CODE_UPS = "UPS";

        /// <summary>
        /// Carrier code for GLS.
        /// </summary>
        public const string CARRIER_CODE_GLS = "GLS";
        
        /// <summary>
        /// Carrier code for FedEx.
        /// </summary>
        public const string CARRIER_CODE_FEDEX = "FedEx";

        /*************
         * Extensions
         *************/
        /// <summary>
        /// Extension file code for only PDF without dots.
        /// </summary>
        public const string EXTENSION_ONLY_NAME_PDF = "pdf";

        /// <summary>
        /// Extension file code for PDF with dots.
        /// </summary>
        public const string EXTENSION_FULL_PDF = ".pdf";

        /// <summary>
        /// Extension file code for full PDF with dots and asterisk.
        /// </summary>
        public const string EXTENSION_FULL_REGEX_PDF = "*.pdf";

        /// <summary>
        /// Extension file code for only XLSX without dots.
        /// </summary>
        public const string EXTENSION_ONLY_NAME_XLSX = "xlsx";

        /// <summary>
        /// Extension file code for XLSX with dots.
        /// </summary>
        public const string EXTENSION_FULL_XLSX = ".xlsx";

        /// <summary>
        /// Extension file code for full XLSX with dots and asterisk.
        /// </summary>
        public const string EXTENSION_FULL_REGEX_XLSX = "*.xlsx";

        /// <summary>
        /// Extension file code for only XLS without dots.
        /// </summary>
        public const string EXTENSION_ONLY_NAME_XLS = "xls";

        /// <summary>
        /// Extension file code for XLS with dots.
        /// </summary>
        public const string EXTENSION_FULL_XLS = ".xls";

        /// <summary>
        /// Extension file code for full XLS with dots and asterisk.
        /// </summary>
        public const string EXTENSION_FULL_REGEX_XLS = "*.xls";

        /// <summary>
        /// Extension file code for XLSM macro with dots.
        /// </summary>
        public const string EXTENSION_MACRO_FILE = ".xlsm";

        /*************
         * Misc codes
         *************/
        /// <summary>
        /// Weight unit for kilos.
        /// </summary>
        public const string TOTAL_SHIP_TYPE_UNIT_KG = "KG";

        /// <summary>
        /// Simplified weight unit version for kilos.
        /// </summary>
        public const string TOTAL_SHIP_TYPE_UNIT_KG_SMALL = "K";

        /// <summary>
        /// Weight unit for pounds.
        /// </summary>
        public const string TOTAL_SHIP_TYPE_UNIT_LBS = "LBS";

        /// <summary>
        /// Simplified weight unit version for pounds.
        /// </summary>
        public const string TOTAL_SHIP_TYPE_UNIT_LBS_SMALL = "L";

        /// <summary>
        /// Simplified package type version for letters.
        /// </summary>
        public const string PACKAGE_TYPE_LETTER_SMALL = "LTR";

        /// <summary>
        /// Simplified package type version for documents.
        /// </summary>
        public const string PACKAGE_TYPE_DOC_SMALL = "DOC";

        /// <summary>
        /// Simplified package type version for unclassified documents.
        /// </summary>
        public const string PACKAGE_TYPE_UNCLASSIFIED_SMALL = "UNC";

        /// <summary>
        /// Package type version for documents.
        /// </summary>
        public const string PACKAGE_TYPE_DOCUMENT = "Document";

        /// <summary>
        /// Package type version for envelope.
        /// </summary>
        public const string PACKAGE_TYPE_ENVELOPE = "Envelope";

        /// <summary>
        /// Package type version for parcels.
        /// </summary>
        public const string PACKAGE_TYPE_PARCELS = "Parcels";

        /// <summary>
        /// Simplified chargeable weight version for gross type.
        /// </summary>
        public const string SHIP_CHRG_WEIGHT_TYPE_GROSS = "G";

        /// <summary>
        /// Simplified chargeable weight version for volumetric type.
        /// </summary>
        public const string SHIP_CHRG_WEIGHT_TYPE_VOLUMETRIC = "V";

        /*************
         * Misc names
         *************/
        /// <summary>
        /// Office software name for Excel.
        /// </summary>
        public const string NAME_SW_OFFICE_EXCEL = "Excel";

        /// <summary>
        /// Office software name for Word.
        /// </summary>
        public const string NAME_SW_OFFICE_WORD = "Word";

        /// <summary>
        /// Office software name for Powerpoint.
        /// </summary>
        public const string NAME_SW_OFFICE_POWERPOINT = "Powerpoint";

        /// <summary>
        /// Office software name for Outlook.
        /// </summary>
        public const string NAME_SW_OFFICE_OUTLOOK = "Outlook";

        /**************
         * HTTP Values
         **************/

        /// <summary>
        /// HTTP client timeout request of 1 hour in milliseconds.
        /// </summary>
        public const long HTTP_CLIENT_TIMEOUT_REQUEST_1_HOUR = 3600000; // 60 min

        /// <summary>
        /// HTTP client timeout request of 2 hours in milliseconds.
        /// </summary>
        public const long HTTP_CLIENT_TIMEOUT_REQUEST_2_HOURS = 7200000; // 120 min

        /// <summary>
        /// HTTP client method GET mapping symbol.
        /// </summary>
        public const string HTTP_CLIENT_REQUEST_METHOD_GET_NAME = "GET";

        /// <summary>
        /// HTTP client method POST mapping symbol.
        /// </summary>
        public const string HTTP_CLIENT_REQUEST_METHOD_POST_NAME = "POST";

        /// <summary>
        /// HTTP client method PUT mapping symbol.
        /// </summary>
        public const string HTTP_CLIENT_REQUEST_METHOD_PUT_NAME = "PUT";

        /// <summary>
        /// HTTP client method DELETE mapping symbol.
        /// </summary>
        public const string HTTP_CLIENT_REQUEST_METHOD_DELETE_NAME = "DELETE";
    }
}
