using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp
{
    internal class Singleton
    {
        public string Username { get; set; }
        public string Password { get; set; }
        private static Singleton instance;

        private Singleton() { }
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
        
    }
}
