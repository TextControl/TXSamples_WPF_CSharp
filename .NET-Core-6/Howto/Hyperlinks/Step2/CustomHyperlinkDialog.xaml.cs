﻿/*-------------------------------------------------------------------------------------------------------------
** CustomHyperlinkDialog.xaml.cs File
**
** Description:
**      A custom dialog to create or edit a hyperlink.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;
using System.Windows.Controls;

namespace Step2 {
	/// <summary>
	/// Interaction logic for CustomHyperlinkDialog.xaml
	/// </summary>
	public partial class CustomHyperlinkDialog : Window {
		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/
		private TXTextControl.HypertextLink m_hlLink;
		private TXTextControl.WPF.TextControl m_txTextControl;


		/*-------------------------------------------------------------------------------------------------------------
		** C O N S T R U C T O R
		**-----------------------------------------------------------------------------------------------------------*/
		public CustomHyperlinkDialog(TXTextControl.HypertextLink link, TXTextControl.WPF.TextControl textControl) {
			m_hlLink = link;
			m_txTextControl = textControl;
			InitializeComponent();

		}

		/*-------------------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** Window_Loaded Handler
        ** Updates the dialog layout according to the handled hyperlink.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Window_Loaded(object sender, RoutedEventArgs e) {
			// Set the caption of the dialog.
			this.Title = m_hlLink == null ? "Insert Hyperlink" : "Edit Hyperlink";

			// Update the text boxes.
			if (m_hlLink != null) {
				// The dialog is opened to edit a hyperlink:
				// Insert the text of the hyperlink text and target into the 
				// corresponding text boxes.
				m_tbxHyperlinkText.Text = m_hlLink.Text;
				m_tbxLink.Text = m_hlLink.Target;
			}
			else {
				// The dialog is opened to create a hyperlink:
				// Insert the text of the current selection into the 'Hyperlink Text' text box.
				TXTextControl.Selection selCurrentSelection = m_txTextControl.Selection;
				if (selCurrentSelection.Length > 0) {
					m_tbxHyperlinkText.Text = selCurrentSelection.Text;
				}
			}

			// Update the enabled state of the 'OK' button.
			m_btnOK.IsEnabled = IsValidHyperlink();
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** TextBox_TextChanged Handler
        ** Update the enabled state of the 'OK' button when the text of the text boxes changes.
        **-----------------------------------------------------------------------------------------------------------*/
		private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
			m_btnOK.IsEnabled = IsValidHyperlink();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** OK_Click Handler
		** Create or edit a hyperlink when clicking the 'OK' button.
		**-----------------------------------------------------------------------------------------------------------*/
		private void OK_Click(object sender, RoutedEventArgs e) {
			if (m_hlLink != null) {
				// The dialog is opened to edit a hyperlink:
				// Update the text of the hyperlink.
				m_hlLink.Text = m_tbxHyperlinkText.Text;
				m_hlLink.Target = m_tbxLink.Text;
			}
			else {
				// The dialog is opened to create a hyperlink:
				// Create a new hyperlink and insert it into the TextControl.
				m_hlLink = new TXTextControl.HypertextLink(m_tbxHyperlinkText.Text, m_tbxLink.Text);
				m_hlLink.DoubledInputPosition = true;
				m_txTextControl.HypertextLinks.Add(m_hlLink);
			}

			// Close the dialog,
			this.DialogResult = true;
		}


		/*-------------------------------------------------------------------------------------------------------------
		** M E T H O D S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** IsValidHyperlink Method
        ** Returns a value indicating whether both text boxes contain text to create or edit 
        ** a hyperlink.
        **
        ** Returns:		True, if both the 'Hyperlink Text' and 'Link' text box contain text. 
        **				Otherwise false.
        **-----------------------------------------------------------------------------------------------------------*/
		private bool IsValidHyperlink() {
			return m_tbxHyperlinkText.Text.Trim().Length > 0 && m_tbxLink.Text.Trim().Length > 0; ;
		}
	}
}
