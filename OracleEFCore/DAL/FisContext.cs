using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace OracleEFCore.DAL
{
    public class FisContext : DbContext
    {
        public static string CreateConnectionString()
        {   
            return File.ReadAllText(@"S:\Projects\My\TutorialEF\OracleEFCore\connstr.txt");
        }

        public DbSet<Zbozi> ZboziTable { get; set; }

        public DbSet<Mn> MnTable { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(CreateConnectionString());            

            var lf = new LoggerFactory();
            lf.AddProvider(new MyLoggerProvider());
            optionsBuilder.UseLoggerFactory(lf);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new BoolToStringConverter("F", "T");

            var myBoolConverter = new ValueConverter<bool, string>(
                v => v ? "T" : "F",
                v => v == "T"
                );

            var enumConverter = new EnumToStringConverter<BoolEnum>();

            var myEnumConverter = new ValueConverter<BooleanEnum, string>(
                v => v == BooleanEnum.TrueValue ? "T" : "F",
                v => v == "T" ? BooleanEnum.TrueValue : BooleanEnum.FalseValue
                );

            modelBuilder.Entity<Zbozi>(ent =>
            {
                ent.ToTable("ZBOZI");
                ent.HasKey(k => k.C_ZBO);
                ent.Property(p => p.NAZEV).IsRequired();
                // field ZRUSENO is Oracle: CHAR(1) NULL
                ent.Property(p => p.ZRUSENO).HasConversion(converter);
                // field ZOBR_INTERNET is Oracle: CHAR(1) NULL
                ent.Property(p => p.ZOBR_INTERNET).HasConversion(myBoolConverter);
                // field SESTAVA is Oracle: CHAR(1) NULL
                ent.Property(p => p.SESTAVA).HasConversion(enumConverter);
                // field SALOBAL is Oracle: CHAR(1) NULL
                ent.Property(p => p.SALOBAL).HasConversion(myEnumConverter);
            });

            modelBuilder.Entity<Mn>(ent =>
            {
                ent.ToTable("MN");
                ent.HasKey(k => new { k.C_MN, k.VER_MN });
                ent.Property(p => p.STDOK).HasMaxLength(80);
            });
        }
    }
}