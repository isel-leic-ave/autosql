using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SqlReflect
{
    public abstract class AbstractDataMapper : IDataMapper
    {
        readonly string connStr;
        readonly DataSet cache;

        public AbstractDataMapper(string connStr) : this(connStr, true)
        {
        }

        public AbstractDataMapper(string connStr, bool withCache)
        {
            this.connStr = connStr;
            if (withCache)
                cache = new DataSet();
        }

        protected abstract string SqlGetById(object id);
        protected abstract string SqlGetAll();
        protected abstract string SqlInsert(object target);
        protected abstract string SqlUpdate(object target);
        protected abstract string SqlDelete(object target);

        protected abstract object Load(IDataReader dr);

        public object GetById(object id)
        {
            string sql = SqlGetById(id);
            IEnumerator iter = Get(sql).GetEnumerator();
            return iter.MoveNext() ? iter.Current : null;
        }
        
        public IEnumerable GetAll()
        {
            return Get(SqlGetAll());
        }

        private IEnumerable Get(string sql)
        {
            string tableName = GetTableNameFromSql(sql, "FROM ");
            if (cache == null)
                return GetFromDb(sql, tableName);

            DataTable table = cache.Tables[tableName];
            return table != null
                ? DataReaderToList(table.CreateDataReader())
                : GetFromDb(sql, tableName);
        }

        private IEnumerable GetFromDb(string sql, string tableName)
        {
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = null;
            DbDataReader dr = null;
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = sql;
                con.Open();
                dr = cmd.ExecuteReader();
                dr = AddToCache(dr, tableName);
                return DataReaderToList(dr);
            }
            finally
            {
                if (dr != null) dr.Dispose();
                if (cmd != null) cmd.Dispose();
                if (con.State != ConnectionState.Closed) con.Close();
            }
        }

        private IList DataReaderToList(IDataReader dr)
        {
            IList res = new List<object>();
            while (dr.Read()) res.Add(Load(dr));
            return res;
        }

        public object Insert(object target)
        {
            string sql = SqlInsert(target);
            string tableName = GetTableNameFromSql(sql, "INTO ");
            return Execute(sql, tableName);
        }

        public void Delete(object target)
        {
            string sql = SqlDelete(target);
            string tableName = GetTableNameFromSql(sql, "FROM ");
            Execute(sql, tableName);
        }


        public void Update(object target)
        {
            string sql = SqlUpdate(target);
            string tableName = GetTableNameFromSql(sql, "UPDATE ");
            Execute(sql, tableName);
        }

        private object Execute(string sql, string tableName)
        {
            RemoveFromCache(tableName);
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = null;
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = sql;
                con.Open();
                return cmd.ExecuteScalar();
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (con.State != ConnectionState.Closed) con.Close();
            }
        }

        private DbDataReader AddToCache(DbDataReader dr, string tableName)
        {
            if (cache == null)
                return dr;
            cache.Tables.Add(tableName).Load(dr);
            return cache.Tables[tableName].CreateDataReader();
        }

        private void RemoveFromCache(string tableName)
        {
            if (cache != null && cache.Tables.Contains(tableName))
                cache.Tables.Remove(tableName);
        }

        private static string GetTableNameFromSql(string sql, string word)
        {
            return sql
                .ToUpper()
                .Split(new[] { word }, StringSplitOptions.None)
                [1]  // Last part
                .Split(' ')
                [0]; // First word
        }
    }
}