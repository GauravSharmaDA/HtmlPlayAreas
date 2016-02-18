using ScriptManager.Models;
using System.Data.Entity;
using System.Configuration;

namespace ScriptManager.Contexts
{
    public class ScriptContext : DbContext
    {
        public ScriptContext() : base("Default")
        {
            Database.SetInitializer(new SeedData());            
        }

        public DbSet<Release> Releases { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubProduct> SubProducts { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Flag> Flags { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
