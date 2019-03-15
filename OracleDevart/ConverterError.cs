using System;
using OracleDevart.DAL;
using System.Linq;

namespace OracleDevart
{
    public static class ConverterError
    {
        public static void SampleBoolToStrError()
        {
            using (var context = new FisContext())
            {
                // TODO: toto je opraveno ve verzi 9.6.696, vyřešili to takto: "WHERE ((DBMS_LOB.COMPARE("p".ZOBR_INTERNET, TO_NCLOB('F'))) = 0)"
                var data = context.ZboziTable.Where(p => p.ZRUSENO == true);

                /*
                 System.FormatException: 'String was not recognized as a valid Boolean.'

                (QueryContext queryContext) => IEnumerable<Zbozi> _InterceptExceptions(
                    source: IEnumerable<Zbozi> _TrackEntities(
                        results: IEnumerable<Zbozi> _ToSequence(() => Zbozi First(IEnumerable<Zbozi> _ShapedQuery(
                                    queryContext: queryContext, 
                                    shaperCommandContext: SelectExpression: 
                                        SELECT "p".C_ZBO, "p".NAZEV, "p".ZRUSENO
                                        FROM ZBOZI "p"
                                        WHERE ((DBMS_LOB.COMPARE("p".ZRUSENO, TO_NCLOB('T'))) = 0) AND ((ROWNUM) <= 1), 
                                    shaper: UnbufferedEntityShaper<Zbozi>))), 
                        queryContext: queryContext, 
                        entityTrackingInfos: { itemType: Zbozi }, 
                        entityAccessors: List<Func<Zbozi, object>> 
                        { 
                            Func<Zbozi, Zbozi>, 
                        }
                    ), 
                    contextType: OracleDevart.DAL.FisContext, 
                    logger: DiagnosticsLogger<Query>, 
                    queryContext: queryContext)

                 */
                Console.WriteLine(data.First().NAZEV);
            }
        }

        public static void SampleMyBoolToStrError()
        {
            using (var context = new FisContext())
            {
                // TODO: toto je opraveno ve verzi 9.6.696, vyřešili to takto: "WHERE ((DBMS_LOB.COMPARE("p".ZOBR_INTERNET, TO_NCLOB('F'))) = 0)"
                var data = context.ZboziTable.Where(p => p.ZOBR_INTERNET == false);

                /*
                System.FormatException: 'String was not recognized as a valid Boolean.'

                (QueryContext queryContext) => IEnumerable<Zbozi> _InterceptExceptions(
                source: IEnumerable<Zbozi> _TrackEntities(
                    results: IEnumerable<Zbozi> _ShapedQuery(
                        queryContext: queryContext, 
                        shaperCommandContext: SelectExpression: 
                            SELECT "p".C_ZBO, "p".NAZEV, "p".ZOBR_INTERNET, "p".ZRUSENO
                            FROM ZBOZI "p"
                            WHERE ((DBMS_LOB.COMPARE("p".ZOBR_INTERNET, TO_NCLOB('T'))) = 0) AND ((ROWNUM) <= :p__p_0), 
                        shaper: UnbufferedEntityShaper<Zbozi>), 
                    queryContext: queryContext, 
                    entityTrackingInfos: { itemType: Zbozi }, 
                    entityAccessors: List<Func<Zbozi, object>> 
                    { 
                        Func<Zbozi, Zbozi>, 
                    }
                ), 
                contextType: OracleDevart.DAL.FisContext, 
                logger: DiagnosticsLogger<Query>, 
                queryContext: queryContext)
                Opening connection to database '' on server 'slon'.
                Opened connection to database '' on server 'slon'.
                Closing connection to database '' on server 'slon'.
                 */
                data.Take(10).ToList().ForEach(p => Program.Log(p));
            }
        }

        public static void SampleMyEnumToStrWrong()
        {
            using (var context = new FisContext())
            {
                var data = context.ZboziTable.Where(p => p.SALOBAL == BooleanEnum.FalseValue);
                /*
                
                Here incorrect SQL:                
                SELECT "p".C_ZBO, "p".NAZEV, "p".SALOBAL, "p".SESTAVA, "p".ZOBR_INTERNET, "p".ZRUSENO
                FROM ZBOZI "p"
                WHERE ("p".SALOBAL = 'FalseValue') AND ((ROWNUM) <= :p__p_0) -- SALOBAL is CHAR 1 (T/F)

                 */
                data.Take(10).ToList().ForEach(p => Program.Log(p));
            }
        }

        /// <summary>
        /// Workarround for BOOL values - convert to two states Enum
        /// </summary>
        public static void SampleEnumToStrCorrectly()
        {
            using (var context = new FisContext())
            {
                var data = context.ZboziTable.Where(p => p.SESTAVA != BoolEnum.T);
                data.Take(10).ToList().ForEach(p => Program.Log(p));
            }
        }

        public static void SampleBoolToStrUpdateCorrectly()
        {
            using (var context = new FisContext())
            {
                var data = context.ZboziTable.OrderBy(p => p.C_ZBO).First();
                data.ZRUSENO = !data.ZRUSENO;
                Console.WriteLine($"{data.C_ZBO}: {data.NAZEV}");
                context.SaveChanges();
            }
        }

        public static void SampleMyEnumToStrUpdateCorrectly()
        {
            using (var context = new FisContext())
            {
                var data = context.ZboziTable.OrderBy(p => p.C_ZBO).First();
                data.SALOBAL = data.SALOBAL == BooleanEnum.TrueValue ? BooleanEnum.FalseValue : BooleanEnum.TrueValue;
                Console.WriteLine($"{data.C_ZBO}: {data.NAZEV}");
                context.SaveChanges();
            }
        }

    }
}