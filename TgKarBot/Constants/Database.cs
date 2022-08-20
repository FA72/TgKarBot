namespace TgKarBot.Constants
{
    internal class Database
    {
        public const string ConnectionString =
            @"Data Source=DESKTOP-33VFDHM;Initial Catalog=TgKarDatabase;Integrated Security=True;Pooling=False";

        public static string InsertIntoTeams = InsertInto(IdTeams, ValueTeams);
        public static string DeleteFromTeams = DeleteFrom(IdTeams);
        public static string UpdateTeams = Upgrade(ValueTeams);
        public static string GetFromTeams = GetFrom(IdTeams, ValueTeams);
        public static string GetFromTeamsByUserId = GetFrom(ValueTeams, IdTeams);

        public static string InsertIntoAsks = InsertInto(IdAsks, ValueAsks);
        public static string DeleteFromAsks = DeleteFrom(IdAsks);
        public static string UpdateAsks = Upgrade(ValueAsks);
        public static string GetFromAsks = GetFrom(IdAsks, ValueAsks);

        public static string InsertIntoRewards = InsertInto(IdRewards, ValueRewards);
        public static string DeleteFromRewards = DeleteFrom(IdRewards);
        public static string UpdateRewards = Upgrade(ValueRewards);
        public static string GetFromRewards = GetFrom(IdRewards, ValueRewards);

        public static string InsertIntoTeamProgress = InsertInto(IdTeamProgress, ValueTeamProgress);
        public static string DeleteFromTeamProgress = DeleteFrom(IdTeamProgress);
        public static string UpdateTeamProgress = Upgrade(ValueTeamProgress);
        public static string GetFromTeamProgress = GetFrom(IdTeamProgress, ValueTeamProgress);

        private const string IdTeams = "TeamId";
        private const string ValueTeams = "UserId";

        private const string IdAsks = "Id";
        private const string ValueAsks = "CorrectAsk";

        private const string IdRewards = "AskId";
        private const string ValueRewards = "Reward";

        private const string IdTeamProgress = "Id";
        private const string ValueTeamProgress = "CorrectAsk";


        private static string InsertInto(string id, string value)
        {
            return $"INSERT INTO [dbo].[Teams] ({id}, {value}) VALUES ";
        }

        private static string DeleteFrom(string id)
        {
            return $"DELETE FROM [dbo].[Teams] WHERE {id} = ";
        }

        private static string Upgrade(string value)
        {
            return $"UPDATE [dbo].[Teams] SET {value} = ";
        }

        private static string GetFrom(string id, string value)
        {
            return $"GET {value} FROM [dbo].[Teams] WHERE {id} = ";
        }
    }
}
