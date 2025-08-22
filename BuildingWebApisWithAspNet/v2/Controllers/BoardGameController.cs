using Asp.Versioning;
using BuildingWebApisWithAspNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuildingWebApisWithAspNet.V2.Controllers
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("2.0")]
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
                Items = new List<BoardGame>
                {
                    new BoardGame()
                    {
                        Name = "Version 2"
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
