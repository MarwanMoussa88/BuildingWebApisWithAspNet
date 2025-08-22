using Asp.Versioning;
using BuildingWebApisWithAspNet.V1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuildingWebApisWithAspNet.V1.Controllers
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
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
                    {
                        Name = "Version 1"
                    }
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
