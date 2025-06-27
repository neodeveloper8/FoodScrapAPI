using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace FoodScrap.Infrastructure.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dish> Dishes { get; set; }

        public virtual DbSet<Restaurant> Restaurants { get; set; }

        public virtual DbSet<RestaurantCategory> RestaurantCategories { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=.\\SQLExpress2017;Database=FoodScrapDB;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Dishes__3214EC071B4CCEB9");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.Restaurant).WithMany(p => p.Dishes)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Dishes__Restaura__3C69FB99");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Restaura__3214EC07A2CA668B");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.CategoryId).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Category).WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Restaurant_Category");
            });

            modelBuilder.Entity<RestaurantCategory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Restaura__3214EC075E38D762");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC07D3777B9B");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Comment).HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Dish).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__DishId__412EB0B6");

                entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__UserId__403A8C7D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC078E504EB9");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D105349E01891A").IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.PasswordHash).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
