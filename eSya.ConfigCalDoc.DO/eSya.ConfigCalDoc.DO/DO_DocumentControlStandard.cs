using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigCalDoc.DO
{
    public class DO_DocumentControlStandard
    {
        public int DocumentId { get; set; }
        public string GeneLogic { get; set; } 
        public string CalendarType { get; set; }
        public bool IsTransationMode { get; set; }
        public bool IsStoreCode { get; set; }
        public bool IsPaymentMode { get; set; }
        public string SchemaId { get; set; } 
        public int ComboId { get; set; }
        public string? DocumentDesc { get; set; } 
        public string? ShortDesc { get; set; }
        public string? DocumentType { get; set; } 
        public bool UsageStatus { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public int Isadd { get; set; }
    }
}
