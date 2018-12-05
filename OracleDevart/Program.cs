using System.Diagnostics;
using System.Linq;
using OracleDevart.DAL;
using static System.Console;

namespace OracleDevart
{
    /// <summary>
    /// assemblyref://Devart.Data.Oracle.EFCore (9.6.646)
    /// </summary>
    class Program
    {
        private static Devart.Data.Oracle.OracleMonitor DbMonitor;
        
        static void Main(string[] args)
        {
            DbMonitor = new Devart.Data.Oracle.OracleMonitor();
            DbMonitor.IsActive = true;

            #region Bugs

            DateTimeError.SampleDateTimeError();

            #endregion

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