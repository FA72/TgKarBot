namespace TgKarBot.Database.Models
{
    internal class TeamProgressModel
    {
        public TeamProgressModel(string teamId, string askId)
        {
            TeamId = teamId;
            AskId = askId;
            Time = DateTime.Now;
            StartDrinkTime = null;
            EndDrinkTime = null;
        }
        public string TeamId { get; set; }
        public string AskId { get; set; }
        public DateTime Time { get; set; }
        public DateTime? StartDrinkTime { get; set; }
        public DateTime? EndDrinkTime { get; set; }
    }
}
