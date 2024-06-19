namespace WebApimyServices.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(u => u.DisplayName)
                   .HasMaxLength(40)
                   .IsRequired(false);

            builder.Property(u => u.UserType)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(u => u.Job)
                   .HasMaxLength(30)
                   .IsRequired(false);

            builder.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasAnnotation("EmailAddress", true);

            builder.Property(u => u.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasAnnotation("Phone", true);

            builder.Property(u => u.ProfilePicture)
                   .HasMaxLength(150);

            builder.HasMany(c => c.Problems) // User has many Problems
                   .WithOne(b => b.User) // Problems belongs to one User
                   .HasForeignKey(b => b.UserId) // Define foreign key
                   .IsRequired(true); // UserId is Not Nullable in Book

            builder.Property(p => p.CreatedDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(d => d.DisplayName)
                   .HasComputedColumnSql("[firstName] +' '+ [lastName]");
        }
    }
}
