using System;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс со значениями для информирования о начале раунда
  /// </summary>
  public class RoundStartInfo
  {
    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="id"> Идентификатор раунда </param>
    /// <param name="theme"> Тема раунда </param>
    public RoundStartInfo(Guid id, string theme)
    {
      this.Id = id;
      this.Theme = theme;
    }

    /// <summary>
    /// Идентификатор раунда
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Тема раунда
    /// </summary>
    public string Theme { get; set; }
  }
}
