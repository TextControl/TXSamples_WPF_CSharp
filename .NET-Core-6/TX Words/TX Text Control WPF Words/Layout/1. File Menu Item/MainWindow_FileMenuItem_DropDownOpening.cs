/*-------------------------------------------------------------------------------------------------------------
** MainWindow_FileMenuItem_DropDownOpening.cs File
**
** Description: Provides all SubmenuOpened handlers associated with 'File' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** File_SubmenuOpened Handler
		**
		** Updates the IsEnabled state of 'File' drop down menu items.
		** 
		** Item: 'File'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_SubmenuOpened(object sender, RoutedEventArgs e) {
			// 'Recent Files'
			m_miFile_RecentFiles.IsEnabled = m_colRecentFiles.Count > 0;

			// 'Save'
			m_miFile_Save.IsEnabled = m_bIsDirtyDocument && !m_bIsUnknownDocument;

			// 'Print'
			m_miFile_Print.IsEnabled = m_txTextControl.CanPrint;
		}
	}
}
