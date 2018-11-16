namespace TutorTerm.DAL.Model
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public short LanguageId { get; set; }

        public virtual Language Language { get; set; }

        public virtual Gender Gender { get; set; }
    }
}