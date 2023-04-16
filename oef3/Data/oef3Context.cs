using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace oef3.Data
{
    public class oef3Context : DbContext
    {
        public oef3Context (DbContextOptions<oef3Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Points>().HasKey(s => new { s.StudentId, s.CourseId }); 
        }

        public DbSet<WebApplication1.Models.Course>? Course { get; set; }

        public DbSet<WebApplication1.Models.Student>? Student { get; set; }

        public DbSet<WebApplication1.Models.Points>? Points { get; set; }


    }
}
