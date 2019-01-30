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

                config.QueryOptions.CaseInsensitiveLike = true;
                var data = LoadPure(context);

                WriteLine("---------------------------------------");
                data.ForEach(Log);
                WriteLine($"Found {data.Count} items.");

                data = LoadByPagging(context);
                #region System.IndexOutOfRangeException: 'Index was outside the bounds of the array.'

                // Wrong SQL, missing "IsProg" column
                /*
                SELECT "t"."Id"
                FROM(
                    SELECT "dtoUziv".C_UZV AS "Id", CASE
                        WHEN(DBMS_LOB.COMPARE("dtoUziv".PROG, 'T')) = 0
                        THEN 1 ELSE 0
                    END AS "IsProg", ROWNUM AS "ROWNUM"
                    FROM UZIV "dtoUziv"
                ) "t"
                WHERE("t"."ROWNUM" > :p__p_0) AND("t"."ROWNUM" <= (: p__p_0 + :p__p_0))
                */

                #endregion

                WriteLine("---------------------------------------");
                data.ForEach(Log);
                WriteLine($"Found {data.Count} items.");
            }

            WriteLine();
            WriteLine("Finnish.");
            ReadKey();
        }

        private static System.Collections.Generic.List<UzivDTO> LoadByPagging(FisContext context)
        {
            var uzivs = context.Set<Uziv>();
            var qry = uzivs.Skip(10).Take(10);

            var data = qry.ProjectTo<UzivDTO>().ToList();
            return data;
        }

        private static System.Collections.Generic.List<UzivDTO> LoadPure(FisContext context)
        {
            var uzivs = context.Set<Uziv>();
            var qry = uzivs;

            var data = qry.ProjectTo<UzivDTO>().ToList();
            return data;
        }

        static void Log(object obj)
        {
            WriteLine(ObjectDumper.Dump(obj));
        }

    }
}