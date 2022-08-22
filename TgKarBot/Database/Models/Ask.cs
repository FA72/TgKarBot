using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Database.Models
{
    internal class Ask
    {
        public Ask(string id, string? correctAsk)
        {
            Id = id;
            CorrectAsk = correctAsk;
        }

        public string Id { get; set; }
        public string? CorrectAsk { get; set; }
    }
}
