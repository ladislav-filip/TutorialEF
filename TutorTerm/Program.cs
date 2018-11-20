using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TutorTerm.DAL;
using TutorTerm.DAL.Model;
using static System.Console;

namespace TutorTerm
{
    class Program
    {
        static Random m_rnd = new Random();
        
        static void Main(string[] args)
        {

            using (var context = new TutorContext())
            {
                // pokud DB existuje, tak jí neprve odstraníme/vymažeme
                //context.Database.EnsureDeleted();
                
                // pokud DB neexistuje, tak se automaticky vytvoří a inicializuje všemis strukturami a daty
                //context.Database.EnsureCreated();
                
                //var data = context.Set<Color>().ToList();
                //data.ForEach(Log);
                //context.Set<User>().ToList().ForEach(Log);                
                context.Set<Car>().Include(p => p.User).ToList().ForEach(Log);
                //context.Set<User>().ToList().ForEach(p => Debug.WriteLine(p.Name + " " + p.Surname));
                
                //CreateAndShowOrders();
            }


            WriteLine("Finnish.");
            ReadKey();
        }

        static void Log(object obj)
        {
            WriteLine(ObjectDumper.Dump(obj));
        }
        
        static void CreateAndShowOrders()
        {
            using (var context = new TutorContext())
            {
                var orders = context.Set<Order>();
                var yearNow = 2018;
                
                var ord = new Order()
                {
                    Prefix = "FA", 
                    Year = yearNow,
                    // vyhledáme poslední volné číslo
                    Number = 1 + orders.Where(p => p.Year == yearNow).Select(p => p.Number).DefaultIfEmpty().Max(),
                    Note = "Objednavka...",
                    Items = new List<OrderItem>()
                };
                // u položky není potřeba nastavovat hodnotu cizího klíče, doplní se automaticky
                ord.Items.Add(new OrderItem()
                {
                    Price = (decimal)m_rnd.NextDouble(),
                    Name = "pivní sud",
                    Note = "Poznámka"
                });
                orders.Add(ord);
                context.SaveChanges();
                WriteLine($"Objednávka uložena. PK položky objednávky = {ord.Items.First().ItemNumber}");
            }

            using (var context = new TutorContext())
            {
                context.Set<Order>().Include(i => i.Items).ToList().ForEach(Log);
            }
        }
    }
}
