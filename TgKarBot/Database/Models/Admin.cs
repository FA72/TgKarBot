using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Database.Models
{
    internal class Admin
    {
        public Admin(string userId)
        {
            UserId = userId;
        }

        public Admin(int id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
