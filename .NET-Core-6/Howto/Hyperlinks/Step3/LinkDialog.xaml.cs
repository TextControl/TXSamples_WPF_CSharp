/*-------------------------------------------------------------------------------------------------------------
** LinkDialog.xaml.cs File
**
** Description:
**      A custom dialog to create or edit a link.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;

namespace Step3 {
	/// <summary>
	/// Interaction logic for LinkDialog.xaml
	/// </summary>
	public partial class LinkDialog : Window {
		/*-------------------------------------------------------------------------------------------------------------
		** E N U M S
		**-----------------------------------------------------------------------------------------------------------*/
		internal enum DialogMode {
			InsertLink,
			EditHyperlink,
			EditDocumentLink
		}


		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/
		private DialogMode m_dmDialogMode;
		private TXTextControl.TextField m_tfLink;
		private TXTextControl.WPF.TextControl m_txTextControl;


		/*-------------------------------------------------------------------------------------------------------------
		** C O N S T R U C T O R
		**-----------------------------------------------------------------------------------------------------------*/
		public LinkDialog(TXTextControl.TextField link, TXTextControl.WPF.TextControl textControl) {
			m_tfLink = link;
			m_txTextControl = textControl;
			InitializeComponent();

			// Determine the dialog mode.
			m_dmDialogMode = m_tfLink == null ?
				DialogMode.InsertLink : m_tfLink is TXTextControl.HypertextLink ?
				DialogMode.EditHyperlink : DialogMode.EditDocumentLink;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** Window_Loaded Handler
        ** Updates the dialog layout according to the handled link.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Window_Loaded(object sender, RoutedEventArgs e) {
			// Update the dialog layout considering the set dialog mode.
			switch (m_dmDialogMode) {
				case DialogMode.InsertLink:
					m_cmbxDocumentTargets.Visibility = Visibility.Collapsed;
					this.Title = "Insert Link";
					break;
				case DialogMode.EditHyperlink:
					this.Title = "Edit Hyperlink";
					m_grdLinkType.Visibility = Visibility.Collapsed;
					m_cmbxDocumentTargets.Visibility = Visibility.Collapsed;
					break;
				case DialogMode.EditDocumentLink:
					this.Title = "Edit Document Link";
					m_grdLinkType.Visibility = Visibility.Collapsed;
					m_tbxHyperlink.Visibility = Visibility.Collapsed;
					break;
			}

			// Fill the document targets combo box with references to the document targets
			// of the document.
			if (m_dmDialogMode != DialogMode.EditHyperlink) {
				TXTextControl.DocumentTargetCollection colDocumentTargets = m_txTextControl.DocumentTargets;
				foreach (TXTextControl.DocumentTarget target in colDocumentTargets) {
					m_cmbxDocumentTargets.Items.Add(new DocumentTargetItem(target));
				}
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Type_CheckedChanged Handler
		** Update the visibility of the corresponding control when the type radio button checked state changed.
		**-----------------------------------------------------------------------------------------------------------*/
		private void Type_CheckedChanged(object sender, RoutedEventArgs e) {
			if (this.IsLoaded) {
				if (!m_rbtnTypeHyperlink.IsChecked.Value) {
					// The document targets combo box is displayed when the 'Document Link' radio button is toggled.
					m_tbxHyperlink.Visibility = Visibility.Collapsed;
					m_cmbxDocumentTargets.Visibility = Visibility.Visible;
				}
				else {
					// The text box is displayed to enter a hyperlink when the 'Hyperlink' radio button is toggled.
					m_cmbxDocumentTargets.Visibility = Visibility.Collapsed;
					m_tbxHyperlink.Visibility = Visibility.Visible;
				}

				// Update the enabled state of the OK button.
				m_btnOK.IsEnabled = IsValidLink();
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** LinkParameter_Changed Handler
		** Update the enabled state of the 'OK' button when the text of any text boxes changed.
		**-----------------------------------------------------------------------------------------------------------*/
		private void LinkParameter_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e) {
			m_btnOK.IsEnabled = IsValidLink();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** LinkParameter_Changed Handler
		** Update the enabled state of the 'OK' button when the selected item of the document targets combo box changed.
		**-----------------------------------------------------------------------------------------------------------*/
		private void DocumentTargets_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
			m_btnOK.IsEnabled = IsValidLink();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** OK_Click Handler
		** Create or edit a link when clicking the 'OK' button.
		**-----------------------------------------------------------------------------------------------------------*/
		private void OK_Click(object sender, RoutedEventArgs e) {
			// Consider the current mode.
			switch (m_dmDialogMode) {
				case DialogMode.InsertLink: {
						// The dialog is opened to create a link:
						if (m_rbtnTypeHyperlink.IsChecked.Value) {
							// Create a new hyperlink and insert it into the TextControl.
							TXTextControl.HypertextLink hlHypertextLink = new TXTextControl.HypertextLink(m_tbxDisplayedText.Text, m_tbxHyperlink.Text);
							hlHypertextLink.DoubledInputPosition = true;
							m_txTextControl.HypertextLinks.Add(hlHypertextLink);
						}
						else {
							// Create a new document link and insert it into the TextControl.
							TXTextControl.DocumentLink dlDocumentLink = new TXTextControl.DocumentLink(m_tbxDisplayedText.Text, (m_cmbxDocumentTargets.SelectedItem as DocumentTargetItem).DocumentTarget);
							dlDocumentLink.DoubledInputPosition = true;
							m_txTextControl.DocumentLinks.Add(dlDocumentLink);
						}
					}
					break;
				case DialogMode.EditHyperlink: {
						// The dialog is opened to edit a hyperlink:
						// Update the text of the hyperlink.
						TXTextControl.HypertextLink hlHypertextLink = m_tfLink as TXTextControl.HypertextLink;
						hlHypertextLink.Text = m_tbxDisplayedText.Text;
						hlHypertextLink.Target = m_tbxHyperlink.Text;
					}
					break;
				case DialogMode.EditDocumentLink: {
						// The dialog is opened to edit a document link:
						// Update the text and the document target of the document link.
						TXTextControl.DocumentLink dlDocumentLink = m_tfLink as TXTextControl.DocumentLink;
						dlDocumentLink.Text = m_tbxDisplayedText.Text;
						dlDocumentLink.DocumentTarget = (m_cmbxDocumentTargets.SelectedItem as DocumentTargetItem).DocumentTarget;
					}
					break;
			}

			// Close the dialog.
			this.DialogResult = true;
		}


		/*-------------------------------------------------------------------------------------------------------------
		** M E T H O D S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** IsValidLink Method
        ** Returns a value indicating whether both the 'Displayed Text' text box contains text and the link specific
        ** control (hyperlink text box or document link combo box) contains a valid value.
        **
        ** Returns:		True, if both the 'Displayed Text' text box contains text and the link specific control 
        **				(hyperlink text box or document link combo box) contains a valid value.
        **				Otherwise false.
        **-----------------------------------------------------------------------------------------------------------*/
		private bool IsValidLink() {
			return m_tbxDisplayedText.Text.Trim().Length > 0 && (m_cmbxDocumentTargets.Visibility == Visibility.Visible ? m_cmbxDocumentTargets.SelectedIndex != -1 : m_tbxHyperlink.Text.Trim().Length > 0); ;
		}
	}
}
