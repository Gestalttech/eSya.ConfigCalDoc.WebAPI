using eSya.ConfigCalDoc.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigCalDoc.IF
{
    public interface ICalendarControlRepository
    {
        Task<List<DO_CalendarHeader>> GetCalendarHeaders();
        Task<DO_ReturnParameter> InsertCalendarHeader(DO_CalendarHeader obj);
    }
}
