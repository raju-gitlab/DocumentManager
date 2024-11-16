using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using CommonApplicationFramework.Common;
using CommonApplicationFramework.ConfigurationHandling;
using CommonApplicationFramework.DataHandling;
using CommonApplicationFramework.ExceptionHandling;
using CommonApplicationFramework.Logging;
using DM.Model.Blob;
using DM.Repository.Interface;
using Flutter.Utility;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Repository.Class
{
    public class BlobTokenAuthRepository : SessionManager,IBlobTokenAuthRepository
    {
        private DBManager dbManager;
        public async Task<BlobCredentialsModel> GetAzureConnectionString(BlobTokenRequsetModel blobToken)
        {
            BlobCredentialsModel response = new BlobCredentialsModel();
            try
            {
                using (dbManager = new DBManager())
                {
                    dbManager.GenerateConnectionString(ConnectionId);
                    dbManager.Open();
                    try
                    {
                        dbManager.CreateParameters(1);
                        dbManager.AddParameters(0, "@GuidCode", blobToken.UserId);
                        DataTable dt = new DataTable();
                        dt.Load(dbManager.ExecuteReader(System.Data.CommandType.Text, QueryConfig.FlutterQueryQuerySettings["GetConnectionString"].ToString()));
                        if (dt.Rows.Count > 0)
                        {
                            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                            response = dt.AsEnumerable().Select(n => new BlobCredentialsModel
                            {
                                AccountKey = n["AccountKey"].ToString(),
                                AccountName = n["AccountName"].ToString(),
                                ContainerName = n["ContainerName"].ToString(),
                                DefaultConnectionProtocol = n["DefaultConnectionProtocol"].ToString(),
                                EndpointSuffix = n["EndpointSuffix"].ToString(),
                                Request = blobToken
                            }).FirstOrDefault();
                            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        }
                    }
                    catch (Exception SqlException)
                    {
                        LogManager.Log(SqlException);
                        throw new RepositoryException("ACTIONFOUNDFAILED", MessageConfig.MessageSettings["ACTIONFOUNDFAILED"].ToString(), SqlException.StackTrace);
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

            return response;
        }

        public async Task<BlobAuthTokenResponseModel> GenerateToken(BlobCredentialsModel token)
        {
            BlobAuthTokenResponseModel response = new BlobAuthTokenResponseModel();
            try
            {
                string ConnectionString = $"DefaultEndpointsProtocol={token.DefaultConnectionProtocol};AccountName={token.AccountName};AccountKey={token.AccountKey};EndpointSuffix={token.EndpointSuffix}";
                string ContainerName = token.ContainerName;
                BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
                DateTime ExpireTime = DateTime.UtcNow.AddMinutes(10);
                Guid SaasTokenGUIDCode = Guid.NewGuid();
                BlobSasBuilder sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = ContainerName,
                    Resource = "c",
                    ExpiresOn = ExpireTime,
                };
                int a = 1;
                if (token.Request.TokenType == 1)
                {
                    sasBuilder.SetPermissions(BlobContainerSasPermissions.List | BlobContainerSasPermissions.Read);
                }
                else {
                    sasBuilder.SetPermissions(BlobContainerSasPermissions.Write);
                }

                Uri sasUri = containerClient.GenerateSasUri(sasBuilder);
                string SaasQuery = sasUri.Query;

                if (!string.IsNullOrEmpty(SaasQuery))
                {
                    using (dbManager = new DBManager())
                    {
                        dbManager.GenerateConnectionString(ConnectionId);
                        dbManager.Open();
                        try
                        {
                            dbManager.CreateParameters(4);
                            dbManager.AddParameters(0, "@UserId", token.Request.UserId);
                            dbManager.AddParameters(1, "@TokenString", SaasQuery);
                            dbManager.AddParameters(2, "@Permissions", (a == 1) ? "read and list" : "Write");
                            dbManager.AddParameters(3, "@GuidCode", SaasTokenGUIDCode);
                            dbManager.ExecuteNonQuery(CommandType.Text, QueryConfig.FlutterQueryQuerySettings["AddAuditLog"].ToString());
                        }
                        catch (Exception SqlException)
                        {
                            LogManager.Log(SqlException);
                        }
                    }
                }

                response = new BlobAuthTokenResponseModel()
                {
                    Token = SaasQuery,
                    ExpireOn = ExpireTime,
                    CreatedOn = DateTime.UtcNow,
                    GuidCode = SaasTokenGUIDCode.ToString()
                };
                
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

            return response;
        }
    }
}
