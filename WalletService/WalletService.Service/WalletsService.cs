using AutoMapper;
using Microsoft.Extensions.Logging;
using Bets.Abstractions.DataAccess.EF.Repositories;
using WalletService.Domain;
using WalletService.Models;
using WalletService.DataAccess.Repositories;
using WalletService.Service.Exeptions;

namespace WalletService.Service
{
    public class WalletsService
    {
        private readonly CreatedEntityRepository<Transactions> _tranRepository;
        private readonly WalletsRepository _walletRepository;
        private readonly ILogger<WalletsService> _logger;
        private readonly IMapper _mapper;

        public WalletsService(CreatedEntityRepository<Transactions> tranRepository
            , WalletsRepository walletRepository
            , ILogger<WalletsService> logger
            , IMapper mapper
            )
        {
            _tranRepository = tranRepository;
            _logger = logger;
            _mapper = mapper;
            _walletRepository = walletRepository;
        }

        public async Task<Guid> CreditAsync(TransactionsRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[WalletsService][CreditAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            if(request.BettorId == Guid.Empty)
            {
                var msg = "BettorId should not be empty";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[WalletsService][CreditAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var tran = _mapper.Map<Transactions>(request);
                await _tranRepository.AddAsync(tran);

                var wallet = await _walletRepository.GetByIdAsync(request.BettorId);
                if (wallet == null)
                {
                    await _walletRepository.AddAsync(new Wallets()
                    {
                        CreatedDate = DateTime.Now,
                        CreatedBy = tran.Id.ToString(),
                        ModifiedBy = tran.Id,
                        ModifiedDate = DateTime.Now,
                        BettorId = request.BettorId,
                        Amount = tran.Amount,
                    });
                }
                else
                {
                    wallet.Amount += tran.Amount;
                    wallet.ModifiedBy = tran.Id;
                    wallet.ModifiedDate = DateTime.Now;
                    await _walletRepository.UpdateAsync(wallet.BettorId, wallet);
                }

                return tran.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[WalletsService][CreditAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Guid> DebitAsync(TransactionsRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[WalletsService][DebitAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            if (request.BettorId == Guid.Empty)
            {
                var msg = "BettorId should not be empty";
                var ex = new ArgumentException(nameof(request), msg);
                _logger.LogError(ex, $"[WalletsService][DebitAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var wallet = await _walletRepository.GetByIdAsync(request.BettorId);

                if (wallet == null)
                {
                    var msg = $"cannot make a debit from a non-existent wallet (BettorId = '{request.BettorId}')";
                    var ex = new NotFoundException(msg);
                    _logger.LogError(ex, $"[WalletsService][DebitAsync] NotFoundException: {msg}");
                    throw ex;
                }

                if (wallet.Amount < request.Amount)
                {
                    var msg = "there are not enough funds in the wallet";
                    var ex = new InvalidOperationException(msg);
                    _logger.LogError(ex, $"[WalletsService][DebitAsync] InvalidOperationException: {msg}");
                    throw ex;
                }

                var tran = _mapper.Map<Transactions>(request);
                await _tranRepository.AddAsync(tran);

                wallet.Amount -= tran.Amount;
                wallet.ModifiedBy = tran.Id;
                wallet.ModifiedDate = DateTime.Now;
                await _walletRepository.UpdateAsync(wallet.BettorId, wallet);

                return tran.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[WalletsService][DebitAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<decimal> GetBalanceAsync(Guid bettorId)
        {
            try
            {
                var wallet = await _walletRepository.GetByIdAsync(bettorId);

                if (wallet == null)
                {
                    var msg = $"wallet not exists (bettorId = '{bettorId}')";
                    var ex = new NotFoundException(msg);
                    _logger.LogError(ex, $"[WalletsService][GetBalanceAsync] NotFoundException: {msg}");
                    throw ex;
                }

                return wallet.Amount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[WalletsService][DebitAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}
