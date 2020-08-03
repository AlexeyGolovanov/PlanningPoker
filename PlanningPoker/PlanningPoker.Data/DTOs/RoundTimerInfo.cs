using System;
using System.Timers;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс со значениями для хранения таймеров
  /// </summary>
  public class RoundTimerInfo
  {
    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="roomId">Идентификатор комнаты</param>
    /// <param name="timer">Объект таймера</param>
    public RoundTimerInfo(Guid roomId, Timer timer)
    {
      this.RoomId = roomId;
      this.Timer = timer;
    }

    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    public Guid RoomId { get; set; }

    /// <summary>
    /// Объект таймера
    /// </summary>
    public Timer Timer { get; set; }
  }
}
