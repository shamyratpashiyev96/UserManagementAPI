using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Models;

namespace UserManagementApi.Services;

public interface IUserService
{
    List<User> GetList();

    void AddNewUser(User newUser);

    User UpdateUser(User updatedUser);

    void DeleteUser(int id);
}

public class UserService : IUserService
{
    private List<User> _users = new List<User>
        {
            new (1, "Alice", "Johnson", 28),
            new (2, "Robert", "Smith", 34),
            new (3, "Elena", "Rodriguez", 25),
            new (4, "Michael", "Chen", 42),
            new (5, "Sarah", "Williams", 31)
        };

    public List<User> GetList()
    {
        return _users;
    }

    public void AddNewUser(User newUser)
    {
        var latestId = _users.OrderByDescending(x => x.Id).FirstOrDefault();
        newUser.SetId(latestId?.Id + 1 ?? 1);
        _users.Add(newUser);
    }

    public User UpdateUser(User updatedUser)
    {
        var targetUser = _users.Where(x => x.Id == updatedUser.Id).FirstOrDefault();
        if (targetUser == default)
        {
            throw new Exception("Target User is not found.");
        }

        targetUser.SetProperties(updatedUser.Firstname, updatedUser.Lastname, updatedUser.Age);
        return targetUser;
    }

    public void DeleteUser(int id)
    {
        var targetUser = _users.Where(x => x.Id == id).FirstOrDefault();
        if (targetUser == default)
        {
            throw new Exception("Target User is not found.");
        }

        _users.Remove(targetUser);
    }
}