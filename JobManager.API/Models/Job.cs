using System;
using System.Collections.Generic;
using System.Text;

namespace JobManager.API.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
