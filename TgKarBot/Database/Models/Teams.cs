namespace TgKarBot.Database.Models
{
    internal class TeamModel
    {
        public TeamModel(string teamId, string userId)
        {
            TeamId = teamId;
            UserId = userId;
        }
        public string TeamId { get; set; }
        public string UserId { get; set; }

    }
}
