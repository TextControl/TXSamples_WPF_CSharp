/*-------------------------------------------------------------------------------------------------------------
** MainWindow_ViewMenuItem_Click.cs File
**
** Description: Provides all Click handlers associated with 'View' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;
using System.Windows.Controls;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
        ** View_PageLayout_Click Handler
        **
        ** Enables a view mode where the text is formatted according to the settings of the PageSize and the 
        ** PageMargins properties. Additionally the TextControl displays the pages in 3D view with gaps and a desktop
        ** background.
        ** 
        ** Item: 'Page Layout'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_PageLayout_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.ViewMode = ViewMode.PageView;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_Draft_Click Handler
        ** 
        ** Enables a view mode where the text is formatted according to the settings of the PageSize and the 
        ** PageMargins properties.
        **
        ** Item: 'Draft'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_Draft_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.ViewMode = ViewMode.Normal;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_ButtonBar_Click Handler
        ** 
        ** Displays or hides the ButtonBar control that can be used to show or to set font and paragraph attributes of  
        ** the TextControl. 
        **
        ** Item: 'Button Bar'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_ButtonBar_Click(object sender, RoutedEventArgs e) {
			m_bbButtonBar.Visibility = m_miView_ButtonBar.IsChecked ? Visibility.Visible : Visibility.Collapsed;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_StatusBar_Click Handler
        ** 
        ** Displays or hides the StatusBar control that can be used to show the position of the current text input 
        ** position and other status information of the TextControl. 
        **
        ** Item: 'Status Bar'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_StatusBar_Click(object sender, RoutedEventArgs e) {
			m_sbStatusBar.Visibility = m_miView_StatusBar.IsChecked ? Visibility.Visible : Visibility.Collapsed;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_HorizontalRuler_Click Handler
        ** 
        ** Displays or hides the horizontal RulerBar control that is connected to the TextControl. 
        **
        ** Item: 'Horizontal Ruler'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_HorizontalRuler_Click(object sender, RoutedEventArgs e) {
			m_rbHorizontalRulerBar.Visibility = m_miView_HorizontalRuler.IsChecked ? Visibility.Visible : Visibility.Collapsed;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_VerticalRuler_Click Handler
        ** 
        ** Displays or hides the vertical RulerBar control that is connected to the TextControl. 
        **
        ** Item: 'Vertical Ruler'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_VerticalRuler_Click(object sender, RoutedEventArgs e) {
			m_rbVerticalRulerBar.Visibility = m_miView_VerticalRuler.IsChecked ? Visibility.Visible : Visibility.Collapsed;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_TableGridlines_Click Handler
        ** 
        ** Sets a value whether table gridlines are shown or not.
        **
        ** Item: 'Table Gridlines'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_TableGridlines_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.Tables.GridLines = m_miView_TableGridlines.IsChecked;
		}

        /*-------------------------------------------------------------------------------------------------------------
        ** View_BookmarkMarkers_Click Handler
        ** 
        ** Sets a value whether bookmark markers are shown or not.
        **
        ** Item: 'Bookmark Markers'
        **-----------------------------------------------------------------------------------------------------------*/
        private void View_BookmarkMarkers_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.DocumentTargetMarkers = m_miView_BookmarkMarkers.IsChecked;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_TextFrameMarkerLines_Click Handler
        ** 
        ** Sets a value whether text frames that have no border line are shown with marker lines.
        **
        ** Item: 'Text Frame Marker Lines'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_TextFrameMarkerLines_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.TextFrameMarkerLines = m_miView_TextFrameMarkerLines.IsChecked;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_DrawingMarkerLines_Click Handler
        ** 
        ** Sets a value whether a marker frame is shown around a drawing to indicate its position and size.
        **
        ** Item: 'Drawing Marker Lines'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_DrawingMarkerLines_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.DrawingMarkerLines = m_miView_DrawingMarkerLines.IsChecked;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_ControlChars_Click Handler
        ** 
        ** Sets a value whether control characters are visible or not.
        **
        ** Item: 'Control Chars'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_ControlChars_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.ControlChars = m_miView_ControlChars.IsChecked;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_EditableRegions_Always_Click Handler
        ** 
        ** Determines that an editable region is always highlighted.
        **
        ** Item: 'Always' of the 'Editable Regions' drop down menu
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_EditableRegions_Always_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.EditableRegionHighlightMode = HighlightMode.Always;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_EditableRegions_Current_Click Handler
        ** 
        ** Determines that an editable region is only highlighted, when it contains the current text input position 
        ** and when the control has the input focus.
        **
        ** Item: 'Current' of the 'Editable Regions' drop down menu
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_EditableRegions_Current_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.EditableRegionHighlightMode = HighlightMode.Activated;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_EditableRegions_Never_Click Handler
        ** 
        ** Determines that an editable region is never highlighted.
        **
        ** Item: 'Never' of the 'Editable Regions' drop down menu
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_EditableRegions_Never_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.EditableRegionHighlightMode = HighlightMode.Never;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_TrackedChanges_Click Handler
        ** 
        ** Sets a value whether a tracked change is always or never highlighted.
        **
        ** Item: 'Tracked Changes'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_TrackedChanges_Click(object sender, RoutedEventArgs e) {
			SetTrackedChangesHighlightMode(m_miView_TrackedChanges.IsChecked ? HighlightMode.Always : HighlightMode.Never);
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_Comments_Always_Click Handler
        ** 
        ** Determines that commented texts are always highlighted.
        **
        ** Item: 'Always' of the 'Comments' drop down menu
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_Comments_Always_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.CommentHighlightMode = HighlightMode.Always;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_Comments_Current_Click Handler
        ** 
        ** Determines that a commented text is only highlighted when it contains the text input position.
        **
        ** Item: 'Current' of the 'Comments' drop down menu
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_Comments_Current_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.CommentHighlightMode = HighlightMode.Activated;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_Comments_Never_Click Handler
        ** 
        ** Determines that no commented text is highlighted.
        **
        ** Item: 'Never' of the 'Comments' drop down menu
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_Comments_Never_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.CommentHighlightMode = HighlightMode.Never;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_Zoom_MenuItem_Click Handler
        ** 
        ** Sets the zoom factor, in percent, for a TextControl. The value is represented by the clicked item's
        ** Tag property value.
        **
        ** Item: Each item of the 'Zoom' drop down menu
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_Zoom_MenuItem_Click(object sender, RoutedEventArgs e) {
			string strZoomItemValue = (e.Source as MenuItem).Tag.ToString();
			m_txTextControl.ZoomFactor = int.Parse(strZoomItemValue);
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** View_RightToLeftLayout_Click Handler
        ** 
        ** Restarts the application with a program's view that has a reversed text appearance. Furthermore
        ** the user can save the current document before closing the application if the document is dirty.
        **
        ** Item: 'Right to Left Layout'
        **-----------------------------------------------------------------------------------------------------------*/
		private void View_RightToLeftLayout_Click(object sender, RoutedEventArgs e) {
			ReverseAppTextAppearance(m_miView_RightToLeftLayout.IsChecked);
		}
	}
}
