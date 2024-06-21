namespace WebApimyServices.Seeding
{
    public class JobSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                        .HasData(
           new Job { Id = 1,  Name = "سباك",                           ImagePath = "assets/images/categories/plumbing.png" },
                new Job { Id = 2,  Name = "فني كهرباء",                     ImagePath = "assets/images/categories/electricity.png" },
                new Job { Id = 3,  Name   = "نجار",                         ImagePath = "assets/images/categories/carpentry.png" },
                new Job { Id = 4,  Name = "فني التدفئة والتكييف والتبريد",  ImagePath = "assets/images/categories/hvac.png" },
                new Job { Id = 5,  Name = "حرفي دهان",                      ImagePath = "assets/images/categories/painting.png" },
                new Job { Id = 6,  Name = "عامل نظافه",                     ImagePath = "assets/images/categories/cleanliness.png" },
                new Job { Id = 7,  Name = "عامل بنا",                       ImagePath = "assets/images/categories/plastering.png" },
                new Job { Id = 8,  Name = "عامل محاره",                     ImagePath = "assets/images/categories/oyster_worker.png" },
                new Job { Id = 9,  Name = "ناقل اثاث",                      ImagePath = "assets/images/categories/moving_furniture.png" },
                new Job { Id = 10, Name = "مبلط",                           ImagePath = "assets/images/categories/flooring.png" },
                new Job { Id = 11, Name = "مكافح حشرات",                    ImagePath = "assets/images/categories/anti_bugs.png" },
                new Job { Id = 12, Name = "مصلح سيارات",                    ImagePath = "assets/images/categories/fixing_cars.png" }
            );
        }
    }
}
