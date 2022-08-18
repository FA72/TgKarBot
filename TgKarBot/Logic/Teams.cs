using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Logic
{
    internal class Teams
    {
        public static string Registrate(string commandId, long userId)
        {
            string directory = Directory.GetCurrentDirectory();
            string teamsPath = directory + "/teams.txt";

            if (File.Exists(teamsPath))
            {
                using var streamReader = new StreamReader(teamsPath);
                var line = streamReader.ReadLine();
                
                while (line != null)
                {
                    var splittedLine = line.Split();
                    if (splittedLine[0] == commandId)
                    {
                        if (splittedLine[1] == userId.ToString())
                            return Constants.Messages.AlreadyRegistered;

                        return Constants.Messages.OtherUser;
                    }
                    line = streamReader.ReadLine();
                }
            }

            File.WriteAllLinesAsync(teamsPath, new[] {$"{commandId} {userId}"});
            return Constants.Messages.DoneTeamRegisteration;
        }
    }
}
