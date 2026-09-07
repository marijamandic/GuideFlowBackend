using Explorer.Blog.API.Public;
using Explorer.Stakeholders.API.Dtos.Chatbot;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Tours.API.Public.Author;
using Microsoft.AspNetCore.Mvc;
using OpenAI;

namespace Explorer.API.Controllers
{
    [Route("api/chatbot")]
    public class ChatbotController: BaseApiController
    {
        private readonly OpenAIClient _openAIClient;
        private readonly IChatLogService _chatLogService;
        private readonly ITourService _tourService;
        private readonly string _apikey = "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";

        public ChatbotController(IChatLogService chatLogService,ITourService tourService)
        {
            _openAIClient = new OpenAIClient(_apikey);
            _chatLogService = chatLogService;
            _tourService = tourService;
        }

        [HttpPost("prompt")]
        public async Task<ChatMessageDto> GenerateResponse([FromBody] ChatMessageDto chatMessageDto)
        {
            var userPrompt = chatMessageDto.Content;

            var databaseSummaryTours = _tourService.GetDatabaseSummary();


            string prompt = $"""
                You are a tour recommendation assistant. A user may ask for suggestions based on the provided database:
                {databaseSummaryTours} 

                User's question: "{userPrompt}"

                Respond with a relevant suggestion, including a brief explanation or state why no match is found
                If needed, ask clarifying questions to better assist. Refuse to answer non-tour-related queries politely.
                In your response, always format the tour name and id exactly as (Name,Id).
                For example, if the tour is called ‘Mountain Adventure’ with Id 1, your response should contain (Mountain Adventure,1).
                """;

            var response = await _openAIClient.GetChatClient("gpt-3.5-turbo").CompleteChatAsync(prompt);
  
            var responseText = response.Value.Content[0].Text;

            return new ChatMessageDto { Content = responseText, Sender = Sender.Chatbot};
  
  
        }

        [HttpPost("chatLog/create")]
        public ActionResult<ChatLogDto> Create(long userId)
        {
            var result = _chatLogService.Create(userId);
            return CreateResponse(result);
        }

        [HttpGet("chatLog/{userId:long}")]
        public ActionResult<ChatLogDto> GetByUser(long userId)
        {
            var result = _chatLogService.GetByUser(userId);
            return CreateResponse(result);
        }

        [HttpPatch("chatLog/update")]
        public ActionResult<ChatLogDto> Update([FromBody] ChatLogDto chatLogDto)
        {
            var result = _chatLogService.Update(chatLogDto);
            return CreateResponse(result);
        }

        [HttpGet("test")]
        public string TestSummary()
        {
            var result = _tourService.GetDatabaseSummary();
            return result;
        }
    }
}
