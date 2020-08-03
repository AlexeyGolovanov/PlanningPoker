using System;
using System.Collections.Generic;
using System.Linq;
using PlanningPoker.Data.Models;

namespace PlanningPoker.Data.Repositories
{
  /// <summary>
  /// Класс базового репозитория
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class BaseRepository<T> : IRepository<T> where T : IEntity
  {
    /// <summary>
    /// Коллекция хранимых сущностей
    /// </summary>
    private static readonly ICollection<T> EntityList = new List<T>();

    /// <summary>
    /// Добавить экземпляр в коллекци.
    /// </summary>
    /// <param name="entity">Добавляемый экземпляр</param>
    public void Add(T entity)
    {
      EntityList.Add(entity);
    }

    /// <summary>
    /// Удалить оюъект из коллекции
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    public void Remove(Guid id)
    {
      EntityList.Remove(this.Get(id));
    }

    /// <summary>
    /// Получить объект их коллекции
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Найденный объект</returns>
    public T Get(Guid id)
    {
      return EntityList.FirstOrDefault(entity => entity.Id == id);
    }

    /// <summary>
    /// Получит всю коллекцию объектов
    /// </summary>
    /// <returns>Полученная коллекция</returns>
    public IEnumerable<T> GetAll()
    {
      return EntityList;
    }

    /// <summary>
    /// Очистить коллекцию
    /// </summary>
    public void Clear()
    {
      EntityList.Clear();
    }
  }
}
