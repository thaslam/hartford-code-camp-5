using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace FitnessLog.WebApi.MediaTypes
{
    public class TheHartfordMediaType : MediaTypeMapping
    {
        // Types or subtypes that begin with x- are non-standard[2] (they are not registered with IANA). 
        public TheHartfordMediaType()
            : base("text/x-hig")
        {
        }

        protected override double OnTryMatchMediaType(HttpResponseMessage response)
        {
            // The quality of the match. 
            // It must be between 0.0 and 1.0. 
            // A value of 0.0 signifies no match. A value of 1.0 signifies a complete match. 

            if (response.RequestMessage.Headers.Accept.Any(m => m.MediaType == "text/x-hig"))
                return 1.0;

            return 0.0;
        }

        protected override double OnTryMatchMediaType(HttpRequestMessage request)
        {
            throw new NotImplementedException();
        }
    }
}