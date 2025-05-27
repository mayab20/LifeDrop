using System;
using System.Collections.Generic;
using LifeDrop.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace LifeDrop.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> appointments { get; set; }

    public virtual DbSet<Bloodinventory> bloodinventories { get; set; }

    public virtual DbSet<Donation> donations { get; set; }

    public virtual DbSet<DonationCenter> donationcenters { get; set; }

    public virtual DbSet<User> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=BloodDonationConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PRIMARY");

            entity.ToTable("appointment");

            entity.HasIndex(e => e.CenterId, "CenterId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TimeSlot).HasColumnType("time");

            entity.HasOne(d => d.Center).WithMany(p => p.appointments)
                .HasForeignKey(d => d.CenterId)
                .HasConstraintName("appointment_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.appointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("appointment_ibfk_1");
        });

        modelBuilder.Entity<Bloodinventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PRIMARY");

            entity.ToTable("bloodinventory");

            entity.HasIndex(e => e.CenterId, "CenterId");

            entity.Property(e => e.BloodType).HasMaxLength(3);

            entity.HasOne(d => d.Center).WithMany(p => p.bloodinventories)
                .HasForeignKey(d => d.CenterId)
                .HasConstraintName("bloodinventory_ibfk_1");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PRIMARY");

            entity.ToTable("donation");

            entity.HasIndex(e => e.DonorId, "DonorId");

            entity.HasIndex(e => e.RecipientId, "FK_Donation_Recipient");

            entity.Property(e => e.DonationLocation).HasMaxLength(100);

            entity.HasOne(d => d.Donor).WithMany(p => p.donationDonors)
                .HasForeignKey(d => d.DonorId)
                .HasConstraintName("donation_ibfk_1");

            entity.HasOne(d => d.Recipient).WithMany(p => p.donationRecipients)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_Recipient");
        });

        modelBuilder.Entity<DonationCenter>(entity =>
        {
            entity.HasKey(e => e.CenterId).HasName("PRIMARY");

            entity.ToTable("donationcenter");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CenterName).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.ContactNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.Username, "Username").IsUnique();

            entity.Property(e => e.BloodType).HasMaxLength(3);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.TelNumber).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
