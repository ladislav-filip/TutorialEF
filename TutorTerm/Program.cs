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
                var data = context.Set<Color>().ToList();
                data.ForEach(p => WriteLine($"{p.ColorId} {p.Name} {p.Alpha}"));
            }


            WriteLine("Finnish.");
        }
    }
}
