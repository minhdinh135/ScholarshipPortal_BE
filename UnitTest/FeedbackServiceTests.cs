using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Services;
using AutoMapper;
using Domain.DTOs.Feedback;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
	public class FeedbackServiceTests
	{
		private readonly FeedbackService _feedbackService;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<IFeedbackRepository> _feedbackRepositoryMock;

		public FeedbackServiceTests()
		{
			_mapperMock = new Mock<IMapper>();
			_feedbackRepositoryMock = new Mock<IFeedbackRepository>();
			_feedbackService = new FeedbackService(_mapperMock.Object, _feedbackRepositoryMock.Object);
		}

		[Fact]
		public async Task GetAllFeedbacks_ShouldReturnFeedbackDtos_WhenFeedbacksExist()
		{
			var feedbacks = new List<Feedback>
		{
			new Feedback { Id = 1, Content = "Great service!", Rating = 5 },
			new Feedback { Id = 2, Content = "Needs improvement.", Rating = 3 }
		};
			var feedbackDtos = new List<FeedbackDto>
		{
			new FeedbackDto { Id = 1, Content = "Great service!", Rating = 5 },
			new FeedbackDto { Id = 2, Content = "Needs improvement.", Rating = 3 }
		};

			_feedbackRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(feedbacks);
			_mapperMock.Setup(mapper => mapper.Map<IEnumerable<FeedbackDto>>(feedbacks)).Returns(feedbackDtos);

			var result = await _feedbackService.GetAllFeedbacks();

			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
			Assert.Equal(feedbackDtos, result);
			_feedbackRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<IEnumerable<FeedbackDto>>(feedbacks), Times.Once);
		}

		[Fact]
		public async Task GetAllFeedbacks_ShouldReturnEmptyList_WhenNoFeedbacksExist()
		{
			var feedbacks = new List<Feedback>();
			_feedbackRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(feedbacks);
			_mapperMock.Setup(mapper => mapper.Map<IEnumerable<FeedbackDto>>(feedbacks)).Returns(new List<FeedbackDto>());

			var result = await _feedbackService.GetAllFeedbacks();

			Assert.NotNull(result);
			Assert.Empty(result);
			_feedbackRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<IEnumerable<FeedbackDto>>(feedbacks), Times.Once);
		}

		[Fact]
		public async Task GetFeedbackById_ShouldReturnFeedbackDto_WhenFeedbackExists()
		{
			var feedback = new Feedback { Id = 1, Content = "Excellent!", Rating = 5 };
			var feedbackDto = new FeedbackDto { Id = 1, Content = "Excellent!", Rating = 5 };

			_feedbackRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(feedback);
			_mapperMock.Setup(mapper => mapper.Map<FeedbackDto>(feedback)).Returns(feedbackDto);

			var result = await _feedbackService.GetFeedbackById(1);

			Assert.NotNull(result);
			Assert.Equal(feedbackDto, result);
			_feedbackRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<FeedbackDto>(feedback), Times.Once);
		}

		[Fact]
		public async Task GetFeedbackById_ShouldThrowServiceException_WhenFeedbackDoesNotExist()
		{
			_feedbackRepositoryMock.Setup(repo => repo.GetById(99)).ReturnsAsync((Feedback)null);

			var exception = await Assert.ThrowsAsync<ServiceException>(
				() => _feedbackService.GetFeedbackById(99)
			);

			Assert.Equal("Feedback with id:99 is not found", exception.Message);
			Assert.IsType<NotFoundException>(exception.InnerException);
			_feedbackRepositoryMock.Verify(repo => repo.GetById(99), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<FeedbackDto>(It.IsAny<Feedback>()), Times.Never);
		}
	}
}
