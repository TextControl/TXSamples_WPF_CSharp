/*-------------------------------------------------------------------------------------------------------------
** MainWindow_FileMenuItem_Click.cs File
**
** Description: Provides all Click handlers associated with 'File' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** File_New_Click Handler
		**
		** Invokes the TextControl.ResetContents method to create a new document.
		** 
		** Item: 'New'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_New_Click(object sender, RoutedEventArgs e) {
			// Check whether the document is dirty. In this case, the user is suggested to save that document. 
			if (SaveDirtyDocumentOnNew()) {
				// Create a new document.
				m_txTextControl.ResetContents();

				// A new document is created. Now:
				UpdateCurrentDocumentInfo(); // Reset the current document information.
				UpdateMainWindowCaption(); // Update the caption of the application's main window.
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_Open_Click Handler
		**
		** Invokes the Open method to load a document by using the internal TextControl 'Open' dialog.
		** 
		** Item: 'Open...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_Open_Click(object sender, RoutedEventArgs e) {
			Open();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_RecentFiles_Item_Click Handler
		**
		** Opens the file that is represented by the clicked item.
		** 
		** Item: Each item of the 'Recent Files' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_RecentFiles_Item_Click(object sender, RoutedEventArgs e) {
			// Get the file that is represented by the clicked item.
			string strFile = (e.Source as MenuItem).Tag as string;

			// Check whether the file still exists.
			if (!File.Exists(strFile)) {
				// If not, open a Message box that asks the user whether the not
				// existing file should be removed from the recent file list.
				if (MessageBox.Show(this, Properties.Resources.MessageBox_OpenRecentFile_FileDoesNotExist_Text, Properties.Resources.MessageBox_OpenRecentFile_FileDoesNotExist_Caption, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK) {
					// Remove the file from the list and update the items collection.
					m_colRecentFiles.Remove(strFile);
					UpdateRecentFileList();
				}
			}
			else {
				// Open the file.
				Open(strFile);
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_Save_Click Handler
		**
		** Invokes the Save method to save a document by saving it at the same location where it was loaded 
        ** before.
		** 
		** Item: 'Save'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_Save_Click(object sender, RoutedEventArgs e) {
			Save(m_strActiveDocumentPath);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_SaveAs_Click Handler
		**
		** Invokes the Save method to save a document by using the internal TextControl 'Save' dialog.
		** 
		** Item: 'Save As...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_SaveAs_Click(object sender, RoutedEventArgs e) {
			Save(null);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_PageSetup_Click Handler
		**
		** Invokes the built-in dialog for setting section attributes.
		** 
		** Item: 'Page Setup...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_PageSetup_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.SectionFormatDialog();
		}


		/*-------------------------------------------------------------------------------------------------------------
		** File_Print_Click Handler
		**
		** Invokes the TextControl Print method to open the TextControl print dialog.
		** 
		** Item: 'Print...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_Print_Click(object sender, RoutedEventArgs e) {
			// Use the active document name to open the print dialog.
			m_txTextControl.Print(m_strActiveDocumentName, true);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_PrintQuick_Click Handler
		**
		** Prints the current document without opening the dialog before.
		** 
		** Item: 'Print Quick'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_PrintQuick_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.Print(m_strActiveDocumentName, new PageRange(1, m_txTextControl.Pages), 1, Collation.Collated);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_SignIn_Click Handler
		**
		** Opens a dialog to sign in to the TextControl a user by its account. If no such account is known,
        ** a new account is created. 
		** 
		** Item: 'Sign In...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_SignIn_Click(object sender, RoutedEventArgs e) {
			// Open the password dialog to sign in or create a user account.
			UserAccessDialog dlgUserAccessDialog = m_uiCurrentUser == null ? new UserAccessDialog(m_strUserName) : new UserAccessDialog(m_uiCurrentUser);
			bool? bDialogResult = dlgUserAccessDialog.ShowDialog();
			if (bDialogResult.HasValue && bDialogResult.Value) {
				// Get the UserInfo instance that represents the current signed in user.
				m_uiCurrentUser = dlgUserAccessDialog.UserInfo;

				// Give the user access to the TextControl.
				m_txTextControl.UserNames = new string[] { m_uiCurrentUser.Name };

				// Hide the Sign In item.
				m_miFile_SignIn.IsEnabled = false;
				m_miFile_SignIn.Visibility = Visibility.Collapsed;

				// Show the [Current User] item.
				m_miFile_CurrentUser.IsEnabled = true;
				m_miFile_CurrentUser.Visibility = Visibility.Visible;
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_CurrentUser_AccountSettings_Click Handler
		**
		** Opens a dialog to edit the account settings of the current signed in user.
		** 
		** Item: 'Account Settings...' of the '[Current User]' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_CurrentUser_AccountSettings_Click(object sender, RoutedEventArgs e) {
			UserAccessDialog dlg = new UserAccessDialog(m_uiCurrentUser);
			bool? bDialogResult = dlg.ShowDialog();
			if (dlg.DeletePassword) {
				// The UserAccessDialog's Delete Account button was clicked. 
				// Set current user to null...
				m_uiCurrentUser = null;
				// ... and sign out.
				SignOut();
			}
			else {
				if (bDialogResult.Value) {
					// Replace the user info object of the current user
					// with a new instance.
					m_uiCurrentUser = dlg.UserInfo;
					m_miFile_CurrentUser.Visibility = Visibility.Visible;
				}
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_CurrentUser_SignOut_Click Handler
		**
		** Signs out the current signed in user.
		** 
		** Item: 'Sign Out' of the '[Current User]' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_CurrentUser_SignOut_Click(object sender, RoutedEventArgs e) {
			SignOut();
			MessageBox.Show(this, string.Format(Properties.Resources.MessageBox_UserAccess_SignOut_Text, m_strUserName), Properties.Resources.MessageBox_UserAccess_SignOut_Caption, MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** File_Exit_Click Handler
		**
		** Closes the application when clicked.
		** 
		** Item: 'Exit'
		**-----------------------------------------------------------------------------------------------------------*/
		private void File_Exit_Click(object sender, RoutedEventArgs e) {
			this.Close();
		}
	}
}
