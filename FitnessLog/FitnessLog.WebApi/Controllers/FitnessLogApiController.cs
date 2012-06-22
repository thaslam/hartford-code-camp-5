using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using FitnessLog.WebApi.Models;
using FitnessLog.WebApi.ActionFilters;

namespace FitnessLog.WebApi.Controllers
{
    public class FitnessLogApiController : ApiController
    {
        public Models.IFitnessLogRepository Repository { get; set; }

        public FitnessLogApiController()
        {
            Repository = new Models.FitnessLogRepository();
        }

        // GET /api/fitnesslogapi
        [TracingFilter]
        public IEnumerable<Log> Get()
        {
            return Repository.GetFitnessLogs();
        }

        // GET /api/fitnesslogapi/5
        [TracingFilter]
        [Authorize(Users = "Haslam-PC\\Tom")]
        //[Authorize(Users = "ad1\\th72590")]
        public HttpResponseMessage<Log> Get(int id)
        {
            var log = Repository.GetFitnessLog(id);
            return new HttpResponseMessage<Models.Log>(
                log, 
                log != null ? 
                System.Net.HttpStatusCode.OK : 
                System.Net.HttpStatusCode.NotFound);
        }

        // POST /api/fitnesslogapi
        [ValidationFilter]
        public HttpResponseMessage Post(LogEntry entry)
        {
            Repository.AddLog(entry);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        // PUT /api/fitnesslogapi
        public HttpResponseMessage<LogEntry> Put(LogEntry entry)
        {
            Repository.UpdateLog(entry);
            return new HttpResponseMessage<LogEntry>(entry, System.Net.HttpStatusCode.OK);
        }

        // DELETE /api/fitnesslogapi/5
        public HttpResponseMessage Delete(LogEntry entry)
        {
            Repository.DeleteLog(entry);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
