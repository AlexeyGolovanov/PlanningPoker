using System;
using System.Collections.Generic;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;

namespace PlanningPoker.Services
{
  /// <summary>
  /// Сервис работы с картами
  /// </summary>
  public class CardService
  {
    /// <summary>
    /// Репозиторий карт
    /// </summary>
    private readonly IRepository<Card> cards;

    /// <summary>
    /// Конструктор сервиса работы с картами
    /// </summary>
    /// <param name="cards">Репозиторий карт</param>
    public CardService(IRepository<Card> cards)
    {
      this.cards = cards;
    }

    /// <summary>
    /// Добавление новой карты
    /// </summary>
    /// <param name="text">Текст карты</param>
    /// <param name="value">Численное значение карты</param>
    /// <returns>Созданная карта</returns>
    public Card Add(string text, double? value)
    {
      var card = new Card(text, value);
      this.cards.Add(card);
      return card;
    }

    /// <summary>
    /// Удаление карты
    /// </summary>
    /// <param name="id">Идентификатор карты</param>
    public void Remove(Guid id)
    {
      this.cards.Remove(id);
    }

    /// <summary>
    /// Получение карты
    /// </summary>
    /// <param name="id">Идентификатор карты</param>
    /// <returns>Найденная карта</returns>
    public Card Get(Guid id)
    {
      return this.cards.Get(id);
    }

    /// <summary>
    /// Получение списка всех карт
    /// </summary>
    /// <returns>Список всех карт</returns>
    public IEnumerable<Card> GetAll()
    {
      return this.cards.GetAll();
    }
  }
}
