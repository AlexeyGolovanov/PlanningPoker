using System;
using System.Collections.Generic;

namespace PlanningPoker.Data.Models
{
  /// <summary>
  /// Объект описываюзий колоду
  /// </summary>
  public class Deck : IEntity
  {
    /// <summary>
    /// Конструктор колоды
    /// </summary>
    /// <param name="name">Название колоды</param>
    public Deck(string name)
    {
      this.Id = Guid.NewGuid();
      this.Name = name;
      this.Cards = new List<Card>();
    }

    /// <summary>
    /// Идентификатор колоды
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Название колоды
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Коллекция карт
    /// </summary>
    public ICollection<Card> Cards { get; }
  }
}
