using System.ComponentModel.DataAnnotations.Schema;

namespace OracleDevart.DAL
{
    [Table("PTPVZ_VAZBY")]
    public class PtpvzVazby
    {
        public string TYP { get; set; }

        public int R_DOK { get; set; }

        public int C_DOK { get; set; }

        public int C_OPER { get; set; }

        public string DRUH_VAZBY { get; set; }
    }
}
