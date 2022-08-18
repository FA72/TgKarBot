using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot
{
    public class StaticLogger
    {
        public static ILogger Logger { get; set; } = LogManager.GetCurrentClassLogger();
    }
}
