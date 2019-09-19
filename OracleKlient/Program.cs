using OracleKlient.DAL;
using System.Linq;
using static System.Console;

namespace OracleKlient
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConverterError.SampleBoolToStrError();

            ConverterError.SampleMyEnumToStrWrong();


            //using (var context = new DAL.FisContext())
            //{

            //    var rkdavs = context.Set<Rkdav>();

            //    //var data = rkdavs.Where(p => p.NAZEV.Contains("pá") || p.RKDAV.Contains("pá")).ToList();
            //    //WriteLine("---------------------------------------");
            //    //data.ForEach(p => WriteLine($"'{p.RKDAV}' : {p.NAZEV}"));
            //    //WriteLine($"Found {data.Count} items.");

            //    var ent = context.Set<Entity>();
            //    var entData = ent.Where(p => p.ENTITA.StartsWith("E")).OrderBy(p => p.NAZEV).ToList();
            //    WriteLine();
            //    WriteLine("---------------------------------------");
            //    entData.ForEach(p => WriteLine($"'{p.NAZEV} : {p.ENTITA}'"));
            //    WriteLine($"Found {entData.Count} items.");
            //}

            WriteLine();
            WriteLine("Finnish.");
            ReadKey();
        }

        internal static void Log(object obj)
        {
            WriteLine(ObjectDumper.Dump(obj));
        }
    }
}
