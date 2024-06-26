namespace WebApimyServices.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(30);
        }
    }
}
