#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;

namespace PlanningPoker.Services
{
  /// <summary>
  /// Сервис работы с раундами
  /// </summary>
  public class RoundService
  {
    /// <summary>
    /// Сервис работы с хабом SignalR
    /// </summary>
    private readonly ISignalRHubService hub;

    /// <summary>
    /// Сервис работы с таймером
    /// </summary>
    private readonly TimerService timerService;

    /// <summary>
    /// Репозиторий комнат
    /// </summary>
    private readonly IRepository<Room> rooms;

    /// <summary>
    /// Репозиторий раундов
    /// </summary>
    private readonly IRepository<Round> rounds;

    /// <summary>
    /// Репозиторий пользователей
    /// </summary>
    private readonly IRepository<User> users;

    /// <summary>
    /// Репозиторий колод
    /// </summary>
    private readonly IRepository<Deck> decks;

    /// <summary>
    /// Конструктор сервиса работы с раундами
    /// </summary>
    /// <param name="hub"> Сервис работы с хабом SignalR </param>
    /// <param name="timerService"> Сервис работы с таймером </param>
    /// <param name="rooms"> Репозиторий комнат </param>
    /// <param name="rounds"> Репозиторий раундов </param>
    /// <param name="users"> Репозиторий пользователей </param>
    /// <param name="decks"> Репозиторий колод </param>
    public RoundService(ISignalRHubService hub, TimerService timerService, IRepository<Room> rooms, IRepository<Round> rounds, 
      IRepository<User> users, IRepository<Deck> decks)
    {
      this.hub = hub;
      this.timerService = timerService;
      this.rooms = rooms;
      this.rounds = rounds;
      this.users = users;
      this.decks = decks;
    }

    /// <summary>
    /// Создание нового раунда
    /// </summary>
    /// <param name="theme"> Тема раунда </param>
    /// <param name="roomId"> Идентификатор комнаты </param>
    /// <param name="time"> Время обсуждения </param>
    /// <param name="deckId"> Идентификатор колоды </param>
    /// <returns></returns>
    public async Task<Round> Add(string theme, Guid roomId, TimeSpan? time = null, Guid? deckId = null)
    {
      var round = new Round(theme);

      var room = this.rooms.Get(roomId);

      this.rounds.Add(round);

      room.ActiveRound = round;

      if (deckId.HasValue)
      {
        room.Deck = this.decks.Get(deckId.Value);
      }

      if (time.HasValue)
      {
        room.ActiveRound.PlannedDuration = time;

        var timer = new Timer(time.Value.TotalMilliseconds);

        timer.Elapsed += async (o, e) =>
        {
          await this.Stop(roomId);
          timer.Stop();
        };

        this.timerService.Start(roomId, round.Id, timer);
      }

      await this.hub.RoundStart(room.Users, room.ActiveRound);

      return round;
    }

    /// <summary>
    /// Получение раунда
    /// </summary>
    /// <param name="id"> Идентификатор раунда </param>
    /// <returns> Найденный раунд </returns>
    public Round Get(Guid id)
    {
      return this.rounds.Get(id);
    }

    /// <summary>
    /// Получение списка всех раундов
    /// </summary>
    /// <returns> Список всех раундов </returns>
    public IEnumerable<Round> GetAll()
    {
      return this.rounds.GetAll();
    }

    /// <summary>
    /// Завршение раунда
    /// </summary>
    /// <param name="roomId"> Идентификатор комнаты </param>
    /// <returns> Активный в настоящий момент раунд в указанной комнате </returns>
    public async Task<Round> Stop(Guid roomId)
    {
      var room = this.rooms.Get(roomId);

      if (room.ActiveRound.PlannedDuration.HasValue)
      {
        this.timerService.Stop(room.ActiveRound.Id);
      }

      room.ActiveRound.EndTime = DateTime.Now;
      room.ActiveRound.IsActive = false;
      room.Rounds.Add(room.ActiveRound);

      await this.hub.RoundStop(room.Users, room.ActiveRound);
      return room.ActiveRound;
    }

    /// <summary>
    /// Удаление раунда
    /// </summary>
    /// <param name="roundId"> Идентификатор раунда </param>
    public void Remove(Guid roundId)
    {
      this.rounds.Remove(roundId);
    }

    /// <summary>
    /// Выбор карты пользователем
    /// </summary>
    /// <param name="roundId"> Идентификатор раунда </param>
    /// <param name="userId"> Идентификатор пользователя </param>
    /// <param name="card"> Выбранная карта </param>
    /// <returns> Документируемый объект голоса </returns>
    public Vote Vote(Guid roundId, Guid userId, Card card)
    {
      var round = this.rounds.Get(roundId);

      var vote = round.Votes.FirstOrDefault(item => item.User.Id == userId);

      if (vote == null)
      {
        var choice = new Vote(this.users.Get(userId), card);
        round.Votes.Add(choice);
        return choice;
      }

      vote.Card = card;
      return vote;
    }

    /// <summary>
    /// Переигровка раунда
    /// </summary>
    /// <param name="roomId"> Идентификатор комнаты </param>
    /// <returns> Пересозданный раунд </returns>
    public async Task<Round> Restart(Guid roomId)
    {
      var room = this.rooms.Get(roomId);

      var round = room.ActiveRound;

      room.Rounds.Remove(round);

      this.Remove(round.Id);

      var newRound = await this.Add(round.Theme, roomId, round.PlannedDuration);

      return newRound;
    }
  }
}
