using System;
using System.Net.Http.Headers;
using System.Text;
using Transporte.Api.Middleware;
using Transporte.Api.Model.Auth;
using Transporte.Business;
using Transporte.Business.Result;
using Transporte.Repository;
using Transporte.Repository.Context;
using Transporte.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Transporte.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TransporteContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("TransporteDatabase")));

            ConfigureFacebookApi(services);
            ConfigureDAL(services);
            ConfigureBusiness(services);

            ConfigureAuth(services);

            services.AddCors();
            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                options.RespectBrowserAcceptHeader = false;
                options.AllowEmptyInputInBodyModelBinding = true;
                options.AllowValidatingTopLevelNodes = false;
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.ErrorResponses = new ApiVersionError();
            });
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AuthSettings");
            services.Configure<AuthSettings>(appSettingsSection);

            var authSettings = appSettingsSection.Get<AuthSettings>();
            var key = Encoding.ASCII.GetBytes(authSettings.Key);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private void ConfigureBusiness(IServiceCollection services)
        {
            services.AddScoped<UsuarioBusiness>();
            services.AddScoped<BusinessValidation>();
        }

        private void ConfigureDAL(IServiceCollection services)
        {
            services.AddScoped<IUsuarioDAL, UsuarioDAL>();
            services.AddScoped<IRefreshTokenDAL, RefreshTokenDAL>();
        }

        private void ConfigureFacebookApi(IServiceCollection services)
        {
            services.AddHttpClient("Facebook", client =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string baseURL = Configuration.GetSection("Facebook:BaseURL").Value;
                string key = Configuration.GetSection("Facebook:Key").Value;

                client.BaseAddress = new Uri(baseURL);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TransporteContext dbContext)
        {
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.SaveChanges();

            app.UseMiddleware<ErrorHandler>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

}
