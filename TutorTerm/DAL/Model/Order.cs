using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TutorTerm.DAL.Model
{
    public class Order
    {
        public string Prefix { get; set; }
        
        /// <summary>
        /// Atribut "Range" nenastaví v DB žáden constraint
        /// </summary>
        [Range(2000,2050)]
        public int Year { get; set; }

        public int Number { get; set; }

        public string Note { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}