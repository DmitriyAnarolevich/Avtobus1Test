using ShortUrlService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.DAL.ORM
{
    public class UrlRepo : IRepository<UrlEntry>
    {
        private readonly DataContext db;
        public UrlRepo(DataContext context)
        {
            db = context;
        }

        public void Create(UrlEntry item)
        {
            item.CreationDate = DateTime.Now;
            db.Urls.Add(item);
        }

        public void Delete(long id)
        {
            var url = db.Urls.FirstOrDefault(u => u.Id == id);
            if (url != null)
            {
                db.Urls.Remove(url);
            }
        }

        public IEnumerable<UrlEntry> Find(Func<UrlEntry, bool> predicate)
        {
            return db.Urls.Where(predicate).ToList();
        }

        public UrlEntry Get(long id)
        {
            return db.Urls.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<UrlEntry> GetAll()
        {
            return db.Urls;
        }

        public void Update(UrlEntry item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
