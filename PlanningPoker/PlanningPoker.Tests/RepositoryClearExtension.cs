using PlanningPoker.Data.Models;
using PlanningPoker.Data.Repositories;
using System.Linq;

namespace PlanningPoker.Tests
{
  public static class RepositoryClearExtension
  {
    public static void Clear<T>(this IRepository<T> repository) where T : IEntity
    {
      var allEntities = repository.GetAll().ToList();

      var amount = allEntities.Count;

      for (var i = 0; i < amount; i++)
      {
        repository.Remove(allEntities[0].Id);
      }
    }
  }
}
