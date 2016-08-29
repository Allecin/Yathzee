using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityFramework
{
    //Manages the entity objects during run time
    //Fills objects with data from the database
    //Tables, relationships are defined in this class
    public class YathzeeContext : DbContext
    {
        public YathzeeContext() : base("Yathzee_EFCodefirst_local")
        {
            Database.SetInitializer<YathzeeContext>(new YathzeeInitialiser());

        }

        //Basic Sets
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameScore> GameScores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove pluralizing tablenames
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Remove cascading delete for all relationships
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Player>().HasMany(a => a.GameScores);
            modelBuilder.Entity<Player>().HasMany(a => a.Games);
            modelBuilder.Entity<Game>().HasMany(a => a.GameScores);

        }
    }
}
