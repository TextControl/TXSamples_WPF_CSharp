/*-------------------------------------------------------------------------------------------------------------
** MainWindow_ViewMenuItem_Methods.cs File
**
** Description: Provides supporting methods to implement the desired behavior of some 'View' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/

using System.Windows;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
        ** 'Tracked Changes' Item
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** HighlightTrackedChanges Method
        ** Steps through all tracked changes to set the specified highlight mode.
        **
        ** Parameters:
        **      highlightMode:  The highlight mode to set.
        **-----------------------------------------------------------------------------------------------------------*/
		private void SetTrackedChangesHighlightMode(HighlightMode highlightMode) {
			// Get tracked changes.
			TrackedChangeCollection colTrackedChanges = m_txTextControl.TrackedChanges;
			int iCount = colTrackedChanges.Count;
			if (iCount > 0) {
				// The highlight setting iteration must be handled as one undo action.
				m_txTextControl.BeginUndoAction(highlightMode == HighlightMode.Always ?
					Properties.Resources.Method_SetTrackedChangesHighlightMode_HighlightTrackedChanges :
					Properties.Resources.Method_SetTrackedChangesHighlightMode_HideTrackedChanges);
				// Step through all tracked changes and set the specified highlight mode.
				for (int i = 1; i <= iCount; i++) {
					colTrackedChanges[i].HighlightMode = highlightMode;
				}
				// End undo action.
				m_txTextControl.EndUndoAction();
			}
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** 'Right to Left Layout' Item
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** LoadRightToLeftSettings Method
        ** Gets the RightToLeft value from the application settings and sets that value as right to left layout.
        **-----------------------------------------------------------------------------------------------------------*/
		private void LoadRightToLeftSettings() {
			this.FlowDirection = Properties.Settings.Default.RightToLeft;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** ReverseAppTextAppearance Method
        ** Restarts the application with a program's view that has a reversed text appearance. Furthermore
        ** the user can save the current document before closing the application if the document is dirty.
        **
        ** Parameters:
        **      isRightToLeft:  A value that indicates whether the application should be restarted with a
        **						right to left appearance.
        **-----------------------------------------------------------------------------------------------------------*/
		private void ReverseAppTextAppearance(bool isRightToLeft) {
			if (SaveDirtyDocumentBeforeReset(isRightToLeft)) {
				Properties.Settings.Default.RightToLeft = isRightToLeft ? System.Windows.FlowDirection.LeftToRight : System.Windows.FlowDirection.RightToLeft;
				SaveRecentFiles();
				m_bRestartApplication = true;
				Application.Current.Shutdown();
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** SaveDirtyDocumentBeforeReset Method
        ** Open a Message Box that asks the user to confirm the restart of the application
        ** with a reversed text appearance. Furthermore, if the document is dirty, the user
        ** can decide how to handle it.
        **
        ** Parameters:
        **      isRightToLeft:  A value indicating the current text appearance
        **
        ** Return value:        If restarting the application with a reversed text appearance should be  
        **                      canceled, the method returns false. Otherwise true.
        **-----------------------------------------------------------------------------------------------------------*/
		private bool SaveDirtyDocumentBeforeReset(bool isRightToLeft) {
			// Some parts of the text to display depend on the current text appearance
			string strText1 = isRightToLeft ? Properties.Resources.MessageBox_SaveDirtyDocumentBeforeRestart_Left : Properties.Resources.MessageBox_SaveDirtyDocumentBeforeRestart_Right;
			string strText2 = isRightToLeft ? Properties.Resources.MessageBox_SaveDirtyDocumentBeforeRestart_Right : Properties.Resources.MessageBox_SaveDirtyDocumentBeforeRestart_Left;

			// Get the message box text.
			string strMessageBoxText = m_bIsDirtyDocument ? // Check whether the document is dirty.
				(m_bIsUnknownDocument ?
				string.Format(Properties.Resources.MessageBox_SaveDirtyDocumentBeforeRestart_Untitled, strText1, strText2) : // The document is unknown.
				string.Format(Properties.Resources.MessageBox_SaveDirtyDocumentBeforeRestart_ToFile, strText1, strText2, m_strActiveDocumentPath) // The document is known.
				) :
				string.Format(Properties.Resources.MessageBox_ChangeFormLayout_Text, strText1, strText2); // The document is not dirty.

			// Show message box.
			bool bKeepGoing = true;
			MessageBoxResult mrSaveFile = MessageBox.Show(this, strMessageBoxText, Properties.Resources.MessageBox_ChangeFormLayout_Caption, m_bIsDirtyDocument ? MessageBoxButton.YesNoCancel : MessageBoxButton.OKCancel, MessageBoxImage.Warning);
			switch (mrSaveFile) {
				case MessageBoxResult.Yes:
					// The dirty document should be saved
					bKeepGoing = Save(m_strActiveDocumentPath); // If it was not saved, the appliation won't be restarted with a reversed text appearance.
					break;
				case MessageBoxResult.Cancel:
					// Cancel restarting the application with a reversed text appearance.
					bKeepGoing = false;
					break;
				case MessageBoxResult.No: // Do not save the dirty document before restarting the application with a reversed text appearance.
				case MessageBoxResult.OK: // Restarting the application with a reversed text appearance.
					break;
			}
			return bKeepGoing;
		}
	}
}

