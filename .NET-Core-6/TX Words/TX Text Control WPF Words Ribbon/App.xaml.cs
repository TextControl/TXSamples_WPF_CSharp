using System.Windows;

namespace TXTextControl.Words {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		private void Application_Startup(object sender, StartupEventArgs e) {
			Application.Current.DispatcherUnhandledException += ExceptionHandler.Application_DispatcherUnhandledException;
		}
	}
}