using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class User
    {
        public User()
        {
            Takes = new HashSet<Take>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Userpassword { get; set; }
        public int? Role { get; set; }

        public virtual ICollection<Take> Takes { get; set; }

        public User(string? username, string? userpassword)
        {
            Username = username;
            Userpassword = userpassword;
        }
    }
}
