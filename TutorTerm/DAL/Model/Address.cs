using Microsoft.EntityFrameworkCore;

namespace TutorTerm.DAL.Model
{
    [Owned]
    public class Address
    {
        public int AddressId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }
    }
}