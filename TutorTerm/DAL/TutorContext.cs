using Microsoft.EntityFrameworkCore;
using TutorTerm.DAL.Model;

namespace TutorTerm.DAL
{
    public class TutorContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data source=s:\tutor.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();

            modelBuilder.Entity<Color>(ent =>
            {
                ent.HasData(new Color[]{
                    // nutno včetně primárních (autoincrement) klíčů
                    // později dodané hodnoty se neprojeví - insert je obsažen jen v první migraci, kdy byly do kódu přidány 
                    // pro aktualizaci je potřeba do migrace ručně doplnit "migrationBuilder.UpdateData"
                    new Color() { ColorId = 1, Name = "Blue", HexValue = "#0000FF", Alpha = true },
                    new Color() { ColorId = 2, Name = "Red", HexValue = "#FF0000", Alpha = false},
                });
                ent.Property(p => p.HexValue).IsRequired().HasMaxLength(6);
                // pokud není nullable, tak použití "IsRequired" nevygeneruje korektní migraci
                ent.Property(p => p.Alpha).HasDefaultValue(true);
            });
        }
    }
}