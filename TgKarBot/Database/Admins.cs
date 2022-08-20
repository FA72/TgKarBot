namespace TgKarBot.Database
{
    internal partial class Database
    {
        public static async Task CreateAdminAsync(string id, string UserId)
        {
            await Create(Constants.Database.InsertIntoAdmins, id, UserId);
        }

        public static async Task<string?> ReadAdminAsync(string UserId)
        {
            return await ReadAsync(Constants.Database.GetFromAdmins, UserId, "UserId");
        }

        public static async Task DeleteAdminAsync(string id)
        {
            await DeleteAsync(Constants.Database.DeleteFromAdmins, id);
        }
    }
}