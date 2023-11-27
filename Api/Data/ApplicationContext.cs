using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<TeamMember> TeamMembers { get; set; } = null!;
    public DbSet<Board> Boards { get; set; } = null!;
    public DbSet<BoardColumn> BoardColumns { get; set; } = null!;
    public DbSet<BoardTask> BoardTasks { get; set; } = null!;
    public DbSet<TaskComment> TaskComments { get; set; } = null!;
    public DbSet<TaskExecutor> TaskExecutors { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskExecutor>().HasKey(e => new { e.TaskId, e.UserId });
    }
}