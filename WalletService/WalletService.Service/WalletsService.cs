using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Bets.Abstractions.DataAccess.EF.Repositories;
using WalletService.Domain;
using WalletService.Models;
using WalletService.DataAccess.Repositories;
using WalletService.Service.Exeptions;
using NotificationService.Models;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WalletService.Service
{
    public class WalletsService
    {
        private readonly Guid _assemblyGuid;

        private readonly CreatedEntityRepository<Transactions> _tranRepository;
        private readonly WalletsRepository _walletRepository;
        private readonly ILogger<WalletsService> _logger;
        private readonly IMapper _mapper;
        private readonly IBusControl _busControl;

        public WalletsService(CreatedEntityRepository<Transactions> tranRepository
            , WalletsRepository walletRepository
            , ILogger<WalletsService> logger
            , IMapper mapper
            , IBusControl busControl
            )
        {
            _tranRepository = tranRepository;
            _logger = logger;
            _mapper = mapper;
            _walletRepository = walletRepository;
            _busControl = busControl;

            var ass = Assembly.GetExecutingAssembly().GetCustomAttribute<GuidAttribute>()?.Value;
            if(!Guid.TryParse(ass, out _assemblyGuid))
            {
                _assemblyGuid = Guid.Parse("00000000-0000-0000-0000-000000000001");
            }
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

                await _busControl.Publish(new IncomingMessageRequest
                {
                    CreatedBy = nameof(CreditAsync),
                    TargetId = request.BettorId,
                    SourceId = _assemblyGuid,
                    Subject = "Зачисление средств",
                    Message = $"{request.Description}. Зачислено {tran.Amount} единиц.",
                    ActualDate = DateTime.UtcNow.AddDays(10),
                });

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

                await _busControl.Publish(new IncomingMessageRequest
                {
                    CreatedBy = nameof(CreditAsync),
                    TargetId = request.BettorId,
                    SourceId = _assemblyGuid,
                    Subject = "Списание средств",
                    Message = $"{request.Description}. Списано {tran.Amount} единиц."
                });

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
