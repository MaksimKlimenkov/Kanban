using Kanban.Models;

namespace Kanban.Interfaces;

public interface IUserRepository
{
    User? GetUser(int id);
    ICollection<User> GetUsers();
}