using ShortUrlService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.DAL.ORM
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UrlEntry> Urls { get; }
        void Save();
    }
}
