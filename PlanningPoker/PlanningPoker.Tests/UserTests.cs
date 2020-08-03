using System.Linq;
using NUnit.Framework;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;
using PlanningPoker.Services;

namespace PlanningPoker.Tests
{
  /// <summary>
  /// ����� ���������� ����� ������� ������ � ��������������
  /// </summary>
  public class UserTests
  {
    private readonly IRepository<Room> rooms = new RoomRepository();
    private readonly IRepository<Round> rounds = new RoundRepository();
    private readonly IRepository<User> users = new UserRepository();
    private readonly IRepository<Deck> decks = new DeckRepository();
    private readonly IRepository<Card> cards = new CardRepository();

    /// <summary>
    /// ������ ������ � ��������������
    /// </summary>
    private UserService userService;

    /// <summary>
    /// ���������� ��������
    /// </summary>
    [SetUp]
    public void InitializeServices()
    {
      this.userService = new UserService(this.users);
    }

    /// <summary>
    /// ������� ������������
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
    /// ���� ���������� ������������
    /// </summary>
    [Test]
    public void AddUser()
    {
      this.userService.Add("UserName", "ConnectionId");
      Assert.AreEqual(1, this.userService.GetAll().Count());
    }

    /// <summary>
    /// ���� ������������ ����� ������������ ������������
    /// </summary>
    [Test]
    public void CorrectUserValues()
    {
      var user = this.userService.Add("UserName", "ConnectionId");

      Assert.AreEqual("UserName", user.Name);
      Assert.AreEqual("ConnectionId", user.ConnectionId);
    }

    /// <summary>
    /// ���� �������� ������������
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
    /// �������� ������������ ��������� ������������
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
    /// �������� ������ ��������� ������ ���� �������������
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