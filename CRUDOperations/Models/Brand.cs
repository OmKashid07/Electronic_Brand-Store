namespace CRUDOperations.Models
{
    public class Brand
    {
        internal static int id;

        public int Id { get; set; }
        public String? Name { get; set; }
        public String? Catogary  { get; set; }
        public int IsActive { get; set; }
    }
}
