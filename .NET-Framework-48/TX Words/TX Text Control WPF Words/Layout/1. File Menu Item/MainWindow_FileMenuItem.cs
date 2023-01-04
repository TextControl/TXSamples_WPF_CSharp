/*-------------------------------------------------------------------------------------------------------------
** MainWindow_FileMenuItem.cs File
**
** Description: Provides methods to set the layout of the 'File' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Collections.Specialized;
using TXTextControl.WPF;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/

		// 'Open...' item
		private readonly PDFImportSettings m_isPDFImportSettings = PDFImportSettings.GenerateTextFrames | PDFImportSettings.LoadEmbeddedFiles;

		// 'Recent Files' item
		private const int m_iMaxRecentFiles = 10;
		private StringCollection m_colRecentFiles;

		// 'Sign In...' and '[Current user]' items
		private UserInfo m_uiCurrentUser = null; // Info about the current user.
		private string m_strUserName = ""; // Environment.UserName


		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** SetFileItemsTexts Method
		**
		** Sets the texts of the 'File' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetFileItemsTexts() {
			// '[Current User]'
			SetItemText(m_miFile_CurrentUser,m_strUserName);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** SetFileItemsImages Method
		**
		** Sets the images of the 'File' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetFileItemsImages() {
			// 'New'
			SetItemImage(m_miFile_New, ResourceProvider.FileMenuItem.TXITEM_New.ToString());

			// 'Open...'
			SetItemImage(m_miFile_Open, ResourceProvider.FileMenuItem.TXITEM_Open.ToString());

			// 'Save'
			SetItemImage(m_miFile_Save, ResourceProvider.FileMenuItem.TXITEM_Save.ToString());

			// 'Save As...'
			SetItemImage(m_miFile_SaveAs, ResourceProvider.FileMenuItem.TXITEM_SaveAs.ToString());

			// 'Page Setup...'
			SetItemImage(m_miFile_PageSetup, RibbonPageLayoutTab.RibbonItem.TXITEM_PageMargins.ToString());

			// 'Print...'
			SetItemImage(m_miFile_Print, ResourceProvider.FileMenuItem.TXITEM_Print.ToString());

			// 'Print Quick'
			SetItemImage(m_miFile_PrintQuick, ResourceProvider.FileMenuItem.TXITEM_PrintQuick.ToString());

			// 'Sign In...'
			SetItemImage(m_miFile_SignIn, ResourceProvider.FileMenuItem.TXITEM_SignIn.ToString());

			// '[Current User]'
			SetItemImage(m_miFile_CurrentUser, ResourceProvider.FileMenuItem.TXITEM_CurrentUser.ToString());
			SetItemImage(m_miFile_CurrentUser_AccountSettings, ResourceProvider.FileMenuItem.TXITEM_AccountSettings.ToString());
			SetItemImage(m_miFile_CurrentUser_SignOut, ResourceProvider.FileMenuItem.TXITEM_SignOut.ToString());

			// 'Exit'
			SetItemImage(m_miFile_Exit, ResourceProvider.FileMenuItem.TXITEM_Exit.ToString());
		}
	}
}
