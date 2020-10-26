using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestWork.Infrastructure.Data.Entities
{
    public partial class TestRappiContext : DbContext
    {
        public TestRappiContext()
        {
        }

        public TestRappiContext(DbContextOptions<TestRappiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IdentificationTypes> IdentificationTypes { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WorkAreas> WorkAreas { get; set; }
        public virtual DbSet<WorkSubAreas> WorkSubAreas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentificationTypes>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Document)
                    .IsRequired()
                    .HasColumnName("document")
                    .HasMaxLength(32);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.IdIdentificationType).HasColumnName("idIdentificationType");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.IdWorkSubArea).HasColumnName("idWorkSubArea");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdIdentificationTypeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdIdentificationType)
                    .HasConstraintName("FK_Users_IdentificationTypes");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");

                entity.HasOne(d => d.IdWorkSubAreaNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdWorkSubArea)
                    .HasConstraintName("FK_Users_WorkAreas");
            });

            modelBuilder.Entity<WorkAreas>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WorkSubAreas>(entity =>
            {
                entity.Property(e => e.IdWorkArea).HasColumnName("idWorkArea");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdWorkAreaNavigation)
                    .WithMany(p => p.WorkSubAreas)
                    .HasForeignKey(d => d.IdWorkArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkSubAreas_WorkAreas");
            });
        }
    }
}
