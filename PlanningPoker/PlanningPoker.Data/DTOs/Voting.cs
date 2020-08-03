using System;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс описываюзий объект со значениями для определения голоса
  /// </summary>
  public class Voting
  {
    /// <summary>
    /// Идентификатор раунда
    /// </summary>
    public Guid RoundId { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор карты
    /// </summary>
    public Guid CardId { get; set; }
  }
}
