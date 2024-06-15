namespace WebApimyServices.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Configurations
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new GovernorateConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProblemConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RateConfiguration());
            #endregion

            #region RenameTables
            //Security-Schema
            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            //Address-Schema
            builder.Entity<Governorate>().ToTable("Governorates", "address");
            builder.Entity<City>().ToTable("Cities", "address");
            #endregion

            #region Seeding
            CategorySeeder.Seed(builder);
            GovernorateSeeder.Seed(builder);
            #endregion
        }

        #region DbSetOfTablesDB
        public DbSet<Problems> Problems { get; set; } 
        public DbSet<City> Cities { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rate> Rates { get; set; }
        #endregion
    }
}
