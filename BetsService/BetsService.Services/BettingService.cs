﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using BetsService.Services.Exceptions;
using BetsService.Models;
using BetsService.DataAccess.Repositories;
using BetsService.DataAccess.DTO;

namespace BetsService.Services
{
    public class BettingService
    {
        private readonly BetsRepository _repository;
        private readonly ILogger<BettingService> _logger;
        private readonly IMapper _mapper;

        public BettingService(BetsRepository bettorsRepository
            , ILogger<BettingService> logger
            , IMapper mapper
            )
        {
            _repository = bettorsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> AddBetsAsync(BetsRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to create bets without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[BettingService][AddBetsAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var address = _mapper.Map<Domain.Bets>(request);
                await _repository.AddAsync(address);

                return address.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettingService][AddBetsAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BetsResponse> GetBetsAsync(Guid id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);
                if (response == null)
                {
                    throw new NotFoundException($"Ставка с идентификатором {id} не найден.");
                }

                var result = _mapper.Map<BetsResponse>(response);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettingService][GetBetsAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<BetsResponse>> GetListBetsAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();

                return _mapper.Map<List<BetsResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettingService][GetListBetsAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<BetsResponse>> GetListByBettorIdAsync(Guid bettorId)
        {
            try
            {
                var response = await _repository.GetListByBettorIdAsync(bettorId);

                return _mapper.Map<List<BetsResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettingService][GetListByBettorIdAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> UpdateStatesAsync(BetsStateUpdateRequest request)
        {
            try
            {
                var count = await _repository.UpdateStatesAsync(request);
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BettingService][UpdateStatesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }
    }
}