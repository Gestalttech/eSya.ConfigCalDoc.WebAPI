using System;
using System.Collections.Generic;

namespace eSya.ConfigCalDoc.DL.Entities
{
    public partial class GtDncnm
    {
        public int DocumentId { get; set; }
        public string ShortDesc { get; set; } = null!;
        public string DocumentDesc { get; set; } = null!;
        public string DocumentType { get; set; } = null!;
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
