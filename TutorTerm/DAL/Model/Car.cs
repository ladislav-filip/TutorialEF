namespace TutorTerm.DAL.Model
{
    public class Car
    {
        public int CarId { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}