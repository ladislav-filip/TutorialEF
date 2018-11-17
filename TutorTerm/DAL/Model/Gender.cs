using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutorTerm.DAL.Model
{
    [Table("Gender")]
    public class Gender
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}