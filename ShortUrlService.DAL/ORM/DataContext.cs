﻿using ShortUrlService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.DAL.ORM
{
    public class DataContext : DbContext
    {
        public DbSet<UrlEntry> Urls { get; set; }
    }
}
