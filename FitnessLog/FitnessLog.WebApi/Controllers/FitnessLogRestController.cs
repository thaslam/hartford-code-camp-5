using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using FitnessLog.WebApi.ActionFilters;

namespace FitnessLog.WebApi.Controllers
{
    public class FitnessLogRestController : ApiController
    {
        // GET /api/fitnesslogrest
        [TracingFilter]
        public IEnumerable<Models.Log> Get()
        {
            using (var fitnessLog = new Models.FitnessLog())
            {
                var logs = (from l in fitnessLog.Logs select l).ToList();
                return logs;
            }
        }

        // GET /api/fitnesslogrest/5
        [TracingFilter]
        public Models.Log Get(int id)
        {
            Models.Log log = null;
            using(var fitnessLog = new Models.FitnessLog())
            {
                log = fitnessLog.Logs.Include("Entries").FirstOrDefault(l => l.LogID == id);
            }

            // Web API JSON Serializer does not support the EF Code First dynamic proxies
            // so we have to use projection to create a POCO version of the return types
            if (log != null)
                return new Models.Log
                {
                    LogID = log.LogID,
                    Title = log.Title,
                    Username = log.Username,
                    Entries = new List<Models.LogEntry>
                       (log.Entries.Select(e => new Models.LogEntry() { DateAndTime = e.DateAndTime, ExerciseName = e.ExerciseName, Lbs = e.Lbs, LogEntryID = e.LogEntryID, Reps = e.Reps, Set = e.Set })),

                };

            throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
        }

        // POST /api/fitnesslogrest
        [ValidationFilter]
        public HttpResponseMessage Post(Models.LogEntry entry)
        {
            using (var fitnessLog = new Models.FitnessLog())
            {
                var log = fitnessLog.Logs.Find(entry.Log.LogID);
                if (log == null)
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

                entry.Log = log;
                log.Entries.Add(entry);

                fitnessLog.SaveChanges();
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        // PUT /api/fitnesslogrest
        public HttpResponseMessage<Models.LogEntry> Put(Models.LogEntry entry)
        {
            using (var fitnessLog = new Models.FitnessLog())
            {
                var log =
                    fitnessLog.Logs.Include("Entries")
                   .FirstOrDefault(l => l.Entries.Any(e => e.LogEntryID == entry.LogEntryID));
                if (log == null)
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

                var logEntry =
                    log.Entries.FirstOrDefault(e => e.LogEntryID == entry.LogEntryID);
                if (logEntry == null)
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

                logEntry.ExerciseName = entry.ExerciseName;
                logEntry.DateAndTime = entry.DateAndTime;
                logEntry.Lbs = entry.Lbs;
                logEntry.Reps = entry.Reps;
                logEntry.Set = entry.Set;
                logEntry.Log = log;

                fitnessLog.SaveChanges();
            }

            return new HttpResponseMessage<Models.LogEntry>(entry, System.Net.HttpStatusCode.OK);
        }

        // DELETE /api/fitnesslogrest/5
        public HttpResponseMessage Delete(Models.LogEntry entry)
        {
            using (var fitnessLog = new Models.FitnessLog())
            {
                var log =
                     fitnessLog.Logs.Include("Entries")
                    .FirstOrDefault(l => l.Entries.Any(e => e.LogEntryID == entry.LogEntryID));
                if (log == null)
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

                var logEntry =
                    log.Entries.FirstOrDefault(e => e.LogEntryID == entry.LogEntryID);
                if (logEntry == null)
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

                log.Entries.Remove(logEntry);

                fitnessLog.SaveChanges();
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
