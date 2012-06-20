using System;
using System.Collections.Generic;

namespace FitnessLog.WebApi.Models
{
    public interface IFitnessLogRepository
    {
        IEnumerable<Log> GetFitnessLogs();

        Log GetFitnessLog(int id);

        Log AddLog(LogEntry entry);

        LogEntry UpdateLog(LogEntry entry);

        void DeleteLog(LogEntry entry);
    }
}