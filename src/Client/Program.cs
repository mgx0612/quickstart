using System;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main()
        {

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);

            }

            var request = new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "mysecret",
                Scope = "api12",
            };

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(request);
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("Hello World!");

            CallApi(tokenResponse.AccessToken);
        }


        static async void CallApi(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);

            var res = await client.GetAsync("http://localhost:5001/identity");
            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine(res.StatusCode);
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }

}