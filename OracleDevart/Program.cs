using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
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

            //CommonTest();

            SaveVazby();

            WriteLine();
            WriteLine("Finnish.");
            ReadKey();
        }

        private static void CommonTest()
        {
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
        }

        static void SaveVazby()
        {
            using (var context = new DAL.FisContext())
            {
                var ent = new PtpvzVazby()
                {
                    TYP = "VZM",
                    R_DOK = 2020,
                    C_DOK = 18410201,
                    C_OPER = 10,
                    DRUH_VAZBY = "VZ"
                };
                context.Add(ent);

                ent = new PtpvzVazby()
                {
                    TYP = "VZM",
                    R_DOK = 2019,
                    C_DOK = 18410201,
                    C_OPER = 10,
                    DRUH_VAZBY = "VZ"
                };
                context.Add(ent);

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    context.ChangeTracker.Entries<PtpvzVazby>().ToList().ForEach(entr =>
                    {
                        Console.WriteLine($"{entr.State} {entr.Entity.C_DOK}");
                    });
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine(@"Entity of type ""{0}"" in state ""{1}"" 
                   has the following validation errors:",
                    eve.Entry.Entity.GetType().Name,
                    eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine(@"- Property: ""{0}"", Error: ""{1}""",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
        }

    }
}