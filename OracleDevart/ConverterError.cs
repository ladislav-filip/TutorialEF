using OracleDevart.DAL;
using System.Linq;

namespace OracleDevart
{
    public static class ConverterError
    {
        public static void SampleError()
        {
            using (var context = new FisContext())
            {

                var data = context.ZboziTable.Where(p => p.ZRUSENO == true);

                /*
                 Devart.Data.Oracle.OracleException: 'ORA-01722: invalid number'

                SELECT "p".C_ZBO, "p".NAZEV, "p".ZRUSENO
                FROM ZBOZI "p"
                WHERE ("p".ZRUSENO = 1) AND (ROWNUM <= :p__p_0)

                 */
                data.Take(10).ToList().ForEach(p => Program.Log(p));
            }
        }
    }
}