using SqlReflect.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace SqlReflect
{
    public abstract class DynamicDataMapper : AbstractDataMapper
    {
        readonly string getAllStmt;
        readonly string getByIdStmt;
        readonly string insertStmt;
        readonly string deleteStmt;
        readonly string updateStmt;

        public DynamicDataMapper(Type klass, string connStr, bool withCache) : base(connStr, withCache)
        {
            TableAttribute table = klass.GetCustomAttribute<TableAttribute>();
            if (table == null) throw new InvalidOperationException(klass.Name + " should be annotated with Table custom attribute !!!!");

            PropertyInfo pk = klass
                .GetProperties()
                .First(p => p.IsDefined(typeof(PKAttribute)));

            string columns = String
                .Join(",", klass.GetProperties().Where(p => p != pk)
                .Select(p => p.Name));

            getAllStmt = "SELECT " + pk.Name + "," + columns + " FROM " + table.Name;
            getByIdStmt = getAllStmt + " WHERE " + pk.Name + "=";
            insertStmt = "INSERT INTO " + table.Name + "(" + columns + ") OUTPUT INSERTED." + pk.Name + " VALUES ";
            deleteStmt = "DELETE FROM " + table.Name + " WHERE " + pk.Name + "=";
            updateStmt = "UPDATE " + table.Name + " SET {0} WHERE " + pk.Name + "={1}";
        }

        protected override string SqlGetAll()
        {
            return getAllStmt;
        }

        protected override string SqlGetById(object id)
        {
            return getByIdStmt + id;
        }
    }
}
