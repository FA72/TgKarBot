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
        public DateTime? StartTime { get; set; }
        public int BonusTime { get; set; } 
        public int Penalty { get; set; }
        public bool IsEnd { get; set; }
    }
}
