using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.DAL.Models
{
    public class UrlEntry
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string FullUrl { get; set; }
        public string ShortUrl { get; set; }
        public long RedirectionCount { get; set; }
        public int UrlHashCode { get; set; }

    }
}
