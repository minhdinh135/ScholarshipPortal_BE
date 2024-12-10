using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Services;
using AutoMapper;
using Domain.DTOs.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.IServices;
using Domain.Entities;


namespace UnitTest
{
	public class ServiceServiceTests
	{
		private readonly ServiceService _serviceService;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<IServiceRepository> _serviceRepositoryMock;
		private readonly Mock<IAccountRepository> _accountRepositoryMock;
		private readonly Mock<IEmailService> _emailServiceMock;

		public ServiceServiceTests()
		{
			_mapperMock = new Mock<IMapper>();
			_serviceRepositoryMock = new Mock<IServiceRepository>();
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_emailServiceMock = new Mock<IEmailService>();
			_serviceService = new ServiceService(_mapperMock.Object, _serviceRepositoryMock.Object, _accountRepositoryMock.Object, _emailServiceMock.Object);
		}

		#region GetServiceById Tests

		[Fact]
		public async Task GetServiceById_ShouldReturnServiceDto_WhenServiceExists()
		{
			// Arrange
			var serviceId = 1;
			var service = new Service { Id = serviceId, Name = "Service 1", Description = "Description 1" };
			var serviceDto = new ServiceDto { Id = serviceId, Name = "Service 1", Description = "Description 1" };

			_serviceRepositoryMock.Setup(repo => repo.GetServiceById(serviceId)).ReturnsAsync(service);
			_mapperMock.Setup(mapper => mapper.Map<ServiceDto>(service)).Returns(serviceDto);

			// Act
			var result = await _serviceService.GetServiceById(serviceId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(serviceDto, result);
			_serviceRepositoryMock.Verify(repo => repo.GetServiceById(serviceId), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<ServiceDto>(service), Times.Once);
		}

		[Fact]
		public async Task GetServiceById_ShouldThrowServiceException_WhenServiceNotFound()
		{
			// Arrange
			var serviceId = 99;
			_serviceRepositoryMock.Setup(repo => repo.GetServiceById(serviceId)).ReturnsAsync((Service)null);

			// Act & Assert
			var exception = await Assert.ThrowsAsync<ServiceException>(() => _serviceService.GetServiceById(serviceId));

			Assert.Equal($"Service with id:{serviceId} not found", exception.Message);
			Assert.IsType<NotFoundException>(exception.InnerException);
			_serviceRepositoryMock.Verify(repo => repo.GetServiceById(serviceId), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<ServiceDto>(It.IsAny<Service>()), Times.Never);
		}

		#endregion

		#region AddService Tests

		[Fact]
		public async Task AddService_ShouldReturnServiceDto_WhenServiceIsAddedSuccessfully()
		{
			// Arrange
			var addServiceDto = new AddServiceDto { Name = "New Service", Description = "Service Description" };
			var service = new Service { Id = 1, Name = "New Service", Description = "Service Description" };
			var serviceDto = new ServiceDto { Id = 1, Name = "New Service", Description = "Service Description" };

			_mapperMock.Setup(mapper => mapper.Map<Service>(addServiceDto)).Returns(service);
			_serviceRepositoryMock.Setup(repo => repo.Add(service)).ReturnsAsync(service);
			_mapperMock.Setup(mapper => mapper.Map<ServiceDto>(service)).Returns(serviceDto);

			// Act
			var result = await _serviceService.AddService(addServiceDto);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(serviceDto, result);
			_serviceRepositoryMock.Verify(repo => repo.Add(service), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<ServiceDto>(service), Times.Once);
		}

		[Fact]
		public async Task AddService_ShouldThrowServiceException_WhenServiceCannotBeAdded()
		{
			// Arrange
			var addServiceDto = new AddServiceDto();
			_serviceRepositoryMock.Setup(x => x.Add(It.IsAny<Service>()))
								  .ThrowsAsync(new ServiceException("An error occurred"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<ServiceException>(() => _serviceService.AddService(addServiceDto));
			Assert.Equal("An error occurred", exception.Message);
		}


		#endregion

		#region UpdateService Tests

		[Fact]
		public async Task UpdateService_ShouldReturnUpdatedServiceDto_WhenServiceExists()
		{
			// Arrange
			var serviceId = 1;
			var updateServiceDto = new UpdateServiceDto { Name = "Updated Service", Description = "Updated Description" };
			var existingService = new Service { Id = serviceId, Name = "Service 1", Description = "Description 1" };
			var updatedService = new Service { Id = serviceId, Name = "Updated Service", Description = "Updated Description" };
			var serviceDto = new ServiceDto { Id = serviceId, Name = "Updated Service", Description = "Updated Description" };

			_serviceRepositoryMock.Setup(repo => repo.GetById(serviceId)).ReturnsAsync(existingService);
			_mapperMock.Setup(mapper => mapper.Map(updateServiceDto, existingService)).Verifiable();
			_serviceRepositoryMock.Setup(repo => repo.Update(existingService)).ReturnsAsync(updatedService);
			_mapperMock.Setup(mapper => mapper.Map<ServiceDto>(updatedService)).Returns(serviceDto);

			// Act
			var result = await _serviceService.UpdateService(serviceId, updateServiceDto);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(serviceDto, result);
			_serviceRepositoryMock.Verify(repo => repo.GetById(serviceId), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map(updateServiceDto, existingService), Times.Once);
			_serviceRepositoryMock.Verify(repo => repo.Update(existingService), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<ServiceDto>(updatedService), Times.Once);
		}

		[Fact]
		public async Task UpdateService_ShouldThrowServiceException_WhenServiceNotFound()
		{
			// Arrange
			var serviceId = 99;
			var updateServiceDto = new UpdateServiceDto { Name = "Updated Service", Description = "Updated Description" };
			_serviceRepositoryMock.Setup(repo => repo.GetById(serviceId)).ReturnsAsync((Service)null);

			// Act & Assert
			var exception = await Assert.ThrowsAsync<ServiceException>(() => _serviceService.UpdateService(serviceId, updateServiceDto));

			Assert.Equal($"Service with id:{serviceId} not found", exception.Message);
			Assert.IsType<NotFoundException>(exception.InnerException);
			_serviceRepositoryMock.Verify(repo => repo.GetById(serviceId), Times.Once);
			_serviceRepositoryMock.Verify(repo => repo.Update(It.IsAny<Service>()), Times.Never);
		}

		#endregion

		#region GetServicesByProviderId Tests

		[Fact]
		public async Task GetServicesByProviderId_ShouldReturnServiceDtos_WhenServicesExist()
		{
			// Arrange
			var providerId = 1;
			var services = new List<Service>
		{
			new Service { Id = 1, Name = "Service 1", Description = "Description 1" },
			new Service { Id = 2, Name = "Service 2", Description = "Description 2" }
		};
			var serviceDtos = new List<ServiceDto>
		{
			new ServiceDto { Id = 1, Name = "Service 1", Description = "Description 1" },
			new ServiceDto { Id = 2, Name = "Service 2", Description = "Description 2" }
		};

			_serviceRepositoryMock.Setup(repo => repo.GetServicesByProviderId(providerId)).ReturnsAsync(services);
			_mapperMock.Setup(mapper => mapper.Map<IEnumerable<ServiceDto>>(services)).Returns(serviceDtos);

			// Act
			var result = await _serviceService.GetServicesByProviderId(providerId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
			_serviceRepositoryMock.Verify(repo => repo.GetServicesByProviderId(providerId), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<IEnumerable<ServiceDto>>(services), Times.Once);
		}

		[Fact]
		public async Task GetServicesByProviderId_ShouldReturnEmptyList_WhenNoServicesExist()
		{
			// Arrange
			var providerId = 1;
			var services = new List<Service>(); // trả về danh sách rỗng
			_serviceRepositoryMock.Setup(x => x.GetServicesByProviderId(providerId))
								  .ReturnsAsync(services);

			// Act
			var result = await _serviceService.GetServicesByProviderId(providerId);

			// Assert
			Assert.Empty(result); // Kiểm tra danh sách trả về là trống
			_mapperMock.Verify(m => m.Map<IEnumerable<ServiceDto>>(It.IsAny<IEnumerable<Service>>()), Times.Once);
		}

		#endregion
	}
}
