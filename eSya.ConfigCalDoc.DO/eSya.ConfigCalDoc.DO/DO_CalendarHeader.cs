using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigCalDoc.DO
{
    public class DO_CalendarHeader
    {
        public string CalenderType { get; set; }
        public int Year { get; set; }
        public int StartMonth { get; set; }
        public string? CalenderKey { get; set; } 
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
    //public class DO_CalendarDetails
    //{
    //    public string CalenderKey { get; set; }
    //    public int MonthId { get; set; }
    //    public DateTime FromDate { get; set; }
    //    public DateTime TillDate { get; set; }
    //    public bool ActiveStatus { get; set; }
    //    public string FormID { get; set; }
    //    public int UserID { get; set; }
    //    public string TerminalID { get; set; }
    //}
}
