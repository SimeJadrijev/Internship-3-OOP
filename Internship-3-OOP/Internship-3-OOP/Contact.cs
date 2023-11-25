using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    public class Contact
    {
        public Contact()
        {

        }
        public string NameAndSurname { get; set; }
        public int phoneNumber { get; set; }
        
        public enum Preference
        {
            favorit,
            normalan,
            blokiran
        }
    }
}
