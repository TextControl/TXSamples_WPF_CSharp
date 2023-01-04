/*-------------------------------------------------------------------------------------------------------------
** MainWindow_AppMenu_Print.cs File
**
** Description:
**     Manages printing a document
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.ComponentModel;
using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace TXTextControl.Words {
	partial class MainWindow {

		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** Print_Click Handler
        ** Invokes the TextControl Print method to open the TextControl print dialog.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Print_Click(object sender, RoutedEventArgs e) {
			// Use the active document name to open the print dialog.
			m_txTextControl.Print(m_strActiveDocumentName, true);
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** PrintQuick_Click Handler
        ** Prints the current document without opening the dialog before.
        **-----------------------------------------------------------------------------------------------------------*/
		private void PrintQuick_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.Print(m_strActiveDocumentName, new PageRange(1, m_txTextControl.Pages), 1, Collation.Collated);
		}

		/*--------------------------------------------------------------------------------------------------------------
        ** TextControl_PropertyChanged_Print Handler
        ** Update the print button's enabled states.
        **------------------------------------------------------------------------------------------------------------*/
		private void TextControl_PropertyChanged_Print(object sender, PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "CanPrint":
					m_rbtnPrintQAT.IsEnabled =
					m_rsmiPrint.IsEnabled =
					m_rbtnPrint.IsEnabled =
					m_rbtnPrintQuick.IsEnabled = m_txTextControl.CanPrint;
					break;
			}
		}
	}
}
