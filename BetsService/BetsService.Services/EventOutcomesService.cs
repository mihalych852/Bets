using Microsoft.Extensions.Logging;
using AutoMapper;
using BetsService.Domain;
using Bets.Abstractions.Domain.Repositories.ModelRequests;
using BetsService.Services.Exceptions;
using BetsService.Models;
using BetsService.DataAccess.Repositories;

namespace BetsService.Services
{
    public class EventOutcomesService
    {
        private readonly EventOutcomesRepository _repository; 
        private readonly ILogger<EventOutcomesService> _logger;
        private readonly IMapper _mapper;

        public EventOutcomesService(EventOutcomesRepository eventOutcomesRepository
            , ILogger<EventOutcomesService> logger
            , IMapper mapper)
        {
            _repository = eventOutcomesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> AddEventOutcomeAsync(EventOutcomeRequest request
            , CancellationToken ct)
        {
            if (request == null)
            {
                var msg = "attempt to create event outcome without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[EventOutcomesService][AddEventOutcomeAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var outcomeObj = _mapper.Map<EventOutcomes>(request);
                outcomeObj.CurrentOdd = CalcCoef();
                await _repository.AddAsync(outcomeObj);

                return outcomeObj.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventOutcomesService][AddEventOutcomeAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<EventOutcomeResponse> GetEventOutcomeAsync(Guid id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);
                if (response == null)
                {
                    throw new NotFoundException($"Исход с идентификатором {id} не найден.");
                }

                return _mapper.Map<EventOutcomeResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventOutcomesService][GetEventOutcomeAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<EventOutcomeResponse>> GetListEventOutcomesAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();

                return _mapper.Map<List<EventOutcomeResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventOutcomesService][GetListEventOutcomesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<EventOutcomeResponse> UpdateEventOutcomeAsync(EventOutcomeUpdateRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[EventOutcomesService][UpdateEventOutcomeAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var response = await _repository.GetByIdAsync(request.Id);
                if (response == null)
                {
                    throw new NotFoundException($"Исход с идентификатором {request.Id} не найден.");
                }

                response.CurrentOdd = request.CurrentOdd;
                response.IsHappened = request.IsHappened;
                response.BetsClosed = request.BetsClosed;
                response.Description = request.Description;
                response.ModifiedBy = request.ModifiedBy;

                await _repository.UpdateAsync(response);

                return _mapper.Map<EventOutcomeResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventOutcomesService][UpdateEventOutcomeAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteEventOutcomeAsync(DeleteRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[EventOutcomesService][DeleteEventOutcomeAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventOutcomesService][DeleteEventAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> DeleteListEventOutcomesAsync(DeleteListRequest request)
        {
            if (request == null)
            {
                var msg = "attempt to transmit a messenger without data";
                var ex = new ArgumentNullException(nameof(request), msg);
                _logger.LogError(ex, $"[EventOutcomesService][DeleteListEventOutcomesAsync] ArgumentNullException: {msg}");
                throw ex;
            }

            try
            {
                var deletedCount = await _repository.DeleteRangeAsync(request);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EventOutcomesService][DeleteListEventOutcomesAsync] Exception: {ex.ToString()}");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int> CloseAsync(IEnumerable<Guid> ids)
        {
            int updCount = 0;
            if (ids != null && ids.Count() > 0)
            {
                updCount = await _repository.CloseAsync(ids);
            }
            return updCount;
        }

        private static decimal CalcCoef()
        {
            var rand = new Random();
            return (decimal)(rand.Next(1, 6) + Math.Round(rand.NextDouble(), 3));
        }
    }
}
