using System;
using System.Collections.Generic;
using PlanningPoker.Data.Models;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс со значениями для информирования о завершении раунда
  /// </summary>
  public class RoundStopInfo
  {
    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="id"> Идентификатор раунда </param>
    /// <param name="theme"> Тема раунда </param>
    /// <param name="votes"> Список голосов </param>
    /// <param name="averageValue"> Среднее значение голосов </param>
    public RoundStopInfo(Guid id, string theme, IEnumerable<Vote> votes, double? averageValue)
    {
      this.Id = id;
      this.Theme = theme;
      this.Votes = votes;
      this.AverageValue = averageValue;
    }

    /// <summary>
    /// Идентификатор раунда
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Тема раунда
    /// </summary>
    public string Theme { get; set; }

    /// <summary>
    /// Список голосов
    /// </summary>
    public IEnumerable<Vote> Votes { get; set; }

    /// <summary>
    /// Среднее значение голосов
    /// </summary>
    public double? AverageValue { get; set; }
  }
}
