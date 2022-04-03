using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        IEnumerable<T> List();

        T Find(object id);

    }
}
