using System;
using OracleDevart.DAL;
using System.Linq;

namespace OracleDevart
{
    public static class DateTimeError
    {

        public static void SampleDateTimeError()
        {
            using (var context = new FisContext())
            {
                // tento záznam na nastaveno D_OD jako BC:
                // update mn set d_od = to_date('-4000/01/01','syyyy/mm/dd') where c_zbo = '6152';
                var data = context.MnTable.Where(p => p.C_ZBO == "6152" && p.D_OD == null).FirstOrDefault();
                if (data != null)
                {
                    Console.WriteLine($"Wrong SQL! Date is: {data.D_OD}");
                }
                
                Program.Log(data);

                // search record with date BC
                data = context.MnTable.Where(p => p.C_ZBO == "6152" && p.D_OD < DateTime.MinValue).FirstOrDefault();
                Program.Log(data);
            }
        }

    }
}