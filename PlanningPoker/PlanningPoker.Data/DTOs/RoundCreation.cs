using System;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс со значениями для создания раунда
  /// </summary>
  public class RoundCreation
  {
    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="roomId">Идентификатор комнаты</param>
    /// <param name="theme">Тема раунда</param>
    /// <param name="plannedDuration">Ожидаемая длительность в милисекундах</param>
    /// <param name="deckId">Идентификатор колоды</param>
    public RoundCreation(Guid roomId, string theme, int? plannedDuration, Guid? deckId = null)
    {
      this.RoomId = roomId;
      this.Theme = theme;
      this.DeckId = deckId;
      this.PlannedDuration = plannedDuration;
    }

    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    public Guid RoomId { get; }

    /// <summary>
    /// Тема раунда
    /// </summary>
    public string Theme { get; }

    /// <summary>
    /// Идентификатор колоды
    /// </summary>
    public Guid? DeckId { get; }

    /// <summary>
    /// Ожидаемая длительность в милисекундах
    /// </summary>
    public int? PlannedDuration { get; }
  }
}
