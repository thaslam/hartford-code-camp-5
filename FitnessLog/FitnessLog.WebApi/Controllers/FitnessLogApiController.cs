using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<Models.Log> Get()
        {
            return Repository.GetFitnessLogs();
        }

        // GET /api/fitnesslogapi/5
        [TracingFilter]
        public Models.Log Get(int id)
        {
            return Repository.GetFitnessLog(id);
            throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
        }

        // POST /api/fitnesslogapi
        public HttpResponseMessage Post(Models.LogEntry entry)
        {
            Repository.AddLog(entry);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        // PUT /api/fitnesslogapi
        public HttpResponseMessage<Models.LogEntry> Put(Models.LogEntry entry)
        {
            Repository.UpdateLog(entry);
            return new HttpResponseMessage<Models.LogEntry>(entry, System.Net.HttpStatusCode.OK);
        }

        // DELETE /api/fitnesslogapi/5
        public HttpResponseMessage Delete(Models.LogEntry entry)
        {
            Repository.DeleteLog(entry);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
