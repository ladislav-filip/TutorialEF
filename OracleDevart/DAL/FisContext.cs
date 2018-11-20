using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace OracleDevart.DAL
{
    public class FisContext : DbContext
    {
        public static string CreateConnectionString()
        {
            var licKey = File.ReadAllText(@"G:\pisek\projects\TutorialEF\OracleDevart\license.key");      
            var connStr = File.ReadAllText(@"G:\pisek\projects\TutorialEF\OracleDevart\connstr.txt");
            var scb = new Devart.Data.Oracle.OracleConnectionStringBuilder(connStr);
            scb.LicenseKey = licKey;
            return scb.ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(CreateConnectionString());            
            //optionsBuilder.UseSqlite(@"Data Source=C:\Users\LF\source\repos\Devart001\Devart001\fis.db");

            var lf = new LoggerFactory();
            lf.AddProvider(new MyLoggerProvider());
            optionsBuilder.UseLoggerFactory(lf);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rkdav>(ent =>
            {
                ent.HasKey(k => k.RKDAV);
                ent.Property(p => p.RKDAV).HasMaxLength(5);
            });
                
            modelBuilder.Entity<Entity>(ent =>
            {
                ent.HasKey(k => k.ENTITA);
            });
        }
    }
}