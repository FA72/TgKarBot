using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Database.Models
{
    internal class Team
    {
        public Team(string teamId, string userId)
        {
            TeamId = teamId;
            UserId = userId;
        }
        public string TeamId { get; set; }
        public string UserId { get; set; }

    }
}
