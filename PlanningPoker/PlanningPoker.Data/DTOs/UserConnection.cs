using System;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс со значениями для подключения пользователя к комнате
  /// </summary>
  public class UserConnection
  {
    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="roomId">Идентификатор комнаты</param>
    public UserConnection(Guid userId, Guid roomId)
    {
      this.UserId = userId;
      this.RoomId = roomId;
    }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    public Guid RoomId { get; set; }
  }
}
