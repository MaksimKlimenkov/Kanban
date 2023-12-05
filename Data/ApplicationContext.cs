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
        modelBuilder.Entity<TeamMember>().HasKey(e => new { e.UserId, e.TeamId });
        base.OnModelCreating(modelBuilder);
    }
}