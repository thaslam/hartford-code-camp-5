using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace FitnessLog.WebApi.MessageHandlers
{
    public class ApiKeyHandler : DelegatingHandler
    {
        public string Key { get; set; }

        public ApiKeyHandler(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Interecept the sending of request to the next handler.  Here was
        /// invoke the checking for a valid key.  If exists then we pass message
        /// down the pipeline, otherwise we stop execution.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!ValidateKey(request))
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return new HttpResponseMessage(HttpStatusCode.Forbidden);
                });
            }
            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Check that key in the query string matches the authorized api key
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateKey(HttpRequestMessage message)
        {
            var query = message.RequestUri.ParseQueryString();
            string key = query["key"];
            return (key == Key);
        }
    }
}