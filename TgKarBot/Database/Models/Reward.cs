using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Database.Models
{
    internal class Reward
    {
        public Reward(string askId, string rewardText)
        {
            AskId = askId;
            RewardText = rewardText;
        }

        public string AskId { get; set; }
        public string RewardText { get; set; }

    }
}
