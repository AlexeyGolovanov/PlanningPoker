using System.Linq;
using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;

namespace PlanningPoker.Tests
{
  /// <summary>
  /// Класс с расширением для интерфеса IRepository
  /// </summary>
  public static class RepositoryClearExtension
  {
    /// <summary>
    /// Метод, осуществляющий очистку репозитория
    /// </summary>
    /// <typeparam name="T"> Тип хранимых в репозитории сущностей </typeparam>
    /// <param name="repository"> Репозиторий для очистки </param>
    public static void Clear<T>(this IRepository<T> repository) where T : IEntity
    {
      var allEntities = repository.GetAll().ToList();

      foreach (var entity in allEntities)
      {
        repository.Remove(entity.Id);
      }
    }
  }
}
