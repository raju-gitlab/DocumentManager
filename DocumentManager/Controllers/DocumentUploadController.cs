using DM.Business.Interface;
using DM.Model.Blob;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManager.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentUploadController : ControllerBase
    {
        private readonly IDocumentUploadBusiness _documentUploadBusiness;
        public DocumentUploadController(IDocumentUploadBusiness documentUploadBusiness)
        {
            this._documentUploadBusiness = documentUploadBusiness;
        }
        [HttpPost]
        public async Task<IActionResult> Upload([FromBody]BlobFileUploadModel fileUpload)
        {
            bool result = await this._documentUploadBusiness.Upload(fileUpload);
            if (result) 
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
