namespace WebApimyServices.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.NameEN)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(c => c.NameAR)
                   .IsRequired()
                   .HasMaxLength(30);
        }
    }
}
