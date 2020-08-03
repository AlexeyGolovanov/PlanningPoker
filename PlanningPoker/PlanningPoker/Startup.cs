using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;
using PlanningPoker.Services;

namespace PlanningPoker
{
  public class Startup
    {
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    /// <summary>
    /// Параметры конфигурации
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// 
    /// Добавление сервисов в контейнер.
    /// </summary>
    /// <param name="services">Коллекция сервисов <see cref="IServiceCollection"/>.</param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddCors(options => options.AddPolicy(
        "CorsPolicy",
        builder =>
        {
          builder.AllowAnyMethod().AllowAnyHeader()
            .WithOrigins("http://localhost:44356")
            .AllowCredentials();
        }));
      services.AddSignalR();

      services.AddTransient<IRepository<Room>, RoomRepository>();
      services.AddTransient<IRepository<Round>, RoundRepository>();
      services.AddTransient<IRepository<User>, UserRepository>();
      services.AddTransient<IRepository<Deck>, DeckRepository>();
      services.AddTransient<IRepository<Card>, CardRepository>();

      services.AddSingleton<ISignalRHubService, SignalRHubService>();
      services.AddSingleton<RoomService>();
      services.AddSingleton<RoundService>();
      services.AddSingleton<UserService>();
      services.AddSingleton<DeckService>();
      services.AddSingleton<CardService>();
      services.AddSingleton<TimerService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DeckService deckService, CardService cardService)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDefaultFiles();
        app.UseStaticFiles();
      }

      app.UseRouting();

      app.UseCors("CorsPolicy");

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHub<SignalRHub>("/signalHub", option =>
        {
          option.Transports = HttpTransportType.WebSockets;
        });
        endpoints.MapControllers();
      });

      this.DecksCreation(deckService, cardService);
    }

    /// <summary>
    /// Создание колод
    /// </summary>
    /// <param name="deckService"> Сервис работы с колодами </param>
    /// <param name="cardService"> Сервис работы с картами </param>
    public void DecksCreation(DeckService deckService, CardService cardService)
    {
      cardService.Add("Один", 1);
      cardService.Add("Два", 2);
      cardService.Add("Три", 3);

      var deckSmall = deckService.Add("3 values");
      deckSmall.Cards.Concat(cardService.GetAll());

      cardService.Add("Четыре", 4);
      cardService.Add("Пять", 5);
      cardService.Add("Шесть", 6);
      cardService.Add("?", null);

      var deckFull = deckService.Add("7 values");
      deckFull.Cards.Concat(cardService.GetAll());
    }
  }
}
