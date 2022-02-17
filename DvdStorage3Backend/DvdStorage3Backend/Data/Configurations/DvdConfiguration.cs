using DVDStorage2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVDStorage2.Data.Configurations
{
    public class DvdConfiguration : IEntityTypeConfiguration<Dvd>
    {
        public void Configure(EntityTypeBuilder<Dvd> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.SpokenLanguages);
            builder.HasMany(x => x.Subtitles);
            builder.HasMany(x => x.Genres);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.Playtime).IsRequired();
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.State).IsRequired();
        }
    }
}
