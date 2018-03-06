using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SqlReflect
{
    public abstract class AbstractDataMapper : IDataMapper
    {
        readonly string connStr;

        public AbstractDataMapper(string connStr)
        {
            this.connStr = connStr;
        }

        protected abstract string SqlGetById(object id);
        protected abstract string SqlGetAll();
        protected abstract string SqlInsert(object target);
        protected abstract string SqlUpdate(object target);
        protected abstract string SqlDelete(object target);

        protected abstract object Load(SqlDataReader dr);

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
            IList res = new List<object>();
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = sql;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read()) res.Add(Load(dr));
            }
            finally
            {
                if (dr != null) dr.Dispose();
                if (cmd != null) cmd.Dispose();
                if (con.State != ConnectionState.Closed) con.Close();
            }
            return res;
        }

        public object Insert(object target)
        {
            string sql = SqlInsert(target);
            Console.WriteLine(sql);
            return Execute(sql);
        }

        public void Delete(object target)
        {
            Execute(SqlDelete(target));
        }


        public void Update(object target)
        {
            Execute(SqlUpdate(target));
        }

        private object Execute(string sql) {
        {
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
        }
    }
}
