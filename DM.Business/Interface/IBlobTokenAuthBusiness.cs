using DM.Model.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Business.Interface
{
    public interface IBlobTokenAuthBusiness
    {
        Task<BlobAuthTokenResponseModel> GenerateToken(BlobTokenRequsetModel token);
    }
}
