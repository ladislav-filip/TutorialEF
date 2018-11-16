namespace TutorTerm.DAL.Model
{
    /// <summary>
    /// Barvy jsou v iniciační migraci také do DB již naplněny
    /// </summary>
    public class Color
    {
        public int ColorId { get; set; }

        public string Name { get; set; }

        public string HexValue { get; set; }

        public bool Alpha { get; set; }
    }
}