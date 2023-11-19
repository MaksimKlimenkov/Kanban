using Kanban.Data;
using Kanban.Interfaces;
using Kanban.Models;

namespace Kanban.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context) => _context = context;

    public User? GetUser(int id) => _context.Users.Where(u => u.Id == id).FirstOrDefault();

    public ICollection<User> GetUsers() => _context.Users.ToList();
}