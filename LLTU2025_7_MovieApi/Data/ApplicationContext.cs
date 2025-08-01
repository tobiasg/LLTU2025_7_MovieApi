﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LLTU2025_7_MovieApi.Models;

namespace LLTU2025_7_MovieApi.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<Actor> Actors { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries<EntityBase>().Where(entity => entity.State == EntityState.Modified))
            {
                entry.Property("UpdatedAt").CurrentValue = DateTimeOffset.UtcNow;
            }

            foreach (var entry in ChangeTracker.Entries<EntityBase>().Where(entity => entity.State == EntityState.Added))
            {
                entry.Property("CreatedAt").CurrentValue = DateTimeOffset.UtcNow;
                entry.Property("UpdatedAt").CurrentValue = DateTimeOffset.UtcNow;
            }


            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
