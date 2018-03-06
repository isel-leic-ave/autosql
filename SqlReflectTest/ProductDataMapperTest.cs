using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlReflectTest.DataMappers;

namespace SqlReflectTest
{
    [TestClass]
    public class ProductDataMapperTest : AbstractProductDataMapperTest
    {
        public ProductDataMapperTest() : base(
            new ProductDataMapper(NORTHWIND),
            new CategoryDataMapper(NORTHWIND),
            new SupplierDataMapper(NORTHWIND))
        {
        }

        [TestMethod]
        public new void TestProductGetAll() {
            base.TestProductGetAll();
        }

        [TestMethod]
        public new void TestProductGetById() {
            base.TestProductGetById();
        }

        [TestMethod]
        public new void TestProductInsertAndDelete()
        {
            base.TestProductInsertAndDelete();
        }
    }
}
