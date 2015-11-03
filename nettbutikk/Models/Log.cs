using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Log
    {
        public int LogID { get; set; }
        public string EventType { get; set; }
        //public nettButikkpls.DAL.Customers ChangedBy{get;set;}
        public string ChangedBy { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangedTime { get; set; }
    }
}
