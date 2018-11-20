using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TutorTerm.DAL.Model;

namespace TutorTerm.DAL
{
    public class TutorContext : DbContext
    {
        /// <summary>
        /// Továrna pro logovací providery
        /// </summary>
        static readonly Microsoft.Extensions.Logging.LoggerFactory LogFactory
            = new Microsoft.Extensions.Logging.LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
        
        public DbSet<Gender> Genders { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data source=s:\tutor.db");    
                optionsBuilder.UseLoggerFactory(LogFactory);
            }            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // zapnutí podpory pro cizí klíče
            Database.ExecuteSqlCommand("PRAGMA foreign_keys = ON");

            var converter = new BoolToStringConverter("T", "F");
            
            modelBuilder.Entity<User>(ent =>
            {
                // one to many
                // entita User má jednu vlastnost Gender, kde Gender může mít kolekci vlastností User spojenou přes cizí klíč User.GenderId
                ent.HasOne<Gender>(s => s.Gender).WithMany(m => m.Users).HasForeignKey(f => f.GenderId);

                ent.OwnsOne(o => o.Address).ToTable("Address");

                ent.Property(p => p.Active)
                    .IsRequired()
                    .HasConversion(converter);

            });
            
            modelBuilder.Entity<Customer>(ent =>
            {                
            });

            modelBuilder.Entity<Color>(ent =>
            {
                ent.Property(p => p.HexValue).IsRequired().HasMaxLength(6);
                // pokud není nullable, tak použití "IsRequired" nevygeneruje korektní migraci
                ent.Property(p => p.Alpha).HasDefaultValue(true);
            });

            modelBuilder.Entity<Order>(ent =>
            {
                // nastavení názvu tabulky v databázi
                ent.ToTable("MyOrders");
                // pořadí klíče má význam pro sestavení indexu
                ent.HasKey(k => new {k.Prefix, k.Year, k.Number});
                
                                
            });

            modelBuilder.Entity<OrderItem>(ent =>
            {
                // primární klíč nemá nýzev dle konvencí, takže je nutné jej zde definovat
                ent.HasKey(k => k.ItemNumber);
                // vytvoříme vazbu přes složený klíč, s kaskádovým odstraňováním položek
                ent.HasOne<Order>(s => s.Order).WithMany(m => m.Items)
                    .HasForeignKey(f => new {f.Prefix, f.Year, f.Number})
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.Entity<Car>(ent =>
            {
                // vazba na "master" tabulku bez nutnosti definovat kolekci "childů"
                ent.HasOne<User>(s => s.User).WithMany().HasForeignKey(f => f.UserId);

                ent.Property(p => p.Name).IsRequired().HasMaxLength(50);
            });
            
            modelBuilder.Seed();
        }
    }
}