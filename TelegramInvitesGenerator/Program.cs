using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace TelegramInvitesGenerator
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await SetTelegramWebhookAsync(host);
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        
        private static async Task SetTelegramWebhookAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var telegramBotClient = scope.ServiceProvider.GetService<ITelegramBotClient>();
            var configuration = scope.ServiceProvider.GetService<IConfiguration>();
            
            await telegramBotClient.SetWebhookAsync(configuration["AppUrl"] + "/bot");
        }
    }
}