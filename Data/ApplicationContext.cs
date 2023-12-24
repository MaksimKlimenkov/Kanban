using Kanban.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Data;

public class ApplicationContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<TeamMember> TeamMembers { get; set; } = null!;
    public DbSet<Board> Boards { get; set; } = null!;
    public DbSet<BoardColumn> BoardColumns { get; set; } = null!;
    public DbSet<BoardTask> BoardTasks { get; set; } = null!;
    public DbSet<TaskComment> TaskComments { get; set; } = null!;
    public DbSet<TaskExecutor> TaskExecutors { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: Add unique indexes
        modelBuilder.Entity<TaskExecutor>().HasKey(e => new { e.TaskId, e.TeamMemberId });
        modelBuilder.Entity<TeamMember>(builder =>
        {
            builder.HasKey(m => new { m.UserId, m.TeamId });
            builder.Property(m => m.Status);
            builder.Property(m => m.Status).HasConversion<string>(
                s => s.ToString(),
                s => (Statuses)Enum.Parse(typeof(Statuses), s));
            builder.Property(m => m.AccessLevel).HasConversion<string>(
                l => l.ToString(),
                l => (AccessLevels)Enum.Parse(typeof(AccessLevels), l));
        });
        base.OnModelCreating(modelBuilder);
    }
}