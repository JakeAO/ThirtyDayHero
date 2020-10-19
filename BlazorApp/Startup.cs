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

            Context context = new Context();
            services.AddSingleton<IContext>(context);
            services.AddSingleton<Context>(context);

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                ContractResolver = new NewtonsoftContractResolver(),
                Converters = new List<JsonConverter>(HackUtil.GetAllJsonConverters())
            };
            context.Set(jsonSettings);

            FirebaseWrapper fbWrapper = new FirebaseWrapper(jsonSettings);
            services.AddSingleton<FirebaseWrapper>(fbWrapper);
            context.Set(fbWrapper);
            
            StateMachine stateMachine = new StateMachine(context);
            stateMachine.ChangeState<LoginState>();
            services.AddSingleton<IStateMachine>(stateMachine);
            services.AddSingleton<StateMachine>(stateMachine);
            context.Set<IStateMachine>(stateMachine);
            context.Set<StateMachine>(stateMachine);

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
