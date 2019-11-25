using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToyDB
{
    [Serializable]
    class SysTables
    { // Creating a HashSet of strings 
        public HashSet<String> sys_table = new HashSet<String>(StringComparer.OrdinalIgnoreCase);
    }   
    
}
