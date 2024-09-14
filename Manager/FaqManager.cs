using Microsoft.Identity.Client;
using Microsoft.Office.SharePoint.Tools;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System.Security;
using TestCsom.Manager.Interfaces;
using TestCsom.Secure._365_Auth;

namespace TestCsom.Manager
{
    public class FaqManager:IFaqManager
    {
        private readonly ClientContext context;
        private readonly IConfiguration configuration;
        private readonly MicrosoftAuth microsoftAuth;
        public FaqManager(IConfiguration configuration, MicrosoftAuth microsoftAuth)
        {
            this.configuration = configuration;
            this.context = new ClientContext(configuration["SharepointInfo:SiteUrl"]);
            this.microsoftAuth=microsoftAuth;
        }

        public async Task<dynamic> GetAllData()
        {
            try
            {
                string tk = await microsoftAuth.GetAccessTokenAsync();
                context.ExecutingWebRequest += (sender, args) =>
                {
                    args.WebRequestExecutor.RequestHeaders["Authorization"] =
                        "Bearer " + tk;
                };

                List list = context.Web.Lists.GetByTitle("FAQ");

                
            }
            catch (Exception ex)
            {
            }
        }









        
    }
}
