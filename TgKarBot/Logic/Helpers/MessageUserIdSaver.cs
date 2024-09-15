using Newtonsoft.Json;

namespace TgKarBot.Logic.Helpers;

public class MessageUserIdSaver
{
    private const string FilePath = "messageUserMapping.json";
    private static readonly Dictionary<long, long> Dictionary = LoadFromFile();

    public static void AddToFile(long messageId, long userId)
    {
        Dictionary[messageId] = userId;
        SaveToFile();
    }

    public static long? GetUserId(long messageId)
    {
        return Dictionary.TryGetValue(messageId, out var userId) ? userId : (long?)null;
    }

    private static Dictionary<long, long> LoadFromFile()
    {
        if (!File.Exists(FilePath)) return new Dictionary<long, long>();

        var json = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<Dictionary<long, long>>(json) ??
               new Dictionary<long, long>();
    }

    private static void SaveToFile()
    {
        var json = JsonConvert.SerializeObject(Dictionary, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }
}