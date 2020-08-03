using System;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс со значениями для завершения раунда
  /// </summary>
  public class RoundFinishing
  {
    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    public Guid RoomId { get; set; }
  }
}
