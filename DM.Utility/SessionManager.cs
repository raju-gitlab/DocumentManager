using CommonApplicationFramework.Caching;
using CommonApplicationFramework.Common;
using CommonApplicationFramework.ConfigurationHandling;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Flutter.Utility
{
    public class SessionManager
    {
        public string Code { get; set; }
        public string RequestId { get; set; }
        public string ConnectionId { get; set; }
        public UserContext Context { get; set; }

        public SessionManager()
        {
            IHttpContextAccessor _contextAccessor = new HttpContextAccessor();

            try
            {
                Code = Convert.ToString(_contextAccessor.HttpContext.Request.Headers["ModuleCode"]);
                RequestId = Convert.ToString(_contextAccessor.HttpContext.Request.Headers["RequestId"]);
                ConnectionId = !string.IsNullOrEmpty(Convert.ToString(_contextAccessor.HttpContext.Request.Headers["ConnectionId"])) ? Convert.ToString(_contextAccessor.HttpContext.Request.Headers["ConnectionId"]) : Environments.Configurations.Settings.Find(x => x.Key.ToString().Equals("ConnectionId")).Value.ToString();
                if (GlobalCacheManager.Instance.Get("usercontext_" + Convert.ToString(_contextAccessor.HttpContext.Request.Headers["RequestId"])) != null)
                {
                    Context = JsonConvert.DeserializeObject<UserContext>(GlobalCacheManager.Instance.Get("usercontext_" + Convert.ToString(_contextAccessor.HttpContext.Request.Headers["RequestId"])));
                    if (Context == null)
                    {
                        Context = new UserContext();
                        Context.UserId = 5663;
                        Context.ApplicationType = new ItemCode()
                        {
                            Code = "FLUTTER"
                        };
                        Context.UserGuid = "FD5E0222-228E-4BB6-9C27-88AB02351F47";
                    }
                    else
                    {
                        Context = Context.InstanceList.Where(n => n.Code == this.ConnectionId).FirstOrDefault().TanentContext;
                    }
                }
                else
                {
                    Context = new UserContext();
                    Context.UserId = 225;
                    Context.ApplicationType = new ItemCode()
                    {
                        Code = "FLUTTER"
                    };
                    Context.UserGuid = "7CEE8980-5A2F-4186-87AC-FD157B2CF054";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ModuleCode or RequestId Cannot be null in API request Header");
            }

        }
    }
}
