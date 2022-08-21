namespace TgKarBot.Database
{
    internal partial class Database
    {
        public static async Task CreateAdminAsync(string id, string UserId)
        {
            await Create(Constants.Database.InsertIntoAdmins, UserId);
        }

        public static async Task<string?> ReadAdminAsync(string UserId)
        {
            return await ReadAsync(Constants.Database.GetFromAdmins, UserId, "UserId");
        }

        public static async Task DeleteAdminAsync(string userId)
        {
            await DeleteAsync(Constants.Database.DeleteFromAdmins, userId);
        }
    }
}