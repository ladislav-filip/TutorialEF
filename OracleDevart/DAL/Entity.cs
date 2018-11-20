using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OracleDevart.DAL
{
    [Table("ENTITY")]
    public class Entity
    {        
        public string ENTITA { get; set; }

        public string NAZEV { get; set; }
    }
}