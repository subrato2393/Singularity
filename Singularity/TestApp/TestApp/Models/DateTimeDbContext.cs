using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestApp.Models
{
    public partial class DateTimeDbContext : DbContext
    {
        public DateTimeDbContext()
        {
        }

        public DateTimeDbContext(DbContextOptions<DateTimeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CurrentTime> CurrentTimes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NPQJR1U;Database=DateTimeDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentTime>(entity =>
            {
                entity.ToTable("CurrentTime");

                entity.Property(e => e.CurrentTime1)
                    .HasColumnType("datetime")
                    .HasColumnName("CurrentTime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
