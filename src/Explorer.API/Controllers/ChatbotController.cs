using Explorer.Tours.API.Public.Author;
using Microsoft.AspNetCore.Mvc;
using OpenAI;

namespace Explorer.API.Controllers
{
    [Route("api/chatbot")]
    public class ChatbotController: BaseApiController
    {
        private readonly OpenAIClient _openAIClient;
        private readonly ITourService _tourService;
        private readonly string _apikey = "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";

        public ChatbotController(ITourService tourService)
        {
            _openAIClient = new OpenAIClient(_apikey);
            _tourService = tourService;
        }

        [HttpGet("/prompt")]
        public async Task<string> GenerateResponse(string userMessage) 
        {

            var databaseSummary = "BLAH";

            /*string prompt = $"""
                You are a tour recommendation assistant. A user has asked you for a suggestion based on the following database contents:
                {databaseSummary}

                The user said: "{userMessage}"
                Based on the data, provide a relevant suggestion with an explanation why or explain why no match was found.
                """;*/

            var prompt = "How Are you doing today?";

            var response = await _openAIClient.GetChatClient("gpt-3.5-turbo").CompleteChatAsync(prompt);
            

            return response.Value.Content.ToString() ?? "I couldn't generate a response. Please try again.";
        }
    }
}
