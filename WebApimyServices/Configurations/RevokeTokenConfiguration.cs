namespace WebApimyServices.Configurations
{
    public class RevokeTokenConfiguration : IEntityTypeConfiguration<RevokedToken>
    {
        public void Configure(EntityTypeBuilder<RevokedToken> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
