using System;

namespace PlanningPoker.Data.Models
{
  /// <summary>
  /// Объект описывабхий карту
  /// </summary>
  public class Card : IEntity
  {
    /// <summary>
    /// Конструктор карты
    /// </summary>
    /// <param name="text">Текст карты</param>
    /// <param name="value">Численное значение карты</param>
    public Card(string text, double? value)
    {
      this.Id = Guid.NewGuid();
      this.Text = text;
      this.Value = value;
    }

    /// <summary>
    /// Идентификатор карты
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Текст карты
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Численное значение карты
    /// </summary>
    public double? Value { get; set; }
  }
}
