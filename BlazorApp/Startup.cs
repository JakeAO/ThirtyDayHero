using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Radzen;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.States;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<IContext>(provider => new Context());
            services.AddScoped<IStateMachine>(provider =>
            {
                IContext context = provider.GetRequiredService<IContext>();
                IStateMachine stateMachine = new StateMachine(context as Context);
                stateMachine.ChangeState<LoginState>();
                context.Set<IStateMachine>(stateMachine);
                return stateMachine;
            });
            services.AddScoped<FirebaseWrapper>(provider =>
            {
                IContext context = provider.GetRequiredService<IContext>();

                JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    ContractResolver = new NewtonsoftContractResolver(),
                    Converters = new List<JsonConverter>(HackUtil.GetAllJsonConverters())
                };
                FirebaseWrapper fbWrapper = new FirebaseWrapper(jsonSettings);

                context.Set(jsonSettings);
                context.Set(fbWrapper);
                return fbWrapper;
            });
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
