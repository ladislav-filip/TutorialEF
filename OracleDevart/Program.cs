using System.Diagnostics;
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

            ConverterError.SampleError();

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