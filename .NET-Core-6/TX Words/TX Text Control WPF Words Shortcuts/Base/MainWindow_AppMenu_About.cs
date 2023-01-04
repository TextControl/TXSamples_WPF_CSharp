/*-------------------------------------------------------------------------------------------------------------
** MainWindow_AppMenu_About.cs File
**
** Description:
**      Handles displaying the 'About' sidebar and manages its content.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;
using TXTextControl.WPF;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** M E T H O D S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** UpdateAboutSidebar Method
        ** Connects a handler to the about viewer's Loaded event.
        **-----------------------------------------------------------------------------------------------------------*/
		private void UpdateAboutSidebar() {
			TextControl txAboutViewer = m_sbSidebarLeft.FindName(Sidebar.AboutItem.TXITEM_AboutViewer.ToString()) as TextControl;
			txAboutViewer.Loaded += AboutViewer_Loaded;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** AboutViewer_Loaded Handler
        ** Loads the About.xml into the about viewer.
        **-----------------------------------------------------------------------------------------------------------*/
		private void AboutViewer_Loaded(object sender, RoutedEventArgs e) {
			TextControl txAboutViewer = (sender as TextControl);
			txAboutViewer.Tables.GridLines = false; // Disable table grid lines.
			txAboutViewer.PageSize = new PageSize(17010, 17010); // Set a size of 30x30cm
			txAboutViewer.XmlEditMode = XmlEditMode.NoValidate;
			txAboutViewer.Load(m_strFilesDirectory + "About.xml", StreamType.XMLFormat);
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** About_Click Handler
        ** Shows or hides the 'About' sidebar
        **-----------------------------------------------------------------------------------------------------------*/
		private void About_CheckedChanged(object sender, RoutedEventArgs e) {
			if (m_sbSidebarLeft != null) {
				if (m_rtbtnAbout.IsChecked) {
					m_sbSidebarLeft.ContentLayout = Sidebar.SidebarContentLayout.About;
					m_sbSidebarLeft.IsShown = true;
					UpdateAboutSidebar();
				}
				else {
					m_sbSidebarLeft.IsShown = false;
				}
			}
		}
	}
}
