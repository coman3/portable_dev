using Portable.Dev.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable.Dev.Dependencies
{
    public sealed class Python : Dependency
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
                    version = ConsoleCommand.GetOutputFromRuningFile(Path.Combine(dir, "python.exe"), "-V");
                    version = version.Trim().Replace("Python ", "");
                    
                }
                catch (Exception ex)
                {
                    // because python is fucking shit, the -V seems to throw a console error insted of being normal....
                    if (!ex.Message.Contains("Python"))
                        version = null;
                    version = ex.Message.Trim().Replace("Python ", "");
                }
                if (version == null) continue;
                yield return version;
            }
                     
        }

        public override IEnumerable<string> GetDependencyDirectories()
        {
            foreach (var item in Variables.Paths.Reverse())
            {
                var pythonFile = Path.Combine(item, "python.exe");
                var scriptsDirectory = Path.Combine(item, "Scripts");
                //Looking for python.exe and scripts folder ensure it is properly installed
                if (File.Exists(pythonFile) && Directory.Exists(scriptsDirectory))
                {
                    yield return item;
                }
            }
            yield break;
        }
    }
}
