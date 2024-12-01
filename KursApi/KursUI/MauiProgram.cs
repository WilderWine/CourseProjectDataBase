using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Windows.Markup;

namespace KursUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            HttpClient client = new HttpClient();
            var h = fget(client);
            int a = 5;
            int b = 5;


            return builder.Build();
        }

        async static Task<HttpResponseMessage> fget(HttpClient client)
        {
            var response = await client.GetAsync("https://localhost:44300/api/values");

            var j = response.Content.ReadAsStringAsync();

            return response;
        }
    }
}