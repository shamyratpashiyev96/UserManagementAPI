using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Models;

public class User
{
    public int Id { get; private set; }

    [Required]
    [StringLength(maximumLength: 150, MinimumLength = 1)]
    public string Firstname { get; set; }

    [Required]
    [StringLength(maximumLength: 150, MinimumLength = 1)]
    public string Lastname { get; set; }


    [Required]
    public int Age { get; set; }

    public User(int id, string firstname, string lastname, int age)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Age = age;
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public void SetProperties(string firstname, string lastname, int age)
    {
        Firstname = firstname;
        Lastname = lastname;
        Age = age;
    }
}