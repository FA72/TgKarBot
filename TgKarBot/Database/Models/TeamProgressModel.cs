namespace TgKarBot.Database.Models
{
    internal class TeamProgressModel
    {
        public TeamProgressModel(string teamId, string askId)
        {
            TeamId = teamId;
            AskId = askId;
        }
        public string TeamId { get; set; }
        public string AskId { get; set; }
        public string Time { get; }
    }
}
