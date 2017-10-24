using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SkeletaDAL;
using SkeletaDAL.ApplicationContext;
using SkeletaDAL.Core.CoreModel;
using SkeletaDAL.Core.Interfaces;
using SkeletaWeb.Helpers;
using SkeletaWeb.Services;
using SkeletaWeb.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using System.Net;

namespace SkeletaWeb
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

			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly(nameof(SkeletaWeb)));
				options.UseOpenIddict();
			});

			// DB Creation and Seeding
			services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

			// Repositories
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IAccountManager, AccountManager>();
			services.AddScoped<IServices, AppServices>();

			//add identity
			services.AddIdentity<ApplicationUser, ApplicationRole>()
					.AddEntityFrameworkStores<ApplicationDbContext>()
					.AddDefaultTokenProviders();

			// Configure Identity options and password complexity here
			services.Configure<IdentityOptions>(options =>
			{
				// User settings
				options.User.RequireUniqueEmail = true;

				//    //// Password settings
				//    //options.Password.RequireDigit = true;
				//    //options.Password.RequiredLength = 8;
				//    //options.Password.RequireNonAlphanumeric = false;
				//    //options.Password.RequireUppercase = true;
				//    //options.Password.RequireLowercase = false;

				//    //// Lockout settings
				//    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				//    //options.Lockout.MaxFailedAccessAttempts = 10;

				options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
				options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
				options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
			});

			EmailSender.Configuration = new SmtpConfig
			{
				Host = Configuration["SmtpConfig:Host"],
				Port = int.Parse(Configuration["SmtpConfig:Port"]),
				UseSSL = bool.Parse(Configuration["SmtpConfig:UseSSL"]),
				Name = Configuration["SmtpConfig:Name"],
				Username = Configuration["SmtpConfig:Username"],
				EmailAddress = Configuration["SmtpConfig:EmailAddress"],
				Password = Configuration["SmtpConfig:Password"]
			};

			// Register OpenIddict services.
			services.AddOpenIddict(options =>
			{
				options.AddEntityFrameworkCoreStores<ApplicationDbContext>();
				options.AddMvcBinders();
				options.EnableTokenEndpoint("/connect/token");
				options.AllowPasswordFlow();
				options.AllowRefreshTokenFlow();
				options.DisableHttpsRequirement();
				//options.AddSigningKey(new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration["STSKey"])));
			});

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = OAuthValidationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = OAuthValidationDefaults.AuthenticationScheme;
			}).AddOAuthValidation();

			services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("BearerAuth", new ApiKeyScheme
				{
					Name = "Authorization",
					Description = "Login with your bearer authentication token. e.g. Bearer <auth-token>",
					In = "header",
					Type = "apiKey"
				});

				c.SwaggerDoc("v1", new Info { Title = "Skeleta API", Version = "v1" });
			});

			Mapper.Initialize(cfg =>
			{
				cfg.AddProfile<AutoMapperProfile>();
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug(LogLevel.Warning);

			Utilities.ConfigureLogger(loggerFactory);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					HotModuleReplacement = true
				});
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseAuthentication();
			EmailTemplates.Initialize(env);

			app.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = MediaTypeNames.ApplicationJson;

					var error = context.Features.Get<IExceptionHandlerFeature>();

					if (error != null)
					{
						var errorMsg = JsonConvert.SerializeObject(new { error = error.Error.Message });
						await context.Response.WriteAsync(errorMsg).ConfigureAwait(false);
					}
				});
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skeleta V1");
			});

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapSpaFallbackRoute(
					name: "spa-fallback",
					defaults: new { controller = "Home", action = "Index" });
			});
		}
	}
}
