using System;

namespace PlanningPoker.Data.DTOs
{
  /// <summary>
  /// Класс содержащий информауию о созданной комнате
  /// </summary>
  public class CreatedRoom
  {
    /// <summary>
    /// конструктор класса 
    /// </summary>
    /// <param name="id">Идентификатор комнаты</param>
    /// <param name="name">Название комнаты</param>
    public CreatedRoom(Guid id, string name)
    {
      this.Id = id;
      this.Name = name;
    }

    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название комнаты
    /// </summary>
    public string Name { get; set; }
  }
}
