using System.Text;
using System.Data.SqlClient;

namespace TgKarBot.Database
{
    internal partial class Database
    {
        private static SqlConnection _sqlConnection = new SqlConnection(Constants.Database.ConnectionString);

        public static async Task Connect()
        {
            await _sqlConnection.OpenAsync();
        }

        private static async Task Create(string insertCommand, string id, string value)
        {
            var request = new StringBuilder();
            request.Append(insertCommand);
            request.Append($"('{id}', '{value}')");

            var sqlQuery = request.ToString();
            await using var command = new SqlCommand(sqlQuery, _sqlConnection);
            await command.ExecuteNonQueryAsync();
        }

        private static async Task<string?> ReadAsync(string getCommand, string id, string valueName)
        {
            var request = new StringBuilder();
            request.Append(getCommand);
            request.Append($"'{id}'");

            var sqlQuery = request.ToString();
            await using var command = new SqlCommand(sqlQuery, _sqlConnection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                return reader[valueName].ToString();

            return null;
        }

        private static async Task<List<string?>> ReadAllAsync(string getCommand, string id, string valueName)
        {
            var request = new StringBuilder();
            request.Append(getCommand);
            request.Append($"'{id}'");

            var sqlQuery = request.ToString();
            await using var command = new SqlCommand(sqlQuery, _sqlConnection);
            await using var reader = await command.ExecuteReaderAsync();

            var list = new List<string?>();

            while (await reader.ReadAsync())
                list.Add(reader[valueName].ToString());

            return list;
        }

        private static async Task UpdateAsync(string updateCommand, string id, string value, string idName)
        {
            var request = new StringBuilder();
            request.Append(updateCommand);
            request.Append($"'{value}' WHERE {idName} = '{id}'");

            var sqlQuery = request.ToString();
            await using var command = new SqlCommand(sqlQuery, _sqlConnection);
            await command.ExecuteNonQueryAsync();
        }

        private static async Task DeleteAsync(string deleteCommand, string id)
        {
            var request = new StringBuilder();
            request.Append(deleteCommand);
            request.Append($"'{id}'");

            var sqlQuery = request.ToString();
            await using var command = new SqlCommand(sqlQuery, _sqlConnection);
            await command.ExecuteNonQueryAsync();
        }
    }
}
