using DM.Business.Interface;
using DM.Model.Blob;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManager.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IBlobTokenAuthBusiness _blobTokenAuthBusiness;
        public TokenController(IBlobTokenAuthBusiness blobTokenAuthBusiness)
        {
            this._blobTokenAuthBusiness = blobTokenAuthBusiness;
        }
        [HttpPost]
        public async Task<IActionResult> Generate([FromBody]BlobTokenRequsetModel blobToken)
        {
            try
            {
                var response = await this._blobTokenAuthBusiness.GenerateToken(blobToken);
                if (response != null) 
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
