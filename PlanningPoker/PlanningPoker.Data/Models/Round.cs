using System;
using System.Collections.Generic;
using PlanningPoker.Data.DTOs;

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
    /// <param name="theme">Тема раунда</param>
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
    public DateTime StartTime { get;  }

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
    public double? AverageValue => this.GetAverage();

    /// <summary>
    /// Идет раунд или заверешен
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// метод подсчета среднего значения голоса
    /// </summary>
    /// <returns>Среднее значение голоса</returns>
    private double? GetAverage()
    {
      double sum = 0;
      var amount = 0;
      foreach (var vote in this.Votes)
      {
        if (vote.Card?.Value != null)
        {
          sum += (double)vote.Card.Value;
          amount++;
        }
      }

      if (amount == 0)
      {
        return null;
      }

      return sum / amount;
    }
  }
}
