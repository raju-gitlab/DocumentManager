
using DM.Model.Blob;

namespace DM.Repository.Interface
{
    public interface IDocumentUploadRepository
    {
        Task<bool> Upload(BlobFileUploadModel fileUpload);
    }
}
