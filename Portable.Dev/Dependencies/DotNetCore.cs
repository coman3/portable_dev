using Portable.Dev.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable.Dev.Dependencies
{
    public sealed class DotNetCore : Dependency
    {
        public override IEnumerable<string> GetInstalledVersions()
        {
            var paths = this.GetDependencyDirectories();
            foreach (var path in paths)
            {
                foreach (var item in Directory.GetDirectories(Path.Combine(path, "sdk")).Select(x => new DirectoryInfo(x).Name).Where(x => x.Contains(".")))
                {
                    yield return item;
                }
            }
            yield break;
        }

        public override IEnumerable<string> GetDependencyDirectories()
        {
            foreach (var item in Variables.Paths.Reverse())
            {
                var sdkDirectory = Path.Combine(item, "sdk");
                var dotnetFile = Path.Combine(item, "dotnet.exe");
                //Looking for an SDK folder, a dotnet.exe file and at least one sdk version
                if (Directory.Exists(sdkDirectory) && File.Exists(dotnetFile) && Directory.GetDirectories(sdkDirectory).Count() > 0)
                {
                    yield return item;
                }
            }
            yield break;
        }

        public override bool VerifyInstallationStatus()
        {
            var directories = GetDependencyDirectories();
            foreach (var dir in directories)
            {
                try
                {
                    var info = ConsoleCommand.GetOutputFromRuningFile(Path.Combine(dir, "dotnet.exe"), "--info");
                    return info.Length > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;

        }
    }
}
