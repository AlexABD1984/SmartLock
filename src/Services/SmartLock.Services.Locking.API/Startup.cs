using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using SmartLock.Services.Locking.API.ApplicationServices;
using SmartLock.Services.Locking.API.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace SmartLock.Services.Locking.API
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
            var identityServerUrl = Configuration.GetValue<string>("IdentityServerUrl");

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddHttpClient<AuditLogHttpClient>(client =>
            {
                var AuditLogServiceBaseUrl = Configuration.GetValue<string>("AuditLogServiceBaseUrl");
                client.BaseAddress = new Uri(AuditLogServiceBaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "LockingServiceHttpClientFactory");
            });
            //.AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
            //.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));

            services.AddHttpClient<AccessRightHttpClient>(client =>
            {
                var accessRightServiceBaseUrl = Configuration.GetValue<string>("AccessRightServiceBaseUrl");
                client.BaseAddress = new Uri(accessRightServiceBaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "LockingServiceHttpClientFactory");
            });
            //.AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
            //.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer( x =>
                {
                    x.Authority = identityServerUrl;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudiences = new[] { "api1" }
                    };
                    x.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents()
                    {
                        OnAuthenticationFailed = async ctx =>
                        {
                            int i = 0;
                        },
                        OnTokenValidated = async ctx =>
                        {
                            int i = 0;
                        },

                        OnMessageReceived = async ctx =>
                        {
                            int i = 0;
                        }
                    };
                });
            //Register Application Services in IoC
            services.AddScoped<IUserAccessService, UserAccessService>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<ILockingService, LockingService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCacheManager();
            services.AddCacheManager<bool>(inline => inline.WithDictionaryHandle());

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Smart Lock - Locking Service API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseAuthentication();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartLock Locking API V1");
                c.RoutePrefix = string.Empty;
            });

            ConfigBroker(app);
        }

        private void ConfigBroker(IApplicationBuilder app)
        {
            //var messageBroker = app.ApplicationServices.GetRequiredService<IMessageBroker>();
            //messageBroker.Subscribe<, >();
        }
    }
    public static class MyExtensionMethods
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "SmartLock - Locking HTTP API",
                    Version = "v1",
                    Description = "The servives for Open or Lock Door by HTTP API",
                    TermsOfService = "This is for Demo"
                });
            });

            return services;

        }
    }
}
