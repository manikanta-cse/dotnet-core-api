using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using city_info_api.Data;
using city_info_api.Entities;
using city_info_api.Models;
using city_info_api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace city_info_api
{
    public class Startup
    {

        //core 1.x
        //public static IConfigurationRoot Configuration;

        //core 2.x
        public static IConfiguration Configuration;


        public Startup(IConfiguration configuration) //  IHostingEnvironment env for core 1.x, IConfiguration for 2.x
        {
            //core 1.x
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appSettings.json",optional:false,reloadOnChange:true);

            //Configuration = builder.Build();

            //core 2.X
            Configuration = configuration;


        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
                //.AddMvcOptions(o=>o.OutputFormatters.Add( new XmlDataContractSerializerOutputFormatter()));
            // .AddJsonOptions(o=>{
            //     if(o.SerializerSettings.ContractResolver!=null){
            //         var castedResolver= o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //         castedResolver.NamingStrategy=null;
            //     }
            // });
#if DEBUG

            //services.AddTransient<IMailService,LocalMailService>();
#else
             services.AddTransient<IMailService,CloudMailService>();
#endif
            var connectionString = Startup.Configuration["connectionStrings:cityInfoDBConnectionString"];

            services.AddDbContext<CityInfoContext>(a=>a.UseSqlServer(connectionString));

            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory,CityInfoContext cityInfoContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //seed data
            cityInfoContext.EnsureSeedDataForContext();


            //auto mapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.City, Models.CityWithooutPointOfIntrestDto>();
                cfg.CreateMap<Entities.City, CityDto>();
                cfg.CreateMap<Entities.PointOfIntrest, PointOfIntrestDto>();
                cfg.CreateMap<PointOfIntrestCreationDto, PointOfIntrest>();
                cfg.CreateMap<PointOfIntrestUpdationDto, PointOfIntrest>();
                cfg.CreateMap<PointOfIntrest,PointOfIntrestUpdationDto>();
            });


            app.UseStatusCodePages();
            app.UseMvc();

            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
