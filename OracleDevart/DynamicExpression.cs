using AutoMapper;
using OracleDevart.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace OracleDevart
{
    public class SimpleDTO
    {
        public string Id { get; set; }

        public string Nazev { get; set; }
    }

    /// <summary>
    /// Toto v Devart nefunguje, ale v SQLite ano...
    /// </summary>
    public static class DynamicExpression
    {
        public static void SampleFullUserMap()
        {
            var conf1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entity, SimpleDTO>()
                    .ForMember(m => m.Id, opt => opt.MapFrom(f => f.ENTITA))
                    .ForMember(m => m.Nazev, opt => opt.MapFrom(ExpDto()));
            });
            //conf1.AssertConfigurationIsValid();
            var map1 = conf1.CreateMapper();


            using (var context = new FisContext())
            {
                var query = context.Set<Entity>();
                // Havaruje na:
                // System.InvalidOperationException: 'The binary operator Add is not defined for the types 'System.String' and 'System.String'.'
                var data = query.ProjectTo<SimpleDTO>(map1.ConfigurationProvider).ToList();

                data.ForEach(Program.Log);
            }
        }

        private static Expression<Func<Entity, string>> ExpDto()
        {
            return f => f.ENTITA + " " + f.NAZEV;
        }
    }
}
