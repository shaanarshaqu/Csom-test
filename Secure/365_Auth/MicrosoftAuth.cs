using Microsoft.Identity.Client;
using Microsoft.Online.SharePoint.TenantAdministration;

namespace TestCsom.Secure._365_Auth
{
    public class MicrosoftAuth
    {
        private readonly IConfiguration _configuration;
        public MicrosoftAuth(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public async Task<string> GetAccessTokenAsync()
        {
            var app = ConfidentialClientApplicationBuilder.Create(_configuration["SharepointInfo:ClientId"])
                .WithClientSecret(_configuration["SharepointInfo:ClientSecret"])
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{_configuration["SharepointInfo:TelentId"]}"))
                .Build();

            // Acquire the token
            AuthenticationResult result = await app.AcquireTokenForClient(new string[] { _configuration["SharepointInfo:Scope"] })
                                                   .ExecuteAsync();
            return result.AccessToken;
        }
    }
}
