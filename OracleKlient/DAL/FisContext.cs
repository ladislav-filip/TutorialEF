using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using static System.Console;

namespace OracleKlient.DAL
{
    /// <summary>
    /// https://docs.oracle.com/cd/E85694_01/ODPNT/ConnectionSetSessionInfo.htm
    /// 
    /// https://docs.oracle.com/database/121/ODPNT/OracleGlobalizationClass.htm#ODPNT3155
    /// </summary>
    public class FisContext : DbContext
    {
        public static string CreateConnectionString()
        {
            //var licKey = File.ReadAllText(@"G:\pisek\projects\TutorialEF\OracleDevart\license.key");
            //var connStr = File.ReadAllText(@"G:\pisek\projects\TutorialEF\OracleDevart\connstr.txt");

            var connStr = @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=orafisdata)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=fisdata)));User Id=KUN;Password=kkkk;";
            var scb = new OracleConnectionStringBuilder(connStr);
            return scb.ToString();
        }
        public FisContext()
        {
            Database.GetDbConnection().StateChange += ConnectionOnStateChange;
        }

        public DbSet<Zbozi> ZboziTable { get; set; }

        private void ConnectionOnStateChange(object sender, StateChangeEventArgs e)
        {
            if (e.OriginalState == ConnectionState.Open || e.CurrentState != ConnectionState.Open)
            {
                return;
            }

            var con = Database.GetDbConnection() as OracleConnection;
            var info = con.GetSessionInfo();
            // ALTER SESSION SET NLS_SORT = BINARY_CI NLS_COMP = LINGUISTIC; 
            // ALTER SESSION SET NLS_SORT = XCZECH;
            info.Sort = "XCZECH";

            WriteLine(ObjectDumper.Dump(info));
            
            

            con.SetSessionInfo(info);
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
