using System.ComponentModel.DataAnnotations;

namespace TgKarBot.Database.Models
{
    internal class RewardModel
    {
        public RewardModel(string askId, string reward, bool isMain = true, int? timeBonus = null)
        {
            AskId = askId;
            Reward = reward;
            IsMain = isMain;
            TimeBonus = timeBonus;
        }
        [Key]
        public string AskId { get; set; }
        public string Reward { get; set; }
        public bool IsMain { get; set; }
        public int? TimeBonus { get; set; }
    }
}
