using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using Telegram.Bot;
using TelegramInvitesGenerator.Services;
using TelegramInvitesGenerator.Services.Abstractions;

namespace TelegramInvitesGenerator
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        static Startup()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<IDocumentGenerator, ExcelDocumentGenerator>();
            services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(_configuration["Telegram:Bot:Token"]));
            services.AddSingleton<IChannelInvitesGenerator, ChannelInvitesGenerator>();
            services.AddSingleton<IBotCommandsRepository, BotCommandsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}