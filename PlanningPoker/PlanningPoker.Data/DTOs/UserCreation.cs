using System;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс созначениями для создания пользователя
  /// </summary>
  public class UserCreation
  {
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Идентификатор подключения к хабу
    /// </summary>
    public string ConnectionId { get; set; }
  }
}
