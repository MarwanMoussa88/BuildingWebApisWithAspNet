using System.ComponentModel.DataAnnotations;
using BuildingWebApisWithAspNet.Attributes;
using BuildingWebApisWithAspNet.DbContexts;
using BuildingWebApisWithAspNet.Entities;
using BuildingWebApisWithAspNet.Extensions;
using BuildingWebApisWithAspNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildingWebApisWithAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicsController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> logger;
        private readonly MyBgListContext context;



        public MechanicsController(ILogger<BoardGamesController> logger, MyBgListContext context)
        {
            this.logger = logger;
            this.context = context;
        }


        [HttpGet(Name = nameof(GetMechanics))]
        public async Task<RestDTO<IEnumerable<Mechanic>>> GetMechanics([FromQuery] RequestDTO<Mechanic> request)
        {
            var query = context.Mechanics.AsQueryable();
            if (!string.IsNullOrEmpty(request.FilterQuery))
                query = query.Where(c => c.Name.Equals(request.FilterQuery));

            query = query.OrderByColumn(request.SortColumn).Skip(request.PageIndex * request.PageSize).Take(request.PageSize);

            return new RestDTO<IEnumerable<Mechanic>>()
            {
                Data = query,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount = await context.Mechanics.Where(c => c.Name.Equals(request.FilterQuery))
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

        [HttpPost(Name = nameof(UpdateMechanic))]
        public async Task<RestDTO<Mechanic>> UpdateMechanic(UpdateMechanicDTO boardGame)
        {
            var currentMechanic = await context.Mechanics.FirstOrDefaultAsync(c => c.Id == boardGame.Id);
            if (currentMechanic is not null)
            {
                if (!string.IsNullOrEmpty(currentMechanic.Name))
                    currentMechanic.Name = boardGame.Name;

                currentMechanic.LastModifiedDate = DateTime.Now;
                context.Mechanics.Update(currentMechanic);
                await context.SaveChangesAsync();
            }
            return new RestDTO<Mechanic>
            {
                Data = currentMechanic,
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

        [HttpDelete(Name = nameof(DeleteMechanic))]
        public async Task<RestDTO<IEnumerable<Mechanic>>> DeleteMechanic(List<int> Ids)
        {
            var mechanics = context.Mechanics.Where(mechanic => Ids.Contains(mechanic.Id));

            context.Mechanics.RemoveRange(mechanics);

            await context.SaveChangesAsync();

            return new RestDTO<IEnumerable<Mechanic>>
            {
                Data = mechanics,
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
