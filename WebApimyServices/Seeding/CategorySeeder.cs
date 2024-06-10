namespace WebApimyServices.Seeding
{
    public class CategorySeeder 
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasData(
                new Category { Id = 1, NameAR = "السباكة",      NameEN = "Plumbing" },
                     new Category { Id = 2, NameAR = "كهرباء",       NameEN = "Electricity" },
                     new Category { Id = 3, NameAR = "نجارة",        NameEN = "Carpentry" },
                     new Category { Id = 4, NameAR = "تكييف",        NameEN = "HVAC" },
                     new Category { Id = 5, NameAR = "دهان",         NameEN = "Painting" },
                     new Category { Id = 6, NameAR = "نظافه",        NameEN = "cleanliness" },
                     new Category { Id = 7, NameAR = "لياسة",        NameEN = "Plastering" },
                     new Category { Id = 8, NameAR = "نقل اثاث",     NameEN = "Moving furniture" },
                     new Category { Id = 9, NameAR = "تبليط",        NameEN = "flooring" },
                     new Category { Id = 10, NameAR = "مكافحة حشرات", NameEN = "Anti Bugs" }
            );
        }
    }
}
