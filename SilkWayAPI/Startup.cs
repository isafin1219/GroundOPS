using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SilkwayAPI.Data;

namespace SilkwayAPI
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
            services.AddMvc();
            #region Auth0
            // 1. Add Authentication Services
            /*string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });*/

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/registration-9a18d";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/registration-9a18d",
                        ValidateAudience = true,
                        ValidAudience = "registration-9a18d",
                        ValidateLifetime = true
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });

            /*services.AddAuthorization(options =>
            {
                options.AddPolicy("read:profile", policy => policy.Requirements.Add(new HasScopeRequirement("read:profile", domain)));
                options.AddPolicy("update:profile", policy => policy.Requirements.Add(new HasScopeRequirement("update:profile", domain)));
                options.AddPolicy("read:reports", policy => policy.Requirements.Add(new HasScopeRequirement("read:reports", domain)));
                options.AddPolicy("add:report", policy => policy.Requirements.Add(new HasScopeRequirement("add:report", domain)));
                options.AddPolicy("update:report", policy => policy.Requirements.Add(new HasScopeRequirement("update:report", domain)));
                options.AddPolicy("delete:report", policy => policy.Requirements.Add(new HasScopeRequirement("delete:report", domain)));
                options.AddPolicy("read:flights", policy => policy.Requirements.Add(new HasScopeRequirement("read:flights", domain)));
            });*/
            #endregion

            services.AddEntityFrameworkNpgsql().AddDbContext<SilkwayAPIContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("SilkwayAPIPostgreSQL")));

            services.AddHangfire(x => x.UsePostgreSqlStorage(Configuration.GetConnectionString("SilkwayAPIPostgreSQL")));

            // register the scope authorization handler
            //services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // 2. Enable authentication middleware
            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            app.UseMvc();
        }
    }
}
