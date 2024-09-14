using Microsoft.Identity.Client;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System.Security;
using TestCsom.Manager.Interfaces;

namespace TestCsom.Manager
{
    public class FaqManager:IFaqManager
    {
        private readonly ClientContext context;
        private readonly IConfiguration configuration;
        public FaqManager(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.context = new ClientContext(configuration["SharepointInfo:SiteUrl"]);
            context.ExecutingWebRequest += (sender, args) =>
            {
                args.WebRequestExecutor.RequestHeaders["Authorization"] =
                    "Bearer " + await GetAccessTokenAsync();
            };
        }

        public dynamic GetAllData()
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }









        private async Task<string> GetAccessTokenAsync()
        {
            var app = ConfidentialClientApplicationBuilder.Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}"))
                .Build();

            // Acquire the token
            AuthenticationResult result = await app.AcquireTokenForClient(scopes)
                                                   .ExecuteAsync();

            return result.AccessToken;
        }
    }
}
