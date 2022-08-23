namespace TgKarBot.Database.Models
{
    internal class AskModel
    {
        public AskModel(string id, string? correctAsk)
        {
            Id = id;
            CorrectAsk = correctAsk;
        }

        public string Id { get; set; }
        public string? CorrectAsk { get; set; }
    }
}
