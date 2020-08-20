#nullable enable
namespace PlanningPoker.Data.Models
{
  /// <summary>
  /// Класс описывающий выбор пользователя
  /// </summary>
  public class Vote
  {
    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="user"> Голосовавший пользователь </param>
    /// <param name="card"> Выбранная карта </param>
    public Vote(User user, Card? card)
    {
      this.User = user;
      this.Card = card;
    }

    /// <summary>
    /// Голосовавший пользователь
    /// </summary>
    public User User { get; }

    /// <summary>
    /// Выбранная карта
    /// </summary>
    public Card? Card { get; set; }
  }
}
