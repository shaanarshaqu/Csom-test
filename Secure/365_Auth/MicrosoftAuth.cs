using Microsoft.Identity.Client;
using Microsoft.Online.SharePoint.TenantAdministration;

namespace TestCsom.Secure._365_Auth
{
    public class MicrosoftAuth
    {
        private readonly IConfiguration configuration;
        public MicrosoftAuth(IConfiguration configuration)
        {
            this.configuration = configuration;
        }



        public async Task<string> GetAccessTokenAsync()
        {
            var app = ConfidentialClientApplicationBuilder.Create(configuration["SharepointInfo:ClientId"])
                .WithClientSecret(configuration["SharepointInfo:ClientSecret"])
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{configuration["SharepointInfo:TelentId"]}"))
                .Build();

            // Acquire the token
            AuthenticationResult result = await app.AcquireTokenForClient(new string[] { configuration["SharepointInfo:Scope"] })
                                                   .ExecuteAsync();
            return result.AccessToken;
        }
    }
}
