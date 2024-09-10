using Infrastructure.ExternalService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class GeminiController : ControllerBase
{
    private readonly GeminiService _geminiService;

    public GeminiController(GeminiService geminiService)
    {
      _geminiService = geminiService;
    }

    [HttpPost("prompt")]
    public async Task<IActionResult> GetGeminiResponse([FromBody] ChatRequest request)
    {
        var response = await _geminiService.GetResponseFromGemini(request.Prompt);
        return Ok(new { Response = response });
    }
}

public class ChatRequest
{
    public string Prompt { get; set; }
}
