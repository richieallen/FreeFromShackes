namespace Swagometer.Models
{
    public class SwagItem
    {
        public string Name { get; set; }
        public string SuppliedBy { get; set; }
        public bool IsMemberOnly { get; set; }
        public bool HasBeenSwagged { get; set; }
    }
}