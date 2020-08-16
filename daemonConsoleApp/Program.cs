using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace daemonConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {       

            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appSettings.json");

            var configuration = builder.Build();  
            var config = configuration.Get<Config>();

            Console.WriteLine(config.ClientId);      

            IConfidentialClientApplication app;
            app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                            .WithClientSecret(config.ClientSecret)
                            .WithAuthority(new Uri(string.Format(config.Instance,config.Tenant)))
                            .Build();
            
            var scopes = new [] {  config.ResourceId+"/.default"};

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(scopes)
                                .ExecuteAsync();

                Console.WriteLine(result.AccessToken);
            }
            catch (MsalUiRequiredException ex)
            {
                // The application doesn't have sufficient permissions.
                // - Did you declare enough app permissions during app creation?
                // - Did the tenant admin grant permissions to the application?
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be in the form "https://resourceurl/.default"
                // Mitigation: Change the scope to be as expected.
            }


            // using(HttpClient httpClient = new HttpClient())
            // {
            //     httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            //     // Call the web API.
            //     HttpResponseMessage response = await httpClient.GetAsync("https://localhost:5001/weatherForecast/forecast/secure");

            //     Console.WriteLine(response.StatusCode);
            //     Console.WriteLine(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            // }
        }
    }
}
