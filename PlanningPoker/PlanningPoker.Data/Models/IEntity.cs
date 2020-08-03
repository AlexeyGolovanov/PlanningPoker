using System;

namespace PlanningPoker.Data.Models
{
  /// <summary>
  /// Базовый интерфейс всех моделей
  /// </summary>
  public interface IEntity
  {
    Guid Id { get; }
  }
}