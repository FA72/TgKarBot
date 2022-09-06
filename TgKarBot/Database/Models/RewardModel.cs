using System.ComponentModel.DataAnnotations;

namespace TgKarBot.Database.Models
{
    internal class RewardModel
    {
        public RewardModel(string askId, string reward, string main)
        {
            AskId = askId;
            Reward = reward;
            Main = main;
        }
        [Key]
        public string AskId { get; set; }
        public string Reward { get; set; }
        public string Main { get; set; }
    }
}
