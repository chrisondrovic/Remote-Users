using DevExpress.Xpf.Core;
using System;
using System.Windows;

namespace Remote_Users {

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
            /// Getting Cross thread errors unless this is added
            DevExpress.Xpf.Core.DXGridDataController.DisableThreadingProblemsDetection = true;
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            DXMessageBox.Show(String.Format("An unhandled exception occurred: {0}", e.Exception.Message));
            e.Handled = true;
        }
    }
}