namespace WebApimyServices.Seeding
{
    public class GovernorateSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Governorate>()
                        .HasData(
                       new Governorate { Id = 1,    Name = "القاهرة"},
                            new Governorate { Id = 2,    Name = "الجيزة"},
                            new Governorate { Id = 3,    Name = "الأسكندرية"},
                            new Governorate { Id = 4,    Name = "الدقهلية"},
                            new Governorate { Id = 5,    Name = "البحر الأحمر"},
                            new Governorate { Id = 6,    Name = "البحيرة"},
                            new Governorate { Id = 7,    Name = "الفيوم"},
                            new Governorate { Id = 8,    Name = "الغربية"},
                            new Governorate { Id = 9,    Name = "الإسماعيلية"},
                            new Governorate { Id = 10,   Name = "المنوفية"},
                            new Governorate { Id = 11,   Name = "المنيا"},
                            new Governorate { Id = 12,   Name = "القليوبية"},
                            new Governorate { Id = 13,   Name = "الوادي الجديد"},
                            new Governorate { Id = 14,   Name = "السويس"},
                            new Governorate { Id = 15,   Name = "اسوان"},
                            new Governorate { Id = 16,   Name = "اسيوط"},
                            new Governorate { Id = 17,   Name = "بني سويف"},
                            new Governorate { Id = 18,   Name = "بورسعيد"},
                            new Governorate { Id = 19,   Name = "دمياط"},
                            new Governorate { Id = 20,   Name = "الشرقية"},
                            new Governorate { Id = 21,   Name = "جنوب سيناء"},
                            new Governorate { Id = 22,   Name = "كفر الشيخ"},
                            new Governorate { Id = 23,   Name = "مطروح"},
                            new Governorate { Id = 24,   Name = "الأقصر"},
                            new Governorate { Id = 25,   Name = "قنا"},
                            new Governorate { Id = 26,   Name = "شمال سيناء"},
                            new Governorate { Id = 27,   Name = "سوهاج"}
                        );
        }
    }
}
