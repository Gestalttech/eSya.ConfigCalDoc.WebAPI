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
    public class DocumentControlRepository: IDocumentControlRepository
    {
        private readonly IStringLocalizer<DocumentControlRepository> _localizer;
        public DocumentControlRepository(IStringLocalizer<DocumentControlRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Document Master
        public async Task<List<DO_DocumentControlMaster>> GetDocumentControlMaster()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var result = db.GtDccnsts.Select(
                        s => new DO_DocumentControlMaster
                        {
                            DocumentId = s.DocumentId,
                            DocumentDesc = s.DocumentDesc,
                            ShortDesc = s.ShortDesc,
                            SchemaId = s.SchemaId,
                            DocumentType = s.DocumentType,
                            UsageStatus = s.UsageStatus,
                            ActiveStatus = s.ActiveStatus
                        }).ToListAsync();

                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> AddOrUpdateDocumentControl(DO_DocumentControlMaster obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (obj.Isadd == 1)
                        {
                            var RecordExist = db.GtDccnsts.Where(w => w.DocumentId == obj.DocumentId || w.DocumentDesc == obj.DocumentDesc || w.ShortDesc == obj.ShortDesc).FirstOrDefault();
                            if (RecordExist != null)
                            {
                                if (RecordExist.DocumentId == obj.DocumentId)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0055", Message = string.Format(_localizer[name: "W0055"]) };
                                }
                                else if (RecordExist.DocumentDesc == obj.DocumentDesc)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0056", Message = string.Format(_localizer[name: "W0056"]) };
                                }
                                else if (RecordExist.ShortDesc == obj.ShortDesc)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0057", Message = string.Format(_localizer[name: "W0057"]) };
                                }

                            }
                            else
                            {
                                var _documentcontrol = new GtDccnst
                                {
                                    DocumentId = obj.DocumentId,
                                    DocumentDesc = obj.DocumentDesc,
                                    ShortDesc = obj.ShortDesc,
                                    DocumentType = obj.DocumentType,
                                    SchemaId = obj.SchemaId,
                                    UsageStatus=obj.UsageStatus,
                                    ActiveStatus = true,
                                    FormId = obj.FormId,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtDccnsts.Add(_documentcontrol);
                            }
                        }
                        else
                        {
                            var updatedDocumentControl = db.GtDccnsts.Where(w => w.DocumentId == obj.DocumentId).FirstOrDefault();
                            if (updatedDocumentControl.DocumentDesc != obj.DocumentDesc)
                            {
                                var RecordExist = db.GtDccnsts.Where(w => w.DocumentDesc == obj.DocumentDesc).Count();
                                if (RecordExist > 0)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0056", Message = string.Format(_localizer[name: "W0056"]) };
                                }
                            }
                            if (updatedDocumentControl.ShortDesc != obj.ShortDesc)
                            {
                                var RecordExist = db.GtDccnsts.Where(w => w.ShortDesc == obj.ShortDesc).Count();
                                if (RecordExist > 0)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0057", Message = string.Format(_localizer[name: "W0057"]) };
                                }
                            }

                            updatedDocumentControl.DocumentDesc = obj.DocumentDesc;
                            updatedDocumentControl.ShortDesc = obj.ShortDesc;
                            updatedDocumentControl.DocumentType = obj.DocumentType;
                            updatedDocumentControl.SchemaId = obj.SchemaId;
                            updatedDocumentControl.UsageStatus = obj.UsageStatus;
                            updatedDocumentControl.ActiveStatus = obj.ActiveStatus;
                            updatedDocumentControl.ModifiedBy = obj.UserID;
                            updatedDocumentControl.ModifiedOn = System.DateTime.Now;
                            updatedDocumentControl.ModifiedTerminal = obj.TerminalID;

                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

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



                    var ds = await db.GtDccnsts.Where(x => x.ActiveStatus == true)
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

                    var ds = await db.GtDccnsts.Where(w => w.ActiveStatus == true).Select(x => new DO_FormDocumentLink
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
