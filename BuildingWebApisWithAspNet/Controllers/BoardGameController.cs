using BuildingWebApisWithAspNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingWebApisWithAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        private readonly ILogger<BoardGameController> logger;

        public BoardGameController(ILogger<BoardGameController> logger)
        {
            this.logger = logger;
        }


        [HttpGet(Name = nameof(GetBoardGames))]
        public RestDTO<List<BoardGame>> GetBoardGames()
        {
            return new RestDTO<List<BoardGame>>()
            {
                Data = new List<BoardGame>
                {
                    new BoardGame()
                },
                Links = new List<LinkDTO>()
                {
                    new LinkDTO()
                    {
                        Href = Url.ActionLink(),
                        Rel = "Self",
                        Type = "GET"
                    }
                }
            };
        }
    }
}
