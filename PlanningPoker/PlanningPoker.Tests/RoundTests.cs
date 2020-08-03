using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;
using PlanningPoker.Services;

namespace PlanningPoker.Tests
{
  /// <summary>
  /// Класс содержащий тесты для сервиса работы с раундами
  /// </summary>
  public class RoundTests
  {
    private readonly IRepository<Room> rooms = new RoomRepository();
    private readonly IRepository<Round> rounds = new RoundRepository();
    private readonly IRepository<User> users = new UserRepository();
    private readonly IRepository<Deck> decks = new DeckRepository();
    private readonly IRepository<Card> cards = new CardRepository();

    /// <summary>
    /// Сервис работы с хабом
    /// </summary>
    private Mock<ISignalRHubService> hubService;

    /// <summary>
    /// Сервис работы с раундами
    /// </summary>
    private RoundService roundService;

    /// <summary>
    /// Сервис работы с пользователями
    /// </summary>
    private UserService userService;

    /// <summary>
    /// Сервис работы с колодами
    /// </summary>
    private DeckService deckService;

    /// <summary>
    /// Сервис работы с комнатами
    /// </summary>
    private RoomService roomService;

    /// <summary>
    /// Сервис работы с картами
    /// </summary>
    private CardService cardService;

    /// <summary>
    /// Подготовка сервисов
    /// </summary>
    [SetUp]
    public void InitializeServices()
    {
      this.hubService = new Mock<ISignalRHubService>();
      this.userService = new UserService(this.users);
      this.roomService = new RoomService(this.hubService.Object, this.rooms, this.users);
      this.roundService = new RoundService(this.hubService.Object, new TimerService(), this.rooms, this.rounds, this.users, this.decks);
      this.deckService = new DeckService(this.decks);
      this.cardService = new CardService(this.cards);
    }

    /// <summary>
    /// Очистка репозиториев
    /// </summary>
    [TearDown]
    public void ClearRepositories()
    {
      this.rooms.Clear();
      this.rounds.Clear();
      this.users.Clear();
      this.decks.Clear();
      this.cards.Clear();
    }

    /// <summary>
    /// Тест работы метода добавления раунда
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task AddRound()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      await this.roundService.Add("Theme", room.Id);

      Assert.AreEqual(1, this.roundService.GetAll().Count());
    }

    /// <summary>
    /// Проверка корректности значений создаваемого раунда
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task CorrectRoundValues()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      var duration = TimeSpan.FromMilliseconds(30000);
      var round = await this.roundService.Add("theme", room.Id, duration);
      var roundGot = this.roundService.Get(round.Id);
      var time = DateTime.Now;
      Assert.AreEqual("theme", roundGot.Theme);
      Assert.AreEqual(time.Minute, roundGot.StartTime.Minute);
    }

    /// <summary>
    /// Тест создания раунда с таймером
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task AddRoundWithTimer()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      var duration = TimeSpan.FromMilliseconds(30000);
      var round = await this.roundService.Add("theme", room.Id, duration);

      Assert.AreEqual(30000, round.PlannedDuration.Value.TotalMilliseconds);
    }

    /// <summary>
    /// Тест удаления раунда
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task DeleteRound()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      var round = await this.roundService.Add("theme", room.Id);
      this.roundService.Remove(round.Id);

      Assert.AreEqual(0, this.roundService.GetAll().Count());
    }

    /// <summary>
    /// Тест работы метода переигровки раунда
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task RoundRestart()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      var round = await this.roundService.Add("theme", room.Id);
      await this.roundService.Stop(room.Id);

      Assert.AreEqual(false, round.IsActive);
      Assert.IsTrue(room.Rounds.Contains(round));

      var roundNew = await this.roundService.Restart(room.Id);

      Assert.AreEqual(0, room.Rounds.Count);
      Assert.AreEqual(null, this.roundService.Get(round.Id));

      Assert.AreEqual(round.Theme, roundNew.Theme);
      Assert.AreEqual(round.PlannedDuration, roundNew.PlannedDuration);
    }

    /// <summary>
    /// Тест работы метода получения раунда
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task GetRound()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      var round = await this.roundService.Add("theme", room.Id);
      Assert.AreEqual(round, this.roundService.Get(round.Id));
    }

    /// <summary>
    /// Тест работы метода получения списка всех раундов
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task GetAllRounds()
    {
      var owner = this.userService.Add("Creator", "TestConnectionId");
      var room = this.roomService.Add("TestRoomName", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      await this.roundService.Add("theme1", room.Id);
      await this.roundService.Stop(room.Id);
      await this.roundService.Add("theme2", room.Id);
      await this.roundService.Stop(room.Id);

      Assert.AreEqual(2, this.roundService.GetAll().Count());
    }

    /// <summary>
    /// Тест метода завершающего раунд
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task StopRound()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      var round = await this.roundService.Add("Topic #1", room.Id);
      await this.roundService.Stop(room.Id);

      Assert.AreEqual(false, round.IsActive);
      Assert.IsTrue(room.Rounds.Contains(round));
    }

    /// <summary>
    /// Проверка корректности завершения раунда по истечению таймера
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task RoundTimeEnd()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      var duration = TimeSpan.FromMilliseconds(5);
      var round = await this.roundService.Add("Theme", room.Id, duration);

      Thread.Sleep(20);
      Assert.AreEqual(false, round.IsActive);
      Assert.IsTrue(room.Rounds.Contains(round));
    }

    /// <summary>
    /// Тест корректности голосования и его сохранения
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task VotingTest()
    {
      this.CreateDeck();

      var deck = this.deckService.GetAll().ToList()[0];

      var owner = this.userService.Add("Owner", "ConnectionId");
      var user = this.userService.Add("User", "ConnectionId2");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      await this.roomService.UserJoin(user.Id, room.Id);
      var round = await this.roundService.Add("theme", room.Id);

      var card1 = this.cardService.Get(deck.Cards.ElementAt(0).Id);
      this.roundService.Vote(round.Id, owner.Id, card1);

      var card2 = this.cardService.Get(deck.Cards.ElementAt(1).Id);
      this.roundService.Vote(round.Id, user.Id, card2);

      await this.roundService.Stop(room.Id);

      var ownerVote = round.Votes.First();

      Assert.AreEqual(owner, ownerVote.User);
      Assert.AreEqual(card1, ownerVote.Card);

      var userVote = round.Votes.ElementAt(1);
      Assert.AreEqual(user, userVote.User);
      Assert.AreEqual(card2, userVote.Card);
    }

    /// <summary>
    /// Метод создабщий тестовую колоду
    /// </summary>
    public void CreateDeck()
    {
      var deck = this.deckService.Add("deck 1");
      var card1 = this.cardService.Add("Один", 1);
      var card2 = this.cardService.Add("Два", 2);
      var card3 = this.cardService.Add("Три", 3);
      this.deckService.AddCard(deck.Id, card1);
      this.deckService.AddCard(deck.Id, card2);
      this.deckService.AddCard(deck.Id, card3);
    }
  }
}
