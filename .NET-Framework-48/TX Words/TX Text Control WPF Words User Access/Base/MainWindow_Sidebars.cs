/*-------------------------------------------------------------------------------------------------------------
** MainWindow_Sidebars.cs File
**
** Description:
**     Manages the layout of the sidebars when the content layout changed.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/

using System;
using System.ComponentModel;
using TXTextControl.WPF;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/
		DependencyPropertyDescriptor m_dpdSidebarContentLayout = DependencyPropertyDescriptor.FromProperty(Sidebar.ContentLayoutProperty, typeof(Sidebar));
		DependencyPropertyDescriptor m_dpdSidebarIsShown = DependencyPropertyDescriptor.FromProperty(Sidebar.IsShownProperty, typeof(Sidebar));


		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** SetSidebarBehavior Method
        ** Connects the sidebars with the corresponding property value changed handlers.
        **-----------------------------------------------------------------------------------------------------------*/
		private void SetSidebarBehavior() {
			// Left sidebar:
			m_dpdSidebarIsShown.AddValueChanged(m_sbSidebarLeft, SidebarLeft_IsShownChanged);

			// Right sidebar:
			m_dpdSidebarContentLayout.AddValueChanged(m_sbSidebarRight, SidebarRight_ContentLayoutChanged);

			// Bottom sidebar:
			m_dpdSidebarContentLayout.AddValueChanged(m_sbSidebarBottom, SidebarBottom_ContentLayoutChanged);
		}


		/*-------------------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** SidebarLeft_IsShownChanged Handler
        ** Toggles the 'About' button if the left sidebar is shown and its ContentLayout is set to 
        ** SidebarContentLayout.About.
        **-----------------------------------------------------------------------------------------------------------*/
		private void SidebarLeft_IsShownChanged(object sender, EventArgs e) {
			m_rtbtnAbout.IsChecked = m_sbSidebarLeft.ContentLayout == Sidebar.SidebarContentLayout.About && m_sbSidebarLeft.IsShown;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** SidebarRight_ContentLayoutChanged Handler
        ** Manages the layout of the right sidebar if its ContentLayout is set to Styles, Comments, TrackedChanges,
        ** Find or Replace.
        **-----------------------------------------------------------------------------------------------------------*/
		private void SidebarRight_ContentLayoutChanged(object sender, EventArgs e) {

			switch (m_sbSidebarRight.ContentLayout) {
				case Sidebar.SidebarContentLayout.Styles:
					m_sbSidebarRight.ShowPinButton = false;
					m_sbSidebarRight.IsPinned = true;
					m_sbSidebarRight.DialogStyle = Sidebar.SidebarDialogStyle.Standard;
					break;
				case Sidebar.SidebarContentLayout.Comments:
				case Sidebar.SidebarContentLayout.TrackedChanges:
					m_sbSidebarRight.ShowPinButton = true;
					m_sbSidebarRight.DialogStyle = Sidebar.SidebarDialogStyle.StandardSizable;
					break;
				default:
					// Find or Replace
					m_sbSidebarRight.ShowPinButton = true;
					m_sbSidebarRight.DialogStyle = Sidebar.SidebarDialogStyle.Standard;
					break;
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** SidebarBottom_ContentLayoutChanged Handler
        ** Manages the layout of the bottom sidebar if its ContentLayout is set to TrackedChanges, Comments, Find,
        ** Replace and GoTo.
        **-----------------------------------------------------------------------------------------------------------*/
		private void SidebarBottom_ContentLayoutChanged(object sender, EventArgs e) {

			switch (m_sbSidebarBottom.ContentLayout) {
				case Sidebar.SidebarContentLayout.TrackedChanges:
				case Sidebar.SidebarContentLayout.Comments:
					m_sbSidebarBottom.ShowTitle = true;
					m_sbSidebarBottom.DialogStyle = Sidebar.SidebarDialogStyle.StandardSizable;
					break;
				default:
					// Find, Replace and GoTo
					m_sbSidebarBottom.ShowTitle = false;
					m_sbSidebarBottom.DialogStyle = Sidebar.SidebarDialogStyle.Standard;
					break;
			}

		}
	}
}
