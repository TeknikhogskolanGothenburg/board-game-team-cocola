using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<User> UserList = new List<User>();

        public List<User> ReturnList()
        {
            UserList.Add(new User() { Id = 1, UserName = "kimo", Password = "123", FirstName = "Elena", LastName = "Pippi" });
            UserList.Add(new User() { Id = 2, UserName = "popo", Password = "321", FirstName = "Fiffi", LastName = "Mårtensson" });

            return UserList;
        }
    }
}
