using System.ComponentModel.DataAnnotations.Schema;

namespace OracleKlient.DAL
{
    [Table("RKDAV")]
    public class Rkdav
    {
        public string RKDAV { get; set; }

        public string NAZEV { get; set; }
    }
}