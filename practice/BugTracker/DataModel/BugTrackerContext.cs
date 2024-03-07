using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.DataModel;

public partial class BugTrackerContext : DbContext
{
    public BugTrackerContext()
    {
    }

    public BugTrackerContext(DbContextOptions<BugTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessRightsLevel> AccessRightsLevels { get; set; }

    public virtual DbSet<LoginsPassword> LoginsPasswords { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceComponent> ServiceComponents { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketAction> TicketActions { get; set; }

    public virtual DbSet<TicketComment> TicketComments { get; set; }

    public virtual DbSet<TicketDeadline> TicketDeadlines { get; set; }

    public virtual DbSet<TicketHistory> TicketHistories { get; set; }

    public virtual DbSet<TicketPriority> TicketPriorities { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    public virtual DbSet<TicketsAssignment> TicketsAssignments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=BugTracker.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessRightsLevel>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_AccessRightsLevels_Name").IsUnique();
        });

        modelBuilder.Entity<LoginsPassword>(entity =>
        {
            entity.HasIndex(e => e.LoginId, "IX_LoginsPasswords_LoginId").IsUnique();

            entity.HasOne(d => d.Login).WithOne(p => p.LoginsPassword)
                .HasForeignKey<LoginsPassword>(d => d.LoginId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Services_Name").IsUnique();
        });

        modelBuilder.Entity<ServiceComponent>(entity =>
        {
            entity.HasOne(d => d.Service).WithMany(p => p.ServiceComponents)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasOne(d => d.Deadline).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.DeadlineId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.OpenedByLogin).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.OpenedByLoginId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Priority).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.ServiceComponent).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ServiceComponentId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Status).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Type).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TicketAction>(entity =>
        {
            entity.HasOne(d => d.Login).WithMany(p => p.TicketActions)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketActions)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TicketComment>(entity =>
        {
            entity.HasOne(d => d.Login).WithMany(p => p.TicketComments)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketComments)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TicketDeadline>(entity =>
        {
            entity.HasOne(d => d.Priority).WithMany(p => p.TicketDeadlines)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.ServiceComponent).WithMany(p => p.TicketDeadlines)
                .HasForeignKey(d => d.ServiceComponentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Type).WithMany(p => p.TicketDeadlines)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TicketHistory>(entity =>
        {
            entity.ToTable("TicketHistory");

            entity.HasOne(d => d.Priority).WithMany(p => p.TicketHistories)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.ServiceComponent).WithMany(p => p.TicketHistories)
                .HasForeignKey(d => d.ServiceComponentId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Status).WithMany(p => p.TicketHistories)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketHistories)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Type).WithMany(p => p.TicketHistories)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TicketPriority>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_TicketPriorities_Name").IsUnique();
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_TicketStatuses_Name").IsUnique();
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_TicketTypes_Name").IsUnique();
        });

        modelBuilder.Entity<TicketsAssignment>(entity =>
        {
            entity.ToTable("TicketsAssignment");

            entity.HasOne(d => d.Login).WithMany(p => p.TicketsAssignments)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketsAssignments)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();

            entity.HasIndex(e => e.Phone, "IX_Users_Phone").IsUnique();
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasIndex(e => e.Login, "IX_UserLogins_Login").IsUnique();

            entity.HasOne(d => d.AccessLevel).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.AccessLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
