using System;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс содержащий значения для создания комнаты
  /// </summary>
  public class RoomCreation
  {
    /// <summary>
    /// Название комнаты
    /// </summary>
    public string RoomName { get; set; }

    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор используемой колоды
    /// </summary>
    public Guid DeckId { get; set; }
  }
}
