using Microsoft.EntityFrameworkCore;
using P013WebSite.Entities;

namespace P013WebSite.Data
{
    public class DatabaseContext : DbContext // DatabaseContext sınıfına EntityFrameworkCore paketinden gelen DbContext sınıfından kalıtım alıyoruz böylece tüm veritabanı işlemlerini yapabileceğiz.
    {
        /*
         * Projede entity framework kullanacağız bu paketi projeye sağ tıklayıp nuget package manager > browse yolunu izleyip önce sql server paketini yüklüyoruz sql veritabanı kullanabilmek için, sql server paketi içerisinde entity framework core paketide bulunmaktadır.
         * Code first ile class larımızı kullanarak veritabanı oluşturma veya değiştirme işlemleri için de Tools paketini yüklüyoruz projeye.
         */
        public DbSet<Category> Categories { get; set; } // EntityFrameworkCore da entity class larımızı kullanarak veritabanı ile iş yapan nesneler db set
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu metot veritabanı ayarlarını yapılandırabildiğimiz metot
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; database=P013WebSite; trusted_connection=true"); // UseSqlServer ile bu projede veritabanı olarak sql server kullanacağımızı belirttik. "" içerisindeki alana connection string yani veritabanı bilgilerini yazıyoruz.
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData( // bu metot veritabanı oluştuktan sonra veritabanına kayıt eklememizi sağlıyor.
                new User // admin paneline giriş yapabilmek için en az 1 tane kullanıcı olması lazım ki bu bilgilerle giriş yapabilelim.
                {
                    Id = 1,
                    Email = "admin@P013WebSite.com",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "Admin",
                    Password = "123"
                }
                );
            // Fluent API
            modelBuilder.Entity<Category>().HasData( // kategoriler tablosuna da aşağıdaki kayıtları ekle
                new Category
                {
                    Id = 1,
                    Name = "Elektronik"
                },
                new Category
                {
                    Id = 2,
                    Name = "Bilgisayar"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
        // Not : buradaki yapılandırmayı da yaptıktan sonra Program.cs ye gidip orada databasecontext sınıfını programa tanımlamamız gerekiyor!
    }
}
