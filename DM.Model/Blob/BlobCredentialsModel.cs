namespace DM.Model.Blob
{
    public class BlobCredentialsModel
    {
	    public string AccountName {get; set;}
        public string AccountKey {get; set;}
        public string DefaultConnectionProtocol {get; set;}
        public string EndpointSuffix {get; set;}
        public string ContainerName {get; set;}
	    public int CreatedBy {get; set;}
	    public DateTime CreatedOn {get; set;}
        public int ModifiedBy {get; set;}
	    public DateTime ModifiedOn {get; set;}
        public bool IsActive {get; set;}
	    public bool IsDeleted {get; set;}
        public Guid GuidCode {get; set;}
        public BlobTokenRequsetModel Request { get;set;}
    }
}
