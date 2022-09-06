using System.ComponentModel.DataAnnotations;

namespace TgKarBot.Database.Models
{
    internal class AdminModel
    {
        public AdminModel(string userId)
        {
            UserId = userId;
        }

        public AdminModel(int id, string userId)
        {
            Id = id;
            UserId = userId;
        }
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
