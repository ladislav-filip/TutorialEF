using OracleKlient.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OracleKlient
{
    public static class ConverterError
    {
        public static void SampleBoolToStrError()
        {
            using (var context = new FisContext())
            {
                // SELECT "p"."C_ZBO", "p"."NAZEV", "p"."SALOBAL", "p"."SESTAVA", "p"."ZOBR_INTERNET", "p"."ZRUSENO" FROM "ZBOZI" "p" WHERE "p"."ZRUSENO" = N'T' FETCH FIRST 1 ROWS ONLY
                var data = context.ZboziTable.Where(p => p.ZRUSENO == true);

                Console.WriteLine(data.First().NAZEV);
            }
        }

        public static void SampleMyEnumToStrWrong()
        {
            using (var context = new FisContext())
            {
                var data = context.ZboziTable.Where(p => p.SALOBAL == BooleanEnum.FalseValue);
                data.Take(10).ToList().ForEach(p => Program.Log(p));
            }
        }
    }
}
