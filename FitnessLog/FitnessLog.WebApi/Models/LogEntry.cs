using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessLog.WebApi.Models
{
    public class LogEntry
    {
        public LogEntry()
        {
            Set = 1; // default set to 1
        }

        public int LogEntryID { get; set; }
        public string DateAndTime { get; set; }
        public string ExerciseName { get; set; }
        public int Lbs { get; set; }
        public int Reps { get; set; }
        public int Set { get; set; }

        public virtual Log Log { get; set; }
    }
}