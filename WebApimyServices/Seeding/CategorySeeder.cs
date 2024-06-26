namespace WebApimyServices.Seeding
{
    public class CategorySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasData(
           new Category { Id = 1,  Name = "السباكة",      ImagePath = "assets/images/categories/plumbing.png" },
                new Category { Id = 2,  Name = "كهرباء",       ImagePath = "assets/images/categories/electricity.png" },
                new Category { Id = 3,  Name = "نجارة",        ImagePath = "assets/images/categories/carpentry.png" },
                new Category { Id = 4,  Name = "تكييف",        ImagePath = "assets/images/categories/hvac.png" },
                new Category { Id = 5,  Name = "دهان",         ImagePath = "assets/images/categories/painting.png" },
                new Category { Id = 6,  Name = "نظافه",        ImagePath = "assets/images/categories/cleanliness.png" },
                new Category { Id = 7,  Name = "لياسة",        ImagePath = "assets/images/categories/plastering.png" },
                new Category { Id = 8,  Name = "نقل اثاث",     ImagePath = "assets/images/categories/moving_furniture.png" },
                new Category { Id = 9,  Name = "تبليط",        ImagePath = "assets/images/categories/flooring.png" },
                new Category { Id = 10, Name = "مكافحة حشرات", ImagePath = "assets/images/categories/anti_bugs.png" },
                new Category { Id = 11, Name = "تصليح سيارات", ImagePath = "assets/images/categories/fixing_cars.png" }
            );
        }
    }
}
