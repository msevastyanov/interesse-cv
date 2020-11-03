using Interesse.DbLayer.Configuration;
using Interesse.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interesse.DbLayer.Context
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Application>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Page>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<PageCategory>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Photo>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Post>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Review>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Teacher>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Vacancy>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TemplateImage>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Application> Application { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<PageCategory> PageCategory { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }
        public DbSet<TemplateImage> TemplateImage { get; set; }


    }
}
