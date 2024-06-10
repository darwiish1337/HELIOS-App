namespace WebApimyServices.Seeding
{
    public class GovernorateSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Governorate>()
                        .HasData(
                       new Governorate { Id = 1,    GoverNameAR = "القاهرة",       GoverNameEN = "Cairo" },
                            new Governorate { Id = 2,    GoverNameAR = "الجيزة",        GoverNameEN = "Giza" },
                            new Governorate { Id = 3,    GoverNameAR = "الأسكندرية",     GoverNameEN = "Alexandria" },
                            new Governorate { Id = 4,    GoverNameAR = "الدقهلية",      GoverNameEN = "Dakahlia" },
                            new Governorate { Id = 5,    GoverNameAR = "البحر الأحمر",   GoverNameEN = "Red Sea" },
                            new Governorate { Id = 6,    GoverNameAR = "البحيرة",       GoverNameEN = "Beheira" },
                            new Governorate { Id = 7,    GoverNameAR = "الفيوم",        GoverNameEN = "Fayoum" },
                            new Governorate { Id = 8,    GoverNameAR = "الغربية",       GoverNameEN = "Gharbiya" },
                            new Governorate { Id = 9,    GoverNameAR = "الإسماعيلية",    GoverNameEN = "Ismailia" },
                            new Governorate { Id = 10,   GoverNameAR = "المنوفية",      GoverNameEN = "Menofia" },
                            new Governorate { Id = 11,   GoverNameAR = "المنيا",        GoverNameEN = "Minya" },
                            new Governorate { Id = 12,   GoverNameAR = "القليوبية",     GoverNameEN = "Qaliubiya" },
                            new Governorate { Id = 13,   GoverNameAR = "الوادي الجديد", GoverNameEN = "New Valley" },
                            new Governorate { Id = 14,   GoverNameAR = "السويس",        GoverNameEN = "Suez" },
                            new Governorate { Id = 15,   GoverNameAR = "اسوان",         GoverNameEN = "Aswan" },
                            new Governorate { Id = 16,   GoverNameAR = "اسيوط",         GoverNameEN = "Assiut" },
                            new Governorate { Id = 17,   GoverNameAR = "بني سويف",      GoverNameEN = "Beni Suef" },
                            new Governorate { Id = 18,   GoverNameAR = "بورسعيد",       GoverNameEN = "Port Said" },
                            new Governorate { Id = 19,   GoverNameAR = "دمياط",         GoverNameEN = "Damietta" },
                            new Governorate { Id = 20,   GoverNameAR = "الشرقية",       GoverNameEN = "Sharkia" },
                            new Governorate { Id = 21,   GoverNameAR = "جنوب سيناء",    GoverNameEN = "South Sinai" },
                            new Governorate { Id = 22,   GoverNameAR = "كفر الشيخ",     GoverNameEN = "Kafr Al sheikh" },
                            new Governorate { Id = 23,   GoverNameAR = "مطروح",         GoverNameEN = "Matrouh" },
                            new Governorate { Id = 24,   GoverNameAR = "الأقصر",         GoverNameEN = "Luxor" },
                            new Governorate { Id = 25,   GoverNameAR = "قنا",           GoverNameEN = "Qena" },
                            new Governorate { Id = 26,   GoverNameAR = "شمال سيناء",    GoverNameEN = "North Sinai" },
                            new Governorate { Id = 27,   GoverNameAR = "سوهاج",         GoverNameEN = "Sohag" }
                        );
        }
    }
}
