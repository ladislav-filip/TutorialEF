using System;
using System.Linq;
using OracleDevart.DAL;

namespace OracleDevart
{
    public static class BracketMissing
    {
        public static void SampleError()
        {
            using (var context = new FisContext())
            {
                var dtNow = DateTime.Now;

                // SELECT "z".C_ZBO, "z".NAZEV
                // FROM ZBOZI "z"
                // LEFT JOIN MN "subMn" ON("z".C_ZBO = "subMn".C_ZBO) AND((("subMn".D_OD < :p__dtNow_0) AND("subMn".D_DO IS NULL OR("subMn".D_DO > :p__dtNow_1))) = 1)
                //
                // ORA-00907: missing right parenthesis
                var data = from z in context.ZboziTable
                    join subMn in context.MnTable
                        on new
                        {
                            czbo = z.C_ZBO,
                            platnost = true
                        }
                        equals new
                        {
                            czbo = subMn.C_ZBO,
                            platnost = subMn.D_OD < dtNow && (subMn.D_DO == null || subMn.D_DO > dtNow) // here
                        }
                        into defaultMn
                    from mn in defaultMn.DefaultIfEmpty()
                    select new
                    {
                        z.C_ZBO,
                        z.NAZEV
                    };


                data.ToList().ForEach(p => Program.Log(p));
            }
        }
    }
}