using AutoMapper;
using Blank.DAL;
using Blank.DAL.Interfaces;
using Blank.Models;
using Blank.Services;
using Blank.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace Blank
{
		public class Startup
		{
				private IHostingEnvironment _env;
				private IConfigurationRoot _config;

				public Startup(IHostingEnvironment env)
				{
						_env = env;
						var builder = new ConfigurationBuilder()
										.SetBasePath(_env.ContentRootPath)
										.AddJsonFile("config.json")
										.AddEnvironmentVariables();

						_config = builder.Build();
				}
				// This method gets called by the runtime. Use this method to add services to the container.
				// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
				public void ConfigureServices(IServiceCollection services)
				{
						services.AddSingleton(_config);

						if (_env.IsDevelopment())
						{
								services.AddScoped<IMailService, DebugMailService>();
						}
						else
						{
								// TODO Implement real Mail Service
						}

						//MVC config
						services.AddMvc(config =>
						{
								if (_env.IsProduction())
								{
										config.Filters.Add(new RequireHttpsAttribute());
								}
						})
						.AddJsonOptions(config =>
						{
								config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
						});

						//Identity services
						services.AddIdentity<BlankUser, IdentityRole>(config =>
						{
								config.User.RequireUniqueEmail = true;
								config.Password.RequiredLength = 6;
								config.Password.RequireDigit = true;
								config.Password.RequiredUniqueChars = 0;
								config.Password.RequireUppercase = false;
								config.Password.RequireNonAlphanumeric = false;
						})
						.AddEntityFrameworkStores<BlankContext>();

						services.ConfigureApplicationCookie(config =>
						{
								config.LoginPath = "/Auth/Login";
								config.Events = new CookieAuthenticationEvents
								{
										OnRedirectToLogin = async ctx =>
										{
												if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
												{
														ctx.Response.StatusCode = 401;
												}
												else
												{
														ctx.Response.Redirect(ctx.RedirectUri);
												}
												await Task.Yield();
										}
								};
						});

						//Context and seedData
						services.AddDbContext<BlankContext>();
						services.AddTransient<BlankSeedData>();
						//IoC mapping
						services.AddScoped<IBlankRepository, BlankRepository>();
						//Other services
						services.AddLogging();
						services.AddTransient<GeoCoordsService>();
				}

				// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
				public void Configure(IApplicationBuilder app, IHostingEnvironment env, BlankSeedData seeder, ILoggerFactory factory)
				{
						//Automapper Mappings
						Mapper.Initialize(config =>
						{
								config.CreateMap<TripViewModel, Trip>().ReverseMap();
								config.CreateMap<StopViewModel, Stop>().ReverseMap();
						});

						//Development enviroment setup
						if (env.IsDevelopment())
						{
								app.UseDeveloperExceptionPage();
								factory.AddDebug(LogLevel.Information);
						}
						else
						{
								factory.AddDebug(LogLevel.Error);
						}

						app.UseStaticFiles();

						app.UseAuthentication();

						app.UseMvc(config =>
						{
								config.MapRoute(
										name: "Default",
										template: "{controller}/{action}/{id?}",
										defaults: new { controller = "App", action = "Index" }
								);
						});

						seeder.EnsureSeedData().Wait();
				}
		}
}
