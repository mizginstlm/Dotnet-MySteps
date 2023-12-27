using System.Reflection;


namespace DotnetSteps.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//whenever the model is getting created as part of the migration the context is going to tell the migrations tooling that we have to apply that configuration that we have defined 

            modelBuilder.Entity<Ability>().HasData(
             new Ability { Id = 1, Name = "Fireball", Damage = 30 },
             new Ability { Id = 2, Name = "Frenzy", Damage = 20 },
             new Ability { Id = 3, Name = "Blizzard", Damage = 50 }
         );
        }

        public DbSet<Character>? Characters => Set<Character>();
        public DbSet<Power> Powers => Set<Power>();
        public DbSet<User> Users => Set<User>();

        public DbSet<Ability> Abilities => Set<Ability>();
    }


}