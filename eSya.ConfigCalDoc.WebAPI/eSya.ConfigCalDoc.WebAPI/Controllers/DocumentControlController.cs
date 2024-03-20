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

        #region Document Master

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
        public async Task<IActionResult> AddOrUpdateDocumentControl(DO_DocumentControlMaster obj)
        {
            var msg = await _documentControlRepository.AddOrUpdateDocumentControl(obj);
            return Ok(msg);

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
