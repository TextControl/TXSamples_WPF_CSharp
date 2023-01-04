/*-------------------------------------------------------------------------------------------------------------
** ExceptionHandler.cs File
**
** Description:
**     Implements the ExceptionHandler class that provides a static handler to handle unhandled execptions.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using System.Windows;

namespace TXTextControl.Words {
	public class ExceptionHandler {
		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------
        ** Application_DispatcherUnhandledException Handler
        ** Handles an exception by showing a message box that explains the reason for the exception.
        **-----------------------------------------------------------------------------------------------------*/
		public static void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
			string strProductName = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute))).Product;
			// TX Spell not available
			if (e.Exception.GetType() == typeof(TargetInvocationException) &&
				e.Exception.InnerException != null &&
				e.Exception.InnerException.Source == "TXSpell") {
				MessageBox.Show(string.Format(Properties.Resources.MessageBox_Application_ThreadException_Text, e.Exception.InnerException.Message), strProductName, MessageBoxButton.OK, MessageBoxImage.Warning);
				e.Handled = true;
				return;
			}

			// TX Text Control Feature is not available
			if (e.Exception is TXTextControl.LicenseLevelException) {
				MessageBox.Show(string.Format(Properties.Resources.MessageBox_Application_ThreadException_Text, e.Exception.Message), strProductName, MessageBoxButton.OK, MessageBoxImage.Information);
				e.Handled = true;
				return;
			}

			// All other exceptions
			MessageBox.Show(string.Format(Properties.Resources.MessageBox_Application_ThreadException_Text, e.Exception.Message), strProductName, MessageBoxButton.OK, MessageBoxImage.Error);
			e.Handled = true;
		}
	}
}
