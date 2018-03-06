using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlReflectTest.DataMappers;
using SqlReflectTest.Model;
using System.Collections.Generic;
using SqlReflect;
using System.Collections;

namespace SqlReflectTest
{
    public abstract class AbstractProductDataMapperTest
    {
        protected static readonly string NORTHWIND = @"
                    Server=(LocalDB)\MSSQLLocalDB;
                    Integrated Security=true;
                    AttachDbFileName=" +
                        Environment.CurrentDirectory +
                        "\\data\\NORTHWND.MDF";

        readonly IDataMapper prods;
        readonly IDataMapper categories;
        readonly IDataMapper suppliers;

        public AbstractProductDataMapperTest(IDataMapper prods, IDataMapper categories, IDataMapper suppliers)
        {
            this.prods = prods;
            this.categories = categories;
            this.suppliers = suppliers;
        }

        public void TestProductGetAll()
        {
            IEnumerable res = prods.GetAll();
            int count = 0;
            foreach (object p in res)
            {
                Console.WriteLine(p);
                count++;
            }
            Assert.AreEqual(77, count);
        }
        public void TestProductGetById()
        {
            Product p = (Product)prods.GetById(10);
            Assert.AreEqual("Ikura", p.ProductName);
            Assert.AreEqual("Seafood", p.Category.CategoryName);
            Assert.AreEqual("Tokyo Traders", p.Supplier.CompanyName);
        }
        public void TestProductInsertAndDelete()
        {
            //
            // Create and insert a new product
            //
            Category c = (Category)categories.GetById(4);
            Supplier s = (Supplier)suppliers.GetById(17);
            Product p = new Product() {
                Category = c,
                Supplier = s,
                ProductName = "Bacalhau",
                ReorderLevel = 23,
                UnitsInStock = 100,
                UnitsOnOrder = 40
            };
            object id = prods.Insert(p);
            //
            // Get the new product object from database
            //
            Product actual = (Product)prods.GetById(id);
            Assert.AreEqual(p.ProductName, actual.ProductName);
            Assert.AreEqual(p.UnitsInStock, actual.UnitsInStock);
            //
            // Delete the created product from database
            //
            prods.Delete(actual);
            actual = (Product)prods.GetById(id);
            Assert.IsNull(actual);
        }

    }
}
