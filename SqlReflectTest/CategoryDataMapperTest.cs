using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlReflectTest.DataMappers;

namespace SqlReflectTest
{
    [TestClass]
    public class CategoryDataMapperTest : AbstractCategoryDataMapperTest
    {
        public CategoryDataMapperTest() : base(new CategoryDataMapper(NORTHWIND))
        {
        }

        [TestMethod]
        public new void TestCategoryGetAll() {
            base.TestCategoryGetAll();
        }

        [TestMethod]
        public new void TestCategoryGetById() {
            base.TestCategoryGetById();
        }


        [TestMethod]
        public new void TestCategoryInsertAndDelete()
        {
            base.TestCategoryInsertAndDelete();
        }

        [TestMethod]
        public new void TestCategoryUpdate() {
            base.TestCategoryUpdate();
        }
    }
}
