using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OracleDevart.DAL;

namespace OracleDevart
{
    public static class PrimitiveError
    {
        public static void SampleError()
        {
            using (var context = new FisContext())
            {
                var dtNow = DateTime.Now;

                var data = from z in context.ZboziTable
                    join subMn in context.MnTable
                        on new
                        {
                            czbo = z.C_ZBO,
                            stdok = true
                        }
                        equals new
                        {
                            czbo = subMn.C_ZBO,
                            stdok = subMn.STDOK.Contains("UVL")
                        }
                        into defaultMn
                    from mn in defaultMn.DefaultIfEmpty()
                    select new
                    {
                        z.C_ZBO,
                        z.NAZEV
                    };

                /*
                 
                System.InvalidOperationException: 'Unexpected primitive type kind 'Default'.'


                Compiling query model: 
                'from Zbozi z in DbSet<Zbozi>
                join Mn subMn in DbSet<Mn>
                on new <>f__AnonymousType1<string, bool>(
                    [z].C_ZBO, 
                    True
                ) equals new <>f__AnonymousType1<string, bool>(
                    [subMn].C_ZBO, 
                    [subMn].STDOK != null && bool [subMn].STDOK.Contains("UVL")
                ) into defaultMn
                from Mn mn in 
                    (from Mn <generated>_1 in [defaultMn]
                    select [<generated>_1]).DefaultIfEmpty()
                select new <>f__AnonymousType2<string, string>(
                    [z].C_ZBO, 
                    [z].NAZEV
                )'
                Optimized query model: 
                'from Zbozi z in DbSet<Zbozi>
                join Mn subMn in DbSet<Mn>
                on new <>f__AnonymousType1<string, bool>(
                    [z].C_ZBO, 
                    True
                ) equals new <>f__AnonymousType1<string, bool>(
                    [subMn].C_ZBO, 
                    [subMn].STDOK != null && bool [subMn].STDOK.Contains("UVL")
                ) into defaultMn
                from Mn mn in 
                    (from Mn <generated>_1 in [defaultMn]
                    select [<generated>_1]).DefaultIfEmpty()
                select new <>f__AnonymousType2<string, string>(
                    [z].C_ZBO, 
                    [z].NAZEV
                )'


                 */
                data.ToList().ForEach(p => Program.Log(p));
            }
        }

        public static void SampleCorrectly()
        {
            using (var context = new FisContext())
            {
                var dtNow = DateTime.Now;

                var data = from z in context.ZboziTable
                    join subMn in context.MnTable
                        on new
                        {
                            czbo = z.C_ZBO,
                            stdok = true
                        }
                        equals new
                        {
                            czbo = subMn.C_ZBO,
                            stdok = EF.Functions.Like(subMn.STDOK, "'%UVL%'")
                        }
                        into defaultMn
                    from mn in defaultMn.DefaultIfEmpty()
                    select new
                    {
                        z.C_ZBO,
                        z.NAZEV
                    };


                data.Take(10).ToList().ForEach(p => Program.Log(p));
            }
        }
    }
}