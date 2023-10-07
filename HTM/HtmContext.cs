using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HTMbackend.HTM;

public partial class HtmContext : DbContext
{
    protected readonly IConfiguration _configuration;
    public HtmContext()
    {
    }
    public HtmContext(IConfiguration configuration) : this()
    {
        //Console.WriteLine("Second");
        _configuration = configuration;
    }
    public HtmContext(DbContextOptions<HtmContext> options)
        : base(options)
    {
    }

    public HtmContext(DbContextOptions<HtmContext> options, IConfiguration configuration)
        : this(options)
    {
        //Console.WriteLine("forth");
        _configuration = configuration;
    }

    public virtual DbSet<Rcm> Rcms { get; set; }

    public virtual DbSet<Rcm2risk> Rcm2risks { get; set; }

    public virtual DbSet<Rcmtype> Rcmtypes { get; set; }

    public virtual DbSet<Risk> Risks { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Organization> Organizations { get; set; }

    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL(_configuration.GetValue<string>("ConnectionString:local"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rcm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rcm");

            entity.HasIndex(e => e.Rcmtype, "RCMtype");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Implement)
                .HasMaxLength(255)
                .HasColumnName("implement");
            entity.Property(e => e.NewRiskFromRcm).HasColumnName("NewRiskFromRCM");
            entity.Property(e => e.Rcmtext)
                .HasMaxLength(255)
                .HasColumnName("RCMtext");
            entity.Property(e => e.Rcmtype).HasColumnName("RCMtype");
            entity.Property(e => e.VerOfEff).HasMaxLength(255);

            entity.HasOne(d => d.RcmtypeNavigation).WithMany(p => p.Rcms)
                .HasForeignKey(d => d.Rcmtype)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rcm_ibfk_1");
        });

        modelBuilder.Entity<Rcm2risk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rcm2risk");

            entity.HasIndex(e => e.RcmId, "rcmID");

            entity.HasIndex(e => e.RiskId, "riskID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RcmId).HasColumnName("rcmID");
            entity.Property(e => e.RiskId).HasColumnName("riskID");

            entity.HasOne(d => d.Rcm).WithMany(p => p.Rcm2risks)
                .HasForeignKey(d => d.RcmId)
                .HasConstraintName("rcm2risk_ibfk_1");

            entity.HasOne(d => d.Risk).WithMany(p => p.Rcm2risks)
                .HasForeignKey(d => d.RiskId)
                .HasConstraintName("rcm2risk_ibfk_2");
        });

        modelBuilder.Entity<Rcmtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rcmtype");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Risk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("risk");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Complete)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("complete");
            entity.Property(e => e.DesignChar)
                .HasMaxLength(255)
                .HasColumnName("designChar");
            entity.Property(e => e.Harm)
                .HasMaxLength(255)
                .HasColumnName("harm");
            entity.Property(e => e.Hazard)
                .HasMaxLength(255)
                .HasColumnName("hazard");
            entity.Property(e => e.HazardSit)
                .HasMaxLength(255)
                .HasColumnName("hazardSit");
            entity.Property(e => e.LifeCycle).HasColumnName("lifeCycle");
            entity.Property(e => e.ProbPast).HasColumnName("Prob_past");
            entity.Property(e => e.ProbPre).HasColumnName("Prob_pre");
            entity.Property(e => e.RcmRational)
                .HasMaxLength(255)
                .HasColumnName("rcmRational");
            entity.Property(e => e.RiskPast).HasColumnName("risk_past");
            entity.Property(e => e.RiskPre).HasColumnName("risk_pre");
            entity.Property(e => e.Scenario)
                .HasMaxLength(255)
                .HasColumnName("scenario");
            entity.Property(e => e.SeverityPast).HasColumnName("severity_past");
            entity.Property(e => e.SeverityPre).HasColumnName("severity_pre");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.FirstName, "FirstName");

            entity.HasIndex(e => e.LastName, "LastName");

            entity.HasIndex(e => e.Username, "Username");

            entity.HasIndex(e => e.Password, "Password");

            entity.HasIndex(e => e.RoleId, "RoleId");

            entity.HasIndex(e => e.OrganizationId, "OrganizationId");

            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationId");

            entity.Property(e => e.RoleId).HasColumnName("RoleId");
            
            entity.HasOne(d => d.Organization).WithMany(p => p.Users)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("user_ibfk_1");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_ibfk_2");


        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("organization");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
