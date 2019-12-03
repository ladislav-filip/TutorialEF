namespace OracleKlient.DAL
{
    public class Zbozi
    {
        public string C_ZBO { get; set; }

        public string NAZEV { get; set; }

        public bool ZRUSENO { get; set; }

        public bool ZOBR_INTERNET { get; set; }

        public BoolEnum SESTAVA { get; set; }

        public BooleanEnum SALOBAL { get; set; }
    }

    public enum BooleanEnum
    {
        FalseValue,

        TrueValue
    }

    public enum BoolEnum
    {
        F,

        T
    }
}
