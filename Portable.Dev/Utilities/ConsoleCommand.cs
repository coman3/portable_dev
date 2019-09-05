using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable.Dev.Utilities
{
    public static class ConsoleCommand
    {

        public static string GetOutputFromRuningFile(string file, string args = "")
        {
            Process process = new Process();
            process.StartInfo.FileName = file;
            process.StartInfo.Arguments = args;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.Start();
            //* Read the output (or the error)
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            if(err.Length > 0)
            {
                throw new Exception(err);
            }
            process.WaitForExit();
            return output.Trim();
        }
    }
}
