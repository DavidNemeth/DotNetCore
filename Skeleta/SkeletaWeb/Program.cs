using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SkeletaDAL.ApplicationContext;
using SkeletaWeb.Helpers;
using System;

namespace SkeletaWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = BuildWebHost(args);
			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var databaseInitializer = services.GetRequiredService<IDatabaseInitializer>();
					databaseInitializer.SeedAsync().Wait();
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogCritical(LoggingEvents.INIT_DATABASE, ex, LoggingEvents.INIT_DATABASE.Name);
				}
			}
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}