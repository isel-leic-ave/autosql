using SqlReflect;
using SqlReflectTest.Model;
using System;
using System.Data.SqlClient;

namespace SqlReflectTest.DataMappers
{
    class SupplierDataMapper : AbstractDataMapper
    {
        const string SQL_GET_ALL = @"SELECT SupplierID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax
                                     FROM Suppliers";
        const string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE SupplierID=";

        public SupplierDataMapper(string connStr) : base(connStr)
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

        protected override string SqlInsert(object target)
        {
            throw new NotImplementedException();
        }
        protected override string SqlUpdate(object target)
        {
            throw new NotImplementedException();
        }

        protected override string SqlDelete(object target)
        {
            throw new NotImplementedException();
        }

        

        protected override object Load(SqlDataReader dr)
        {
            Supplier s = new Supplier();
            s.SupplierID = (int)dr["SupplierID"];
            s.CompanyName = (string)dr["CompanyName"];
            s.ContactName = (string)dr["ContactName"];
            s.ContactTitle = (string)dr["ContactTitle"];
            s.Address = (string)dr["Address"];
            s.City = (string)dr["City"];
            s.Region = dr["Region"] is DBNull ? null : (string)dr["Region"];
            s.PostalCode = (string)dr["PostalCode"];
            s.Country = (string)dr["Country"];
            s.Phone = (string)dr["Phone"];
            s.Fax = dr["Fax"] is DBNull ? null : (string)dr["Fax"];
            return s;
        }
    }
}
