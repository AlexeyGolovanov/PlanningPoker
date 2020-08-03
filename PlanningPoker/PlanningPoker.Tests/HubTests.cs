using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;
using PlanningPoker.Services;

namespace PlanningPoker.Tests
{
  /// <summary>
  /// Класс содержащий тесты для сервиса работы с хабом
  /// </summary>
  public class HubTests
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
    /// Сервис работы с комнатами
    /// </summary>
    private RoomService roomService;

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
    /// Тест работы метода, отсылаюзего клиентам обновленный список комнат
    /// </summary>
    [Test]
    public void UpdateRoomList()
    {
      this.hubService.Setup(service => service.UpdateRoomList(It.IsAny<IEnumerable<Room>>())).Verifiable();
      var owner = this.userService.Add("Name", "ConnectionId");
      this.roomService.Add("Room", owner, new Deck("Deck"));
      this.hubService.VerifyAll();
    }

    /// <summary>
    /// Тест работы метода, отсылаюзего клиентам обновленный список пользователей
    /// </summary>
    [Test]
    public async Task UpdateUserList()
    {
      this.hubService.Setup(service => service.UpdateUserList(It.IsAny<IEnumerable<User>>())).Verifiable();
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      var user = this.userService.Add("User", "ConnectionId2");
      await this.roomService.UserJoin(user.Id, room.Id);
      this.hubService.VerifyAll();
    }

    /// <summary>
    /// Тест метода оповещабщего о начале раунда
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task RoundStart()
    {
      this.hubService.Setup(service => service.RoundStart(It.IsAny<IEnumerable<User>>(), It.IsAny<Round>())).Verifiable();
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      await this.roundService.Add("Theme", room.Id);
      this.hubService.VerifyAll();
    }

    /// <summary>
    /// Тест метода оповещабщего о завершении раунда
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task RoundStop()
    {
      this.hubService.Setup(service => service.RoundStop(It.IsAny<IEnumerable<User>>(), It.IsAny<Round>())).Verifiable();
      var owner = this.userService.Add("Owner", "ConnectionID");
      var room = this.roomService.Add("Room", owner, new Deck("Deck"));
      await this.roomService.UserJoin(owner.Id, room.Id);
      await this.roundService.Add("Theme", room.Id);
      await this.roundService.Stop(room.Id);
      this.hubService.VerifyAll();
    }
  }
}