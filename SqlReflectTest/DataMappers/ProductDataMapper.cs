using SqlReflect;
using SqlReflectTest.Model;
using System;
using System.Data;

namespace SqlReflectTest.DataMappers
{
    class ProductDataMapper : AbstractDataMapper
    {
        const string COLUMNS = "ProductName, SupplierID, CategoryID, UnitsInStock, UnitsOnOrder, ReorderLevel";
        const string SQL_GET_ALL = "SELECT ProductID, " + COLUMNS + " FROM Products";
        const string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE ProductId=";
        const string SQL_INSERT = "INSERT INTO Products (" + COLUMNS + ") OUTPUT INSERTED.ProductId VALUES ";
        const string SQL_DELETE = "DELETE FROM Products WHERE ProductId = ";

        readonly IDataMapper categories;
        readonly IDataMapper suppliers;

        public ProductDataMapper(string connStr) : base(connStr)
        {
            categories = new CategoryDataMapper(connStr);
            suppliers = new SupplierDataMapper(connStr);
        }

        protected override string SqlGetAll()
        {
            return SQL_GET_ALL;
        }
        protected override string SqlGetById(object id)
        {
            return SQL_GET_BY_ID + id;
        }

        protected override string SqlInsert(object target)
        {
            Product p = (Product)target;
            string values = "('" + p.ProductName + "', "
                + "'" + p.Supplier.SupplierID + "', "
                + "'" + p.Category.CategoryID+ "', "
                + "'" + p.UnitsInStock + "', "
                + "'" + p.UnitsOnOrder + "', "
                + "'" + p.ReorderLevel + "')";
            return SQL_INSERT + values;
        }

        protected override string SqlUpdate(object target)
        {
            throw new NotImplementedException();
        }

        protected override string SqlDelete(object target)
        {
            Product p = (Product)target;
            return SQL_DELETE + p.ProductID;
        }
        
        protected override object Load(IDataReader dr)
        {
            Product p = new Product();
            p.ProductID = (int) dr["ProductID"];
            p.ProductName = (string) dr["ProductName"];
            p.Supplier = (Supplier) suppliers.GetById(dr["SupplierID"]);
            p.Category = (Category) categories.GetById(dr["CategoryID"]);
            p.UnitsInStock = (short) dr["UnitsInStock"];
            p.UnitsOnOrder = (short) dr["UnitsOnOrder"];
            p.ReorderLevel = (short) dr["ReorderLevel"];
            return p;
        }
    }
}
