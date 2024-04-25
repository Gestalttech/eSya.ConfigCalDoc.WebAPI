using eSya.ConfigCalDoc.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigCalDoc.IF
{
    public interface IDocumentControlRepository
    {
        #region Document Control Master
        Task<List<DO_DocumentControlMaster>> GetDocumentControlMaster();
        Task<DO_ReturnParameter> AddOrUpdateDocumentControlMaster(DO_DocumentControlMaster obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveDocumentControlMaster(int DocumentId, bool status);
        #endregion

        #region Document Standard
        Task<List<DO_DocumentControlMaster>> GetActiveDocumentControlMaster();
        Task<List<DO_DocumentControlStandard>> GetDocumentControlStandardbyDocumentId(int DocumentId);
        Task<DO_ReturnParameter> AddOrUpdateDocumentControlStandard(DO_DocumentControlStandard obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveDocumentStandardControl(bool status, int documentId, int ComboId);
        #endregion

        #region Form Document Link
        Task<List<DO_Forms>> GetFormsForDocumentControl();
        Task<List<DO_FormDocumentLink>> GetFormDocumentlink(int formID);
        Task<DO_ReturnParameter> UpdateFormDocumentLinks(List<DO_FormDocumentLink> obj);
        #endregion

        #region  Document Link with Form
        Task<List<DO_FormDocumentLink>> GetActiveDocumentControls();
        Task<List<DO_FormDocumentLink>> GetDocumentFormlink(int documentID);
        Task<DO_ReturnParameter> UpdateDocumentFormlink(List<DO_FormDocumentLink> obj);
        #endregion
    }
}
