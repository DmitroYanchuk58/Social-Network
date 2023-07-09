using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RGR.Models;

public partial class DbForRgrContext : DbContext
{
    public DbForRgrContext()
    {
    }

    public DbForRgrContext(DbContextOptions<DbForRgrContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=DbForRgr;uid=root;pwd=tiesta2105", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.IdAccount).HasName("PRIMARY");

            entity.ToTable("account");

            entity.HasIndex(e => e.Email, "Email_UNIQUE").IsUnique();

            entity.Property(e => e.IdAccount)
                .ValueGeneratedNever()
                .HasColumnName("idAccount");
            entity.Property(e => e.Avatarka).HasColumnType("mediumblob");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nickname).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => new { e.IdPost, e.IdAccount })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("post");

            entity.HasIndex(e => e.IdAccount, "fk_Post_Account_idx");

            entity.Property(e => e.IdPost).HasColumnName("idPost");
            entity.Property(e => e.IdAccount).HasColumnName("idAccount");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Image).HasColumnType("mediumblob");
            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Post_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
