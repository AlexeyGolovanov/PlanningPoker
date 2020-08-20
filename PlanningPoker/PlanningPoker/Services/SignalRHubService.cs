using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlanningPoker.Data.DTOs;
using PlanningPoker.Data.Models;

namespace PlanningPoker.Services
{
  /// <summary>
  /// Сервис взамодействия с хабом SignalR
  /// </summary>
  public class SignalRHubService : ISignalRHubService
  {
    /// <summary>
    /// Абстракция для хаба SignalR
    /// </summary>
    private readonly IHubContext<SignalRHub> context;

    /// <summary>
    /// Конструктор сервиса взамодействия с хабом SignalR
    /// </summary>
    /// <param name="context"> Абстракция для хаба SignalR </param>
    public SignalRHubService(IHubContext<SignalRHub> context)
    {
      this.context = context;
    }

    /// <summary>
    /// Обновление списка комнат
    /// </summary>
    /// <param name="rooms"> Список комнат </param>
    /// <returns> Без возвращаемого значения </returns>
    public async Task UpdateRoomList(IEnumerable<Room> rooms)
    {
      await this.context.Clients.All.SendAsync("UpdateRoomList", rooms);
    }

    /// <summary>
    /// Обговление списка пользователей
    /// </summary>
    /// <param name="users"> Список пользователей </param>
    /// <returns> Без возвращаемого значения </returns>
    public async Task UpdateUserList(IEnumerable<User> users)
    {
      foreach (var user in users)
      {
        await this.context.Clients.Client(user.ConnectionId).SendAsync("UpdateUserList", users);
      }
    }

    /// <summary>
    /// Информация о начале раунда
    /// </summary>
    /// <param name="users"> Список пользователей </param>
    /// <param name="round"> Рассматриваемый раунд </param>
    /// <returns> Без возвращаемого значения </returns>
    public async Task RoundStart(IEnumerable<User> users, Round round)
    {
      foreach (var user in users)
      {
        await this.context.Clients.Client(user.ConnectionId).SendAsync("RoundStart", new RoundStartInfo(round.Id, round.Theme));
      }
    }

    /// <summary>
    /// Информация озавршении раунда
    /// </summary>
    /// <param name="users"> Список пользователей </param>
    /// <param name="round"> Рассматриваемый раунд </param>
    /// <returns> Без возвращаемого значения </returns>
    public async Task RoundStop(IEnumerable<User> users, Round round)
    {
      foreach (var user in users)
      {
        await this.context.Clients.Client(user.ConnectionId).SendAsync("RoundStop", new RoundStopInfo(round.Id, round.Theme, round.Votes, round.AverageValue));
      }
    }
  }
}
