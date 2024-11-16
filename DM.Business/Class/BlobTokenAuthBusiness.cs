using DM.Business.Interface;
using DM.Model.Blob;
using DM.Repository.Interface;
using Flutter.Utility;

namespace DM.Business.Class
{
    public class BlobTokenAuthBusiness : SessionManager, IBlobTokenAuthBusiness
    {
        private readonly IBlobTokenAuthRepository _blobTokenAuthRepository;
        public BlobTokenAuthBusiness(IBlobTokenAuthRepository blobTokenAuthRepository)
        {
            this._blobTokenAuthRepository = blobTokenAuthRepository;
        }
        public async Task<BlobAuthTokenResponseModel> GenerateToken(BlobTokenRequsetModel token)
        {
            var response = await this._blobTokenAuthRepository.GetAzureConnectionString(token);
            if (response != null) 
            {
                return await this._blobTokenAuthRepository.GenerateToken(response);
            }
            return default;
        }
    }
}
