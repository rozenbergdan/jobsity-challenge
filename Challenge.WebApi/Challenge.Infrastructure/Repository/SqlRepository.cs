using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Repository
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext db;
        public SqlRepository(ChallengeDbContext context)
        {
            this.db = context;
        }

        public void Add(T item)
        {
            using (db)
            {
                db.Add(item);
                db.SaveChanges();
            }
        }

        public T Find(object id)
        {
            using (db)
            {
                return db.Set<T>().Find(id);
            }
        }

        public IEnumerable<T> List()
        {
            using (db)
            {
                return db.Set<T>().ToList();
            }
        }
    }
}
