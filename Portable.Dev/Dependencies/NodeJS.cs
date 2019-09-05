using Portable.Dev.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable.Dev.Dependencies
{
    public sealed class NodeJS : Dependency
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
                    version = ConsoleCommand.GetOutputFromRuningFile(Path.Combine(dir, "node.exe"), "-v");   
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
                var nodeJsFile = Path.Combine(item, "node.exe");
                var npmCmdFile = Path.Combine(item, "npm.cmd");
                //Looking for node.exe and npm.cmd to ensure it is properly installed
                if (File.Exists(nodeJsFile) && File.Exists(npmCmdFile))
                {
                    yield return item;
                }
            }
            yield break;
        }
    }
}
