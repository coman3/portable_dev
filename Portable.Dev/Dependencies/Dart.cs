using Portable.Dev.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable.Dev.Dependencies
{
    public sealed class Dart : Dependency
    {

        public override bool VerifyInstallationStatus()
        {
            try
            {
                return GetInstalledVersions()?.ToArray().Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public override IEnumerable<string> GetInstalledVersions()
        {
            var directories = this.GetDependencyDirectories();
            foreach (var dir in directories)
            {
                var version = "";
                try
                {
                    version = ConsoleCommand.GetOutputFromRuningFile(Path.Combine(dir, "dart.exe"), "--version");
                }
                catch (Exception ex)
                {
                    // Cmon Google, you are better than python surely..... Fuckin hell.
                    if (!ex.Message.Contains("Dart"))
                        continue;
                    version = ex.Message.Split(':')[1].Trim().Split(' ')[0];
                }
                yield return version;
            }
            yield break;           
        }

        public override IEnumerable<string> GetDependencyDirectories()
        {
            foreach (var item in Variables.Paths.Reverse())
            {
                var dartExeFile = Path.Combine(item, "dart.exe");
                var pubCmdFile = Path.Combine(item, "pub.bat");
                //Looking for node.exe and npm.cmd to ensure it is properly installed
                if (File.Exists(dartExeFile) && File.Exists(pubCmdFile))
                {
                    yield return item;
                }
            }
            yield break;
        }
    }
}
