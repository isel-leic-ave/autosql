using SqlReflect;
using SqlReflectTest.Model;
using System;
using System.Data.SqlClient;

namespace SqlReflectTest.DataMappers
{
    class CategoryDataMapper : AbstractDataMapper
    {
        const string COLUMNS = "CategoryName, Description";
        const string SQL_GET_ALL = @"SELECT CategoryID, " + COLUMNS + " FROM Categories";
        const string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE CategoryID=";
        const string SQL_INSERT = "INSERT INTO Categories (" + COLUMNS + ") OUTPUT INSERTED.CategoryID VALUES ";
        const string SQL_DELETE = "DELETE FROM Categories WHERE CategoryID = ";
        const string SQL_UPDATE = "UPDATE Categories SET CategoryName={1}, Description={2} WHERE CategoryID = {0}";

        public CategoryDataMapper(string connStr) : base(connStr)
        {
        }

        protected override string SqlGetAll()
        {
            return SQL_GET_ALL;
        }
        protected override string SqlGetById(object id)
        {
            return SQL_GET_BY_ID + id; 
        }

        protected override object Load(SqlDataReader dr)
        {
            Category c = new Category();
            c.CategoryID = (int)dr["CategoryID"];
            c.CategoryName = (string)dr["CategoryName"];
            c.Description = (string)dr["Description"];
            return c;
        }

        protected override string SqlInsert(object target)
        {
            Category c = (Category)target;
            string values = "'" + c.CategoryName + "' , '" + c.Description + "'";
            return SQL_INSERT + "(" + values + ")";
        }

        protected override string SqlUpdate(object target)
        {
            Category c = (Category)target;
            return String.Format(SQL_UPDATE,
                c.CategoryID,
                "'" + c.CategoryName + "'",
                "'" + c.Description + "'");
        }

        protected override string SqlDelete(object target)
        {
            Category c = (Category)target;
            return SQL_DELETE + c.CategoryID;
        }
    }
}
