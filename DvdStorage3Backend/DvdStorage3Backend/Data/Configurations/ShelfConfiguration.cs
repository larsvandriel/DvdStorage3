using DVDStorage2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVDStorage2.Data.Configurations
{
    public class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Dvds);
            builder.Property(x => x.Name);
        }
    }
}
