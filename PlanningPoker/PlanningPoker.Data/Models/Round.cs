using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanningPoker.Data.Models
{
  /// <summary>
  /// Объект описывающий раунд
  /// </summary>
  public class Round : IEntity
  {
    /// <summary>
    /// Конструктор объекта раунда
    /// </summary>
    /// <param name="theme"> Тема раунда </param>
    public Round(string theme)
    {
      this.Id = Guid.NewGuid();
      this.Theme = theme;
      this.StartTime = DateTime.Now;
      this.IsActive = true;
      this.Votes = new List<Vote>();
    }

    /// <summary>
    /// Идентификатор раунда
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Тема раунда
    /// </summary>
    public string Theme { get; }

    /// <summary>
    /// Установленная длительность
    /// </summary>
    public TimeSpan? PlannedDuration { get; set; }
    
    /// <summary>
    /// Время начала раунда
    /// </summary>
    public DateTime StartTime { get; }

    /// <summary>
    /// Время заверщения раугда
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Действительная длительность
    /// </summary>
    public TimeSpan ActualDuration => this.EndTime - this.StartTime;

    /// <summary>
    /// Список голосов
    /// </summary>
    public ICollection<Vote> Votes { get; }

    /// <summary>
    /// Среднее значение голосов
    /// </summary>
    public double? AverageValue => Votes.Average(vote => vote.Card?.Value);

    /// <summary>
    /// Активен или заверешен раунд
    /// </summary>
    public bool IsActive { get; set; }
  }
}
