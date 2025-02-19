using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GyrusWebAPI.Models
{
    public partial class DemoAPICoreDatabaseContext : DbContext
    {
        public DemoAPICoreDatabaseContext()
        {
        }

        public DemoAPICoreDatabaseContext(DbContextOptions<DemoAPICoreDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserMaster> UserMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-8TCMRKVA\\MSSQLSERVER01;Initial Catalog=DemoAPICoreDatabase;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("UserMaster");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Address)
                    .HasColumnType("text")
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .HasColumnName("email")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password")
                    .IsFixedLength();

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .HasColumnName("username")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
