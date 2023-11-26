using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    public class Call
    {
        public Call()
        {
                
        }
        public Call(DateTime callStart, string callStatus)
        {
            CallStart = callStart;
            CallStatus = callStatus;
        }
        public DateTime CallStart { get; set; }
        public string CallStatus { get; set; }
        /*
        public enum CallStatus
        {
            u_tijeku,
            propusten,
            zavrsen
        }
        */
    }
}
