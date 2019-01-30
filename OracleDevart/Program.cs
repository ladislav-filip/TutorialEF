using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

            Mapper.Initialize(cfg => 
                cfg.CreateMap<Uziv, UzivDTO>().ForMember(dst => dst.IsProg, opt => opt.MapFrom(src =>src.PROG == "T"))
            );
            
            using (var context = new DAL.FisContext())
            {
                var config = Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;
                
                // toto nastavení je platné pro všechny následující Query dotazy
                config.QueryOptions.CaseInsensitiveLike = true;

                var uzivs = context.Set<Uziv>();
                var qry = uzivs.Skip(10).Take(10);

                var data = qry.ProjectTo<UzivDTO>().ToList();

                WriteLine("---------------------------------------");
                data.ForEach(Log);
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

        static void Log(object obj)
        {
            WriteLine(ObjectDumper.Dump(obj));
        }

    }
}