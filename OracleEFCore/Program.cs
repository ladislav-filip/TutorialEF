using static System.Console;

namespace OracleEFCore
{
    class Program
    {

        static void Main(string[] args)
        {
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
