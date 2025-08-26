using BuildingWebApisWithAspNet.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingWebApisWithAspNet.DbContexts
{
    public class MyBgListContext : DbContext
    {
        public DbSet<BoardGame> BoardGames => Set<BoardGame>();
        public DbSet<Mechanic> Mechanic => Set<Mechanic>();
        public DbSet<Domain> Domains => Set<Domain>();
        public DbSet<BoardGameDomain> BoardGameDomains => Set<BoardGameDomain>();
        public DbSet<BoardGameMechanic> BoardGameMechanics => Set<BoardGameMechanic>();
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<BoardGameCategory> BoardGameCategories => Set<BoardGameCategory>();

        public MyBgListContext(DbContextOptions<MyBgListContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BoardGameDomain>().HasKey(c => new { c.BoardGameId, c.DomainId });
            modelBuilder.Entity<BoardGameMechanic>().HasKey(c => new { c.BoardGameId, c.MechanicId });
            modelBuilder.Entity<BoardGameCategory>().HasKey(c => new { c.BoardGameId, c.CategoryId });

            modelBuilder.Entity<BoardGameDomain>()
                        .HasOne(c => c.BoardGame)
                        .WithMany(c => c.BoardGameDomains)
                        .HasForeignKey(c => c.BoardGameId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGameDomain>()
                        .HasOne(c => c.Domain)
                        .WithMany(c => c.BoardGameDomains)
                        .HasForeignKey(c => c.DomainId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<BoardGameDomain>()
                        .HasOne(c => c.BoardGame)
                        .WithMany(c => c.BoardGameDomains)
                        .HasForeignKey(c => c.BoardGameId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGameMechanic>()
                        .HasOne(c => c.BoardGame)
                        .WithMany(c => c.BoardGameMechanics)
                        .HasForeignKey(c => c.BoardGameId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGameMechanic>()
                        .HasOne(c => c.Mechanic)
                        .WithMany(c => c.BoardGameMechanics)
                        .HasForeignKey(c => c.MechanicId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Publisher>()
                        .HasMany(c => c.BoardGames)
                        .WithOne(c => c.Publisher)
                        .HasForeignKey(c => c.PublisherId)
                        .IsRequired();

            modelBuilder.Entity<BoardGame>()
                        .HasMany(c => c.BoardGameCategories)
                        .WithOne(c => c.BoardGame)
                        .HasForeignKey(c => c.BoardGameId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                        .HasMany(c => c.BoardGameCategories)
                        .WithOne(c => c.Category)
                        .HasForeignKey(c => c.CategoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
