using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using FitnessLog.WebApi.Models;
namespace FitnessLog.WebApi.MediaTypes
{
    public class TheHartfordMediaFormatter : MediaTypeFormatter
    {
        public TheHartfordMediaFormatter()
        {
        }

        // Override the CanWriteType method to indicate which types the formatter can serialize.
        protected override bool CanWriteType(Type type)
        {
            return true;
        }

        // Override the CanReadType method to indicate which types the formatter can deserialize. 
        // This formatter does not support deserialization, so the method simply returns false. 
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

        // This method encapsulates the formatting to convert an object into the HIG media format
        private void WriteHigFormat(object value, System.IO.Stream stream)
        {
            var builder = new StringBuilder();
            
            builder.Append(string.Format("[hig:{0}]", value.GetType().Name));

            foreach (var p in value.GetType().GetProperties())
            {
                builder.Append(string.Format("[hig:{0}:{1}]", p.Name, Clean(p.GetValue(value, null).ToString())));
            }

            builder.Append(string.Format("[hig:end]"));
            
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(builder.ToString());
            }
        }

        #region Clean strings code
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
        #endregion
    }
}