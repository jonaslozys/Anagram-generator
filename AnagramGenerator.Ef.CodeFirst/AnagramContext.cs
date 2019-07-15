using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Ef.CodeFirst.Models;

namespace AnagramGenerator.Ef.CodeFirst
{
    public class AnagramContext : DbContext
    {
        public AnagramContext(DbContextOptions<AnagramContext> options) : base(options) { }

        public DbSet<Word> Words { get; set; }
        public DbSet<CachedWord> CachedWords { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
    }
}
