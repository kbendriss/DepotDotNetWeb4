using BasketBallLiveScore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BasketBallLiveScore.Data
{
    public class BasketballContext : DbContext
    {
        public BasketballContext(DbContextOptions<BasketballContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<BasketballGame> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TeamStaff> TeamStaff { get; set; }
        public DbSet<GameStaff> GameStaff { get; set; }

        public DbSet<PointsScored> PointsScored { get; set; }
        public DbSet<Substitution> Substitutions { get; set; }

        public DbSet<Foul> Fouls { get; set; }

        public DbSet<TimerState> TimerStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ajout des contraintes uniques pour Username et Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Ajout de la contrainte unique combinée pour FirstName et LastName
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.FirstName, u.LastName })
                .IsUnique();


            // Relation entre BasketballGame et User (SetupManager)
            modelBuilder.Entity<BasketballGame>()
                .HasOne<User>() // Un `BasketballGame` est associé à un seul `User`.
                .WithMany() // Pas de navigation inverse nécessaire.
                .HasForeignKey(g => g.SetupManagerId)
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade : suppression des matchs avec l'utilisateur.

            // Relation entre Team et BasketballGame
            modelBuilder.Entity<Team>()
                .HasOne<BasketballGame>() // Une équipe est associée à un match.
                .WithMany() // Pas de navigation nécessaire depuis `BasketballGame`.
                .HasForeignKey(t => t.BasketballGameId)
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade : suppression des équipes avec le match.

            // Relation entre TeamStaff et Team
            modelBuilder.Entity<TeamStaff>()
                .HasOne<Team>() // Un membre du personnel appartient à une seule équipe.
                .WithMany() // Pas de liste de `TeamStaff` dans `Team`.
                .HasForeignKey(ts => ts.StaffTeamId) // Clé étrangère.
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade : suppression du personnel d'équipe avec l'équipe.

            // Relation entre Player et Team
            modelBuilder.Entity<Player>()
                .HasOne<Team>() // Un `Player` est associé à une seule `Team`.
                .WithMany() // Pas de liste de `Players` dans `Team`.
                .HasForeignKey(p => p.PlayerTeamId) // Clé étrangère.
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade : suppression des joueurs avec l'équipe.


            // Relation entre PointsScored et BasketballGame (Restrict pour éviter les conflits)
            modelBuilder.Entity<PointsScored>()
                .HasOne<BasketballGame>()
                .WithMany()
                .HasForeignKey(ps => ps.BasketballGameId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation entre PointsScored et Team (Restrict pour éviter les conflits)
            modelBuilder.Entity<PointsScored>()
                .HasOne<Team>()
                .WithMany()
                .HasForeignKey(ps => ps.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation entre PointsScored et Player (Cascade)
            modelBuilder.Entity<PointsScored>()
                .HasOne<Player>()
                .WithMany()
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);


            // Relation avec le joueur qui commet la faute
            modelBuilder.Entity<Foul>()
                .HasOne<Player>()
                .WithMany()
                .HasForeignKey(f => f.CommittingPlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec le joueur contre qui la faute est commise
            modelBuilder.Entity<Foul>()
                .HasOne<Player>()
                .WithMany()
                .HasForeignKey(f => f.AgainstPlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec l'équipe
            modelBuilder.Entity<Foul>()
                .HasOne<Team>()
                .WithMany()
                .HasForeignKey(f => f.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec le match
            modelBuilder.Entity<Foul>()
                .HasOne<BasketballGame>()
                .WithMany()
                .HasForeignKey(f => f.BasketballGameId)
                .OnDelete(DeleteBehavior.Cascade);


            // Relation avec le joueur qui sort
            modelBuilder.Entity<Substitution>()
                .HasOne<Player>()
                .WithMany()
                .HasForeignKey(s => s.PlayerOutId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec le joueur qui entre
            modelBuilder.Entity<Substitution>()
                .HasOne<Player>()
                .WithMany()
                .HasForeignKey(s => s.PlayerInId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec l'équipe
            modelBuilder.Entity<Substitution>()
                .HasOne<Team>()
                .WithMany()
                .HasForeignKey(s => s.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec le match
            modelBuilder.Entity<Substitution>()
                .HasOne<BasketballGame>()
                .WithMany()
                .HasForeignKey(s => s.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation entre BasketballGame et GameStaff
            modelBuilder.Entity<GameStaff>()
                .HasOne<BasketballGame>() // Un `GameStaff` est associé à un seul `BasketballGame`.
                .WithMany() // `BasketballGame` peut avoir plusieurs `GameStaff` (si navigation inverse nécessaire, ajoutez la propriété correspondante dans `BasketballGame`).
                .HasForeignKey(gs => gs.BasketballGameId) // Clé étrangère reliant `GameStaff` à `BasketballGame`.
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade : suppression du GameStaff avec le match.

            modelBuilder.Entity<TimerState>()
               .HasOne<BasketballGame>() // Un TimerState est lié à un seul BasketballGame
               .WithOne() // Un BasketballGame a un seul TimerState
               .HasForeignKey<TimerState>(ts => ts.BasketballGameId) // BasketballGameId est la clé étrangère dans TimerState
               .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade : suppression du TimerState ass

            base.OnModelCreating(modelBuilder);
        }

    }

    // Factory pour la génération du contexte avec la configuration JSON
    public class BasketballContextFactory : IDesignTimeDbContextFactory<BasketballContext>
    {
        public BasketballContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BasketballContext>();

            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new BasketballContext(optionsBuilder.Options);
        }
    }
}
