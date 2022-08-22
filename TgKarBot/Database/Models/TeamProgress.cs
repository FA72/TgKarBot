using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Database.Models
{
    internal class TeamProgress
    {
        public TeamProgress(string teamId, string askId)
        {
            TeamId = teamId;
            AskId = askId;
        }
        public string TeamId { get; set; }
        public string AskId { get; set; }

    }
}
