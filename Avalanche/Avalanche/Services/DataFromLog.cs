using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalanche.Data;
using Avalanche.Models.Tables;

namespace Avalanche.Services
{
    public class DataFromLog
    {
        public static List<Log> GetPosts(ApplicationDbContext context)
        {
            List<Log> logs = context.Logs.ToList();
            return logs;
        }
    }
}
