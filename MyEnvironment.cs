using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp
{
    internal class MyEnvironment
    {
        internal static string GetBaseUrl()
        {
            return "https://localhost:7036/api/";
            //return "https://pekan-dotnet-university.herokuapp.com/api";
        }
    }
}
