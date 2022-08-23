namespace TgKarBot.Database.Models
{
    internal class RewardModel
    {
        public RewardModel(string askId, string reward)
        {
            AskId = askId;
            Reward = reward;
        }

        public string AskId { get; set; }
        public string Reward { get; set; }

    }
}
