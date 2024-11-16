using CommonApplicationFramework.ConfigurationHandling;
using CommonApplicationFramework.DataHandling;
using CommonApplicationFramework.ExceptionHandling;
using CommonApplicationFramework.Logging;
using DM.Model.Blob;
using DM.Repository.Interface;
using Flutter.Utility;
using System.Data;


namespace DM.Repository.Class
{
    public class DocumentUploadRepository : SessionManager, IDocumentUploadRepository
    {
        private DBManager dbManager;
        public async Task<bool> Upload(BlobFileUploadModel fileUpload)
        {
            try
            {
                using (dbManager = new DBManager())
                {
                    dbManager.GenerateConnectionString(ConnectionId);
                    dbManager.Open();
                    try
                    {
                        dbManager.CreateParameters(9);
                        dbManager.AddParameters(0, "@UserId", fileUpload.UserId);
                        dbManager.AddParameters(1, "@FileName", fileUpload.FileName);
                        dbManager.AddParameters(2, "@FileSize", fileUpload.FileSize);
                        dbManager.AddParameters(3, "@AliasName", fileUpload.AliasName);
                        dbManager.AddParameters(4, "@FileType", fileUpload.FileType);
                        dbManager.AddParameters(5, "@UploadTime", fileUpload.UploadTime);
                        dbManager.AddParameters(6, "@IsUploaded", fileUpload.IsUploaded);
                        dbManager.AddParameters(7, "@FilePath", fileUpload.FilePath);
                        dbManager.AddParameters(8, "@GuidCode", fileUpload.GuidCode);
                        int result = dbManager.ExecuteNonQuery(CommandType.Text, QueryConfig.FlutterQueryQuerySettings["InsertFileUploadDetails"].ToString());
                        if (result == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (Exception SqlException)
                    {
                        LogManager.Log(SqlException);
                    }
                }

            }
            catch (RepositoryException sqlEx)
            {
                LogManager.Log(sqlEx);
                throw new RepositoryException("ACTIONFOUNDFAILED", sqlEx.ErrorMessage, sqlEx.ErrorCode);
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                throw new RepositoryException("ACTIONFOUNDFAILED", MessageConfig.MessageSettings["ACTIONFOUNDFAILED"].ToString(), ex.StackTrace);
            }

            return false;
        }
    }
}
