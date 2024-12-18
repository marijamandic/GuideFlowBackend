using Explorer.Blog.API.Public;
using Explorer.Stakeholders.API.Dtos.Chatbot;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Tours.API.Public.Author;
using Microsoft.AspNetCore.Mvc;
using OpenAI;
using OpenAI.Chat;

namespace Explorer.API.Controllers
{
    [Route("api/chatbot")]
    public class ChatbotController: BaseApiController
    {
        private readonly OpenAIClient _openAIClient;
        private readonly IChatLogService _chatLogService;
        private readonly ITourService _tourService;
        private readonly IPostService _postService;
        private readonly IClubService _clubService;
        private readonly string _apikey = "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";

        public ChatbotController(IChatLogService chatLogService,ITourService tourService, IPostService postService, IClubService clubService)
        {
            _openAIClient = new OpenAIClient(_apikey);
            _chatLogService = chatLogService;
            _tourService = tourService;
            _postService = postService;
            _clubService = clubService;
        }

        [HttpGet("prompt")]
        public async Task<string> GenerateResponse(string userMessage)
        {
            var databaseSummaryTours = _tourService.GetDatabaseSummary();
            var databaseSummaryBlogs = _postService.GetDatabaseSummary();
            var databaseSummaryClubs = _clubService.GetDatabaseSummary();

            string prompt = $"""
                 You are a tour recommendation assistant. 
                 A user can ask you for a suggestion based on the following database contents:
                {databaseSummaryTours}
                
                The message from user that you have to answer: "{userMessage}"
                Based on the data, provide a relevant suggestion with an explanation why or explain why no match was found.

                You can send questions to the user and clarify what he needs help with.
                """;

            var response = await _openAIClient.GetChatClient("gpt-3.5-turbo").CompleteChatAsync(userMessage);
            try
            {
                var responseText = response.Value.Content[0].Text;
                return responseText;
            }catch (IndexOutOfRangeException ex) {
                return ex.Message;
            }
        }

        [HttpPost("ChatLog/create")]
        public ActionResult<ChatLogDto> Create(long userId)
        {
            var result = _chatLogService.Create(userId);
            return CreateResponse(result);
        }

        [HttpGet("ChatLog/{userId:long}")]
        public ActionResult<ChatLogDto> GetByUSer(long userId)
        {
            var result = _chatLogService.GetByUser(userId);
            return CreateResponse(result);
        }

        [HttpPut("ChatLog/update")]
        public ActionResult<ChatLogDto> Update([FromBody] ChatLogDto chatLogDto)
        {
            var result = _chatLogService.Update(chatLogDto);
            return CreateResponse(result);
        }
    }
}
