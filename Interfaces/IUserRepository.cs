using Kanban.Models;

namespace Kanban.Interfaces;

public interface IUserRepository
{
    User? GetUser(string id);
    IQueryable<User> GetUsers();
    bool UserExists(string id);
    bool CreateUser(User user);
    bool UpdateUser(User user);
    bool Save();
}