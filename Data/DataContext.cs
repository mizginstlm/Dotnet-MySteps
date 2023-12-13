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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }//whenever the model is getting created as part of the migration the context is going to tell the migrations tooling that we have to apply that configuration that we have defined 

        public DbSet<Character>? Characters => Set<Character>();
    }




}