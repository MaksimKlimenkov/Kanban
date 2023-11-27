using Kanban.Models;

namespace Kanban.Interfaces;

public interface IUserRepository
{
    User? GetUser(int id);
    IQueryable<User> GetUsers();
    bool UserExists(int id);
    bool CreateUser(User user);
    bool UpdateUser(User user);
    bool Save();
}