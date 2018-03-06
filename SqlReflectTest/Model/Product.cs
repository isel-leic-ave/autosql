using SqlReflect.Attributes;
using System.Collections.Generic;

namespace SqlReflectTest.Model
{
    [Table("Products")]
    public class Product
    {
        [PK]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }

        public override string ToString()
        {
            return ProductName;
        }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null &&
                   ProductID == product.ProductID &&
                   ProductName == product.ProductName &&
                   EqualityComparer<Supplier>.Default.Equals(Supplier, product.Supplier) &&
                   EqualityComparer<Category>.Default.Equals(Category, product.Category) &&
                   UnitsInStock == product.UnitsInStock &&
                   UnitsOnOrder == product.UnitsOnOrder &&
                   ReorderLevel == product.ReorderLevel;
        }

        public override int GetHashCode()
        {
            var hashCode = -931275965;
            hashCode = hashCode * -1521134295 + ProductID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + EqualityComparer<Supplier>.Default.GetHashCode(Supplier);
            hashCode = hashCode * -1521134295 + EqualityComparer<Category>.Default.GetHashCode(Category);
            hashCode = hashCode * -1521134295 + UnitsInStock.GetHashCode();
            hashCode = hashCode * -1521134295 + UnitsOnOrder.GetHashCode();
            hashCode = hashCode * -1521134295 + ReorderLevel.GetHashCode();
            return hashCode;
        }
    }
}
