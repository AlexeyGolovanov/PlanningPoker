using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Data.DTOs;
using PlanningPoker.Data.Models;
using PlanningPoker.Services;

namespace PlanningPoker.Controllers
{
  /// <summary>
  /// Контроллер раундов
  /// </summary>
  [ApiController]
  [Route("api/[controller]")]
  public class RoundController : ControllerBase
  {
    /// <summary>
    /// Сервис работы с раундами
    /// </summary>
    private readonly RoundService roundService;

    /// <summary>
    /// Сервис работы с картами
    /// </summary>
    private readonly CardService cardService;

    /// <summary>
    /// Конструктор контроллера раундов
    /// </summary>
    /// <param name="roundService">Сервис работы с раундами</param>
    /// <param name="cardService">ервис работы с картами</param>
    public RoundController(RoundService roundService, CardService cardService)
    {
      this.roundService = roundService;
      this.cardService = cardService;
    }

    /// <summary>
    /// Начало раунда
    /// </summary>
    /// <param name="values">Оюект со значениями для старта раунда</param>
    [HttpPost("roundStart")]
    public async void Start(RoundCreation values)
    {
      TimeSpan? time = null;

      if (values.PlannedDuration.HasValue)
      {
        time = TimeSpan.FromMilliseconds(values.PlannedDuration.Value);
      }

      var currentRound = await this.roundService.Add(values.Theme, values.RoomId, time, values.DeckId);
    }

    /// <summary>
    /// Завершение раунда
    /// </summary>
    /// <param name="values">Обект со значениями для завершения раунда</param>
    [HttpPost("roundStop")]
    public async void Stop(RoundFinishing values)
    {
      await roundService.Stop(values.RoomId);
    }

    /// <summary>
    /// Получение всех раундов
    /// </summary>
    /// <returns>Список всех раундов</returns>
    [HttpGet("getAll")]
    public IEnumerable<Round> GetAll()
    {
      return this.roundService.GetAll();
    }

    /// <summary>
    /// Переигрывание раунда
    /// </summary>
    /// <param name="values">Объект со значениями для переигровки раунда</param>
    [HttpPost("roundRestart")]
    public async void Restart(UserConnection values)
    {
      await roundService.Restart(values.RoomId);
    }

    /// <summary>
    /// Выбор карты
    /// </summary>
    /// <param name="values">Оюхект со значеними для выбора карты пользователем</param>
    /// <returns>Документируемый объект сделанного выбора</returns>
    [HttpPost("vote")]
    public Vote Vote(Voting values)
    { 
      return roundService.Vote(values.RoundId, values.UserId, cardService.Get(values.CardId));
    }
  }
}
