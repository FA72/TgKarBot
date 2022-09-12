using System.ComponentModel.DataAnnotations;

namespace TgKarBot.Database.Models
{
    internal class TeamModel
    {
        public const string TeamIdparam = "@teamId";

        public TeamModel(string teamId, string userId)
        {
            TeamId = teamId;
            UserId = userId;
        }
        [Key]
        public string TeamId { get; set; }
        public string UserId { get; set; }
        public string StartTime { get; }
    }
}
