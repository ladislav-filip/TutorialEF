using System.ComponentModel.DataAnnotations.Schema;

namespace OracleDevart.DAL
{
    [Table("UZIV")]
    public class Uziv 
    {
        // TODO : po oprave converteru zmenit na Bool zakomentene veci

        [Column("C_UZV")]
        public int Id { get; set; }

        //public virtual ICollection<FisnetNast> FisnetNastCollection { get; set; } = new List<FisnetNast>();

        //public virtual ICollection<Hlmenu> HlmenuCollection { get; set; } = new List<Hlmenu>();

        //public virtual ICollection<Param> ParamCollection { get; set; } = new List<Param>();

        //public virtual Uzvhes Uzvhes { get; set; }

        public int? C_OSB { get; set; }

        //public string EMAIL { get; set; }

        //public string FUNKCE { get; set; }

        //public string JMENO { get; set; }

        //public string ORG { get; set; }

        //public string PRACOVISTE { get; set; }
        
        public string PROG { get; set; }
        //public bool? PROG { get; set; }

        //    public string SKUPINA { get; set; }
        //    //public bool? SKUPINA { get; set; }

        //    public string SU { get; set; }
        //    //public bool? SU { get; set; }

        //    public string TEL { get; set; }

        //    public string ZABLOKOVANO { get; set; }
        //    //public bool? ZABLOKOVANO { get; set; }

        //    public string ZRUSENO { get; set; }
        //    //public bool? ZRUSENO { get; set; }

        //    public string Nazev => $"[{Id}] {JMENO}";
        //
    }
}
