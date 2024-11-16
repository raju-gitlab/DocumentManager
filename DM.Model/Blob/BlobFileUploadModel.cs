namespace DM.Model.Blob
{
    public class BlobFileUploadModel
    {
        public string FileName { get; set; }
        public string AliasName { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        public string UploadTime { get; set; }
        public string IsUploaded { get; set; }
        public string FilePath { get; set; }
        public string IsDeleted { get; set; }
        public string GuidCode { get; set; }
        public string UserId { get; set; }
    }
}
