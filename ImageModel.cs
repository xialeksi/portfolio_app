using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp
{
    public class ImageModel
    {
        public int idimage { get; set; }
        public int idproject { get; set; }
        public string filename { get; set; }
        public string desc { get; set; }

        public ImageModel(){}
        public ImageModel(int idi, int idp, string fn, string d)
        {
            idimage = idi;
            idproject = idp;
            filename = fn;
            desc = d;
        }
    }
}
