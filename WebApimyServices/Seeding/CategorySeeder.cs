namespace WebApimyServices.Seeding
{
    public class CategorySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasData(
           new Category { Id = 1,  NameAR = "السباكة",      NameEN = "Plumbing",         ImagePath = "assets/images/categories/plumbing.png" },
                new Category { Id = 2,  NameAR = "كهرباء",       NameEN = "Electricity",      ImagePath = "assets/images/categories/electricity.png" },
                new Category { Id = 3,  NameAR = "نجارة",        NameEN = "Carpentry",        ImagePath = "assets/images/categories/carpentry.png" },
                new Category { Id = 4,  NameAR = "تكييف",        NameEN = "HVAC",             ImagePath = "assets/images/categories/hvac.png" },
                new Category { Id = 5,  NameAR = "دهان",         NameEN = "Painting",         ImagePath = "assets/images/categories/painting.png" },
                new Category { Id = 6,  NameAR = "نظافه",        NameEN = "cleanliness",      ImagePath = "assets/images/categories/cleanliness.png" },
                new Category { Id = 7,  NameAR = "لياسة",        NameEN = "Plastering",       ImagePath = "assets/images/categories/plastering.png" },
                new Category { Id = 8,  NameAR = "نقل اثاث",     NameEN = "Moving furniture", ImagePath = "assets/images/categories/moving_furniture.png" },
                new Category { Id = 9,  NameAR = "تبليط",        NameEN = "flooring",         ImagePath = "assets/images/categories/flooring.png" },
                new Category { Id = 10, NameAR = "مكافحة حشرات", NameEN = "Anti Bugs",        ImagePath = "assets/images/categories/anti_bugs.png" },
                new Category { Id = 11, NameAR = "تصليح سيارات", NameEN = "Fixing Cars",      ImagePath = "assets/images/categories/fixing_cars.png" }
            );
        }
    }
}
