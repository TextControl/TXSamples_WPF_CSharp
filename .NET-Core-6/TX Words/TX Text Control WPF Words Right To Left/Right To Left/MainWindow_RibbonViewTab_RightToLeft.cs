/*-------------------------------------------------------------------------------------------------------------
** MainWindow_RibbonViewTab_RightToLeft.cs File
**
** Description:
**     Mangages the alignment/orientation of the application.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Media.Imaging;

namespace TXTextControl.Words {
	partial class MainWindow {

		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/

		private RibbonGroup m_rgAppViewGroup = null;
		private RibbonButton m_rbtnRightToLeft = null;
		private bool m_bRestartApplication = false;

		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** RightToLeftFormLayout_Click Handler
        ** Restarts the application with a program's view that has a reversed text appearance. Furthermore
        ** the user can save the current document before closing the application if the document is dirty.
        **-----------------------------------------------------------------------------------------------------------*/
		private void RightToLeftFormLayout_Click(object sender, System.EventArgs e) {
			bool bIsRightToLeft = (bool)(sender as RibbonButton).Tag;
			if (SaveDirtyDocumentBeforeReset(bIsRightToLeft)) {
				Properties.Settings.Default.RightToLeft = bIsRightToLeft ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
				SaveRecentFiles();
				m_bRestartApplication = true;
				Application.Current.Shutdown();
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** MainWindow_Closed Handler
		** Restarts the application if the corresponding flag is set.
		**-----------------------------------------------------------------------------------------------------------*/
		private void MainWindow_Closed(object sender, System.EventArgs e) {
			if (m_bRestartApplication) {
				System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** M E T H O D S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** LoadRightToLeftSettings Method
		** Gets the RightToLeft value from the application settings and sets that value as right to left layout.
		**-----------------------------------------------------------------------------------------------------------*/
		private void LoadRightToLeftSettings() {
			this.FlowDirection = Properties.Settings.Default.RightToLeft;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** AddAppViewGroup Method
        ** Creates a ribbon group with a ribbon button that restarts the application with a program's view that
        ** has a reversed text appearance. That ribbon group is added to the specified ribbon tab. 
        **
        ** Parameters:
        **      ribbonTab:   The ribbon tab where to add the created ribbon group.
        **-----------------------------------------------------------------------------------------------------------*/
		private void AddAppViewGroup(RibbonTab ribbonTab) {
			// Create the icon for the ribbon group and ribbon button.
			BitmapSource bmpSmallIcon = GetSmallIcon("TXTextControl.Words.Images.RightToLeft_Small.svg");
			BitmapSource bmpLargeIcon = GetLargeIcon("TXTextControl.Words.Images.RightToLeft_Large.svg");

			// Create the ribbon group
			m_rgAppViewGroup = new RibbonGroup() {
				Header = Properties.Resources.RibbonViewTab_ApplicationView,
				SmallImageSource = bmpSmallIcon,
				LargeImageSource = bmpLargeIcon,
				KeyTip = Properties.Resources.RibbonViewTab_ApplicationView_KeyTip
			};

			// Add a ribbon button that restarts the application with a program's
			// view that has a reversed text appearance.
			AddRightToLeftButton(m_rgAppViewGroup, bmpSmallIcon, bmpLargeIcon);

			// Add the ribbon group to the ribbon tab
			ribbonTab.Items.Add(m_rgAppViewGroup);
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** AddRightToLeftButton Method
        ** Creates a ribbon button that restarts the application with a program's view that
        ** has a reversed text appearance. That ribbon button is added to the specified ribbon group. 
        **
        ** Parameters:
        **      ribbonGroup:    The ribbon group where to add the created ribbon button.
        **      smallIcon:      The bitmap that is used as the ribbon button's small icon.
        **      largeIcon:      The bitmap that is used as the ribbon button's large icon.
        **-----------------------------------------------------------------------------------------------------------*/
		private void AddRightToLeftButton(RibbonGroup ribbonGroup, BitmapSource smallIcon, BitmapSource largeIcon) {
			// Get the current text appearance.
			bool bIsRightToLeft = IsFormLayoutRightToLeft();

			// Create the ribbon button
			m_rbtnRightToLeft = new RibbonButton() {
				// The button's text represents the required text appearance 
				Label = bIsRightToLeft ? Properties.Resources.RibbonViewTab_LeftToRight : Properties.Resources.RibbonViewTab_RightToLeft,
				SmallImageSource = smallIcon,
				LargeImageSource = largeIcon,
				KeyTip = Properties.Resources.RibbonViewTab_LeftToRight_KeyTip,
				Tag = bIsRightToLeft,
				Margin = new Thickness(30, 0, 30, 0) // A way to center the button inside the group
			};

			// Add tool tips
			m_rbtnRightToLeft.ToolTipTitle = bIsRightToLeft ? Properties.Resources.RibbonViewTab_LeftToRight_ToolTip_Title : Properties.Resources.RibbonViewTab_RightToLeft_ToolTip_Title;
			m_rbtnRightToLeft.ToolTipDescription = bIsRightToLeft ? Properties.Resources.RibbonViewTab_LeftToRight_ToolTip_Description : Properties.Resources.RibbonViewTab_RightToLeft_ToolTip_Description;

			// Add the handler that restarts the application with a reversed text appearance. 
			m_rbtnRightToLeft.Click += RightToLeftFormLayout_Click;

			// Add the ribbon button to the ribbon group
			ribbonGroup.Items.Add(m_rbtnRightToLeft);
		}



		/*-------------------------------------------------------------------------------------------------------------
		** IsFormLayoutRigthToLeft Method
		** Calculates whether the text appearance is set to 'right to left'.
		**
	    ** Return value:    True if the text appearance is set to 'right to left'. Otherwise false.
		**-----------------------------------------------------------------------------------------------------------*/
		private bool IsFormLayoutRightToLeft() {
			switch (this.FlowDirection) {
				case FlowDirection.RightToLeft:
					return true;
				case FlowDirection.LeftToRight:
					return false;
				default:
					// Inherit: Check system's settings
					return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
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
			MessageBoxResult mbrSaveFile = MessageBox.Show(this, strMessageBoxText, Properties.Resources.MessageBox_ChangeFormLayout_Caption, m_bIsDirtyDocument ? MessageBoxButton.YesNoCancel : MessageBoxButton.OKCancel, MessageBoxImage.Warning);
			switch (mbrSaveFile) {
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

