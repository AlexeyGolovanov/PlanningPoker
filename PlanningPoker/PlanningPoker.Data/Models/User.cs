using System;

namespace PlanningPoker.Data.Models
{
  /// <summary>
  /// Класс описываюзий пользователя
  /// </summary>
  public class User : IEntity
  {
    /// <summary>
    /// Конструктор класса пользователя
    /// </summary>
    /// <param name="name">Имя пользователя</param>
    /// <param name="connectionId">Идентификатор подключения SignalR хаба</param>
    public User(string name, string connectionId)
    {
      this.Id = Guid.NewGuid();
      this.Name = name;
      this.ConnectionId = connectionId;
    }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Идентификатор подключения SignalR хаба
    /// </summary>
    public string ConnectionId { get; set; }
  }
}
