using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WpfApp_CPTest_III
{
    // Представляет таблицу в БД
    public class CompanyTableCustom
    {
        public string ИНН { get; set; }
        public string Наименование { get; set; }
        public string Город { get; set; }
        public string Область { get; set; }
        public bool Открыт { get; set; }
    }

    public class CompanyDbContext : DbContext
    {
        // Определение набора данных (DbSet) для работы с таблицей CompanyTableCustom. DbSet представляет собой
        // набор объектов, которые соответствуют строкам таблицы в базе данных.
        public DbSet<CompanyTableCustom> Companies { get; set; }

        // задания конфигурации подключения к базе данных.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Если конфигурация еще не определена
            if (!optionsBuilder.IsConfigured)
            {
                // Использование строки подключения для определения параметров сервера, базы данных и настроек безопасности.
                optionsBuilder.UseSqlServer("Server=LAPTOP-K6VSE9NR;Database=CompanyDB;Integrated Security=true;Encrypt=true;TrustServerCertificate=true");
            }
        }

        // needed for keyless values
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Указание того, что в данном случае у сущности CompanyTableCustom нет первичного ключа.
            modelBuilder.Entity<CompanyTableCustom>().HasNoKey();
            // Задание схемы и имени таблицы, к которой будет привязана сущность CompanyTableCustom.
            modelBuilder.Entity<CompanyTableCustom>().ToTable("CompanyTableCustom", "dbo");
        }
    }

    public class CompanyDataAccess
    {
        private readonly CompanyDbContext _dbContext;

        public CompanyDataAccess()
        {
            // Создаем экземпляр контекста базы данных
            _dbContext = new CompanyDbContext();
        }

        // Метод для получения списка всех компаний из базы данных
        public List<CompanyTableCustom> GetAllCompanies()
        {
            return _dbContext.Companies.ToList();
        }

        // Метод для получения компании по её ИНН
        public CompanyTableCustom GetCompanyByINN(string inn)
        {
            return _dbContext.Companies.FirstOrDefault(c => c.ИНН == inn);
        }

        // Получение массива открытых компаний
        public List<List<string>> GetOpenCompanies()
        {
            List<CompanyTableCustom> openCompanies = _dbContext.Companies.Where(c => c.Открыт).ToList();
            return openCompanies.Select(c => new List<string> { c.ИНН, c.Наименование, c.Город, c.Область }).ToList();
        }

        // Получение массива закрытых компаний
        public List<List<string>> GetClosedCompanies()
        {
            List<CompanyTableCustom> closedCompanies = _dbContext.Companies.Where(c => !c.Открыт).ToList();
            return closedCompanies.Select(c => new List<string> { c.ИНН, c.Наименование, c.Город, c.Область }).ToList();
        }
    }
}
