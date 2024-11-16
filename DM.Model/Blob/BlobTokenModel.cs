using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Model.Blob
{
    public class BlobTokenModel
    {
        public string FileName {get; set;}
	    public string AliasName {get; set;}
	    public string FileType {get; set;}
	    public string FileSize {get; set;}
	    public DateTime UploadTime {get; set;}
        public bool IsUploaded {get; set;}
	    public string FilePath  {get; set;}
	    public bool IsDeleted {get; set;}
        public Guid GuidCode { get; set; }
    }
}
