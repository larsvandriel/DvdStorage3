using DVDStorage2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVDStorage2.Data.Configurations
{
    public class FilmSeriesConfiguration : IEntityTypeConfiguration<FilmSerie>
    {
        public void Configure(EntityTypeBuilder<FilmSerie> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Dvds);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
        }
    }
}
