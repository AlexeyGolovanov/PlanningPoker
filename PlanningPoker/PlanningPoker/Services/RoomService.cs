using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;

namespace PlanningPoker.Services
{
  /// <summary>
  /// Сервис работы с комнатами
  /// </summary>
  public class RoomService
  {
    /// <summary>
    /// Сервис работы с хабом SignalR
    /// </summary>
    private readonly ISignalRHubService hub;

    /// <summary>
    /// Репозиторий комнат
    /// </summary>
    private readonly IRepository<Room> rooms;

    /// <summary>
    /// Репозиторий пользователей
    /// </summary>
    private readonly IRepository<User> users;

    /// <summary>
    /// Конструктор сервиса работы с комнатами
    /// </summary>
    /// <param name="hub">Сервис работы с хабом SignalR</param>
    /// <param name="rooms">Репозиторий комнат</param>
    /// <param name="users">Репозиторий пользователей</param>
    public RoomService(ISignalRHubService hub, IRepository<Room> rooms, IRepository<User> users)
    {
      this.hub = hub;
      this.rooms = rooms;
      this.users = users;
    }

    /// <summary>
    /// Создание новойй комнаты
    /// </summary>
    /// <param name="name">Название комнаты</param>
    /// <param name="owner">Пользователь - создатель</param>
    /// <param name="deck">Используемая по умолчанию колода</param>
    /// <returns></returns>
    public Room Add(string name, User owner, Deck deck)
    {
      var room = new Room(name, owner, deck);
      this.rooms.Add(room);
      this.hub.UpdateRoomList(this.GetAll());
      return room;
    }

    /// <summary>
    /// Удаление комнаты
    /// </summary>
    /// <param name="id">Идентификатор комнаты</param>
    public void Remove(Guid id)
    {
      this.rooms.Remove(this.Get(id).Id);
    }

    /// <summary>
    /// Получение экземпляра комнаты
    /// </summary>
    /// <param name="id">Идентификатор комнаты</param>
    /// <returns>Найденная комната</returns>
    public Room Get(Guid id)
    {
      return this.rooms.Get(id);
    }

    /// <summary>
    /// Получение списка всех комнат
    /// </summary>
    /// <returns>Список всех комнат</returns>
    public IEnumerable<Room> GetAll()
    {
      return this.rooms.GetAll();
    }

    /// <summary>
    /// Присоединение пользователя к комнате
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="roomId">Идентификатор комнаты</param>
    /// <returns>Экземпляр комнаты, к которой присоединился пользователь</returns>
    public async Task<Room> UserJoin(Guid userId, Guid roomId)
    {
      var room = this.Get(roomId);
      var user = this.users.Get(userId);

      room.Users.Add(user);

      await this.hub.UpdateUserList(room.Users);

      return room;
    }

    /// <summary>
    /// Отключение пользователя от комнаты
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="roomId">Идентификатор комнаты</param>
    public void UserLeave(Guid userId, Guid roomId)
    {
      var room = this.Get(roomId);
      room.Users.Remove(this.users.Get(userId));
      this.hub.UpdateUserList(room.Users);
    }
  }
}
