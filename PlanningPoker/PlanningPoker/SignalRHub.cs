using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlanningPoker.Data.Models;
using PlanningPoker.Services;

namespace PlanningPoker
{
  /// <summary>
  /// Конкретезированный класс SignalR хаба
  /// </summary>
  public class SignalRHub : Hub
  {
    /// <summary>
    /// Сервис работы с хабом SignalR
    /// </summary>
    private readonly ISignalRHubService hubService;

    /// <summary>
    /// Сервис работы с комнатами
    /// </summary>
    private readonly RoomService roomService;

    /// <summary>
    /// Сервис работы с колодами
    /// </summary>
    private readonly DeckService deckService;

    private readonly UserService userService;

    /// <summary>
    /// Конструктор класса SignalR хаба
    /// </summary>
    /// <param name="userService"> Сервис работы с пользователями </param>
    /// <param name="roomService"> Сервис работы с комнатами </param>
    /// <param name="deckService"> Сервис работы с колодами </param>
    /// <param name="hubService"> Сервис работы с хабом SignalR </param>
    public SignalRHub(UserService userService, RoomService roomService, DeckService deckService, ISignalRHubService hubService)
    {
      this.hubService = hubService;
      this.roomService = roomService;
      this.deckService = deckService;
      this.userService = userService;
    }

    /// <summary>
    /// Обновление списка комнат
    /// </summary>
    /// <returns> Без возвращаемого значения</returns>
    public async Task UpdateRoomList()
    {
      await this.hubService.UpdateRoomList(roomService.GetAll());
    }

    /// <summary>
    /// Обновление списка пользователей
    /// </summary>
    /// <returns> Без возвращаемого значения </returns>
    public async Task UpdateUserList()
    {
      await this.hubService.UpdateUserList(userService.GetAll());
    }

    /// <summary>
    /// Обновить список колод
    /// </summary>
    /// <returns> Без возвращаемого значения </returns>
    public async Task SendDecks()
    {
      await this.Clients.Caller.SendAsync("SendDecks", deckService.GetAll());
    }

    /// <summary>
    /// Метод вызываемый при отключении пользователя
    /// </summary>
    /// <param name="exception"> Возникающая ошибка </param>
    /// <returns> Без возвразаемого значения </returns>
    public override async Task OnDisconnectedAsync(Exception exception)
    {
      var rooms = roomService.GetAll();
      Room foundRoom = null;
      User foundUser = null;
      foreach (var room in rooms)
      {
        foreach (var user in room.Users)
        {
          if (user.ConnectionId == Context.ConnectionId)
          {
            foundRoom = room;
            foundUser = user;
            break;
          }
        }
      }

      if (foundUser == null)
      {
        await base.OnDisconnectedAsync(exception);
        return;
      }

      this.roomService.UserLeave(foundRoom.Id, foundUser.Id);

      await this.hubService.UpdateUserList(foundRoom.Users);

      await base.OnDisconnectedAsync(exception);
    }
  }
}
