using SqlReflect.Attributes;

namespace SqlReflectTest.Model
{
    [Table("Categories")]
    public class Category
    {
        [PK]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}