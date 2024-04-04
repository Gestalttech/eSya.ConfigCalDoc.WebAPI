using eSya.ConfigCalDoc.DL.Entities;
using eSya.ConfigCalDoc.DO;
using eSya.ConfigCalDoc.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigCalDoc.DL.Repository
{
    public class CalendarControlRepository: ICalendarControlRepository
    {
        private readonly IStringLocalizer<CalendarControlRepository> _localizer;
        public CalendarControlRepository(IStringLocalizer<CalendarControlRepository> localizer)
        {
            _localizer = localizer;
        }
        public async Task<List<DO_CalendarHeader>> GetCalendarHeaders()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var result = await db.GtEcclcos

                                 .Select(c => new DO_CalendarHeader
                                 {
                                     CalenderType = c.CalenderType,
                                     Year = c.Year,
                                     StartMonth=c.StartMonth,
                                     CalenderKey = c.CalenderKey,
                                     FromDate = c.FromDate,
                                     TillDate = c.TillDate,
                                     ActiveStatus = c.ActiveStatus
                                 }).OrderByDescending(x => x.Year).ToListAsync();
                   
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertCalendarHeader(DO_CalendarHeader obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region Calendar Header
                        var _finyearExist = db.GtEcclcos.Where(x => x.Year == obj.Year && x.StartMonth==obj.StartMonth && x.CalenderType.ToUpper().Replace(" ", "") == obj.CalenderType.ToUpper().Replace(" ", "")).FirstOrDefault();

                        if (_finyearExist == null)
                        {
                            GtEcclco _chead = new GtEcclco()
                            {
                                CalenderType = obj.CalenderType,
                                Year = Convert.ToInt32(obj.Year),
                                StartMonth=obj.StartMonth,
                                CalenderKey = (obj.CalenderType + obj.Year + obj.StartMonth).ToString(),
                                FromDate = obj.FromDate,
                                TillDate = obj.TillDate,
                                //YearEndStatus = false,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEcclcos.Add(_chead);
                            await db.SaveChangesAsync();
                            #endregion
                            #region
                            //#region Insrert into Calendar details

                            //List<DO_CalendarDetails> lstmonth = new List<DO_CalendarDetails>();

                            //for (int i = obj.FromDate.Month; i <= obj.TillDate.Month; i++)
                            //{
                            //    var dt = new DO_CalendarDetails();

                            //    //dt.FromDate = new DateTime(obj.FromDate.Month);

                            //    dt.CalenderKey = (obj.CalenderType + obj.Year + obj.StartMonth).ToString();

                            //    var firstday = new DateTime(obj.FromDate.Year, obj.FromDate.Month, 1);
                            //    var lastday = firstday.AddMonths(1).AddDays(-1);

                            //    dt.FromDate = firstday;
                            //    dt.TillDate = lastday;

                            //    //obj.FromDate=obj.FromDate.Date.AddDays(-(obj.FromDate.Date.Day - 1));
                            //    //obj.TillDate = obj.FromDate.AddMonths(1).AddTicks(-1);

                            //    dt.MonthId=(i.ToString().Length == 1) ? Convert.ToInt32(obj.Year.ToString() + "" + 0 + i.ToString())
                            //        : Convert.ToInt32(obj.Year.ToString() + "" + i.ToString());
                            //    lstmonth.Add(dt);

                            //    //if (i.ToString().Length == 1)
                            //    //{
                            //    //    string strMonth = 0 + i.ToString();
                            //    //    dt.MonthId = Convert.ToInt32(obj.Year.ToString() + "" + strMonth);
                            //    //}
                            //    //else
                            //    //{
                            //    //    dt.MonthId = Convert.ToInt32(obj.Year.ToString() + "" + i.ToString());

                            //    //}
                            //    //lstmonth.Add(dt);
                            //}
                            //    //List<int> MonthIds = new List<int>();
                            //    //string months;
                            //    //var financeyear = Convert.ToInt32(obj.Year);

                            //    //for (int i = obj.FromDate.Month; i <= obj.TillDate.Month; i++)
                            //    //{
                            //    //    if (i.ToString().Length == 1)
                            //    //    {
                            //    //        string strMonth = 0 + i.ToString();
                            //    //        months = financeyear.ToString() + "" + strMonth;
                            //    //    }
                            //    //    else
                            //    //    {
                            //    //        months = financeyear.ToString() + "" + i.ToString();

                            //    //    }

                            //    //    MonthIds.Add(Convert.ToInt32(months));
                            //    //}

                            //    GtEccldt cdetails = new GtEccldt();

                            //foreach (var month in lstmonth)
                            //{
                            //    var patIdExists = db.GtEccldts.Where(x => x.CalenderKey.ToUpper().Replace(" ", "") == month.CalenderKey.ToUpper().Replace(" ", "") && x.MonthId==month.MonthId).FirstOrDefault();

                            //    if (patIdExists != null)
                            //    {
                            //        return new DO_ReturnParameter() { Status = false, StatusCode = "W00169", Message = string.Format(_localizer[name: "W00169"]) };

                            //    }
                            //    else
                            //    {
                            //        cdetails.CalenderKey = month.CalenderKey;
                            //        cdetails.MonthId = month.MonthId;
                            //        cdetails.FromDate = month.FromDate;
                            //        cdetails.TillDate = month.TillDate;
                            //        cdetails.ActiveStatus = obj.ActiveStatus;
                            //        cdetails.FormId = obj.FormID;
                            //        cdetails.CreatedBy = obj.UserID;
                            //        cdetails.CreatedOn = DateTime.Now;
                            //        cdetails.CreatedTerminal = obj.TerminalID;
                            //        db.GtEccldts.Add(cdetails);
                            //        await db.SaveChangesAsync();
                            //    }
                            //}

                            //#endregion
                            #endregion

                            dbContext.Commit();

                          return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0054", Message = string.Format(_localizer[name: "W0054"]) };
                        }
                    }

                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
