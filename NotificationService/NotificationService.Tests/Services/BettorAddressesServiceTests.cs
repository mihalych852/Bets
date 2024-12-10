using AutoFixture.AutoMoq;
using AutoFixture;
using Moq;
using NotificationService.DataAccess.Repositories;
using NotificationService.Services;
using FluentAssertions;
using NotificationService.Domain.Directories;
using Bets.Abstractions.Domain.Repositories.Interfaces;

namespace NotificationService.Tests.Services
{
    public class BettorAddressesServiceTests
    {
        private readonly Mock<ICanCreateEntitiesRepository<BettorAddresses>> _bettorAddressRepositoryMock;
        private readonly BettorAddressesService _bettorAddressesService;

        public BettorAddressesServiceTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            _bettorAddressRepositoryMock = fixture.Freeze<Mock<ICanCreateEntitiesRepository<BettorAddresses>>>();
            _bettorAddressesService = fixture.Build<BettorAddressesService>().OmitAutoProperties().Create();
        }

        [Fact]
        public async void AddBettorAddressesAsync_PassedSuccess_BettorAddressestCreteAndSave()
        {
            // Arrange
            var request = TestHelper.CreateBaseBettorAddressesRequest();

            // Act
            var result = await _bettorAddressesService.AddBettorAddressesAsync(request, CancellationToken.None);

            // Assert
            _bettorAddressRepositoryMock
                .Verify(repo => repo.AddAsync(It.IsAny<BettorAddresses>()), Times.Once);
        }
    }
}
