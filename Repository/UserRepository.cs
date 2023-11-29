using Kanban.Data;
using Kanban.Interfaces;
using Kanban.Models;

namespace Kanban.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context) => _context = context;

    public bool CreateUser(User user)
    {
        _context.Add(user);
        return Save();
    }

    public User? GetUser(string id) => _context.Users.Where(u => u.Id == id).FirstOrDefault();

    public IQueryable<User> GetUsers() => _context.Users;

    public bool UpdateUser(User user)
    {
        _context.Update(user);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }

    public bool UserExists(string id) => _context.Users.Any(u => u.Id == id);
}