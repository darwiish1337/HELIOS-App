
namespace WebApimyServices.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.Property(c => c.Id)
                    .HasMaxLength(50); 

            builder.Property(c => c.Name)
                    .HasMaxLength(20); 

            builder.Property(c => c.NormalizedName)
                    .HasMaxLength(20); 

            builder.Property(c => c.ConcurrencyStamp)
                    .HasMaxLength(50);
        }
    }
}
