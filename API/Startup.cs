using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using API.Helpers;
using API.Middleware;
using API.Errors;
using Microsoft.OpenApi.Models;
using API.Extensions;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddAutoMapper(typeof(MappingProfiles));         
            services.AddControllers();
            services.AddDbContext<StoreContext>(options =>
               options.UseSqlServer(
                   _config.GetConnectionString("DefaultConnection")));

            //ovime si pročistio startup (bit će dosta toga još, recimo identity), pozivaš se 
            //na ovo dolje koje u sebi sadrži services
            //koje si premjestio iz startupa u extensions/Applicationservices i swaggerdocumentation
            services.AddApplicationServices();
            services.AddSwaggerDocumentation();
              services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            //ovime preusmjeravamo na errorcontroller, 0 je statuscode
            //ako request dođe do servera, a mi nemamo odgovarajući naziv za njega, on odlazi ovdje
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            
            //i ovdje si kao i gore malo pročistio i pozvao se na ovo dolje (housekeeping kako kaže on:))
            app.UseSwaggerDocumention();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
