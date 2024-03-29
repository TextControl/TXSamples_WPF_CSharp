﻿/*-------------------------------------------------------------------------------------------------------------
** DocumentTargetDialog.xaml.cs File
**
** Description:
**      A custom dialog to create or edit a document targets.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;

namespace Step3 {
	/// <summary>
	/// Interaction logic for DocumentTargetDialog.xaml
	/// </summary>
	public partial class DocumentTargetDialog : Window {
		/*-------------------------------------------------------------------------------------------------------------
		** E N U M S
		**-----------------------------------------------------------------------------------------------------------*/
		internal enum DialogMode {
			Insert,
			Edit
		}


		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/
		private TXTextControl.DocumentTarget m_dtDocumentTarget;
		private TXTextControl.WPF.TextControl m_txTextControl;
		private DialogMode m_dmDialogMode;


		/*-------------------------------------------------------------------------------------------------------------
		** C O N S T R U C T O R
		**-----------------------------------------------------------------------------------------------------------*/
		public DocumentTargetDialog(TXTextControl.DocumentTarget documentTarget, TXTextControl.WPF.TextControl textControl) {
			m_dtDocumentTarget = documentTarget;
			m_txTextControl = textControl;
			InitializeComponent();

			// Determine the dialog mode.
			m_dmDialogMode = m_dtDocumentTarget == null ? DialogMode.Insert : DialogMode.Edit;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** Window_Loaded Handler
        ** Updates the dialog layout according to the handled document target.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Window_Loaded(object sender, RoutedEventArgs e) {
			// Fill the 'Document targets at current input position' list box with all document targets that 
			// are located at the current input position.
			TXTextControl.DocumentTarget[] rdtCurrentDocumentTargets = m_txTextControl.DocumentTargets.GetItems();
			if (rdtCurrentDocumentTargets != null) {
				int iCurrentIndex = -1;
				for (int i = 0; i < rdtCurrentDocumentTargets.Length; i++) {
					TXTextControl.DocumentTarget rdtCurrentDocumentTarget = rdtCurrentDocumentTargets[i];
					m_lbxCurrentDocumentTargets.Items.Add(new DocumentTargetItem(rdtCurrentDocumentTarget));
					// Determine the index of that item that represents the document target that
					// should be edited.
					if (m_dmDialogMode == DialogMode.Edit && rdtCurrentDocumentTarget.GetHashCode() == m_dtDocumentTarget.GetHashCode()) {
						iCurrentIndex = i;
					}
				}
				// Select the item that represents the document target that should be edited.
				m_lbxCurrentDocumentTargets.SelectedIndex = iCurrentIndex;
			}

			// Update the caption of the dialog, the visibility of the change button and the selection
			// mode of the 'Document targets at current input position' combo box
			switch (m_dmDialogMode) {
				case DialogMode.Insert:
					this.Title = "Insert Document Target";
					m_btnChangeName.Visibility = Visibility.Collapsed;
					m_lbxCurrentDocumentTargets.IsEnabled = false;
					break;
				case DialogMode.Edit:
					this.Title = "Edit Docoment Targets";
					break;
			}

			// Fill the 'Document targets in document' list box with all document targets of the document.
			TXTextControl.DocumentTargetCollection colDocumentTargets = m_txTextControl.DocumentTargets;
			foreach (TXTextControl.DocumentTarget target in colDocumentTargets) {
				m_lbxAllDocumentTargets.Items.Add(new DocumentTargetItem(target));
			}

			// Update the enabled state of the 'OK' and 'Change Name' button.
			m_btnOK.IsEnabled =
			m_btnChangeName.IsEnabled = m_tbxTargetName.Text.Trim().Length > 0;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** TargetName_TextChanged Handler
		** Update the enabled state of the 'OK' and the 'Change Name' button when the text of the 'Target Name' 
		** text box changed.
		**-----------------------------------------------------------------------------------------------------------*/
		private void TargetName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
			m_btnOK.IsEnabled =
			m_btnChangeName.IsEnabled = m_tbxTargetName.Text.Trim().Length > 0;
		}


		/*-------------------------------------------------------------------------------------------------------------
		** OK_Click Handler
		** Create or edit a hyperlink when clicking the 'OK' button.
		**-----------------------------------------------------------------------------------------------------------*/
		private void OK_Click(object sender, RoutedEventArgs e) {
			switch (m_dmDialogMode) {
				case DialogMode.Insert:
					// The dialog is opened to create a document target:
					// Create a new document target and insert it into the TextControl.
					TXTextControl.DocumentTarget dtDocumentTarget = new TXTextControl.DocumentTarget(m_tbxTargetName.Text);
					dtDocumentTarget.Deleteable = m_chbxCanBeDeleted.IsChecked.Value;
					m_txTextControl.DocumentTargets.Add(dtDocumentTarget);
					break;
				case DialogMode.Edit:
					// The dialog is opened to edit a document target:
					// Update the TargetName and the Deleteable property values of the document target.
					foreach (DocumentTargetItem item in m_lbxCurrentDocumentTargets.Items) {
						item.DocumentTarget.TargetName = item.DisplayedText;
						item.DocumentTarget.Deleteable = item.IsDocumentTargetDeletable;
					}
					break;
			}
			// Close the dialog.
			this.DialogResult = true;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** CurrentDocumentTargets_ItemSelected Handler
		** Update the text of the 'Target Name' text box with the displayed text of the new selected item. Furthermore,
		** the 'Can be deleted during editing' check box is updated.
		**-----------------------------------------------------------------------------------------------------------*/
		private void CurrentDocumentTargets_ItemSelected(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
			if (m_lbxCurrentDocumentTargets.SelectedIndex != -1) {
				DocumentTargetItem dtiSelectedItem = m_lbxCurrentDocumentTargets.SelectedItem as DocumentTargetItem;
				m_tbxTargetName.Text = dtiSelectedItem.DisplayedText;
				m_chbxCanBeDeleted.IsChecked = dtiSelectedItem.IsDocumentTargetDeletable;
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** ChangeName_Click Handler
		** Change the DisplayedText property value of the selected item inside the 'Document targets at current input 
		** position' list box to the text of the 'Target Name' text box when the 'Change Name' button is clicked.
		**-----------------------------------------------------------------------------------------------------------*/
		private void ChangeName_Click(object sender, RoutedEventArgs e) {
			DocumentTargetItem dtiNewItem = new DocumentTargetItem((m_lbxCurrentDocumentTargets.SelectedItem as DocumentTargetItem).DocumentTarget);
			dtiNewItem.DisplayedText = m_tbxTargetName.Text;
			m_lbxCurrentDocumentTargets.Items[m_lbxCurrentDocumentTargets.SelectedIndex] = dtiNewItem;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** CanBeDeleted_CheckedChanged Handler
		** Change the IsDocumentTargetDeletable property value of the selected item inside the 'Document targets at  
		** current input position' list box to the check state of the 'Can be deleted during editing' check box.
		**-----------------------------------------------------------------------------------------------------------*/
		private void CanBeDeleted_CheckedChanged(object sender, RoutedEventArgs e) {
			if (m_lbxCurrentDocumentTargets.SelectedIndex != -1) {
				DocumentTargetItem dtiSelectedItem = m_lbxCurrentDocumentTargets.SelectedItem as DocumentTargetItem;
				dtiSelectedItem.IsDocumentTargetDeletable = m_chbxCanBeDeleted.IsChecked.Value;
			}
		}
	}
}
