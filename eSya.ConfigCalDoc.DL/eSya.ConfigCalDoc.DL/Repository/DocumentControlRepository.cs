using eSya.ConfigCalDoc.DL.Entities;
using eSya.ConfigCalDoc.DO;
using eSya.ConfigCalDoc.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigCalDoc.DL.Repository
{
    public class DocumentControlRepository: IDocumentControlRepository
    {
        private readonly IStringLocalizer<DocumentControlRepository> _localizer;
        public DocumentControlRepository(IStringLocalizer<DocumentControlRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Document Control Master
        public async Task<List<DO_DocumentControlMaster>> GetDocumentControlMaster()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var result = db.GtDncnms
                        .Select(s => new DO_DocumentControlMaster
                        {
                            DocumentId = s.DocumentId,
                            ShortDesc = s.ShortDesc,
                            DocumentDesc = s.DocumentDesc,
                            DocumentType = s.DocumentType,
                            ActiveStatus = s.ActiveStatus,
                        }).OrderBy(x=>x.DocumentId).ToListAsync();

                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> AddOrUpdateDocumentControlMaster(DO_DocumentControlMaster obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (obj.Isadd == 1)
                        {
                            var _docIdExists = db.GtDncnms.Where(w => w.DocumentId == obj.DocumentId).Count();
                            if (_docIdExists > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0055", Message = string.Format(_localizer[name: "W0055"]) };
                            }
                            var _descshort = db.GtDncnms.Where(w => w.ShortDesc.ToUpper().Replace(" ", "") == obj.ShortDesc.ToUpper().Replace(" ", "")).Count();
                            if (_descshort > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0057", Message = string.Format(_localizer[name: "W0057"]) };
                            }
                            var _desc = db.GtDncnms.Where(w => w.DocumentDesc.ToUpper().Replace(" ", "") == obj.DocumentDesc.ToUpper().Replace(" ", "")).Count();
                            if (_desc > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0056", Message = string.Format(_localizer[name: "W0056"]) };
                            }
                            var _ctrlmaster = new GtDncnm
                            {
                                DocumentId = obj.DocumentId,
                                ShortDesc = obj.ShortDesc,
                                DocumentDesc = obj.DocumentDesc,
                                DocumentType = obj.DocumentType,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormId,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtDncnms.Add(_ctrlmaster);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }
                        else
                        {
                           
                            var _descshortExists = db.GtDncnms.Where(w => w.ShortDesc.ToUpper().Replace(" ", "") == obj.ShortDesc.ToUpper().Replace(" ", "")
                               && w.DocumentId != obj.DocumentId).Count();
                            if (_descshortExists > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0057", Message = string.Format(_localizer[name: "W0057"]) };
                            }
                            var _descExists = db.GtDncnms.Where(w => w.DocumentDesc.ToUpper().Replace(" ", "") == obj.DocumentDesc.ToUpper().Replace(" ", "")
                               && w.DocumentId != obj.DocumentId).Count();
                            if (_descExists > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0056", Message = string.Format(_localizer[name: "W0056"]) };
                            }
                            var ctrlstand = db.GtDncnms.Where(w => w.DocumentId == obj.DocumentId).FirstOrDefault();
                            if (ctrlstand != null)
                            {
                                ctrlstand.ShortDesc = obj.ShortDesc;
                                ctrlstand.DocumentDesc = obj.DocumentDesc;
                                ctrlstand.DocumentType = obj.DocumentType;
                                ctrlstand.ActiveStatus = obj.ActiveStatus;
                                ctrlstand.ModifiedBy = obj.UserID;
                                ctrlstand.ModifiedOn = System.DateTime.Now;
                                ctrlstand.ModifiedTerminal = obj.TerminalID;
                                await db.SaveChangesAsync();
                                dbContext.Commit();
                                return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                            }
                            else
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0115", Message = string.Format(_localizer[name: "W0115"]) };

                            }

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
        public async Task<DO_ReturnParameter> ActiveOrDeActiveDocumentControlMaster(int DocumentId,bool status)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        GtDncnm docmaster = db.GtDncnms.Where(d => d.DocumentId == DocumentId).FirstOrDefault();
                        if (docmaster == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0115", Message = string.Format(_localizer[name: "W0115"]) };
                        }

                        docmaster.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };



                    }


                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region Document Standard
        public async Task<List<DO_DocumentControlMaster>> GetActiveDocumentControlMaster()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var result = db.GtDncnms.Where(x=>x.ActiveStatus)
                        .Select(s => new DO_DocumentControlMaster
                        {
                            DocumentId = s.DocumentId,
                            DocumentDesc = s.DocumentDesc,
                        }).OrderBy(x=>x.DocumentDesc).ToListAsync();

                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_DocumentControlStandard>> GetDocumentControlStandardbyDocumentId(int DocumentId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    
                    var result = db.GtDccnsts.Where(x=>x.DocumentId==DocumentId)
                        .Join(db.GtDncnms,
                         ds => ds.DocumentId,
                         dm => dm.DocumentId,
                        (ds, dm) => new { ds, dm })
                        .Select(
                        s => new DO_DocumentControlStandard
                        {
                            DocumentId = s.ds.DocumentId,
                            ComboId = s.ds.ComboId,
                            GeneLogic =s.ds.GeneLogic,
                            CalendarType=s.ds.CalendarType,
                            IsTransationMode=s.ds.IsTransationMode,
                            IsStoreCode=s.ds.IsStoreCode,
                            IsPaymentMode=s.ds.IsPaymentMode,
                            SchemaId=s.ds.SchemaId,
                            UsageStatus = s.ds.UsageStatus,
                            ActiveStatus = s.ds.ActiveStatus,
                            DocumentDesc = s.dm.DocumentDesc,
                            ShortDesc = s.dm.ShortDesc,
                            DocumentType = s.dm.DocumentType,
                            
                        }).ToListAsync();

                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> AddOrUpdateDocumentControlStandard(DO_DocumentControlStandard obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (obj.Isadd == 1)
                        {


                            //int maxcomboId= db.GtDccnsts.Where(x=>x.DocumentId== obj.DocumentId).Select(x=>x.ComboId).DefaultIfEmpty().Max()+1;

                            int maxcomboId = db.GtDccnsts.Select(x => x.ComboId).DefaultIfEmpty().Max() + 1;
                            var _documentcontrol = new GtDccnst
                                {
                                    DocumentId = obj.DocumentId,
                                    ComboId = maxcomboId,
                                    GeneLogic = obj.GeneLogic,
                                    CalendarType = obj.CalendarType,
                                    IsTransationMode = obj.IsTransationMode,
                                    IsStoreCode = obj.IsStoreCode,
                                    IsPaymentMode = obj.IsPaymentMode,
                                    SchemaId = obj.SchemaId,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId = obj.FormId,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtDccnsts.Add(_documentcontrol);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }
                        else
                        {
                            var ctrlstand = db.GtDccnsts.Where(w => w.DocumentId == obj.DocumentId && w.ComboId==obj.ComboId).FirstOrDefault();
                            if (ctrlstand != null)
                            {
                                ctrlstand.GeneLogic = obj.GeneLogic;
                                ctrlstand.CalendarType = obj.CalendarType;
                                ctrlstand.IsTransationMode = obj.IsTransationMode;
                                ctrlstand.IsStoreCode = obj.IsStoreCode;
                                ctrlstand.IsPaymentMode = obj.IsPaymentMode;
                                ctrlstand.SchemaId = obj.SchemaId;
                                ctrlstand.UsageStatus = obj.UsageStatus;
                                ctrlstand.ActiveStatus = obj.ActiveStatus;
                                ctrlstand.ModifiedBy = obj.UserID;
                                ctrlstand.ModifiedOn = System.DateTime.Now;
                                ctrlstand.ModifiedTerminal = obj.TerminalID;
                                await db.SaveChangesAsync();
                                dbContext.Commit();
                                return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                            }
                            else
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0115", Message = string.Format(_localizer[name: "W0115"]) };

                            }

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
        public async Task<DO_ReturnParameter> ActiveOrDeActiveDocumentStandardControl(bool status, int documentId, int ComboId)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        GtDccnst docstandard = db.GtDccnsts.Where(d => d.DocumentId == documentId && d.ComboId== ComboId).FirstOrDefault();
                        if (docstandard == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0115", Message = string.Format(_localizer[name: "W0115"]) };
                        }

                        docstandard.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };



                    }


                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region Form Document Link
        public async Task<List<DO_Forms>> GetFormsForDocumentControl()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = await db.GtEcfmfds.Where(x => x.ActiveStatus == true)
                        .Join(db.GtEcfmpas.Where(x => x.ParameterId == 2),
                        f => f.FormId,
                        p => p.FormId,
                        (f, p) => new { f, p })
                      .Select(x => new DO_Forms
                      {
                          FormID = x.f.FormId,
                          FormName = x.f.FormName,
                          FormCode = x.f.FormCode,
                          ActiveStatus = x.f.ActiveStatus
                      }).ToListAsync();
                    var Distinctforms = ds.GroupBy(x => x.FormID).Select(y => y.First());
                    return Distinctforms.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_FormDocumentLink>> GetFormDocumentlink(int formID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {



                    var ds = await db.GtDncnms.Where(x => x.ActiveStatus == true)
                   .GroupJoin(db.GtDncnfds.Where(w => w.FormId == formID),
                     d => d.DocumentId,
                     l => l.DocumentId,
                    (emp, depts) => new { emp, depts })
                   .SelectMany(z => z.depts.DefaultIfEmpty(),
                    (a, b) => new DO_FormDocumentLink
                    {
                        FormId = formID,
                        DocumentId = a.emp.DocumentId,
                        DocumentName = a.emp.DocumentDesc,
                        ActiveStatus = b == null ? false : b.ActiveStatus
                    }).ToListAsync();

                    var Distinctdocuments = ds.GroupBy(x => x.DocumentId).Select(y => y.First());
                    return Distinctdocuments.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> UpdateFormDocumentLinks(List<DO_FormDocumentLink> obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var _link in obj)
                        {
                            var LinkExist = db.GtDncnfds.Where(w => w.FormId == _link.FormId && w.DocumentId == _link.DocumentId).FirstOrDefault();
                            if (LinkExist != null)
                            {
                                if (_link.ActiveStatus != LinkExist.ActiveStatus)
                                {
                                    LinkExist.ActiveStatus = _link.ActiveStatus;
                                    LinkExist.ModifiedBy = _link.UserID;
                                    LinkExist.ModifiedOn = System.DateTime.Now;
                                    LinkExist.ModifiedTerminal = _link.TerminalID;
                                }
                            }
                            else
                            {
                                if (_link.ActiveStatus)
                                {
                                    var formdoclink = new GtDncnfd
                                    {
                                        FormId = _link.FormId,
                                        DocumentId = _link.DocumentId,
                                        ActiveStatus = _link.ActiveStatus,

                                        CreatedBy = _link.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = _link.TerminalID
                                    };
                                    db.GtDncnfds.Add(formdoclink);
                                }

                            }
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }
        #endregion

        #region  Document Link with Form
        public async Task<List<DO_FormDocumentLink>> GetActiveDocumentControls()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = await db.GtDncnms.Where(w => w.ActiveStatus == true).Select(x => new DO_FormDocumentLink
                    {
                        DocumentId = x.DocumentId,
                        DocumentName = x.DocumentDesc,
                    }).ToListAsync();
                    return ds;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_FormDocumentLink>> GetDocumentFormlink(int documentID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = await db.GtEcfmfds.Where(x => x.ActiveStatus == true)
                        .Join(db.GtEcfmpas.Where(x => x.ParameterId == 2),
                        f => f.FormId,
                        p => p.FormId,
                        (f, p) => new { f, p })
                   .GroupJoin(db.GtDncnfds.Where(w => w.DocumentId == documentID),
                     d => d.f.FormId,
                     l => l.FormId,
                    (form, doc) => new { form, doc })
                   .SelectMany(z => z.doc.DefaultIfEmpty(),
                    (a, b) => new DO_FormDocumentLink
                    {
                        DocumentId = documentID,
                        FormId = a.form.f.FormId,
                        FormName = a.form.f.FormName,
                        ActiveStatus = b == null ? false : b.ActiveStatus
                    }).ToListAsync();

                    var Distinctform = ds.GroupBy(x => x.FormId).Select(y => y.First());
                    return Distinctform.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> UpdateDocumentFormlink(List<DO_FormDocumentLink> obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var _link in obj)
                        {
                            var LinkExist = db.GtDncnfds.Where(w => w.FormId == _link.FormId && w.DocumentId == _link.DocumentId).FirstOrDefault();
                            if (LinkExist != null)
                            {
                                if (_link.ActiveStatus != LinkExist.ActiveStatus)
                                {
                                    LinkExist.ActiveStatus = _link.ActiveStatus;
                                    LinkExist.ModifiedBy = _link.UserID;
                                    LinkExist.ModifiedOn = System.DateTime.Now;
                                    LinkExist.ModifiedTerminal = _link.TerminalID;
                                }
                            }
                            else
                            {
                                if (_link.ActiveStatus)
                                {
                                    var formdoclink = new GtDncnfd
                                    {
                                        FormId = _link.FormId,
                                        DocumentId = _link.DocumentId,
                                        ActiveStatus = _link.ActiveStatus,

                                        CreatedBy = _link.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = _link.TerminalID
                                    };
                                    db.GtDncnfds.Add(formdoclink);
                                }

                            }
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }
        #endregion
    }
}
