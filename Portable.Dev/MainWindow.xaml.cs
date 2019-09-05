using Portable.Dev.Dependencies;
using Portable.Dev.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Portable.Dev
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBlock_Path.Text = string.Join("\n", Variables.Paths);
            DotNetCore DotNetDependency = new DotNetCore();
            if (DotNetDependency.VerifyInstallationStatus())
            {
                this.TextBlock_DotNetCore.Text = string.Join("; ", DotNetDependency.GetInstalledVersions());
            }
            else
            {
                this.TextBlock_DotNetCore.Text = "Not Installed";
            }

            NodeJS NodeJSDependency = new NodeJS();
            if (NodeJSDependency.VerifyInstallationStatus())
            {
                this.TextBlock_NodeJS.Text = string.Join("; ", NodeJSDependency.GetInstalledVersions());
            }
            else
            {
                this.TextBlock_DotNetCore.Text = "Not Installed";
            }

            Python PythonDependency = new Python();
            if (PythonDependency.VerifyInstallationStatus())
            {
                this.TextBlock_Python.Text = string.Join("; ", PythonDependency.GetInstalledVersions());
            }
            else
            {
                this.TextBlock_Python.Text = "Not Installed";
            }
            Flutter FlutterDependency = new Flutter();
            if (FlutterDependency.VerifyInstallationStatus())
            {
                this.TextBlock_Flutter.Text = string.Join("; ", FlutterDependency.GetInstalledVersions());
            }
            else
            {
                this.TextBlock_Flutter.Text = "Not Installed";
            }

            Dart DartDependency = new Dart();
            if (DartDependency.VerifyInstallationStatus())
            {
                this.TextBlock_Dart.Text = string.Join("; ", DartDependency.GetInstalledVersions());
            }
            else
            {
                this.TextBlock_Dart.Text = "Not Installed";
            }

            Git GitDependency = new Git();
            if (GitDependency.VerifyInstallationStatus())
            {
                this.TextBlock_Git.Text = string.Join("; ", GitDependency.GetInstalledVersions());
            }
            else
            {
                this.TextBlock_Git.Text = "Not Installed";
            }
        }
    }
}
