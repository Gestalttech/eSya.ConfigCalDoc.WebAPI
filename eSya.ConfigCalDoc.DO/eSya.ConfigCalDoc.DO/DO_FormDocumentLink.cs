﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigCalDoc.DO
{
    public class DO_FormDocumentLink
    {
        public int FormId { get; set; }
        public string? FormName { get; set; }
        public int DocumentId { get; set; }
        public string? DocumentName { get; set; }
        public bool ActiveStatus { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
    public class DO_Forms
    {
        public int FormID { get; set; }
        public string FormName { get; set; }
        public string FormCode { get; set; }
        public bool ActiveStatus { get; set; }

    }
}