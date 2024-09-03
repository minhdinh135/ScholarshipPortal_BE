using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class GeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<string> GetResponseFromGemini(string prompt)
    {
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        // Set the API key in the request header
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("x-goog-api-key", _apiKey);

        var response = await _httpClient.PostAsync("https://generativelanguage.googleapis.com/v1/models/gemini-pro:generateContent", content);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseString);

        // Parse the response to get the text from the candidates
        var candidates = jsonResponse.GetProperty("candidates");
        if (candidates.GetArrayLength() > 0)
        {
            var candidate = candidates[0];
            var contentParts = candidate.GetProperty("content").GetProperty("parts");
            if (contentParts.GetArrayLength() > 0)
            {
                var text = contentParts[0].GetProperty("text").GetString();
                return FormatResponse(text.Trim());
            }
        }

        return "No response text found.";
    }

    private string FormatResponse(string responseText)
    {
      // Simple formatting example
      // Replace double newlines with a single newline for cleaner output
      var formattedText = responseText.Replace("\n", "\r").Trim();
      return formattedText;
    }
}


