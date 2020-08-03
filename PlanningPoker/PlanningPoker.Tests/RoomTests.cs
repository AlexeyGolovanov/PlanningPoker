using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;
using PlanningPoker.Services;

namespace PlanningPoker.Tests
{
  /// <summary>
  /// Класс содержащийесты для сервиса работы с комнатами
  /// </summary>
  public class RoomTests
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
    /// Сервис работы с пользователями
    /// </summary>
    private UserService userService;

    /// <summary>
    /// Сервис работы с колодами
    /// </summary>
    private DeckService deckService;

    /// <summary>
    /// сервис работы с комнатами
    /// </summary>
    private RoomService roomService;

    /// <summary>
    /// подготовка сервисов
    /// </summary>
    [SetUp]
    public void InitializeServices()
    {
      this.hubService = new Mock<ISignalRHubService>();
      this.userService = new UserService(this.users);
      this.roomService = new RoomService(this.hubService.Object, this.rooms, this.users);
      this.deckService = new DeckService(this.decks);
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
    /// Тестирование работы добавление комнаты
    /// </summary>
    [Test]
    public void AddRoom()
    {
      this.roomService.Add("Room", new User("Owner", "ConnectionId"), new Deck("Empty deck"));
      Assert.AreEqual(1, this.roomService.GetAll().Count());
    }

    /// <summary>
    /// Тест корректности значений созданной комнаты
    /// </summary>
    [Test]
    public void CorrectRoomValues()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var deck = this.deckService.Add("Empty deck");
      var room = this.roomService.Add("Room", owner, deck);

      Assert.AreEqual("Room", room.Name);
      Assert.AreEqual(owner, room.Owner);
      Assert.AreEqual(deck, room.Deck);
    }

    /// <summary>
    /// Тест удаления комнаты
    /// </summary>
    [Test]
    public void DeleteRoom()
    {
      Assert.AreEqual(0, this.roomService.GetAll().Count());

      var room = this.roomService.Add("Room", new User("Owner", "ConnectionId"), new Deck("Empty Deck"));

      Assert.AreEqual(1, this.roomService.GetAll().Count());
      this.roomService.Remove(room.Id);
      Assert.AreEqual(0, this.roomService.GetAll().Count());
    }

    /// <summary>
    /// Тест корректности получения комнаты
    /// </summary>
    [Test]
    public void GetRoom()
    {
      var roomCreated = this.roomService.Add("Room", new User("Owner", "ConnectionId"), new Deck("Empty Deck"));
      var roomGot = this.roomService.Get(roomCreated.Id);
      Assert.AreEqual(roomCreated, roomGot);
    }

    /// <summary>
    /// Тест корректности получения списка всез комнат
    /// </summary>
    [Test]
    public void GetAllRooms()
    {
      Assert.AreEqual(0, this.roomService.GetAll().Count());
      this.roomService.Add("Room1", new User("Owner1", "ConnectionId1"), new Deck("deck1"));
      this.roomService.Add("Room2", new User("Owner2", "ConnectionId2"), new Deck("deck2"));
      this.roomService.Add("Room3", new User("Owner3", "ConnectionId3"), new Deck("deck3"));
      Assert.AreEqual(3, this.roomService.GetAll().Count());
    }

    /// <summary>
    /// Тест работы метода полключения пользователя к комнате
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task UserJoin()
    {
      var owner = this.userService.Add("Owner", "ConnectionId");
      var room = this.roomService.Add("Room", owner, new Deck("deck"));

      Assert.AreEqual(0, room.Users.Count);
      await this.roomService.UserJoin(owner.Id, room.Id);
      Assert.AreEqual(1, room.Users.Count);
    }

    /// <summary>
    /// Тест работы метода удалябщего пользователя из комнаты
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task RemoveUser()
    {
      var room = this.roomService.Add("Room", new User("Owner", "ConnectionId"), new Deck("Empty Deck"));
      var user = this.userService.Add("Name", "ConnectionId2");
      await this.roomService.UserJoin(user.Id, room.Id);
      Assert.AreEqual(1, this.roomService.Get(room.Id).Users.Count);

      this.roomService.UserLeave(user.Id, room.Id);

      Assert.AreEqual(0, this.roomService.Get(room.Id).Users.Count);
    }
  }
}
