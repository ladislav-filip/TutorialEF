namespace TutorTerm.DAL.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Address Address { get; set; }
    }
}