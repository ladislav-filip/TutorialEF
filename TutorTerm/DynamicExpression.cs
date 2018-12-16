using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using TutorTerm.DAL;
using TutorTerm.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace TutorTerm
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public short Vek { get; set; }
    }

    public class UserFullDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }
    }

    public static class DynamicExpression
    {
        public static void SampleUserWhere()
        {
            using (var context = new TutorContext())
            {
                var param = Expression.Parameter(typeof(User), "p");
                var exp = Expression.Lambda<Func<User, bool>>(
                    Expression.Equal(
                        Expression.Property(param, "Name"),
                        Expression.Constant("Petr")
                    ),
                    param
                );
                var query = context.Set<User>().Where(exp).ToList();
                query.ForEach(Program.Log);
            }
        }

        public static void SampleUserDynField()
        {
            using (var context = new TutorContext())
            {
                var query = context.Set<User>().AsNoTracking().Select(DynamicSelectGenerator<User>("Name,Surname")).ToList();
                //query.ForEach(Program.Log);
            }
        }

        public static void SampleUserMap()
        {
            var conf1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>()
                    .ForMember(m => m.Id, opt => opt.MapFrom(f => f.UserId));
            });
            //conf1.AssertConfigurationIsValid();
            var map1 = conf1.CreateMapper();


            using (var context = new TutorContext())
            {
                var query = context.Set<User>();
                var data = query.ProjectTo<UserDTO>(map1.ConfigurationProvider).ToList();

                //data.ForEach(Program.Log);
            }
        }

        public static void SampleFullUserMap()
        {
            var conf1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserFullDTO>()
                    .ForMember(m => m.Id, opt => opt.MapFrom(f => f.UserId))
                    .ForMember(m => m.FullName, opt => opt.MapFrom(f => f.Name + " " + f.Surname));
            });
            //conf1.AssertConfigurationIsValid();
            var map1 = conf1.CreateMapper();


            using (var context = new TutorContext())
            {
                var query = context.Set<User>();
                var data = query.ProjectTo<UserFullDTO>(map1.ConfigurationProvider).ToList();

                //data.ForEach(Program.Log);
            }
        }

        public static Func<T, T> DynamicSelectGenerator<T>(string Fields = "")
        {
            string[] EntityFields;
            if (Fields == "")
                // get Properties of the T
                EntityFields = typeof(T).GetProperties().Select(propertyInfo => propertyInfo.Name).ToArray();
            else
                EntityFields = Fields.Split(',');

            // input parameter "o"
            var xParameter = Expression.Parameter(typeof(T), "o");

            // new statement "new Data()"
            var xNew = Expression.New(typeof(T));

            // create initializers
            var bindings = EntityFields.Select(o => o.Trim())
                .Select(o =>
                {

                    // property "Field1"
                    var mi = typeof(T).GetProperty(o);

                    // original value "o.Field1"
                    var xOriginal = Expression.Property(xParameter, mi);

                    // set value "Field1 = o.Field1"
                    return Expression.Bind(mi, xOriginal);
                }
            );

            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<T, T>>(xInit, xParameter);

            // compile to Func<Data, Data>
            return lambda.Compile();
        }

        public static void SaveMockUsers(string fileJson)
        {
            var res = JsonConvert.DeserializeObject<User[]>(File.ReadAllText(fileJson));
            for (var i = 0; i < 30; i++)
            {
                using (var context = new TutorContext())
                {
                    var users = context.Set<User>();
                    var sw = Stopwatch.StartNew();
                    foreach (var u in res)
                    {
                        u.UserId = 0;
                        users.Add(u);
                    }
                    context.SaveChanges();

                    Console.WriteLine("Time elapsed: " + sw.ElapsedMilliseconds);
                }
            }
        }
    }
}
