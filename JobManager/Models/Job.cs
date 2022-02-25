using System;
using System.Collections.Generic;
using System.Text;

namespace JobManager.Models
{
    public class Job
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
