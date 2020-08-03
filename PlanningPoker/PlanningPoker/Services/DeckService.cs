using System;
using System.Collections.Generic;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;

namespace PlanningPoker.Services
{
  /// <summary>
  /// Сервис работы с колодами
  /// </summary>
  public class DeckService
  {
    /// <summary>
    /// Репозиторий колод
    /// </summary>
    private readonly IRepository<Deck> decks;

    /// <summary>
    /// Конструктор сервиса работы с колодами
    /// </summary>
    /// <param name="decks">Репозиторий колод</param>
    public DeckService(IRepository<Deck> decks)
    {
      this.decks = decks;
    }

    /// <summary>
    /// Добавление новой колоды
    /// </summary>
    /// <param name="name">Название колоды</param>
    /// <returns>Созданная колода</returns>
    public Deck Add(string name)
    {
      var deck = new Deck(name); 
      this.decks.Add(deck);
      return deck;
    }

    /// <summary>
    /// Удаление колоды
    /// </summary>
    /// <param name="id">Идентификатор колоды</param>
    public void Remove(Guid id)
    {
      this.decks.Remove(id);
    }

    /// <summary>
    /// Получение колоды из имеющихся
    /// </summary>
    /// <param name="id">Идентификатор колоды</param>
    /// <returns>Найденная колода</returns>
    public Deck Get(Guid id)
    {
      return this.decks.Get(id);
    }

    /// <summary>
    /// Получения списка всех колод
    /// </summary>
    /// <returns>Список всех колод</returns>
    public IEnumerable<Deck> GetAll()
    {
      return this.decks.GetAll();
    }

    /// <summary>
    /// Добавление карты в колоду
    /// </summary>
    /// <param name="deckId">Идентификатор колоды</param>
    /// <param name="card">Карта для добавления</param>
    public void AddCard(Guid deckId, Card card)
    {
      var deck = this.decks.Get(deckId);
      deck.Cards.Add(card);
    }
  }
}
