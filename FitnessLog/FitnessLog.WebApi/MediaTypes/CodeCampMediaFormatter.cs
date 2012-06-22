using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using FitnessLog.WebApi.Models;
using System.Web.Script.Serialization;

namespace FitnessLog.WebApi.MediaTypes
{
    public class CodeCampMediaFormatter : MediaTypeFormatter
    {
        public CodeCampMediaFormatter()
        {
        }

        /// <summary>
        /// Override the CanWriteType method to indicate which types the formatter can serialize.
        /// </summary>
        /// <param name="type">Type of object attempting to be returned</param>
        protected override bool CanWriteType(Type type)
        {
            return true;
        }

        /// <summary>
        /// Override the CanReadType method to indicate which types the formatter can deserialize. 
        /// This formatter does not support deserialization, so the method simply returns false. 
        /// </summary>
        /// <param name="type">Type of object attempting to be read</param>
        protected override bool CanReadType(Type type)
        {
            return false;
        }

        // This method serializes a type by writing it to a stream. If your formatter supports deserialization, also override the ReadFromStream method.
        protected override Task  OnWriteToStreamAsync(
            Type type, object value, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, FormatterContext formatterContext, System.Net.TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
                {
                    WriteHigFormat(value, stream);
                });
        }

        /// <summary>
        /// This method encapsulates the formatting to convert an object into the HIG media format
        /// </summary>
        /// <param name="value">Object to serialize</param>
        /// <param name="stream">Stream to write response to</param>
        private void WriteHigFormat(object value, System.IO.Stream stream)
        {
            // format message
            var json = Clean(ToJson(value).Replace("{", "<cc5> ").Replace("}", "</cc5> "));
            
            // write message out to response stream
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(json);
            }
        }

        #region String manipulation code
        static char[] _specialChars = new char[] { ',', '\n', '\r', '"' };

        private string Clean(object o) 
        { 
            if (o == null) 
            { 
                return ""; 
            } 
            string field = o.ToString(); 
            if (field.IndexOfAny(_specialChars) != -1) 
            { 
                return String.Format("\"{0}\"", field.Replace("\"", "\"\"")); 
            } 
            else return field;
        }

        public string ToJson(object value)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(value);
        }
        #endregion
    }
}