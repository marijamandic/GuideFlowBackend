using Explorer.Blog.API.Public;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Mvc;
using OpenAI;

namespace Explorer.API.Controllers
{
    [Route("api/chatbot")]
    public class ChatbotController: BaseApiController
    {
        private readonly OpenAIClient _openAIClient;
        private readonly ITourService _tourService;
        private readonly IPostService _postService;

        public ChatbotController(ITourService tourService, IPostService postService)
        {
            _openAIClient = new OpenAIClient("key");
            _tourService = tourService;
            _postService = postService;
        }


        [HttpGet("databaseSummaryTours")]
        public ActionResult<string> GetDatabaseSummaryTours()
        {
            try
            {
                // Call the service to get the database summary
                string databaseSummary = _tourService.GetDatabaseSummary();

                // Return the summary as an ActionResult
                return Ok(databaseSummary);
            }
            catch (Exception ex)
            {
                // Log the error and return a 500 status code
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("databaseSummaryBlogs")]
        public ActionResult<string> GetDatabaseSummaryBlogs()
        {
            try
            {
                // Call the service to get the database summary
                string databaseSummary = _postService.GetDatabaseSummary();

                // Return the summary as an ActionResult
                return Ok(databaseSummary);
            }
            catch (Exception ex)
            {
                // Log the error and return a 500 status code
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Ovo ne sme da stoji bez HttpNESTO anotacije i zato ne pali swagger
        //public ActionResult<string> GenerateResponse(string userMessage) 
        //{
        //   /* string prompt = $"""
        //    You are a tour recommendation assistant. A user has asked you for a suggestion based on the following database contents:
        //    {databaseSummary}

        //    The user said: "{userMessage}"
        //    Based on the data, provide a relevant suggestion with an explanation why or explain why no match was found.
        //""";*/

        //    return new ActionResult<string>("key");
        //}
    }
}
