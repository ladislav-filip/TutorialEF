using System.Diagnostics;
using System.Linq;
using TutorTerm.DAL;
using TutorTerm.DAL.Model;
using static System.Console;

namespace TutorTerm
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var context = new TutorContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                var data = context.Set<Color>().ToList();
                data.ForEach(Log);

                //context.Set<User>().ToList().ForEach(Log);
                context.Set<User>().ToList().ForEach(p => Debug.WriteLine(p.Name + " " + p.Surname));
            }


            WriteLine("Finnish.");
            ReadKey();
        }

        static void Log(object obj)
        {
            WriteLine(ObjectDumper.Dump(obj));
        }
        
        
    }
}
