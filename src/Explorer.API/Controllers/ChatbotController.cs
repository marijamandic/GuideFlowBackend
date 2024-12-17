using Explorer.Blog.API.Public;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Tours.API.Public.Author;
using Microsoft.AspNetCore.Mvc;
using OpenAI;
using OpenAI.Chat;

namespace Explorer.API.Controllers
{
    [Route("api/chatbot")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly OpenAIClient _openAIClient;
        private readonly ITourService _tourService;
        private readonly IPostService _postService;
        private readonly IClubService _clubService;
        private readonly string _apikey = "API_KEY";

        public ChatbotController(ITourService tourService, IPostService postService, IClubService clubService)
        {
            _openAIClient = new OpenAIClient(_apikey);
            _tourService = tourService;
            _postService = postService;
            _clubService = clubService;
        }

        [HttpGet("/prompt")]
        public async Task<string> GenerateResponse(string userMessage)
        {
            var databaseSummaryTours = _tourService.GetDatabaseSummary();
            var databaseSummaryBlogs = _postService.GetDatabaseSummary();
            var databaseSummaryClubs = _clubService.GetDatabaseSummary();

            string prompt = $"""
                You are being used as the chat bot support for the tourism application. Your job is to answer a users questions about anything tourism related. 
                You are a tour recommendation assistant. A user can ask you for a suggestion based on the following database contents:
                {databaseSummaryTours}
                {databaseSummaryBlogs}
                {databaseSummaryClubs}
                
                The message from user that you have to answer: "{userMessage}"
                Based on the data, provide a relevant suggestion with an explanation why or explain why no match was found.

                You can send questions to the user and clarify what he needs help with.
                """;

            var response = await _openAIClient.GetChatClient("gpt-3.5-turbo").CompleteChatAsync(prompt);
            return response.Value.Content.ToString() ?? "I couldn't generate a response. Please try again.";
        }
    }
}
