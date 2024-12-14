using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreTheyUsingAI.Services
{
    public abstract class DbService<T>
    {
        internal string _connectionString;
        public abstract List<T> Get(long id);
        public abstract bool Post(T newObj);
    }
}
