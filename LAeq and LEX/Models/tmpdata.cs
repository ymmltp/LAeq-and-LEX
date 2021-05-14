using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAeq_and_LEX.Models
{
    public class tmpdata
    {
    }

    public class serverData {
        public string id { get; set; }
        public string deviceid { get; set; }
        public string item { get; set; }
        public string dtime { get; set; }
        public string data { get; set; }
        public string rssi { get; set; }
        public string battery { get; set; }
        public string nt { get; set; }
    }
    public class laeqresult
    {
        public int interval { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public int during { get; set; }
        public double laeq { get; set; }
        public double lex { get; set; }
    }
}