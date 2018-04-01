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

        public static void Savesettings(ApplicationDbContext context, string frequency, int no_freq, bool avgOinst)
        {
            while(context.Settings.Single() !=null)
            {
                Settings setting1 = context.Settings.Single();
                context.Settings.Remove(setting1);
            }
            Settings setting = new Settings(frequency, no_freq, avgOinst);
            context.Settings.Add(setting);

            context.SaveChanges();
            //context.Settings.Remove();
        }
    }
}
