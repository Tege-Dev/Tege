using System;

namespace NoteTakingApp
{
    public class User
    {
        public User() { }
        public User(string username, string name, string surname)
        {
            Name = name;
            Username = username;
            Surname = surname;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
