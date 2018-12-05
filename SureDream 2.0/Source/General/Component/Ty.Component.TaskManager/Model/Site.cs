using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.TaskManager
{
    public class Site
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public List<Pole> Poles { get; set; } = new List<Pole>();
    }

    public class  Pole
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
