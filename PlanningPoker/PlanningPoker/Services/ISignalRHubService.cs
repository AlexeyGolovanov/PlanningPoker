using System.Collections.Generic;
using System.Threading.Tasks;
using PlanningPoker.Data.Models;

namespace PlanningPoker.Services
{
  /// <summary>
  /// Интерфейс сервиса, работаюзего с хабом SignalR
  /// </summary>
  public interface ISignalRHubService
  {
    /// <summary>
    /// Обновление списка комнат
    /// </summary>
    /// <param name="rooms"> Список комнат </param>
    /// <returns> Без возвращаемого значения </returns>
    Task UpdateRoomList(IEnumerable<Room> rooms);

    /// <summary>
    /// Обновление списка пользователей
    /// </summary>
    /// <param name="users"> Список пользователей </param>
    /// <returns> Без возвращаемого значения </returns>
    Task UpdateUserList(IEnumerable<User> users);

    /// <summary>
    /// Информация о начале раунда
    /// </summary>
    /// <param name="users"> Список пользователей </param>
    /// <param name="round"> Рассматриваемый раунд </param>
    /// <returns> Без возвращаемого значения </returns>
    Task RoundStart(IEnumerable<User> users, Round round);

    /// <summary>
    /// Информация о заврешении раунда
    /// </summary>
    /// <param name="users"> Список пользователей </param>
    /// <param name="round"> Рассматриваемый раунд </param>
    /// <returns> Без возвращаемого значения </returns>
    Task RoundStop(IEnumerable<User> users, Round round);
  }
}
