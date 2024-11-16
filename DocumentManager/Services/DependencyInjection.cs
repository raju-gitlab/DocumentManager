
using DM.Business.Class;
using DM.Business.Interface;
using DM.Repository.Class;
using DM.Repository.Interface;

namespace DocumentManager.Services
{
    public static class DependencyInjection
    {
        public static void Resolve(this IServiceCollection services)
        {
            services.AddScoped<IBlobTokenAuthBusiness, BlobTokenAuthBusiness>();
            services.AddScoped<IBlobTokenAuthRepository, BlobTokenAuthRepository>();

            services.AddScoped<IDocumentUploadBusiness, DocumentUploadBusiness>();
            services.AddScoped<IDocumentUploadRepository, DocumentUploadRepository>();
        }
    }
}
