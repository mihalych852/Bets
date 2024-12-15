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

        [Fact]
        public async void GetListBettorAddressesAsync_PassedSuccess_BettorAddressResponse()
        {
            // Arrange
            BettorAddresses address1 = TestHelper.CreateBettorAddresses();
            BettorAddresses address2 = TestHelper.CreateBettorAddresses();
            address2.Id = Guid.Parse("A6C8C6B1-4349-45B0-AB31-244740AAF0F0");
            address2.Address = "222@ya.ru";

            _bettorAddressRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<BettorAddresses>([address1, address2]));

            // Act
            var result = await _bettorAddressesService.GetListBettorAddressesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<BettorAddressResponse>>();
            result.Should().HaveCount(2);
            _bettorAddressRepositoryMock
                .Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void GetListByBettorIdAsync_PassedSuccess_BettorAddressResponse()
        {
            // Arrange
            BettorAddresses address1 = TestHelper.CreateBettorAddresses();
            BettorAddresses address2 = TestHelper.CreateBettorAddresses();
            address2.Id = Guid.Parse("A6C8C6B1-4349-45B0-AB31-244740AAF0F0");
            address2.Address = "222@ya.ru";
            address2.BettorId = Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435895");
            address2.Bettor = new Bettors()
            {
                Id = Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435895"),
                Nickname = "test222"
            };
            BettorAddresses address3 = TestHelper.CreateBettorAddresses();
            address3.Id = Guid.Parse("451533D5-D8D5-4A11-9C7B-EB9F14E1A32F");
            address3.Address = "333@yandex.ru";

            var bettoId = address1.BettorId;

            _bettorAddressRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<BettorAddresses>([address1, address2, address3]));

            _bettorAddressRepositoryMock.Setup(repo => repo.GetListByBettorIdAsync(bettoId))
                .ReturnsAsync(new List<BettorAddresses>([address1, address3]));

            // Act
            var result = await _bettorAddressesService.GetListByBettorIdAsync(bettoId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<BettorAddressResponse>>();
            result.Should().HaveCount(2);
            result.Select(r => r.Bettor.Id).Distinct().ToList().Should().HaveCount(1);
            result.Select(r => r.Bettor.Id).Should().Contain(bettoId);
            _bettorAddressRepositoryMock
                .Verify(repo => repo.GetListByBettorIdAsync(bettoId), Times.Once);
        }

        [Fact]
        public async void GetListByBettorIdAsync_NotFound_ListEmpty()
        {
            // Arrange
            BettorAddresses address1 = TestHelper.CreateBettorAddresses();
            BettorAddresses address2 = TestHelper.CreateBettorAddresses();
            address2.Id = Guid.Parse("A6C8C6B1-4349-45B0-AB31-244740AAF0F0");
            address2.Address = "222@ya.ru";
            address2.BettorId = Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435895");
            address2.Bettor = new Bettors()
            {
                Id = Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435895"),
                Nickname = "test222"
            };
            BettorAddresses address3 = TestHelper.CreateBettorAddresses();
            address3.Id = Guid.Parse("451533D5-D8D5-4A11-9C7B-EB9F14E1A32F");
            address3.Address = "333@yandex.ru";

            var bettoId = Guid.Parse("C4BDA62E-FC74-4256-A956-4760B3858CBD");

            _bettorAddressRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<BettorAddresses>([address1, address2, address3]));

            _bettorAddressRepositoryMock.Setup(repo => repo.GetListByBettorIdAsync(bettoId))
                .ReturnsAsync(new List<BettorAddresses>());

            // Act
            var result = await _bettorAddressesService.GetListByBettorIdAsync(bettoId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<BettorAddressResponse>>();
            result.Should().HaveCount(0);
            _bettorAddressRepositoryMock
                .Verify(repo => repo.GetListByBettorIdAsync(bettoId), Times.Once);
        }

        [Fact]
        public async void GetDefaultByBettorIdAsync_PassedSuccess_WithMinPriority()
        {
            // Arrange
            BettorAddresses address1 = TestHelper.CreateBettorAddresses();
            address1.Priority = 3;
            BettorAddresses address2 = TestHelper.CreateBettorAddresses();
            address2.Id = Guid.Parse("A6C8C6B1-4349-45B0-AB31-244740AAF0F0");
            address2.Address = "222@ya.ru";
            address2.Priority = 1;
            BettorAddresses address3 = TestHelper.CreateBettorAddresses();
            address3.Id = Guid.Parse("451533D5-D8D5-4A11-9C7B-EB9F14E1A32F");
            address3.Address = "333@yandex.ru";
            address3.Priority = 2;

            var bettoId = address1.BettorId;

            var expectedResult = new BettorAddressResponse()
            {
                Id = address2.Id,
                Address = address2.Address,
                Priority = address2.Priority,
                Messenger = new MessengerResponse()
                {
                    Id = address2.Messenger.Id,
                    Name = address2.Messenger.Name,
                },
                Bettor = new BettorResponse()
                {
                    Id = address2.Bettor.Id,
                    Nickname = address2.Bettor.Nickname,
                }
            };

            _bettorAddressRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<BettorAddresses>([address1, address2, address3]));

            _bettorAddressRepositoryMock.Setup(repo => repo.GetByBettorIdWithMinPriorityAsync(bettoId))
                .ReturnsAsync(address2);

            // Act
            var result = await _bettorAddressesService.GetDefaultByBettorIdAsync(bettoId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BettorAddressResponse>()
                .Which.Should().BeEquivalentTo(expectedResult);
            _bettorAddressRepositoryMock
                .Verify(repo => repo.GetByBettorIdWithMinPriorityAsync(bettoId), Times.Once);
        }
    }
}
