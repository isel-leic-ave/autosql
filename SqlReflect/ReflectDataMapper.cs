using System;
using System.Data;

namespace SqlReflect
{
    public class ReflectDataMapper : AbstractDataMapper
    {
        public ReflectDataMapper(Type klass, string connStr) : base(connStr)
        {
        }

        public ReflectDataMapper(Type klass, string connStr, bool withCache) : base(connStr, withCache)
        {
        }

        protected override object Load(IDataReader dr)
        {
            throw new NotImplementedException();
        }

        protected override string SqlGetAll()
        {
            throw new NotImplementedException();
        }

        protected override string SqlGetById(object id)
        {
            throw new NotImplementedException();
        }

        protected override string SqlInsert(object target)
        {
            throw new NotImplementedException();
        }

        protected override string SqlDelete(object target)
        {
            throw new NotImplementedException();
        }

        protected override string SqlUpdate(object target)
        {
            throw new NotImplementedException();
        }
    }
}
