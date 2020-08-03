using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Data.DTOs;
using PlanningPoker.Data.Models;
using PlanningPoker.Services;

namespace PlanningPoker.Controllers
{
  /// <summary>
  /// Контроллер пользователей
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    /// <summary>
    /// Сервис работы с пользователями
    /// </summary>
    private readonly UserService userService;

    /// <summary>
    /// Конструктор контроллера пользователей
    /// </summary>
    /// <param name="userService">Сервис работы с пользователями</param>
    public UserController(UserService userService)
    {
      this.userService = userService;
    }

    /// <summary>
    /// Создание нового пользователя
    /// </summary>
    /// <param name="values">Объект с данными для создания пользователя</param>
    /// <returns>Созданный объект пользователя</returns>
    [HttpPost("createUser")]
    public User CreateUser(UserCreation values)
    {
      if (userService.GetAll().FirstOrDefault(user => user.Id == values.UserId) != null)
      {
        if (values.UserId != null)
        {
          return this.userService.Reconnect(values.UserId.Value, values.ConnectionId);
        }
      }

      return userService.Add(values.UserName, values.ConnectionId);
    }

    /// <summary>
    /// Получение пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Найденный пользователь</returns>
    [HttpGet("get/{id}")]
    public User GetUser(string id)
    {
      return userService.Get(new Guid(id));
    }

    /// <summary>
    /// получение списка всех пользователей
    /// </summary>
    /// <returns>Список всех пользователей</returns>
    [HttpGet("getAll")]
    public IEnumerable<User> GetAll()
    {
      return userService.GetAll();
    }
  }
}
