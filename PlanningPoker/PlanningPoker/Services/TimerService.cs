using System;
using System.Collections.Concurrent;
using System.Timers;
using PlanningPoker.Data.DTOs;

namespace PlanningPoker.Services
{
  /// <summary>
  /// Сервис работы с таймерами
  /// </summary>
  public class TimerService
  {
    /// <summary>
    /// Словарь содержащий в качестве ключа идентификатор раунда, а в значении информацию для таймера
    /// </summary>
    private readonly ConcurrentDictionary<Guid, RoundTimerInfo> timers = new ConcurrentDictionary<Guid, RoundTimerInfo>();

    /// <summary>
    /// Запуск таймера
    /// </summary>
    /// <param name="roomId">Идентификатор комнаты</param>
    /// <param name="roundId">Идентификатор раунда</param>
    /// <param name="timer">Экземпляр таймера</param>
    /// <returns>Объект с информацией для таймера</returns>
    public RoundTimerInfo Start(Guid roomId, Guid roundId, Timer timer)
    {
      if (timers.TryAdd(roundId, new RoundTimerInfo(roomId, timer)))
      {
        timers[roundId].Timer.Start();
      }

      return timers[roundId];
    }

    /// <summary>
    /// Остановка таймера
    /// </summary>
    /// <param name="roundId">Идентификатор раунда</param>
    /// <returns>Был ли остановлен таймер</returns>
    public bool Stop(Guid roundId)
    {
      if (timers.TryRemove(roundId, out var value))
      {
        value.Timer.Stop();
        value.Timer.Dispose();
        return true;
      }

      return false;
    }
  }
}
