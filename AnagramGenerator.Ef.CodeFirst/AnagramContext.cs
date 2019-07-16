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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.AvailableSearches)
                .HasDefaultValue(20)
                .ValueGeneratedOnAdd();
        }
        public DbSet<Word> Words { get; set; }
        public DbSet<CachedWord> CachedWords { get; set; }
        public DbSet<SearchLog> SearchLogs { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
