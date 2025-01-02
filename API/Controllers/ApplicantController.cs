using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Applicant;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/applicants")]
public class ApplicantController : ControllerBase
{
    private readonly IApplicantService _applicantService;
    private readonly IApplicationService _applicationService;
    private readonly IPdfService _pdfService;
    private readonly ICloudinaryService _cloudinaryService;

    public ApplicantController(IApplicantService applicantService,
        IApplicationService applicationService, IPdfService pdfService,
        ICloudinaryService cloudinaryService)
    {
        _applicantService = applicantService;
        _applicationService = applicationService;
        _pdfService = pdfService;
        _cloudinaryService = cloudinaryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllApplicants()
    {
        var applicants = await _applicantService.GetAllApplicantProfiles();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicants successfully", applicants));
    }

    [HttpPost("contract")]
    public async Task<IActionResult> GetContract([FromBody] ScholarshipContractRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.ApplicantName))
        {
            return BadRequest("Invalid input data");
        }

        var pdfBytes = await _pdfService.GenerateScholarshipContractPdf(
            request.ApplicantName,
            request.ScholarshipAmount,
            request.ScholarshipProviderName,
            request.Deadline
        );

        return File(pdfBytes, "application/pdf", "Scholarship_Contract.pdf");
    }

    [HttpPost("contract-uploaded")]
    public async Task<IActionResult> GetContractUploaded([FromBody] ScholarshipContractRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.ApplicantName))
        {
            return BadRequest("Invalid input data");
        }

        var resultUrl = await _cloudinaryService.CreateAndUploadScholarshipContract(
            request.ApplicantName,
            request.ScholarshipAmount,
            request.ScholarshipProviderName,
            request.Deadline
        );

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Scholarship contract uploaded successfully", resultUrl));
    }


    [HttpGet("{applicantId}")]
    public async Task<IActionResult> GetApplicantProfile(int applicantId)
    {
        var applicant = await _applicantService.GetApplicantProfileDetails(applicantId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicant profile successfully", applicant));
    }

    [HttpGet("{applicantId}/applications")]
    public async Task<IActionResult> GetApplicationsByApplicantId(int applicantId)
    {
        var applications = await _applicationService.GetApplicationsByApplicantId(applicantId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applications successfully", applications));
    }

    [HttpGet("by-applicantId-and-scholarshipId")]
    public async Task<IActionResult> GetApplicationsByApplicantIdAndScholarshipId([FromQuery] int applicantId,
        [FromQuery] int scholarshipId)
    {
        var applications = await _applicationService.GetApplicationsByApplicantIdAndScholarshipProgramId(applicantId, scholarshipId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applications successfully", applications));
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AddApplicantProfile(int id, AddApplicantProfileDto addApplicantProfileDto)
    {
        try
        {
            var addedProfile = await _applicantService.AddApplicantProfile(id, addApplicantProfileDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add applicant profile successfully", addedProfile));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Add applicant profile failed", null));
        }
    }

    [HttpPut("{applicantId}")]
    public async Task<IActionResult> UpdateApplicantProfile(int applicantId,
        UpdateApplicantProfileDto updateApplicantProfileDto)
    {
        try
        {
            var updatedProfile = await _applicantService.UpdateApplicantProfile(applicantId, updateApplicantProfileDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile successfully",
                updatedProfile));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update applicant profile failed",
                null));
        }
    }

    [HttpPut("{applicantId}/profile")]
    public async Task<IActionResult> UpdateApplicantProfileDetails(int applicantId,
        UpdateApplicantProfileDetails updateProfileDetails)
    {
        try
        {
            var updatedProfile =
                await _applicantService.UpdateApplicantProfileDetails(applicantId, updateProfileDetails);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile successfully",
                updatedProfile));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{applicantId}/general-info")]
    public async Task<IActionResult> UpdateProfileGeneralInformation(int applicantId,
        UpdateApplicantGeneralInformationRequest request)
    {
        try
        {
            await _applicantService.UpdateProfileGeneralInformation(applicantId, request);
            
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile successfully"));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("{applicantId}/profile-experience")]
    public async Task<IActionResult> AddProfileExperience(int applicantId,
        AddExperienceRequest request)
    {
        try
        {
            await _applicantService.AddProfileExperience(applicantId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add applicant profile experience successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{applicantId}/profile-experience/{experienceId}")]
    public async Task<IActionResult> UpdateProfileExperience(int applicantId, int experienceId,
        UpdateExperienceRequest request)
    {
        try
        {
            await _applicantService.UpdateProfileExperience(applicantId, experienceId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile experience successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpDelete("profile-experience/{experienceId}")]
    public async Task<IActionResult> DeleteProfileExperience(int experienceId)
    {
        try
        {
            await _applicantService.DeleteProfileExperience(experienceId);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Delete applicant profile experience successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("{applicantId}/profile-education")]
    public async Task<IActionResult> AddProfileEducation(int applicantId,
        AddEducationRequest request)
    {
        try
        {
            await _applicantService.AddProfileEducation(applicantId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add applicant profile education successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{applicantId}/profile-education/{educationId}")]
    public async Task<IActionResult> UpdateProfileEducation(int applicantId, int educationId,
        UpdateEducationRequest request)
    {
        try
        {
            await _applicantService.UpdateProfileEducation(applicantId, educationId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile education successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpDelete("profile-education/{educationId}")]
    public async Task<IActionResult> DeleteProfileEducation(int educationId)
    {
        try
        {
            await _applicantService.DeleteProfileEducation(educationId);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Delete applicant profile education successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("{applicantId}/profile-skill")]
    public async Task<IActionResult> AddProfileSkill(int applicantId,
        AddApplicantSkillRequest request)
    {
        try
        {
            await _applicantService.AddProfileSkill(applicantId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add applicant profile skill successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{applicantId}/profile-skill/{skillId}")]
    public async Task<IActionResult> UpdateProfileSkill(int applicantId, int skillId,
        UpdateApplicantSkillRequest request)
    {
        try
        {
            await _applicantService.UpdateProfileSkill(applicantId, skillId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile skill successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpDelete("profile-skill/{skillId}")]
    public async Task<IActionResult> DeleteProfileSkill(int skillId)
    {
        try
        {
            await _applicantService.DeleteProfileSkill(skillId);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Delete applicant profile skill successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("{applicantId}/profile-certificate")]
    public async Task<IActionResult> AddProfileCertificate(int applicantId,
        AddApplicantCertificateRequest request)
    {
        try
        {
            await _applicantService.AddProfileCertificate(applicantId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add applicant profile certificate successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{applicantId}/profile-certificate/{certifcateId}")]
    public async Task<IActionResult> UpdateProfileCertificate(int applicantId, int certifcateId,
        UpdateApplicantCertificateRequest request)
    {
        try
        {
            await _applicantService.UpdateProfileCertificate(applicantId, certifcateId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile certificate successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpDelete("profile-certificate/{certificateId}")]
    public async Task<IActionResult> DeleteProfileCertificate(int certificateId)
    {
        try
        {
            await _applicantService.DeleteProfileCertificate(certificateId);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Delete applicant profile certificate successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("{applicantId}/profile/pdf")]
    public async Task<IActionResult> ExportApplicantProfileToPdf(int applicantId)
    {
        try
        {
            var applicantProfile = await _applicantService.GetApplicantProfileDetails(applicantId);
            var pdf = await _applicantService.ExportApplicantProfileToPdf(applicantId);

            return File(pdf, "application/pdf",
                $"ApplicantCV_{applicantProfile.FirstName}{applicantProfile.LastName}.pdf");
        }
        catch (NotFoundException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
        }
    }
}