/*-------------------------------------------------------------------------------------------------------------
** MainWindow_ViewMenuItem_DropDownOpening.cs File
**
** Description: Provides all SubmenuOpened handlers associated with 'View' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;
using System.Windows.Controls;


namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** View_SubmenuOpened Handler
		**
		** Updates the checked state of 'View' drop down menu items.
		** 
		** Item: 'View'
		**-----------------------------------------------------------------------------------------------------------*/
		private void View_SubmenuOpened(object sender, RoutedEventArgs e) {
			// 'Page Layout'
			m_miView_PageLayout.IsChecked = m_txTextControl.ViewMode == ViewMode.PageView;

			// 'Draft'
			m_miView_Draft.IsChecked = m_txTextControl.ViewMode == ViewMode.Normal;

			// 'Button Bar'
			m_miView_ButtonBar.IsChecked = m_bbButtonBar.Visibility == Visibility.Visible;

			// 'Status Bar'
			m_miView_StatusBar.IsChecked = m_sbStatusBar.Visibility == Visibility.Visible;

			// 'Horizontal Ruler'
			m_miView_HorizontalRuler.IsChecked = m_rbHorizontalRulerBar.Visibility == Visibility.Visible;

			// 'Vertical Ruler'
			m_miView_VerticalRuler.IsChecked = m_rbVerticalRulerBar.Visibility == Visibility.Visible;

			// 'Table Gridlines'
			m_miView_TableGridlines.IsChecked = m_txTextControl.Tables.GridLines;

			// 'Bookmark Markers'
			m_miView_BookmarkMarkers.IsChecked = m_txTextControl.DocumentTargetMarkers;

			// 'Text Frame Marker Lines'
			m_miView_TextFrameMarkerLines.IsChecked = m_txTextControl.TextFrameMarkerLines;

			// 'Drawing Marker Lines'
			m_miView_DrawingMarkerLines.IsChecked = m_txTextControl.DrawingMarkerLines;

			if (m_plTXLicense >= VersionInfo.ProductLevel.Professional) {
				// 'Tracked Changes'

				// Step through all tracked changes to get their common highlight mode.
				TrackedChangeCollection colTrackedChanges = m_txTextControl.TrackedChanges;
				int iCount = colTrackedChanges.Count;
				HighlightMode hmCurrentHighlightMode = HighlightMode.Always;
				for (int i = 1; i < iCount; i++) {
					hmCurrentHighlightMode = colTrackedChanges[i].HighlightMode;
					// Check whether the current tracked change highlight mode differs to the next one's
					if (hmCurrentHighlightMode != colTrackedChanges[i + 1].HighlightMode) {
						// In that case set the 'Tracked Changes' item's checked value to false.
						m_miView_TrackedChanges.IsChecked = false;
						return;
					}
				}

				// The 'Tracked Changes' item is checked if the highlight mode of all tracked changes
				// is set to HighlightMode.Always
				m_miView_TrackedChanges.IsChecked = hmCurrentHighlightMode == HighlightMode.Always;
			}

			// 'Right to Left Layout'
			m_miView_RightToLeftLayout.IsChecked = this.FlowDirection == System.Windows.FlowDirection.RightToLeft;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** View_EditableRegions_SubmenuOpened Handler
		**
		** Updates the checked state of 'Editable Regions' drop down menu items.
		** 
		** Item: 'Editable Regions'
		**-----------------------------------------------------------------------------------------------------------*/
		private void View_EditableRegions_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Set the check states of the 'Editable Regions' drop down items.
			HighlightMode hmHighlightMode = m_txTextControl.EditableRegionHighlightMode;
			m_miView_EditableRegions_Always.IsChecked = hmHighlightMode == HighlightMode.Always;
			m_miView_EditableRegions_Current.IsChecked = hmHighlightMode == HighlightMode.Activated;
			m_miView_EditableRegions_Never.IsChecked = hmHighlightMode == HighlightMode.Never;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** View_Comments_SubmenuOpened Handler
		**
		** Updates the checked state of 'Comments' drop down menu items.
		** 
		** Item: 'Comments'
		**-----------------------------------------------------------------------------------------------------------*/
		private void View_Comments_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Set the check states of the 'Comments' drop down items.
			HighlightMode hmHighlightMode = m_txTextControl.CommentHighlightMode;
			m_miView_Comments_Always.IsChecked = hmHighlightMode == HighlightMode.Always;
			m_miView_Comments_Current.IsChecked = hmHighlightMode == HighlightMode.Activated;
			m_miView_Comments_Never.IsChecked = hmHighlightMode == HighlightMode.Never;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** View_Zoom_SubmenuOpened Handler
		**
		** Updates the checked state of 'Zoom' drop down menu items.
		** 
		** Item: 'Zoom'
		**-----------------------------------------------------------------------------------------------------------*/
		private void View_Zoom_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Set the check states of the 'Zoom' drop down items.
			string strZoomFactor = m_txTextControl.ZoomFactor.ToString();
			foreach (MenuItem item in m_miView_Zoom.Items) {
				item.IsChecked = item.Tag.ToString() == strZoomFactor;
			}
		}
	}
}
