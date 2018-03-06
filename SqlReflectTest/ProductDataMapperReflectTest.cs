using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlReflectTest.Model;
using SqlReflect;

namespace SqlReflectTest
{
    [TestClass]
    public class ProductDataMapperReflectTest : AbstractProductDataMapperTest
    {
        public ProductDataMapperReflectTest() : base(
            new ReflectDataMapper(typeof(Product), NORTHWIND),
            new ReflectDataMapper(typeof(Category), NORTHWIND),
            new ReflectDataMapper(typeof(Supplier), NORTHWIND))
        {
        }

        [TestMethod]
        public void TestProductGetAllReflect()
        {
            base.TestProductGetAll();
        }

        [TestMethod]
        public void TestProductGetByIdReflect()
        {
            base.TestProductGetById();
        }

        [TestMethod]
        public void TestProductInsertAndDeleteReflect()
        {
            base.TestProductInsertAndDelete();
        }
    }
}
