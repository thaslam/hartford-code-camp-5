using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FitnessLog.WebApi.Models
{
    public class FitnessLog : DbContext
    {
        public DbSet<Log> Logs { get; set; }
    }
}