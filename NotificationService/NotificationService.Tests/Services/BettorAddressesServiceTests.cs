using AutoFixture.AutoMoq;
using AutoFixture;
using Moq;
using NotificationService.DataAccess.Repositories;
using NotificationService.Services;
using FluentAssertions;
using NotificationService.Domain.Directories;
using NotificationService.Services.Exceptions;
using NotificationService.Models;
using AutoMapper;
using NotificationService.Services.Helpers;

namespace NotificationService.Tests.Services
{
    public class BettorAddressesServiceTests
    {
        private readonly Mock<IBettorAddressRepository> _bettorAddressRepositoryMock;
        private readonly BettorAddressesService _bettorAddressesService;

        public BettorAddressesServiceTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            _bettorAddressRepositoryMock = fixture.Freeze<Mock<IBettorAddressRepository>>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            fixture.Register<IMapper>(() => new Mapper(mapperConfig));

            _bettorAddressesService = fixture.Build<BettorAddressesService>().OmitAutoProperties().Create();
        }

        [Fact]
        public async void AddBettorAddressesAsync_PassedSuccess_BettorAddressestCreteAndSave()
        {
            // Arrange
            var request = TestHelper.CreateBaseBettorAddressesRequest();

            // Act
            _ = await _bettorAddressesService.AddBettorAddressesAsync(request, CancellationToken.None);

            // Assert
            _bettorAddressRepositoryMock
                .Verify(repo => repo.AddAsync(It.IsAny<BettorAddresses>()), Times.Once);
        }

        [Fact]
        public async void GetBettorAddressesAsync_AddressNotFound_NotFoundException()
        {
            // Arrange
            var addressId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
            BettorAddresses address = null;

            _bettorAddressRepositoryMock.Setup(repo => repo.GetByIdAsync(addressId))
                .ReturnsAsync(address);

            // Act
            var act = async () => await _bettorAddressesService.GetBettorAddressesAsync(addressId);

            // Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal($"Адрес с идентификатором {addressId} не найден.", exception.Message);
        }

        [Fact]
        public async void GetBettorAddressesAsync_PassedSuccess_BettorAddressResponse()
        {
            // Arrange
            BettorAddresses address = TestHelper.CreateBettorAddresses();
            var addressId = address.Id;

            var expectedResult = new BettorAddressResponse()
            {
                Id = addressId,
                Address = address.Address,
                Priority = address.Priority,
                Messenger = new MessengerResponse()
                {
                    Id = address.Messenger.Id,
                    Name = address.Messenger.Name,
                },
                Bettor = new BettorResponse()
                {
                    Id = address.Bettor.Id,
                    Nickname = address.Bettor.Nickname,
                }
            };

            _bettorAddressRepositoryMock.Setup(repo => repo.GetByIdAsync(addressId))
                .ReturnsAsync(address);

            // Act
            var result = await _bettorAddressesService.GetBettorAddressesAsync(addressId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BettorAddressResponse>()
                .Which.Should().BeEquivalentTo(expectedResult);
            _bettorAddressRepositoryMock
                .Verify(repo => repo.GetByIdAsync(addressId), Times.Once);
        }
    }
}
