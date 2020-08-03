using System;
using System.Collections.Generic;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;

namespace PlanningPoker.Services
{
  /// <summary>
  /// Сервис работы с пользователями
  /// </summary>
  public class UserService
  {
    /// <summary>
    /// Репозиторий пользователей
    /// </summary>
    private readonly IRepository<User> users;

    /// <summary>
    /// Конструктор сервиса работы с пользователями
    /// </summary>
    /// <param name="users">Репозиторий пользователей</param>
    public UserService(IRepository<User> users)
    {
      this.users = users;
    }

    /// <summary>
    /// Добавоение нового пользователя
    /// </summary>
    /// <param name="name">Имя пользователя</param>
    /// <param name="connectionId">Идентификатор подключения SignalR</param>
    /// <returns>Экземпляр созданного пользователя</returns>
    public User Add(string name, string connectionId)
    {
      var user = new User(name, connectionId);
      this.users.Add(user);

      return user;
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    public void Remove(Guid id)
    { 
      this.users.Remove(id);
    }

    /// <summary>
    /// Получение пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Найденный пользователь</returns>
    public User Get(Guid id)
    {
      var user = this.users.Get(id);
      return user;
    }

    /// <summary>
    /// Получение списка всех пользователей
    /// </summary>
    /// <returns>Список всех пользователей</returns>
    public IEnumerable<User> GetAll()
    {
      return this.users.GetAll();
    }

    /// <summary>
    /// Переподключение пользователя к комнате
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="connectionId">Идентификатор подключения SignalR</param>
    /// <returns>Экземпляр переподключенного пользователя</returns>
    public User Reconnect(Guid userId, string connectionId)
    {
      var user = this.users.Get(userId);
      user.ConnectionId = connectionId;
      return user;
    }
  }
}