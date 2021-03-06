﻿using System;
using System.Collections.Generic;

namespace PlanningPoker.Data.Repositories
{
  /// <summary>
  /// Общий интерфейс для работы с репозиториями
  /// </summary>
  /// <typeparam name="T"> Тип хранимых объектов </typeparam>
  public interface IRepository<T>
  {
    /// <summary>
    /// Добавить объект в коллекцию
    /// </summary>
    /// <param name="entity"> Добавляемый объект </param>
    void Add(T entity);

    /// <summary>
    /// Удалить объект из коллекции
    /// </summary>
    /// <param name="id"> Идентификатор объекта </param>
    void Remove(Guid id);

    /// <summary>
    /// Получить объект из коллекции
    /// </summary>
    /// <param name="id"> Идентификатор объекта </param>
    /// <returns> Найденный объект </returns>
    T Get(Guid id);

    /// <summary>
    /// Получит всю коллекцию объектов
    /// </summary>
    /// <returns> Полученная коллекция </returns>
    IEnumerable<T> GetAll();
  }
}
