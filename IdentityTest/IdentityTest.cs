using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace IdentityTest
{
    public class IdentityTest : TestKitBase
    {
        private string SERVERURL_IDENTITY = "http://localhost:5000";
        private string SERVERURL_API = "http://localhost:5001/identity";

        public IdentityTest(ITestOutputHelper output) : base(output)
        {            
        }

        [Fact]
        public async Task AuthTest()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(SERVERURL_IDENTITY);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var client2 = new HttpClient();
            client2.SetBearerToken(tokenResponse.AccessToken);

            var response = await client2.GetAsync(SERVERURL_API);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

        }
    }
}
