using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable.Dev.Dependencies
{
    public abstract class Dependency
    {

        public abstract bool VerifyInstallationStatus();
        public abstract IEnumerable<string> GetInstalledVersions();
        public abstract IEnumerable<string> GetDependencyDirectories();

    }
}
