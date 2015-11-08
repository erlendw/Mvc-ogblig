using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nettbutikkpls.Models;

namespace nettbutikkpls.Models
{
    public class Log
    {
        public int LogID { get; set; }
        public string EventType { get; set; }
        public string ChangedBy { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string ChangedTime { get; set; }

        public string toString()
        {
            string print = "Changed: " + ChangedTime;
            print += ", Changed by: " + ChangedBy;
            print += ", Original value: : " + OriginalValue;
            print += ", New value: " + NewValue;
            print += ", Event type: " + EventType;
            return print;
        }
    }
}
