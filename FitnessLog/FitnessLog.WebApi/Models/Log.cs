using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessLog.WebApi.Models
{
    public class Log
    {
        public Log()
        {
            Entries = new List<LogEntry>();
        }

        public int LogID { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }

        public ICollection<LogEntry> Entries { get; set; }
    }
}