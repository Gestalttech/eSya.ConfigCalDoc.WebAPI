using eSya.ConfigCalDoc.DO;
using eSya.ConfigCalDoc.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigCalDoc.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalendarControlController : ControllerBase
    {
        private readonly ICalendarControlRepository _calendarControlRepository;

        public CalendarControlController(ICalendarControlRepository calendarControlRepository)
        {
            _calendarControlRepository = calendarControlRepository;
        }
        /// <summary>
        /// Get Calendar Headers
        /// UI Reffered - Calendar Headers, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCalendarHeaders()
        {
            var cities = await _calendarControlRepository.GetCalendarHeaders();
            return Ok(cities);
        }

        /// <summary>
        /// Insert into Calendar Headers
        /// UI Reffered - Calendar Headers,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertCalendarHeader(DO_CalendarHeader obj)
        {
            var msg = await _calendarControlRepository.InsertCalendarHeader(obj);
            return Ok(msg);
        }
    }
}
