using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Model.Blob
{
    public class BlobAuthTokenResponseModel
    {
        public string Token { get; set; }
        public string GuidCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpireOn { get; set; }
    }
}
