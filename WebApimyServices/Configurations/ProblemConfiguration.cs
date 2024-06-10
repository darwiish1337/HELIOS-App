namespace WebApimyServices.Configurations
{
    public class ProblemConfiguration : IEntityTypeConfiguration<Problems>
    {
        public void Configure(EntityTypeBuilder<Problems> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(p => p.Description)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.ProblemImg)
                   .HasMaxLength(100);

            builder.Property(p => p.UserId)
                   .IsRequired();

            builder.Property(p => p.CategoryId)
                   .IsRequired();

            // Relation With User
            builder.HasOne(c => c.User) // User has many Problems
                    .WithMany(p => p.Problems) // Problems belongs to one Category
                    .HasForeignKey(p => p.UserId) // Define foreign key
                    .IsRequired(true) // UserId is Not Null in Problems
                    .OnDelete(DeleteBehavior.Restrict);

            // Relation With Category
            builder.HasOne(c => c.Category) // Category has many Problems
                    .WithMany(p => p.Problems) // Problems belongs to one Category
                    .HasForeignKey(p => p.CategoryId) // Define foreign key
                    .IsRequired(true) // CategoryId is Not Null in Problems
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.CreatedDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.Status)
                   .HasDefaultValue(false);
        }
    }
}
