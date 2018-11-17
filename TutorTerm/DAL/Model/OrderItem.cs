namespace TutorTerm.DAL.Model
{
    public class OrderItem
    {
        /// <summary>
        /// Toto je primární klíč a je potřeba jej explicitně nastavit 
        /// </summary>
        public int ItemNumber { get; set; }
        
        public string Prefix { get; set; }
        
        public int Year { get; set; }

        public int Number { get; set; }

        public string Note { get; set; }

        public virtual Order Order { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}