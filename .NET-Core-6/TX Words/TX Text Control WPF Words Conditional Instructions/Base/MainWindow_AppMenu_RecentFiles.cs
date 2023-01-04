/*-------------------------------------------------------------------------------------------------------------
** MainWindow_AppMenu_RecentFiles.cs File
**
** Description:
**     Manages the recent files.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Windows.Controls.Ribbon;

namespace TXTextControl.Words {
	partial class MainWindow {

		/*-------------------------------------------------------------------------------------------------------------
        ** M E M B E R   V A R I A B L E S
        **-----------------------------------------------------------------------------------------------------------*/
		private const int m_iMaxRecentFiles = 10;
		private StringCollection m_colRecentFiles;


		/*-------------------------------------------------------------------------------------------------------------
        ** P R O P E R T I E S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** RecentFiles Property
        ** Gets or sets the list of recent files.
        **-----------------------------------------------------------------------------------------------------------*/
		internal StringCollection RecentFiles {
			get { return m_colRecentFiles; }
			set
			{
				m_colRecentFiles = value ?? new StringCollection();
				// Remove empty entries.
				for (int i = m_colRecentFiles.Count - 1; i >= 0; i--) {
					if (string.IsNullOrEmpty(m_colRecentFiles[i])) {
						m_colRecentFiles.RemoveAt(i);
					}
				}
				UpdateRecentFileList();
			}
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** RecentFiles_SelectionChanged Handler
        ** Opens the file that is represented by the selected RibbonGalleryItem.
        **-----------------------------------------------------------------------------------------------------------*/
		private void RecentFiles_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
			// Get the file that is represented by the selected RibbonGalleryItem.
			string strFile = (e.NewValue as RibbonGalleryItem).Tag as string;

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

			m_rgalRecentFiles.SelectedItem = null; // Deselect recent file.
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** LoadRecentFiles Method
        ** Gets the recent files from the application settings.
        **-----------------------------------------------------------------------------------------------------------*/
		private void LoadRecentFiles() {
			this.RecentFiles = Properties.Settings.Default.RecentFiles;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** SaveRecentFiles Method
        ** Saves the recent files list to the Properties.Settings.Default.RecentFiles property when the 
        ** application is closing (see MainWindow_FormClosing Handler).
        **-----------------------------------------------------------------------------------------------------------*/
		private void SaveRecentFiles() {
			Properties.Settings.Default.RecentFiles = this.RecentFiles;
			Properties.Settings.Default.Save();
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** AddRecentFile Method
        ** Inserts the specified file path as first entry inside the recent files list. 
        **
        ** Parameters:
        **      filePath:   The file path to add.
        **-----------------------------------------------------------------------------------------------------------*/
		private void AddRecentFile(string filePath) {
			if (!string.IsNullOrEmpty(filePath)) {
				// Check whether the list already contains that file.
				if (m_colRecentFiles.Contains(filePath)) {
					// In that case remove that file.
					m_colRecentFiles.Remove(filePath);
				}
				else {
					// Remove last entry if the current number of entries equals to the
					// maximum number of recent files.
					if (m_colRecentFiles.Count == m_iMaxRecentFiles) {
						m_colRecentFiles.RemoveAt(m_iMaxRecentFiles - 1);
					}
				}
				// Insert the file path at the top of the list.
				m_colRecentFiles.Insert(0, filePath);

				// Update the recent file items inside the ribbon's ApplicationMenuHelpPaneItems collection.
				UpdateRecentFileList();
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** UpdateRecentFileList Method
        ** Updates the recent file items inside the ribbon's ApplicationMenuHelpPaneItems collection.
        **-----------------------------------------------------------------------------------------------------------*/
		private void UpdateRecentFileList() {

			// Remove all recent file items from the collection.
			m_rgcRecentFiles.Items.Clear();

			// Create and insert for each recent file path entry a RibbonButton that represents a recent file.
			for (int i = 0; i < m_colRecentFiles.Count && i < m_iMaxRecentFiles; i++) {
				m_rgcRecentFiles.Items.Add(CreateRecentFileButton(i));
			}

		}

		/*-------------------------------------------------------------------------------------------------------------
        ** CreateRecentFileButton Method
        ** Creates and returns a RibbonButton that represents a recent file.
        **
        ** Parameters:
        **      index:   The index of the recent file inside the recent files collection.
        **
        ** Return value:    A RibbonButton that represents a recent file.
        **-----------------------------------------------------------------------------------------------------------*/
		private RibbonGalleryItem CreateRecentFileButton(int index) {
			// Create a labeled RibbonButton with no icon
			RibbonGalleryItem rbtnRecentFileButton = new RibbonGalleryItem();

			// Get the path and name of the file.
			string strFilePath = m_colRecentFiles[index];
			string strFileName = Path.GetFileName(strFilePath);

			// Determine the displayed text of the button (index plus file name) 
			// and store the file path as Tag value.
			rbtnRecentFileButton.KeyTip = (index + 1).ToString();
			rbtnRecentFileButton.Content = rbtnRecentFileButton.KeyTip + ". " + strFileName;
			rbtnRecentFileButton.Tag = strFilePath;

			// Provide file name and path by setting the tool tip.
			rbtnRecentFileButton.ToolTipTitle = strFileName;
			rbtnRecentFileButton.ToolTipDescription = strFilePath;

			return rbtnRecentFileButton;
		}
	}
}
