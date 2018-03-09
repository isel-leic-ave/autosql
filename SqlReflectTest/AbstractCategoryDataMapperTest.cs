using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlReflectTest.Model;
using SqlReflect;
using System.Collections;

namespace SqlReflectTest
{
    public abstract class AbstractCategoryDataMapperTest
    {
        protected static readonly string NORTHWIND = @"
                    Server=(LocalDB)\MSSQLLocalDB;
                    Integrated Security=true;
                    AttachDbFileName=" +
                        Environment.CurrentDirectory +
                        "\\data\\NORTHWND.MDF";

        readonly IDataMapper categories;

        public AbstractCategoryDataMapperTest(IDataMapper categories)
        {
            this.categories = categories;
        }

        public void TestCategoryGetAll()
        {
            IEnumerable res = categories.GetAll();
            int count = 0;
            foreach (object p in res)
            {
                Console.WriteLine(p);
                count++;
            }
            Assert.AreEqual(8, count);
        }
        public void TestCategoryGetById()
        {
            Category c = (Category)categories.GetById(3);
            Assert.AreEqual("Confections", c.CategoryName);
            Assert.AreEqual("Desserts, candies, and sweet breads", c.Description);
        }

        public void TestCategoryInsertAndDelete()
        {
            //
            // Create and Insert new Category
            // 
            Category c = new Category()
            {
                CategoryName = "Fish",
                Description = "Live under water!"
            };
            object id = categories.Insert(c);
            //
            // Get the new category object from database
            //
            Category actual = (Category)categories.GetById(id);
            Assert.AreEqual(c.CategoryName, actual.CategoryName);
            Assert.AreEqual(c.Description, actual.Description);
            //
            // Delete the created category from database
            //
            categories.Delete(actual);
            object res = categories.GetById(id);
            actual = res != null ? (Category)res : default(Category);
            Assert.IsNull(actual.CategoryName);
            Assert.IsNull(actual.Description);
        }

        public void TestCategoryUpdate()
        {
            Category original = (Category)categories.GetById(3);
            Category modified = new Category()
            {
                CategoryID = original.CategoryID,
                CategoryName = "Mushrooms",
                Description = "Agaricus bisporus"
            };
            categories.Update(modified);
            Category actual = (Category)categories.GetById(3);
            Assert.AreEqual(modified.CategoryName, actual.CategoryName);
            Assert.AreEqual(modified.Description, actual.Description);
            categories.Update(original);
            actual = (Category)categories.GetById(3);
            Assert.AreEqual("Confections", actual.CategoryName);
            Assert.AreEqual("Desserts, candies, and sweet breads", actual.Description);
        }
    }
}
