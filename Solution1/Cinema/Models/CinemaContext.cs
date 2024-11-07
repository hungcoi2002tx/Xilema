using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Models;

public partial class CinemaContext : DbContext
{
    public CinemaContext()
    {
    }

    public CinemaContext(DbContextOptions<CinemaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Show> Shows { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =fpt-alumnidbserver.database.windows.net; database = Cinema;uid=hungnm;pwd=Hung532@;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACD430D7CBA");

            entity.Property(e => e.BookingId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("BookingID");
            entity.Property(e => e.SeatNumber).HasMaxLength(10);
            entity.Property(e => e.ShowId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("ShowID");

            entity.HasOne(d => d.Show).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ShowId)
                .HasConstraintName("FK_Bookings_Shows");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryCode).HasName("PK__Countrie__5D9B0D2D7CEC0069");

            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CountryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.FilmId).HasName("PK__Films__6D1D229CDB87EF1D");

            entity.Property(e => e.FilmId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("FilmID");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GenreId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("GenreID");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.Films)
                .HasForeignKey(d => d.CountryCode)
                .HasConstraintName("FK_Films_Countries");

            entity.HasOne(d => d.Genre).WithMany(p => p.Films)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Films_Genres");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385055E0ED94B96");

            entity.Property(e => e.GenreId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("GenreID");
            entity.Property(e => e.GenreName).HasMaxLength(100);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__3286391969BA6932");

            entity.Property(e => e.RoomId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("RoomID");
        });

        modelBuilder.Entity<Show>(entity =>
        {
            entity.HasKey(e => e.ShowId).HasName("PK__Shows__6DE3E0D25E8077F1");

            entity.Property(e => e.ShowId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("ShowID");
            entity.Property(e => e.FilmId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("FilmID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RoomId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("RoomID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Film).WithMany(p => p.Shows)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK_Shows_Films");

            entity.HasOne(d => d.Room).WithMany(p => p.Shows)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_Shows_Rooms");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F3B2CBE41");

            entity.HasIndex(e => e.Account, "UQ__Users__EA162E112C6F2521").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Account)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
