using System.Linq;
using NUnit.Framework;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;
using PlanningPoker.Services;

namespace PlanningPoker.Tests
{
  /// <summary>
  /// Класс содержащий тесты сервиса работы с пользователями
  /// </summary>
  public class UserTests
  {
    private readonly IRepository<Room> rooms = new RoomRepository();
    private readonly IRepository<Round> rounds = new RoundRepository();
    private readonly IRepository<User> users = new UserRepository();
    private readonly IRepository<Deck> decks = new DeckRepository();
    private readonly IRepository<Card> cards = new CardRepository();

    /// <summary>
    /// Сервис работы с пользователями
    /// </summary>
    private UserService userService;

    /// <summary>
    /// подготовка сервисов
    /// </summary>
    [SetUp]
    public void InitializeServices()
    {
      this.userService = new UserService(this.users);
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
    /// Тест добавления пользователя
    /// </summary>
    [Test]
    public void AddUser()
    {
      this.userService.Add("UserName", "ConnectionId");
      Assert.AreEqual(1, this.userService.GetAll().Count());
    }

    /// <summary>
    /// Тест корректности полей создаваемого пользователя
    /// </summary>
    [Test]
    public void CorrectUserValues()
    {
      var user = this.userService.Add("UserName", "ConnectionId");

      Assert.AreEqual("UserName", user.Name);
      Assert.AreEqual("ConnectionId", user.ConnectionId);
    }

    /// <summary>
    /// Тест удаления пользователя
    /// </summary>
    [Test]
    public void DeleteUser()
    {
      var user = this.userService.Add("UserName", "ConnectionId");
      Assert.AreEqual(1, this.userService.GetAll().Count());

      this.userService.Remove(user.Id);
      Assert.AreEqual(0, this.userService.GetAll().Count());
    }

    /// <summary>
    /// Проверка корректности получения пользователя
    /// </summary>
    [Test]
    public void GetUser()
    {
      var userCreated = this.userService.Add("UserName", "ConnectionId");
      var userGot = this.userService.Get(userCreated.Id);

      Assert.AreEqual(userCreated, userGot);

      Assert.AreNotEqual(this.userService.Add("UserName2", "ConnectionId2"), userGot);
    }

    /// <summary>
    /// Проверка метода получения списка всех пользователей
    /// </summary>
    [Test]
    public void GetAllUser()
    {
      Assert.IsEmpty(this.userService.GetAll());

      var user1 = this.userService.Add("User 1", "Connection 1");
      this.userService.Add("User 2", "Connection 2");
      this.userService.Add("User 3", "Connection 3");

      Assert.AreEqual(3, this.userService.GetAll().Count());

      this.userService.Remove(user1.Id);
      Assert.AreEqual(2, this.userService.GetAll().Count());
    }
  }
}