using System;
using System.Collections.Generic;

namespace PlanningPoker.Data.Models
{
  /// <summary>
  /// Класс описывающий комнату
  /// </summary>
  public class Room : IEntity
  {
    /// <summary>
    /// Конструктор объекта комнаты
    /// </summary>
    /// <param name="name">Название комнаты</param>
    /// <param name="owner">Владелец комнаты</param>
    /// <param name="defaultDeck">Использумая колода</param>
    public Room(string name, User owner, Deck defaultDeck)
    {
      this.Id = Guid.NewGuid();
      this.Name = name;
      this.Owner = owner;
      this.Deck = defaultDeck;
      this.Users = new List<User>();
      this.Rounds = new List<Round>();
    }

    /// <summary>
    /// Идентификатор Комнаты
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Название комнаты
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Владелец комнаты
    /// </summary>
    public User Owner { get; set; }

    /// <summary>
    /// Список пользователей в комнате
    /// </summary>
    public ICollection<User> Users { get; set; }

    /// <summary>
    /// Используемая колода
    /// </summary>
    public Deck Deck { get; set; }

    /// <summary>
    /// Список прошедгих раундов
    /// </summary>
    public ICollection<Round> Rounds { get; set; }

    /// <summary>
    /// Активный в данный момент раунд
    /// </summary>
    public Round ActiveRound { get; set; }
  }
}
