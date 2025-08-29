using System.Globalization;
using BuildingWebApisWithAspNet.DbContexts;
using BuildingWebApisWithAspNet.Models.Csv;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildingWebApisWithAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly MyBgListContext context;
        private readonly ILogger<SeedController> logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SeedController(MyBgListContext context, ILogger<SeedController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]

        public async Task<IActionResult> SeedData(int? Id)
        {
            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("pt-BR"))
            {
                HasHeaderRecord = true,
                Delimiter = ";",
                HeaderValidated = null,
            };
            var filePath = Path.Combine(Path.Combine(webHostEnvironment.WebRootPath, "Dataset"), "bgg_dataset.csv");
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);

            var records = csv.GetRecords<BggRecord>().ToList();

            BggRecord currentRecord = null;
            if (Id.HasValue)
                currentRecord = records.FirstOrDefault(c => c.ID == Id);

            return currentRecord == null ? await CreateBoardGameData(records) : await CreateBoardGameData([currentRecord]);

        }

        private async Task<IActionResult> CreateBoardGameData(List<BggRecord> records)
        {
            var now = DateTime.Now;
            int skippedRows = 0;
            var existingBoardGames = await context.BoardGames.ToDictionaryAsync(bg => bg.Id);
            var existingDomains = await context.Domains.ToDictionaryAsync(d => d.Name);
            var existingMechanics = await context.Mechanics.ToDictionaryAsync(m => m.Name);
            foreach (var record in records)
            {
                if (!record.ID.HasValue
                || string.IsNullOrEmpty(record.Name)
                || existingBoardGames.ContainsKey(record.ID.Value))
                {
                    skippedRows++;
                    continue;
                }
                var boardgame = new Entities.BoardGame()
                {
                    Id = record.ID.Value,
                    Name = record.Name,
                    BGGRank = record.BGGRank ?? 0,
                    ComplexityAverage = record.ComplexityAverage ?? 0,
                    MaxPlayers = record.MaxPlayers ?? 0,
                    MinAge = record.MinAge ?? 0,
                    MinPlayers = record.MinPlayers ?? 0,
                    OwnedUsers = record.OwnedUsers ?? 0,
                    PlayTime = record.PlayTime ?? 0,
                    RatingAverage = record.RatingAverage ?? 0,
                    UsersRated = record.UsersRated ?? 0,
                    Year = record.YearPublished ?? 0,
                    CreatedDate = now,
                    LastModifiedDate = now,
                };
                context.BoardGames.Add(boardgame);
                existingBoardGames.Add(key: record.ID.Value, boardgame);

                if (!string.IsNullOrEmpty(record.Domains))
                {
                    foreach (var domainName in record.Domains
                    .Split(',', StringSplitOptions.TrimEntries)
                    .Distinct(StringComparer.InvariantCultureIgnoreCase))
                    {
                        var domain = existingDomains.GetValueOrDefault(domainName);
                        if (domain is null)
                        {
                            domain = new Entities.Domain()
                            {
                                Name = domainName,
                                CreatedDate = now,
                                LastModifiedDate = now
                            };
                            context.Domains.Add(domain);
                            existingDomains.Add(domainName, domain);
                        }
                        context.BoardGameDomains.Add(new Entities.BoardGameDomain()
                        {
                            BoardGame = boardgame,
                            Domain = domain,
                            CreatedDate = now
                        });
                    }
                }
                if (!string.IsNullOrEmpty(record.Mechanics))
                {
                    foreach (var mechanicName in record.Mechanics
                    .Split(',', StringSplitOptions.TrimEntries)
                    .Distinct(StringComparer.InvariantCultureIgnoreCase))
                    {
                        var mechanic = existingMechanics.GetValueOrDefault(mechanicName);
                        if (mechanic is null)
                        {
                            mechanic = new Entities.Mechanic()
                            {
                                Name = mechanicName,
                                CreatedDate = now,
                                LastModifiedDate = now
                            };
                            context.Mechanics.Add(mechanic);
                            existingMechanics.Add(key: mechanicName, mechanic);
                        }
                        context.BoardGameMechanics.Add(new Entities.BoardGameMechanic()
                        {
                            BoardGame = boardgame,
                            Mechanic = mechanic,
                            CreatedDate = now
                        });
                    }
                }
            }

            using var transaction = context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [BoardGame].[BoardGames] ON");
            var saved = await context.SaveChangesAsync();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [BoardGame].[BoardGames] OFF");
            transaction.Commit();

            return new JsonResult(new
            {
                BoardGames = context.BoardGames.Count(),
                Domains = context.Domains.Count(),
                Mechanics = context.Mechanics.Count(),
                SkippedRows = skippedRows,
                SavedRows = saved
            });
        }



    }
}
