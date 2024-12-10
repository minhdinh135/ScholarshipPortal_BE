using Application.Exceptions;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Request;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;

namespace UnitTest
{
	public class RequestServiceTests
	{
		private readonly RequestService _requestService;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<IRequestRepository> _requestRepositoryMock;

		public RequestServiceTests()
		{
			_mapperMock = new Mock<IMapper>();
			_requestRepositoryMock = new Mock<IRequestRepository>();
			_requestService = new RequestService(_mapperMock.Object, _requestRepositoryMock.Object);
		}

		#region GetRequestByApplicantId Tests

		[Fact]
		public async Task GetRequestByApplicantId_ShouldReturnRequestDtos_WhenRequestsExist()
		{
			var applicantId = 1;
			var requests = new List<Request>
		{
			new Request { Id = 1, Description = "Request 1", ApplicantId = applicantId },
			new Request { Id = 2, Description = "Request 2", ApplicantId = applicantId }
		};
			var requestDtos = new List<RequestDto>
		{
			new RequestDto { Id = 1, Description = "Request 1" },
			new RequestDto { Id = 2, Description = "Request 2" }
		};

			_requestRepositoryMock.Setup(repo => repo.GetRequestsByApplicantId(applicantId)).ReturnsAsync(requests);
			_mapperMock.Setup(mapper => mapper.Map<IEnumerable<RequestDto>>(requests)).Returns(requestDtos);

			var result = await _requestService.GetRequestByApplicantId(applicantId);

			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
			_requestRepositoryMock.Verify(repo => repo.GetRequestsByApplicantId(applicantId), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<IEnumerable<RequestDto>>(requests), Times.Once);
		}

		[Fact]
		public async Task GetRequestByApplicantId_ShouldReturnEmptyList_WhenNoRequestsExist()
		{
			// Arrange
			var applicantId = 1;
			var requests = new List<Request>(); // trả về danh sách rỗng
			_requestRepositoryMock.Setup(x => x.GetRequestsByApplicantId(applicantId))
								  .ReturnsAsync(requests);

			// Act
			var result = await _requestService.GetRequestByApplicantId(applicantId);

			// Assert
			Assert.Empty(result); // Kiểm tra danh sách trả về là trống
			_mapperMock.Verify(m => m.Map<IEnumerable<RequestDto>>(It.IsAny<IEnumerable<Request>>()), Times.Once);
		}

		#endregion

		#region GetRequestById Tests

		[Fact]
		public async Task GetRequestById_ShouldReturnRequestDto_WhenRequestExists()
		{
			var requestId = 1;
			var request = new Request { Id = requestId, Description = "Request 1" };
			var requestDto = new RequestDto { Id = requestId, Description = "Request 1" };

			_requestRepositoryMock.Setup(repo => repo.GetRequestById(requestId)).ReturnsAsync(request);
			_mapperMock.Setup(mapper => mapper.Map<RequestDto>(request)).Returns(requestDto);

			var result = await _requestService.GetRequestById(requestId);

			Assert.NotNull(result);
			Assert.Equal(requestDto, result);
			_requestRepositoryMock.Verify(repo => repo.GetRequestById(requestId), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<RequestDto>(request), Times.Once);
		}

		[Fact]
		public async Task GetRequestById_ShouldThrowServiceException_WhenRequestNotFound()
		{
			var requestId = 99;
			_requestRepositoryMock.Setup(repo => repo.GetRequestById(requestId)).ReturnsAsync((Request)null);

			var exception = await Assert.ThrowsAsync<ServiceException>(() => _requestService.GetRequestById(requestId));

			Assert.Equal($"Request with id:{requestId} is not found", exception.Message);
			Assert.IsType<NotFoundException>(exception.InnerException);
			_requestRepositoryMock.Verify(repo => repo.GetRequestById(requestId), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<RequestDto>(It.IsAny<Request>()), Times.Never);
		}

		#endregion

		#region CreateRequest Tests

		[Fact]
		public async Task CreateRequest_ShouldReturnRequestId_WhenRequestCreatedSuccessfully()
		{
			var createRequestDto = new ApplicantCreateRequestDto
			{
				ApplicantId = 1,
				Description = "Request Description",
				RequestFileUrls = new List<string> { "fileUrl1", "fileUrl2" },
				ServiceIds = new List<int> { 1, 2 }
			};

			var request = new Request
			{
				Description = createRequestDto.Description,
				ApplicantId = createRequestDto.ApplicantId,
				RequestDate = DateTime.Now,
				Status = RequestStatusEnum.Pending.ToString(),
				RequestDetails = new List<RequestDetail>
			{
				new RequestDetail
				{
					ServiceId = 1,
					RequestDetailFiles = new List<RequestDetailFile>
					{
						new RequestDetailFile { FileUrl = "fileUrl1", UploadedBy = RoleEnum.Applicant.ToString(), UploadDate = DateTime.Now }
					}
				},
				new RequestDetail
				{
					ServiceId = 2,
					RequestDetailFiles = new List<RequestDetailFile>
					{
						new RequestDetailFile { FileUrl = "fileUrl2", UploadedBy = RoleEnum.Applicant.ToString(), UploadDate = DateTime.Now }
					}
				}
			}
			};

			var addedRequest = new Request { Id = 1 };

			_requestRepositoryMock.Setup(repo => repo.Add(It.IsAny<Request>())).ReturnsAsync(addedRequest);

			var result = await _requestService.CreateRequest(createRequestDto);

			Assert.Equal(1, result);
			_requestRepositoryMock.Verify(repo => repo.Add(It.IsAny<Request>()), Times.Once);
		}

		[Fact]
		public async Task CreateRequest_ShouldThrowServiceException_WhenExceptionOccurs()
		{
			var createRequestDto = new ApplicantCreateRequestDto
			{
				ApplicantId = 1,
				Description = "Request Description",
				RequestFileUrls = new List<string> { "fileUrl1" },
				ServiceIds = new List<int> { 1 }
			};

			_requestRepositoryMock.Setup(repo => repo.Add(It.IsAny<Request>())).ThrowsAsync(new Exception("Some error"));

			var exception = await Assert.ThrowsAsync<ServiceException>(() => _requestService.CreateRequest(createRequestDto));

			Assert.Equal("Some error", exception.Message);
			_requestRepositoryMock.Verify(repo => repo.Add(It.IsAny<Request>()), Times.Once);
		}

		#endregion

		#region CancelRequest Tests

		[Fact]
		public async Task CancelRequestAsync_ShouldReturnTrue_WhenRequestDeletedSuccessfully()
		{
			var requestId = 1;
			_requestRepositoryMock.Setup(repo => repo.DeleteRequestAsync(requestId)).ReturnsAsync(true);

			var result = await _requestService.CancelRequestAsync(requestId);

			Assert.True(result);
			_requestRepositoryMock.Verify(repo => repo.DeleteRequestAsync(requestId), Times.Once);
		}

		[Fact]
		public async Task CancelRequestAsync_ShouldReturnFalse_WhenRequestDeletionFails()
		{
			var requestId = 99;
			_requestRepositoryMock.Setup(repo => repo.DeleteRequestAsync(requestId)).ReturnsAsync(false);

			var result = await _requestService.CancelRequestAsync(requestId);

			Assert.False(result);
			_requestRepositoryMock.Verify(repo => repo.DeleteRequestAsync(requestId), Times.Once);
		}
	}
	#endregion
}
