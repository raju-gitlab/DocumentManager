using DM.Business.Interface;
using DM.Model.Blob;
using DM.Repository.Interface;
using Flutter.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Business.Class
{
    public class DocumentUploadBusiness : SessionManager,IDocumentUploadBusiness
    {
        private readonly IDocumentUploadRepository _documentUploadRepository;
        public DocumentUploadBusiness(IDocumentUploadRepository documentUploadRepository)
        {
            this._documentUploadRepository = documentUploadRepository;
        }
        public async Task<bool> Upload(BlobFileUploadModel fileUpload)
        {
            return await this._documentUploadRepository.Upload(fileUpload);
        }
    }
}
