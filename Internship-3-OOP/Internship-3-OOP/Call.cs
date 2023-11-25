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
        public DateTime CallStart { get; set; }
        public enum CallStatus
        {
            u_tijeku,
            propusten,
            zavrsen
        }
    }
}
