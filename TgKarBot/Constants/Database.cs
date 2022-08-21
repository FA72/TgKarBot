namespace TgKarBot.Constants
{
    internal class Database
    {
        public const string ConnectionString =
            @"Data Source=DESKTOP-33VFDHM;Initial Catalog=TgKarDatabase;Integrated Security=True;Pooling=False";

        public static string InsertIntoTeams = InsertInto(Teams, IdTeams, ValueTeams);
        public static string DeleteFromTeams = DeleteFrom(Teams, IdTeams);
        public static string UpdateTeams = Upgrade(Teams, ValueTeams);
        public static string GetFromTeams = GetFrom(Teams, IdTeams, ValueTeams);
        public static string GetFromTeamsByUserId = GetFrom(Teams, ValueTeams, IdTeams);

        public static string InsertIntoAsks = InsertInto(Asks, IdAsks, ValueAsks);
        public static string DeleteFromAsks = DeleteFrom(Asks, IdAsks);
        public static string UpdateAsks = Upgrade(Asks, ValueAsks);
        public static string GetFromAsks = GetFrom(Asks, IdAsks, ValueAsks);

        public static string InsertIntoRewards = InsertInto(Rewards, IdRewards, ValueRewards);
        public static string DeleteFromRewards = DeleteFrom(Rewards, IdRewards);
        public static string UpdateRewards = Upgrade(Rewards, ValueRewards);
        public static string GetFromRewards = GetFrom(Rewards, IdRewards, ValueRewards);

        public static string InsertIntoTeamProgress = InsertInto(TeamProgress, IdTeamProgress, ValueTeamProgress);
        public static string DeleteFromTeamProgress = DeleteFrom(TeamProgress, IdTeamProgress);
        public static string UpdateTeamProgress = Upgrade(TeamProgress, ValueTeamProgress);
        public static string GetFromTeamProgress = GetFrom(TeamProgress, IdTeamProgress, ValueTeamProgress);

        private const string Teams = "Teams";
        private const string IdTeams = "TeamId";
        private const string ValueTeams = "UserId";

        private const string Asks = "Asks";
        private const string IdAsks = "Id";
        private const string ValueAsks = "CorrectAsk";

        private const string Rewards = "Rewards";
        private const string IdRewards = "AskId";
        private const string ValueRewards = "Reward";

        private const string TeamProgress = "TeamProgress";
        private const string IdTeamProgress = "Id";
        private const string ValueTeamProgress = "CorrectAsk";


        private static string InsertInto(string table, string id, string value)
        {
            return $"INSERT INTO [dbo].[{table}] ({id}, {value}) VALUES ";
        }

        private static string DeleteFrom(string table, string id)
        {
            return $"DELETE FROM [dbo].[{table}] WHERE {id} = ";
        }

        private static string Upgrade(string table, string value)
        {
            return $"UPDATE [dbo].[{table}] SET {value} = ";
        }

        private static string GetFrom(string table, string id, string value)
        {
            return $"SELECT {value} FROM [dbo].[{table}] WHERE {id} = ";
        }
    }
}
