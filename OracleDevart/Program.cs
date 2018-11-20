using System;
using System.Linq;
using OracleDevart.DAL;
using static System.Console;

namespace OracleDevart
{
class Program
    {
        private static Devart.Data.Oracle.OracleMonitor DbMonitor;
        
        static void Main(string[] args)
        {
            DbMonitor = new Devart.Data.Oracle.OracleMonitor();
            DbMonitor.IsActive = true;
            
            using (var context = new DAL.FisContext())
            {
                var config = Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;
                
                // toto nastavení je platné pro všechny následující Query dotazy
                config.QueryOptions.CaseInsensitiveLike = true;

                var rkdavs = context.Set<Rkdav>();
                //var data = rkdavs.ToList();
                //var data = rkdavs.Where(p => p.RKDAV == "1D                  ").ToList();
                //var data = rkdavs.Where(p => OracleFunctions.Like(p.RKDAV, "1D  x")).ToList();
                //var data = rkdavs.Where(p => EF.Functions.Like(p.RKDAV, "'1D   '", "*")).ToList();
                //var data = rkdavs.Where(p => p.RKDAV.Contains("1d")).ToList();
                var data = rkdavs.Where(p => p.NAZEV.Contains("pá") || p.RKDAV.Contains("pá")).ToList();
                WriteLine("---------------------------------------");
                data.ForEach(p => WriteLine($"'{p.RKDAV}' : {p.NAZEV}"));
                WriteLine($"Found {data.Count} items.");

                var ent = context.Set<Entity>();
                var entData = ent.Where(p => p.NAZEV.Contains("maj")).ToList();
                WriteLine();
                WriteLine("---------------------------------------");
                entData.ForEach(p => WriteLine($"'{p.ENTITA}' : {p.NAZEV}"));
                WriteLine($"Found {data.Count} items.");
            }

            WriteLine();
            WriteLine("Finnish.");
            ReadKey();
        }

    }
}