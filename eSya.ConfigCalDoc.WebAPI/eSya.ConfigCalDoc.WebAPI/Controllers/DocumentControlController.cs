using eSya.ConfigCalDoc.DO;
using eSya.ConfigCalDoc.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigCalDoc.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentControlController : ControllerBase
    {
        private readonly IDocumentControlRepository _documentControlRepository;

        public DocumentControlController(IDocumentControlRepository documentControlRepository)
        {
            _documentControlRepository = documentControlRepository;
        }
        #region Document Control Master

        /// <summary>
        /// Getting Document Control List.
        /// UI Reffered - Document Control Grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDocumentControlMaster()
        {
            var ds = await _documentControlRepository.GetDocumentControlMaster();
            return Ok(ds);
        }

        /// <summary>
        /// Insert or Update Document Control .
        /// UI Reffered -Document Control
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDocumentControlMaster(DO_DocumentControlMaster obj)
        {
            var msg = await _documentControlRepository.AddOrUpdateDocumentControlMaster(obj);
            return Ok(msg);

        }

        /// <summary>
        /// Activate Or De Activate
        /// UI Reffered - Document Control 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveDocumentControlMaster(int DocumentId, bool status)
        {
            var ds = await _documentControlRepository.ActiveOrDeActiveDocumentControlMaster(DocumentId, status);
            return Ok(ds);
        }
        #endregion

        #region Document Control Standard
        /// <summary>
        /// Getting Active Document Control List.
        /// UI Reffered - Document Control 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetActiveDocumentControlMaster()
        {
            var ds = await _documentControlRepository.GetActiveDocumentControlMaster();
            return Ok(ds);
        }
        /// <summary>
        /// Getting Document Control List.
        /// UI Reffered - Document Control Grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDocumentControlStandardbyDocumentId(int DocumentId)
        {
            var ds = await _documentControlRepository.GetDocumentControlStandardbyDocumentId(DocumentId);
            return Ok(ds);
        }

        /// <summary>
        /// Insert or Update Document Control .
        /// UI Reffered -Document Control
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDocumentControlStandard(DO_DocumentControlStandard obj)
        {
            var msg = await _documentControlRepository.AddOrUpdateDocumentControlStandard(obj);
            return Ok(msg);

        }
        /// <summary>
        /// Activate or D-Activate
        /// UI Reffered - Document Control Grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveDocumentStandardControl(bool status, int documentId, int ComboId)
        {
            var ds = await _documentControlRepository.ActiveOrDeActiveDocumentStandardControl(status, documentId, ComboId);
            return Ok(ds);
        }
        #endregion

        #region Form Document Link
        /// <summary>
        /// Getting Forms (IsDocumentControl) List.
        /// UI Reffered - Document Control -> Forms Tree
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetFormsForDocumentControl()
        {
            var ds = await _documentControlRepository.GetFormsForDocumentControl();
            return Ok(ds);
        }

        /// <summary>
        /// Getting Forms-Document Link .
        /// UI Reffered - Document Control -> Documents Grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetFormDocumentlink(int formID)
        {
            var ds = await _documentControlRepository.GetFormDocumentlink(formID);
            return Ok(ds);
        }

        /// <summary>
        /// Update Form-Document Links .
        /// UI Reffered - Document Control
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateFormDocumentLinks(List<DO_FormDocumentLink> obj)
        {
            var msg = await _documentControlRepository.UpdateFormDocumentLinks(obj);
            return Ok(msg);

        }
        #endregion

        #region Document Link with Form
        /// <summary>
        /// Docuemt  List.
        /// UI Reffered - Document Control -> Docuemnt Tree
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetActiveDocumentControls()
        {
            var ds = await _documentControlRepository.GetActiveDocumentControls();
            return Ok(ds);
        }

        /// <summary>
        /// Getting Getting Forms (IsDocumentControl=true) .
        /// UI Reffered - Document Control -> Form Grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDocumentFormlink(int documentID)
        {
            var ds = await _documentControlRepository.GetDocumentFormlink(documentID);
            return Ok(ds);
        }

        /// <summary>
        /// Update Document-Form Links .
        /// UI Reffered - Document Control
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateDocumentFormlink(List<DO_FormDocumentLink> obj)
        {
            var msg = await _documentControlRepository.UpdateDocumentFormlink(obj);
            return Ok(msg);

        }
        #endregion
    }
}
