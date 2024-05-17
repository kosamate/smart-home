using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SonsOfUncleBob.Database
{
    public class HistoryDbContext : DbContext
    {
        public DbSet<SignalRecord> Signals { get; private set; }
        public DbSet<Room> Rooms { get; private set; }

        public DbSet<SignalType> SignalTypes { get; private set; }

        public string DbPath { get; }

        public HistoryDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "history.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("history"); //.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SignalRecord>().HasKey(sr  => sr.Id);
            modelBuilder.Entity<SignalType>().HasKey(st => st.Id);
            modelBuilder.Entity<Room>().HasKey(r => r.Id);

        }
    }

    public class SignalRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public Room? Room { get; set; }
        public SignalType? Type { get; set; }
        public DateTime Timestamp { get; set; }
        public float Value { get;set; }


    }

    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class SignalType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UnitOfMeasure { get; set; }

    }
}