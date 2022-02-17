using DVDStorage2.Data.Configurations;
using DVDStorage2.Models;
using Microsoft.EntityFrameworkCore;

namespace DVDStorage2.Data
{
    public class DvdStorageContext: DbContext
    {
        public DvdStorageContext(DbContextOptions<DvdStorageContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Dvd> Dvds { get; set; }
        public DbSet<FilmSerie> FilmSeries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Shelf> Shelves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CabinetConfiguration());
            modelBuilder.ApplyConfiguration(new DvdConfiguration());
            modelBuilder.ApplyConfiguration(new FilmSeriesConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguarion());
            modelBuilder.ApplyConfiguration(new ShelfConfiguration());
        }
    }
}
