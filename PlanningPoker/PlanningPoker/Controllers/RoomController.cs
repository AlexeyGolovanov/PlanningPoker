using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Data.DTOs;
using PlanningPoker.Data.Models;
using PlanningPoker.Services;

namespace PlanningPoker.Controllers
{
  /// <summary>
  /// Контроллер комнат
  /// </summary>
  [ApiController]
  [Route("api/[controller]")]
  public class RoomsController : ControllerBase
  {
    /// <summary>
    /// Сервис работы с комнатами
    /// </summary>
    private readonly RoomService roomService;

    /// <summary>
    /// Сервис работы с колодами
    /// </summary>
    private readonly DeckService deckService;

    /// <summary>
    /// Сервис работы с пользователями
    /// </summary>
    private readonly UserService userService;

    /// <summary>
    /// Конструктор контроллера комнат
    /// </summary>
    /// <param name="roomService">Сервис работы с комнатами</param>
    /// <param name="deckService">Сервис работы с колодами</param>
    /// <param name="userService">Сервис работы с пользователями</param>
    public RoomsController(RoomService roomService, DeckService deckService, UserService userService)
    {
      this.roomService = roomService;
      this.deckService = deckService;
      this.userService = userService;
    }

    /// <summary>
    /// Добавление новой комнаты
    /// </summary>
    /// <param name="values">Объект с информацией для создания комнаты</param>
    /// <returns>Объект с информацией о созданной комнате</returns>
    [HttpPost("create")]
    public CreatedRoom AddRoom(RoomCreation values)
    {
      var createdRoom = roomService.Add(values.RoomName, this.userService.Get(values.UserId), this.deckService.Get(values.DeckId));
      return new CreatedRoom(createdRoom.Id, createdRoom.Name);
    }

    /// <summary>
    /// Получение комнаты
    /// </summary>
    /// <param name="id"> Идентификатор комнаты</param>
    /// <returns>Объект найденной комнаты</returns>
    [HttpGet("get/{id}")]
    public Room GetRoom(Guid id)
    {
      return this.roomService.Get(id);
    }

    /// <summary>
    /// Получение списка всех комнат
    /// </summary>
    /// <returns>Список всех комнат</returns>
    [HttpGet("getAll")]
    public IEnumerable<Room> GetAllRoom()
    {
      return this.roomService.GetAll();
    }

    /// <summary>
    /// Подключение полбзователя к комнате
    /// </summary>
    /// <param name="values">Оюхект с информацией для подключения пользователя к комнате</param>
    /// <returns>Обхект комнаты, к которой произошло подключение</returns>
    [HttpPost("UserJoin")]
    public async Task<Room> JoinUser(UserConnection values)
    {
      return await this.roomService.UserJoin(values.UserId, values.RoomId);
    }
  }
}