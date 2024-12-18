using Bets.Abstractions.Model;
using BetsService.Models;
using BetsService.Services;
using Bets.Abstractions.Domain.Repositories.ModelRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using BetsService.Api.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using System;

namespace BetsService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly EventsService _service;
        private readonly IDistributedCache _cache;

        public EventsController(ILogger<EventsController> logger
            , EventsService service
            , IDistributedCache cache)
        {
            _logger = logger;
            _service = service;
            _cache = cache;
        }

        /// <summary>
        /// Добавить событие
        /// </summary>
        /// <param name="request">Содержит описание события, время его начала и время окончания приема ставок</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор нового события</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddEventAsync([FromBody] EventRequest request
            , CancellationToken ct)
        {
            try
            {
                var EventId = await _service.AddEventAsync(request, ct);
                return Ok(CreateResponse.CreateSuccessResponse(EventId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(CreateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Получение события по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        /// <returns>EventResponse</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEventAsync([FromRoute] Guid id)
        {
            try
            {
                var cachKey = "Event." + id;
                //var result = await _cache.GetOrSetAsync(cachKey,
                //    async () => await _service.GetEventAsync(id),
                //    _logger);
                var result =
                    new EventResponse
                    {
                        Id = new Guid("b314e08e-5e6c-4215-b8df-506e884960f7"),
                        Description = "test1",
                        EventStartTime = DateTime.Now,
                        BetsEndTime = DateTime.Now.AddDays(2),
                        EventOutcomes = new List<EventOutcomeResponse>() {
                            new EventOutcomeResponse() {
                                Id = new Guid(),
                                EventId = new Guid("b314e08e-5e6c-4215-b8df-506e884960f7"),
                                Description = "Out 1", BetsClosed = false, CurrentOdd = 2},
                            new EventOutcomeResponse() {
                                    Id = new Guid(),
                                    EventId = new Guid("b314e08e-5e6c-4215-b8df-506e884960f7"),
                                    Description = "Out 2", BetsClosed = false, CurrentOdd = 2}
                            }
                    };
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех событий
        /// </summary>
        /// <returns>List of EventResponse</returns>
        [HttpGet]
        public async Task<IActionResult> GetListEventsAsync()
        {
            try
            {
                var result = await _service.GetListEventsAsync();
                result = new List<EventResponse>() {
                    new EventResponse {
                        Id = new Guid("b314e08e-5e6c-4215-b8df-506e884960f7"),
                        Description = "test1",
                        EventStartTime = DateTime.Now,
                        BetsEndTime = DateTime.Now.AddDays(2),
                        Status = 0,
                        EventOutcomes = new List<EventOutcomeResponse>() {
                            new EventOutcomeResponse() {
                                Id = new Guid(),
                                EventId = new Guid("b314e08e-5e6c-4215-b8df-506e884960f7"),
                                Description = "Out 1", BetsClosed = false, CurrentOdd = 2},
                            new EventOutcomeResponse() {
                                    Id = new Guid(),
                                    EventId = new Guid("b314e08e-5e6c-4215-b8df-506e884960f7"),
                                    Description = "Out 1", BetsClosed = false, CurrentOdd = 2 }
                        }
                    },
                        new EventResponse {
                        Id = new Guid("b314e08e-5e6c-4215-b8df-506e884960f0"),
                        Description = "test12",
                        EventStartTime = DateTime.Now,
                        BetsEndTime = DateTime.Now.AddDays(2),
                        Status = 0,
                        EventOutcomes = new List<EventOutcomeResponse>() {
                            new EventOutcomeResponse() {
                                Id = new Guid(),
                                EventId = new Guid("b314e08e-5e6c-4215-b8df-506e884960f0"),
                                Description = "Out 1", BetsClosed = false, CurrentOdd = 2},
                            new EventOutcomeResponse() {
                                    Id = new Guid(),
                                    EventId = new Guid("b314e08e-5e6c-4215-b8df-506e884960f0"),
                                    Description = "Out 1", BetsClosed = false, CurrentOdd = 2 }
                        }
                        }
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление информации о событии
        /// </summary>
        /// <param name="request">Данные для обновления</param>
        /// <returns>EventResponse</returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateEventAsync([FromBody] EventUpdateRequest request)
        {
            try
            {
                var result = await _service.UpdateEventAsync(request);

                var cachKey = "Event." + request.Id;
                await _cache.SetAsync(cachKey, result, _logger);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="request">Идентификатор записи и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteEventAsync([FromBody] DeleteRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteEventAsync(request);

                var cachKey = "Event." + request.Id;
                await _cache.RemoveAsync(cachKey);

                return Ok(UpdateResponse.CreateSuccessResponse(deletedCount));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(UpdateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Удаление нескольких записей
        /// </summary>
        /// <param name="request">Список идентификаторов записей и кто удаляет</param>
        /// <returns>Кол-во удаленных записей</returns>
        [HttpPost]
        [Route("delete/list")]
        public async Task<IActionResult> DeleteListEventsAsync([FromBody] DeleteListRequest request)
        {
            try
            {
                var deletedCount = await _service.DeleteListEventsAsync(request);

                //с помощью IDistributedCache вы можете работать только с одним ключом за раз.
                //Массовое удаление ключей и аналогичные сценарии выходят за рамки возможностей этого фасада.
                //Если вам нужно работать с более сложными сценариями,
                //вам следует подключаться к кэшу напрямую, используя соответствующий клиент.
                //Например, для поддержки Redis вы можете использовать пакет StackExchange.Redis
                foreach (var id in request.Ids)
                {
                    var cachKey = "Event." + id;
                    await _cache.RemoveAsync(cachKey);
                }

                return Ok(UpdateResponse.CreateSuccessResponse(deletedCount));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(UpdateResponse.CreateErrorResponse(ex.Message));
            }
        }
    }
}
