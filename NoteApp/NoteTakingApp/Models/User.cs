using System;

namespace NoteTakingApp.Models
{
    public class User
    {
        public User() { }
        public User(string username, string name, string surname)
        {
            this.Name = name;
            this.Username = username;
            this.Surname = surname;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
