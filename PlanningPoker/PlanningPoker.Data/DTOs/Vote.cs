#nullable enable
using PlanningPoker.Data.Models;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс описывающий выбор пользователя
  /// </summary>
  public class Vote
  {
    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="user">Голосовавший пользователь</param>
    /// <param name="card">Выбранная карта</param>
    public Vote(User user, Card? card)
    {
      this.User = user;
      this.Card = card;
    }

    /// <summary>
    /// Голосовавший пользователь
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Выбранная карта
    /// </summary>
    public Card? Card { get; set; }
  }
}
