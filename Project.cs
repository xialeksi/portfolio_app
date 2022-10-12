using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp
{
    public class Project
    {
        public int idproject { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Project(int id, string n, string d)
        {
            idproject = id;
            name = n;
            description = d;
        }
    }
}