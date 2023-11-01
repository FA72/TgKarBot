using System.ComponentModel.DataAnnotations;

namespace TgKarBot.Database.Models
{
    internal class TaskModel
    {
        public TaskModel(string id, string? text)
        {
            Id = id;
            Text = text;
        }
        [Key]
        public string Id { get; set; }
        public string? Text { get; set; }
    }
}
