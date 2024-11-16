using DM.Model.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Repository.Interface
{
    public interface IBlobTokenAuthRepository
    {
        Task<BlobCredentialsModel> GetAzureConnectionString(BlobTokenRequsetModel UserId);
        Task<BlobAuthTokenResponseModel> GenerateToken(BlobCredentialsModel token);
    }
}
