using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortUrlService.DAL.Models;

namespace ShortUrlService.DAL.ORM
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext db;
        private UrlRepo urlRepo;

        public IRepository<UrlEntry> Urls
        {
            get
            { 
               if (urlRepo == null)
                {
                    urlRepo = new UrlRepo(db);
                }

                return urlRepo;
            }
        }

        public UnitOfWork()
        {
            db = new DataContext();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
