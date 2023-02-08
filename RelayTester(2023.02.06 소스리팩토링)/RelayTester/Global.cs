using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayTester
{
    static class Global
    {
        private static string inout;


        public static string globalInOut
        {
            get { return inout; }

            set { inout = value; }
        }
    }
}
