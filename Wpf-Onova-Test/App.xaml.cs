using Onova;
using Onova.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_Onova_Test {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: Application {
        protected override async void OnStartup(StartupEventArgs e) {
            await CheckUpdate();
        }

        private async Task CheckUpdate() {
            using IUpdateManager _updateManager = new UpdateManager(
                new LocalPackageResolver(@"c:\temp\wpf-onova-test", "Wpf-Onova-Test-v*.zip"),
                new ZipPackageExtractor());

            var check = await _updateManager.CheckForUpdatesAsync();

            if (check.CanUpdate) {
                await _updateManager.PrepareUpdateAsync(check!.LastVersion!);
                MessageBox.Show("app will be update and restart.");

                _updateManager.LaunchUpdater(check!.LastVersion!, true);
                this.Shutdown();
            }
        }
    }
}
