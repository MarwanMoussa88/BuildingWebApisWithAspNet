using System.ComponentModel.DataAnnotations;
using BuildingWebApisWithAspNet.Attributes;
using BuildingWebApisWithAspNet.DbContexts;
using BuildingWebApisWithAspNet.Extensions;
using BuildingWebApisWithAspNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildingWebApisWithAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> logger;
        private readonly MyBgListContext context;



        public BoardGamesController(ILogger<BoardGamesController> logger, MyBgListContext context)
        {
            this.logger = logger;
            this.context = context;
        }


        [HttpGet(Name = nameof(GetBoardGames))]
        public async Task<RestDTO<IEnumerable<Entities.BoardGame>>> GetBoardGames([FromQuery] RequestDTO<BoardGame> request)
        {
            IQueryable<Entities.BoardGame> query = context.BoardGames;
            if (!string.IsNullOrEmpty(request.FilterQuery))
                query = query.Where(c => c.Name.Equals(request.FilterQuery));

            query = query.OrderByColumn(request.SortColumn).Skip(request.PageIndex * request.PageSize).Take(request.PageSize);

            return new RestDTO<IEnumerable<Entities.BoardGame>>()
            {
                Data = query,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount = await context.BoardGames.Where(c => c.Name.Equals(request.FilterQuery))
                                                     .CountAsync(),
                Links = new List<LinkDTO>()
                {
                    new LinkDTO()
                    {
                        Href = Url.Action(
                            action: ControllerContext.ActionDescriptor.ActionName,
                            ControllerContext.ActionDescriptor.ControllerName,
                            request,
                            Request.Scheme),
                        Rel = "Self",
                        Type = "GET"
                    }
                }
            };
        }

        [HttpPost(Name = nameof(UpdateBoardGame))]
        public async Task<RestDTO<Entities.BoardGame>> UpdateBoardGame(UpdateBoardGameDTO boardGame)
        {
            var currentBoardGame = await context.BoardGames.FirstOrDefaultAsync(c => c.Id == boardGame.Id);
            if (currentBoardGame != null)
            {
                if (!string.IsNullOrEmpty(currentBoardGame.Name))
                    currentBoardGame.Name = boardGame.Name;
                if (boardGame.Year.HasValue && boardGame.Year.Value > 0)
                    currentBoardGame.Year = boardGame.Year.Value;

                currentBoardGame.LastModifiedDate = DateTime.Now;
                context.BoardGames.Update(currentBoardGame);
                await context.SaveChangesAsync();
            }
            return new RestDTO<Entities.BoardGame>
            {
                Data = currentBoardGame,
                Links = new List<LinkDTO>()
                {
                    new LinkDTO()
                    {
                        Href = Url.Action(
                            action: ControllerContext.ActionDescriptor.ActionName,
                            ControllerContext.ActionDescriptor.ControllerName,
                            boardGame,
                            Request.Scheme),
                        Rel = "Self",
                        Type = ControllerContext.HttpContext.Request.Method
                    }
                }
            };
        }

        [HttpDelete(Name = nameof(DeleteBoardGame))]
        public async Task<RestDTO<IEnumerable<Entities.BoardGame>>> DeleteBoardGame(List<int> Ids)
        {
            var boardGames = context.BoardGames.Where(boardGame => Ids.Contains(boardGame.Id));

            context.BoardGames.RemoveRange(boardGames);

            await context.SaveChangesAsync();

            return new RestDTO<IEnumerable<Entities.BoardGame>>
            {
                Data = boardGames,
                Links = new List<LinkDTO>()
                {
                    new LinkDTO()
                    {
                        Href = Url.Action(
                            action: ControllerContext.ActionDescriptor.ActionName,
                            ControllerContext.ActionDescriptor.ControllerName,
                            new {Ids},
                            Request.Scheme),
                        Rel = "Self",
                        Type = ControllerContext.HttpContext.Request.Method
                    }
                }
            };
        }
    }
}
