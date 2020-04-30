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
        //디스커버리 버전
        private string SERVERURL_IDENTITY = "http://localhost:5000";
        private string SERVERURL_API = "http://localhost:5001/identity";

        //None 디스커버리 버전
        private string tokenEndpoint = "http://localhost:5000/connect/token";
        private string apiurl = "http://localhost:5001/identity";

        public IdentityTest(ITestOutputHelper output) : base(output)
        {            
        }

        [Fact]
        public async Task JwtTokenTest_Docker()
        {            
            var client = new HttpClient();

            // request token by user
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = "ro.angular",
                ClientSecret = "secret",

                UserName = "test01",
                Password = "Test!234",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                throw new Exception("AuthFailed:");
            }

            Console.WriteLine(tokenResponse.Json);
            

            // call api
            var client2 = new HttpClient();
            client2.SetBearerToken(tokenResponse.AccessToken);

            var response = await client2.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                throw new Exception("AuthFailed:");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

        }

        [Fact]
        public async Task AuthTest()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(SERVERURL_IDENTITY);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                throw new Exception("AuthFailed:");
            }
            // request token by user
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.angular",
                ClientSecret = "secret",

                UserName = "test01",
                Password = "Test!234",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                throw new Exception("AuthFailed:");
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

            for(int i = 0; i < 10; i++)
            {
                //토큰 재사용
                var response2 = await client2.GetAsync(SERVERURL_API);
                if (!response2.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response2.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }
            }

            Assert.True(response.IsSuccessStatusCode);

        }
    }
}
