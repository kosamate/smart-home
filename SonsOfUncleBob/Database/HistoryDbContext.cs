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
            => options.UseSqlite($"Data Source={DbPath}");
    }

    public class SignalRecord
    {
        [Key, Column(Order = 0)]
        public Guid Id { get; private init; }
        public Room Room { get; private set; }
        public SignalType Type { get; private set; }
        public DateTime Timestamp { get; private set; }
        public float Value { get; private set; }

        public SignalRecord(Room room, SignalType type, DateTime timestamp, float v)
        {
            Id = Guid.NewGuid();
            Room = room;
            Type = type;
            Timestamp = timestamp;
            Value = v;
        }

    }

    public class Room
    {
        [Key, Column(Order = 0)]
        public Guid Id { get; private init; }
        public string Name { get; private set; }
        public Room(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }

    public class SignalType
    {
        [Key, Column(Order = 0)]
        public Guid Id { get; private init; }
        public string Name { get; private set; }
        public string UnitOfMeasure { get; private set; }

        public SignalType(string name, string unitOfMeasure)
        {
            Id = Guid.NewGuid();
            Name = name;
            UnitOfMeasure = unitOfMeasure;
        }
    }
}