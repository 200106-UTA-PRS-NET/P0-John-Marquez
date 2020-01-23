using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBox.Domain.Models
{
    public partial class Entity : DbContext
    {
        public Entity()
        {
        }

        public Entity(DbContextOptions<Entity> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Records> Records { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=PizzaDb; Trusted_Connection=True;");
            }
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "PizzaStore");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("fname")
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("lname")
                    .IsUnicode(false);

                entity.Property(e => e.Pword)
                    .IsRequired()
                    .HasColumnName("pword")
                    .IsUnicode(false);

                entity.Property(e => e.Uname)
                    .IsRequired()
                    .HasColumnName("uname")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "PizzaStore");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("fname")
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("lname")
                    .IsUnicode(false);

                entity.Property(e => e.Pword)
                    .IsRequired()
                    .HasColumnName("pword")
                    .IsUnicode(false);

                entity.Property(e => e.Uname)
                    .IsRequired()
                    .HasColumnName("uname")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.ToTable("Locations", "PizzaStore");

                entity.Property(e => e.Locat)
                    .HasColumnName("locat")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza", "PizzaStore");

                entity.Property(e => e.Large)
                    .HasColumnName("large")
                    .HasColumnType("money");

                entity.Property(e => e.Med)
                    .HasColumnName("med")
                    .HasColumnType("money");

                entity.Property(e => e.PizzaType)
                    .IsRequired()
                    .HasColumnName("pizzaType")
                    .IsUnicode(false);

                entity.Property(e => e.Small)
                    .HasColumnName("small")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<Records>(entity =>
            {
                entity.ToTable("Records", "PizzaStore");

                entity.Property(e => e.AmountP).HasColumnName("amountP");

                entity.Property(e => e.Crust)
                    .IsRequired()
                    .HasColumnName("crust")
                    .IsUnicode(false);

                entity.Property(e => e.DateT)
                    .HasColumnName("dateT")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocatId).HasColumnName("locatId");

                entity.Property(e => e.PizzaType)
                    .IsRequired()
                    .HasColumnName("pizzaType")
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasColumnName("size")
                    .IsUnicode(false);

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("money");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Locat)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.LocatId)
                    .HasConstraintName("FK__Records__locatId__18EBB532");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Records__userId__17F790F9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
