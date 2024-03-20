using System;
using System.Collections.Generic;

namespace eSya.ConfigCalDoc.DL.Entities
{
    public partial class GtDccnst
    {
        public int DocumentId { get; set; }
        public string DocumentDesc { get; set; } = null!;
        public string ShortDesc { get; set; } = null!;
        public string DocumentType { get; set; } = null!;
        public string SchemaId { get; set; } = null!;
        public bool UsageStatus { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
