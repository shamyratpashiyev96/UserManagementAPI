namespace UserManagementApi.Models;

public class User
{
    public int Id { get; private set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
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