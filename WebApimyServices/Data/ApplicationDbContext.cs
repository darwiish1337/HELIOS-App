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
            builder.ApplyConfiguration(new RevokeTokenConfiguration());
            builder.ApplyConfiguration(new JobConfiguration());
            #endregion

            #region RenameTables
            //Security-Schema

            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<RevokedToken>().ToTable("RevokedTokens", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            //AppData-Schema
            builder.Entity<ApplicationUser>().ToTable("Users", "AppData");
            builder.Entity<Rate>().ToTable("Rates", "AppData");
            builder.Entity<Problems>().ToTable("Problems", "AppData");
            builder.Entity<Category>().ToTable("Categories", "AppData");
            builder.Entity<Job>().ToTable("Jobs", "AppData");

            //Address-Schema
            builder.Entity<Governorate>().ToTable("Governorates", "address");
            builder.Entity<City>().ToTable("Cities", "address");
            #endregion

            #region Seeding
            CategorySeeder.Seed(builder);
            GovernorateSeeder.Seed(builder);
            JobSeeder.Seed(builder);
            #endregion
        }

        #region DbSetOfTablesDB
        public DbSet<Problems> Problems { get; set; } 
        public DbSet<City> Cities { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<RevokedToken> RevokedTokens { get; set; }
        public DbSet<Job> Jobs { get; set; }
        #endregion
    }
}
