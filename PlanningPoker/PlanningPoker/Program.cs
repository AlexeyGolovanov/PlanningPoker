using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PlanningPoker
{
  /// <summary>
  /// Основной класс
  /// </summary>
  public class Program
    {
      /// <summary>
      /// Точка входа приложения
      /// </summary>
      /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
