using Portable.Dev.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable.Dev.Dependencies
{
    public sealed class Flutter : Dependency
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
                    version = ConsoleCommand.GetOutputFromRuningFile(Path.Combine(dir, "flutter.bat"), "--version");
                    version = version.Split('•')[0].Replace("Flutter ", "").Trim();
                }
                catch (Exception)
                {
                    continue;
                }
                yield return version;
            }
            yield break;           
        }

        public override IEnumerable<string> GetDependencyDirectories()
        {
            foreach (var item in Variables.Paths.Reverse())
            {
                var flutterCmdFile = Path.Combine(item, "flutter.bat");
                var flutterFile = Path.Combine(item, "flutter");
                //Looking for node.exe and npm.cmd to ensure it is properly installed
                if (File.Exists(flutterCmdFile) && File.Exists(flutterFile))
                {
                    yield return item;
                }
            }
            yield break;
        }
    }
}
