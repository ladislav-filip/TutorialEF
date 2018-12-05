using Microsoft.EntityFrameworkCore;
using OracleDevart.DAL;
using System.Linq;

namespace OracleDevart
{
    internal static class OracleFunc
    {
        internal static void Like()
        {
            using (var context = new FisContext())
            {
                var ents = context.Set<Entity>();

                // namespace or class "Devart.Data.Oracle.Entity.OracleFunctions" not found
                //ents = ents.Where(p => Devart.Data.Oracle.Entity.OracleFunctions.Like(p.ENTITA, "HOD"));

                // created bad SQL syntax:
                // SELECT "p".ENTITA, "p".NAZEV FROM ENTITY "p" WHERE "p".ENTITA LIKE HOD
                // ORA-00904: "HOD": invalid identifier
                var data = ents.Where(p => EF.Functions.Like(p.ENTITA, "HOD"));

                data.ToList().ForEach(p => Program.Log(p));
            }
        }
    }
}
