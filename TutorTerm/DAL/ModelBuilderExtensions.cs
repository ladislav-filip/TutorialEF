using Microsoft.EntityFrameworkCore;
using TutorTerm.DAL.Model;

namespace TutorTerm.DAL
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Naplnění databáze počátečními daty
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>().HasData(new Color[]{
                // nutno včetně primárních (autoincrement) klíčů
                // později dodané hodnoty se neprojeví - insert je obsažen jen v první migraci, kdy byly do kódu přidány 
                // pro aktualizaci je potřeba do migrace ručně doplnit "migrationBuilder.UpdateData"
                new Color() { ColorId = 1, Name = "Blue", HexValue = "#0000FF", Alpha = true },
                new Color() { ColorId = 2, Name = "Red", HexValue = "#FF0000", Alpha = false},
            });

            modelBuilder.Entity<Language>().HasData(new Language[]
            {
                new Language() { LanguageId = 1, Name = "czech", Code = "cs"}, 
                new Language() { LanguageId = 2, Name = "polski", Code = "pl"},
                new Language() { LanguageId = 3, Name = "русский", Code = "ru"},
                new Language() { LanguageId = 4, Name = "english", Code = "en" }
            });

            modelBuilder.Entity<Gender>().HasData(new Gender[]
            {
                new Gender() { Id = 1, Name = "male" },
                new Gender() { Id = 2, Name = "female" }
            });

            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User() { UserId = 1, Name = "Petr", Surname = "Veliký", GenderId = 1, LanguageId = 3 },
                new User() { UserId = 2, Name = "Alžběta", Surname = "Druhá", GenderId = 2, LanguageId = 4 },
                new User() { UserId = 3, Name = "Karel", Surname = "Čtrvtý", GenderId = 1, LanguageId = 1 }
            });

            modelBuilder.Entity<Car>().HasData(new Car[]
            {
                new Car() { CarId = 1, UserId = 1, Name = "NJB 45-28"},
                new Car() { CarId = 2, UserId = 1, Name = "FMC 22-22"},
                new Car() { CarId = 3, UserId = 3, Name = "1T5 00-01"}
            });
        }
    }
}