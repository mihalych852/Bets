using Microsoft.AspNetCore.Mvc;
using WalletService.Service;
using Bets.Abstractions.Model;
using WalletService.Models;

namespace WalletService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly ILogger<WalletsController> _logger;
        private readonly WalletsService _service;

        public WalletsController(ILogger<WalletsController> logger
            , WalletsService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Пополнить счет (кошелек)
        /// </summary>
        /// <param name="request">Данные транзакции</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор транзакции</returns>
        [HttpPost]
        [Route("CreditAsync")]
        public async Task<IActionResult> CreditAsync([FromBody] TransactionsRequest request
            , CancellationToken ct)
        {
            try
            {
                var transactionId = await _service.CreditAsync(request, ct);
                return Ok(CreateResponse.CreateSuccessResponse(transactionId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(CreateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Перевисти со счета (из кошелька)
        /// </summary>
        /// <param name="request">Данные транзакции</param>
        /// <param name="ct"></param>
        /// <returns>Идентификатор транзакции</returns>
        [HttpPost]
        [Route("DebitAsync")]
        public async Task<IActionResult> DebitAsync([FromBody] TransactionsRequest request
            , CancellationToken ct)
        {
            try
            {
                var transactionId = await _service.DebitAsync(request, ct);
                return Ok(CreateResponse.CreateSuccessResponse(transactionId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(CreateResponse.CreateErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Получить текущий баланс счета (кошелька)
        /// </summary>
        /// <param name="id">Идентификатор счета (кошелька)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("balance/{id}")]
        public async Task<IActionResult> GetBalanceAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.GetBalanceAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
