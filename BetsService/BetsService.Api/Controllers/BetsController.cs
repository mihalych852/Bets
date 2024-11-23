using Bets.Abstractions.Model;
using Microsoft.AspNetCore.Mvc;
using BetsService.Models;
using BetsService.Services;
using BetsService.DataAccess.DTO;

namespace BetsService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetsController : ControllerBase
    {
        private readonly ILogger<BetsController> _logger;
        private readonly BettingService _service;

        public BetsController(ILogger<BetsController> logger
            , BettingService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Создает ставку
        /// </summary>
        /// <param name="request">Содержит данные о ставке</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор новой записи</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddBetAsync([FromBody] BetsRequest request
            , CancellationToken ct)
        {
            try
            {
                var betId = await _service.AddBetsAsync(request, ct);
                return Ok(CreateResponse.CreateSuccessResponse(betId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(CreateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Получение ставки по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор ставки</param>
        /// <returns>BetResponse</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBetAsync([FromRoute] Guid id)
        {
            try
            {
                //_logger.LogInformation($"Get bet '{id}'");
                var result = await _service.GetBetsAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех ставок
        /// </summary>
        /// <returns>List of BetResponse</returns>
        [HttpGet]
        public async Task<IActionResult> GetListBetsAsync()
        {
            try
            {
                var result = await _service.GetListBetsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех ставок по идентификатору игрока
        /// </summary>
        /// <param name="bettorId">Идентификатор игрока</param>
        /// <returns>List of BetResponse</returns>
        [HttpGet]
        [Route("forBettor/{bettorId}")]
        public async Task<IActionResult> GetListBetsAsync([FromRoute] Guid bettorId)
        {
            try
            {
                var result = await _service.GetListByBettorIdAsync(bettorId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("states")]
        public async Task<IActionResult> UpdateStatesAsync(BetsStateUpdateRequest request)
        {
            try
            {
                var updateCount = await _service.UpdateStatesAsync(request);
                return Ok(UpdateResponse.CreateSuccessResponse(updateCount));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(UpdateResponse.CreateErrorResponse(ex.Message));
            }
        }
    }
}
