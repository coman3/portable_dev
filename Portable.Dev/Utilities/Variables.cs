using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable.Dev.Utilities
{
    public static class Variables
    {
        public static string Path => Environment.GetEnvironmentVariable("path");
        public static string[] Paths => Path.Split(';').Distinct().ToArray();
    }
}
