using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EasyCash.Shared.Entity;

namespace EasyCash.Domain;


public class User : BaseEntity<int>
{
    [MaxLength(100)]
    public string Name { get; private set; }
    [MaxLength(100)]
    public string Email { get; private set; }
    //[MaxLength(255)]
    [JsonIgnore]
    public string Password { get; private set; }
    public Role Role { get; private set; }

    private User(string name, string email, string password, Role role, int id = 0, bool isDeleted= false)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        Id = id;
        IsDeleted = isDeleted;
    }

    public User()
    {

    }

    public void ActivateUser () => IsDeleted = false;
    public void DeactivateUser () => IsDeleted = true;

    public static User Create(string name, string email, string password, Role role, int id = 0, bool isDeleted= false)
        => new User(name, email, password, role, id, isDeleted);
}


public enum Role
{
    ADMIN = 1, USER
}


