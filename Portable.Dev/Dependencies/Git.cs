using Portable.Dev.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable.Dev.Dependencies
{
    public sealed class Git : Dependency
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
                    version = ConsoleCommand.GetOutputFromRuningFile(Path.Combine(dir, "git.exe"), "--version");
                    version = version.Trim().Replace("git version ", "");
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
                var gitExeFile = Path.Combine(item, "git.exe");
                var gitkExeFile = Path.Combine(item, "gitk.exe");
                //Looking for node.exe and npm.cmd to ensure it is properly installed
                if (File.Exists(gitExeFile) && File.Exists(gitkExeFile))
                {
                    yield return item;
                }
            }
            yield break;
        }
    }
}
