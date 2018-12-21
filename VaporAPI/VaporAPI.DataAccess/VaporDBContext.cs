using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VaporAPI.DataAccess
{
    public partial class VaporDBContext : DbContext
    {
        public VaporDBContext()
        {
        }

        public VaporDBContext(DbContextOptions<VaporDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Developer> Developer { get; set; }
        public virtual DbSet<Dlc> Dlc { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameTag> GameTag { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserDlc> UserDlc { get; set; }
        public virtual DbSet<UserGame> UserGame { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Developer>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Website).IsRequired();
            });

            modelBuilder.Entity<Dlc>(entity =>
            {
                entity.ToTable("DLC");

                entity.Property(e => e.Dlcid).HasColumnName("DLCId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Dlc)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_DLCGame");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Developer)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.DeveloperId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_GameDeveloper");
            });

            modelBuilder.Entity<GameTag>(entity =>
            {
                entity.HasKey(e => new { e.GameId, e.TagId });

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameTag)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_GameTagGame");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.GameTag)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_GameTagTag");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK_Username");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreditCard).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Wallet)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<UserDlc>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.Dlcid });

                entity.ToTable("UserDLC");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.Dlcid).HasColumnName("DLCId");

                entity.HasOne(d => d.Dlc)
                    .WithMany(p => p.UserDlc)
                    .HasForeignKey(d => d.Dlcid)
                    .HasConstraintName("FK_UserDLCDLC");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserDlc)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK_UserDLCUser");
            });

            modelBuilder.Entity<UserGame>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.GameId });

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.UserGame)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_UserGameGame");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserGame)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK_UserGameUser");
            });
        }
    }
}
