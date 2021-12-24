using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TouhouAPI;
using TouhouArticleMaker.Domain;
using TouhouData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TouhouData.Context;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace TouhouArticleMaker.IntegrationTests
{

    public static class IntegrationTests
    {
        private static HttpClient _httpClient;

        private static int usernameCounter;

        public static bool IsAuthenticate { get; private set; }

        private static Random random = new Random();

        public static string UserName { get; set; }

        public static async Task<HttpClient> DoSetup()
        {
            var webApp = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {

                    var touhouDescriptor = services
                                                .SingleOrDefault(
                                                     d => d.ServiceType == typeof(DbContextOptions<TouhouContext>)
                                                 );

                    var userDescriptor = services
                                               .SingleOrDefault(
                                                    d => d.ServiceType == typeof(DbContextOptions<UserContext>)
                                                );

                    if (touhouDescriptor != null)
                    {
                        services.Remove(touhouDescriptor);
                    }

                    if (userDescriptor != null)
                    {
                        services.Remove(userDescriptor);
                    }

                    services.AddDbContext<TouhouContext>(options => { options.UseInMemoryDatabase("dbTestTouhou"); });
                    services.AddDbContext<UserContext>(options => { options.UseInMemoryDatabase("dbTestUser"); });

                });
            });

            lock (random)
            {
                usernameCounter = random.Next(2000, 5000);
                _httpClient = webApp.CreateClient();
            }
            await AuthenticateAsync();
            return _httpClient;
        }

        public static async Task AuthenticateAsync()
        {
            var token = await GetJwtAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            IsAuthenticate = true;
        }

        private static async Task<string> GetJwtAsync()
        {
            //Temp fix
            var token = "";
            var hasError = false;
            do
            {
                UserName = $"YoukaiStalker{usernameCounter}";
                var user = new User(new Name("Flandre", "Scarlet"), new Title($"YoukaiStalker{usernameCounter}"), "F1@ndreS", new Email("flandre.scarlet@gensoukyo.com"), new Shared.EntityValidation());

                var json = JsonConvert.SerializeObject(user);
                var response = await _httpClient.PostAsync("https://localhost:50001/account/register", new StringContent(json, Encoding.UTF8, "application/json"));
                token = await response.Content.ReadAsStringAsync();

            if (token.Contains("Duplicate"))
            {
                hasError = true;
                usernameCounter++;
            }
            else
            {
                hasError = false;
            }

        } while (hasError);   

            return token;
        }
    }
}
