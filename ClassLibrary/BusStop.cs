using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class BusStop
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public IEnumerable<string> Lines { get; set; }
        public List<BusStop> names = new List<BusStop>();

    }
}
