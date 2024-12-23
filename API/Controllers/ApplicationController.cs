using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Application;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;
using ServiceException = Application.Exceptions.ServiceException;

namespace SSAP.API.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IAwardMilestoneService _awardMilestoneService;
        private readonly ILogger<ApplicationController> _logger;
        private readonly IMapper _mapper;

        public ApplicationController(IApplicationService applicationService, ILogger<ApplicationController> logger,
            IAwardMilestoneService awardMilestoneService, IMapper mapper)
        {
            _applicationService = applicationService;
            _logger = logger;
            _awardMilestoneService = awardMilestoneService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var profiles = await _applicationService.GetAll();
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
                return StatusCode(500, "Error retrieving data from the database.");
            }
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = default, [FromQuery] string sortOrder = default)
        {
            var categories = await _applicationService.GetAll(pageIndex, pageSize, sortBy, sortOrder);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicantions successfully", categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            try
            {
                var profile = await _applicationService.GetApplicationById(id);
                if (profile == null) return NotFound("Application not found.");
                /*if(profile.Status == ApplicationStatusEnum.NeedExtend.ToString())
                {
                    Console.WriteLine("Hello asdasdsadsad");
                    var awards = await _awardMilestoneService.GetByScholarshipId(profile.ScholarshipProgramId.Value);
                    var profileEntity = _mapper.Map<Domain.Entities.Application>(profile);
                    var award = awards.Where(x =>
                        x.FromDate < profileEntity.UpdatedAt &&
                        x.ToDate > profile.UpdatedAt)
                    .FirstOrDefault();

                    if(award.ToDate < DateTime.Now){
                        profileEntity.Status = ApplicationStatusEnum.Rejected.ToString();
                        var profileUpdateDto = _mapper.Map<UpdateApplicationStatusRequest>(profile);
                        await _applicationService.Update(profile.Id, profileUpdateDto);
                    }
                }*/

                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
                return StatusCode(500, "Error retrieving data from the database.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddApplicationDto dto)
        {
            try
            {
                var addedProfile = await _applicationService.Add(dto);
                return Ok(addedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add applicant profile: {ex.Message}");
                return StatusCode(500, "Error adding data to the database.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateApplicationStatusRequest updateApplicationStatusRequest)
        {
            try
            {
                var updatedApplication = await _applicationService.Update(id, updateApplicationStatusRequest);
                return Ok(updatedApplication);
            }
            catch (ServiceException ex)
            {
                _logger.LogError($"Failed to update applicant profile: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedProfile = await _applicationService.Delete(id);
                if (deletedProfile == null) return NotFound("Application not found.");

                return Ok(deletedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete applicant profile: {ex.Message}");
                return StatusCode(500, "Error deleting data from the database.");
            }
        }

        [HttpGet("with-documents-and-account-profile/{id}")]
        public async Task<IActionResult> GetWithDocumentsAndAccount(int id)
        {
            /*try
            {*/
            var profile = await _applicationService.GetWithDocumentsAndAccount(id);
            if (profile == null) return NotFound("Application not found.");

            await _applicationService.CheckApplicationAward(profile);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicantion successfully", profile));
            //}
            /*catch (Exception ex)
            {
                _logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
                return StatusCode(500, "Error retrieving data from the database.");
            }*/
        }

        [HttpGet("get-by-scholarship/{scholarshipId}")]
        public async Task<IActionResult> GetByScholarship(int scholarshipId)
        {
            try
            {
                var profile = await _applicationService.GetByScholarshipId(scholarshipId);
                if (profile == null) return NotFound("Application not found.");
                return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicantion successfully", profile));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get applicant profile by id {scholarshipId}: {ex.Message}");
                return StatusCode(500, "Error retrieving data from the database.");
            }
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _applicationService.GetAllReviews();

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get reviews successfully", reviews));
        }

        [HttpGet("reviews/result")]
        public async Task<IActionResult> GetReviewsResult([FromQuery] int scholarshipProgramId, [FromQuery] bool isFirstReview)
        {
            var reviews = await _applicationService.GetReviewsResult(scholarshipProgramId, isFirstReview);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get reviews successfully", reviews));
        }

        [HttpPost("reviews/assign-expert")]
        public async Task<IActionResult> AssignApplicationsToExpert(AssignApplicationsToExpertRequest request)
        {
            try
            {
                await _applicationService.AssignApplicationsToExpert(request);

                return Ok(new ApiResponse(StatusCodes.Status200OK, "Assign successfully"));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }

        [HttpPut("reviews/result")]
        public async Task<IActionResult> UpdateReviewResult(UpdateReviewResultDto updateReviewResultDto)
        {
            try
            {
                await _applicationService.UpdateReviewResult(updateReviewResultDto);

                return Ok(new ApiResponse(StatusCodes.Status200OK, "Update result successfully"));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }

        [HttpPut("extend")]
        public async Task<IActionResult> ExtendApplication(ExtendApplicationDto dto)
        {
            try
            {
                await _applicationService.ExtendApplication(dto);

                return Ok(new ApiResponse(StatusCodes.Status200OK, "Update result successfully"));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }
    }
}
