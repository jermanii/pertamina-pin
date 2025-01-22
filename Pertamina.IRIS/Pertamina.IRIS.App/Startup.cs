using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pertamina.IRIS.Models.Entities;
using Microsoft.AspNetCore.Http.Features;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Pertamina.IRIS.DependencyInjection;
using Pertamina.IRIS.Models.Base;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Pertamina.IRIS.App
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
            services.AddControllers();
            services.AddMvc();
            services.AddMvcCore();
            //services.AddApplicationInsightsTelemetry();
            services.AddDbContext<DB_PINMContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(DB_PINMContext).Assembly.FullName))
                );
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            }); 
            
            services.Configure<FormOptions>(options =>
            {
                // Set the limit to 128 MB
                options.MultipartBodyLengthLimit = 134217728; 
            });

            #region Injection
            services.AddAutoMapper(typeof(AutoMapperExtension));
            services.ConfigureServices();
            services.ConfigureRepository();
            services.ConfigurePagination();
            services.ConfigureFiltering();
            services.ConfigureSorting();
            services.ConfigureWording();
            services.ConfigureMimeType();
            services.AddHttpContextAccessor();
            #endregion

            #region Global Variable
            var globalVariable = new GlobalVariableBaseModel()
            {
                PrefixApiUrl = Configuration["GlobalVariable:PrefixApiUrl"],
                PrefixRootName = Configuration["GlobalVariable:PrefixRootName"],
                PrefixPicName = Configuration["GlobalVariable:PrefixPicName"]
            };

            services.AddSingleton(globalVariable);
            #endregion

            #region Authentiation
            Action<AppSettingsBaseModel> appSettings = (opt =>
            {
                opt.ApplicationCookiesName = Configuration["Application:CookiesName"];
                opt.ApplicationFolderApp = Configuration["Application:FolderApp"];
                opt.IdamanUrlLogin = Configuration["Idaman:UrlLogin"];
                opt.IdamanUrlApi = Configuration["Idaman:UrlApi"];
                opt.IdamanClientId = Configuration["Idaman:ClientId"];
                opt.IdamanObjectId = Configuration["Idaman:ObjectId"];
                opt.IdamanClientSecret = Configuration["Idaman:ClientSecret"];
                opt.IdamanScopes = Configuration["Idaman:Scopes"];
            });

            services.Configure(appSettings);
            services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<AppSettingsBaseModel>>().Value);

            services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF5cWWtCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWH9dcHZWQmldUUx1W0U=");

            services.AddMemoryCache();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = Configuration["Idaman:UrlLogin"];
                    options.RequireHttpsMetadata = false;
                    options.ClientId = Configuration["Idaman:ClientId"];
                    options.ClientSecret = Configuration["Idaman:ClientSecret"];
                    options.ResponseType = "code id_token";
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    var scopes = Configuration["Idaman:Scopes"].Replace(" ", "").Split(",");
                    foreach (var scope in scopes)
                    {
                        options.Scope.Add(scope);
                    }

                    options.Events = new OpenIdConnectEvents
                    {
                        // Your events here
                        OnRemoteFailure = ctx =>
                        {
                            ctx.HandleResponse();
                            ctx.Response.Redirect("/");
                            return Task.FromResult(0);
                        }
                    };
                });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppSettingsBaseModel appSettingsBaseModel)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to
                // change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\img")),
                RequestPath = new PathString("/StaticFiles")
            });
            app.UseCookiePolicy();
            app.UseAuthentication();
            if (!string.IsNullOrEmpty(appSettingsBaseModel.ApplicationFolderApp))
            {
                app.UsePathBase(appSettingsBaseModel.ApplicationFolderApp);
            }

            app.UseCors();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=Login}/{action=Index}");
            });
        }
    }
}