using System.ComponentModel.DataAnnotations.Schema;

namespace OracleDevart.DAL
{
    [Table("UZIV")]
    public class Uziv 
    {
        [Column("C_UZV")]
        public int Id { get; set; }

        public int? C_OSB { get; set; }
       
        public string PROG { get; set; }

    }
}
