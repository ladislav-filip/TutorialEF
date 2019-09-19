using System.ComponentModel.DataAnnotations.Schema;

namespace OracleKlient.DAL
{
    [Table("ENTITY")]
    public class Entity
    {        
        public string ENTITA { get; set; }

        public string NAZEV { get; set; }
    }
}