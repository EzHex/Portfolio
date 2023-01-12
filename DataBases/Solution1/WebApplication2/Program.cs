using NLog;

namespace WebApplication2
{
	public class Program
	{
		Logger log = LogManager.GetCurrentClassLogger();

		private static void ConfigureLogging()
		{
			var config = new NLog.Config.LoggingConfiguration();

			var console =
				new NLog.Targets.ConsoleTarget("console")
				{
					Layout = @"${date:format=HH\:mm\:ss}|${level}| ${message} ${exception}"
				};
			config.AddTarget(console);
			config.AddRuleForAllLevels(console);

			LogManager.Configuration = config;
		}

		public static void Main(string[] args)
		{
			ConfigureLogging();

			var self = new Program();
			self.Run(args);
		}

		private void Run(string[] args)
		{
			try
			{
				var builder = WebApplication.CreateBuilder(args);

				//set the address and port the Kestrel server should bind to
				builder.WebHost.ConfigureKestrel(opts =>
				{
					opts.Listen(System.Net.IPAddress.Loopback, 5000);
				});

				//add services to the container.
				builder.Services.AddRazorPages()
					.AddViewOptions(o =>
                    {
						o.HtmlHelperOptions.ClientValidationEnabled = false;
                    });

				//build the app
				var app = builder.Build();

				//initialize configuration helper
				Config.CreateSingletonInstance(app.Configuration);

				//
				app.UseStaticFiles();
				app.UseRouting();
				app.UseAuthorization();

				app.MapDefaultControllerRoute();
				app.MapRazorPages();

				app.Run();
			}
			catch (Exception e)
			{
				log.Error(e, "Unhandled exception caught when initializing program. The main thread is now dead.");
			}
		}

	}
}