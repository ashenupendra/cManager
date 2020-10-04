using System;
using System.Collections.Generic;
using System.Text;

namespace cManager.Shared
{
    public class Auditable
    {
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string ModifiedBy { get; set; }
    }
}
