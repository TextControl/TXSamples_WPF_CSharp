/*-------------------------------------------------------------------------------------------------------------
** MainWindow_ViewMenuItem.cs File
**
** Description: Provides methods to set the layout of the 'View' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/

using TXTextControl.WPF;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** SetViewItemsTexts Method
		**
		** Sets the texts of the 'View' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetViewItemsTexts() {
			// 'Zoom'
			SetItemText(m_miView_Zoom_50, Properties.Resources.Item_View_Zoom_Factor_Text, "_50");
			SetItemText(m_miView_Zoom_75, Properties.Resources.Item_View_Zoom_Factor_Text, "_75");
			SetItemText(m_miView_Zoom_100, Properties.Resources.Item_View_Zoom_Factor_Text, "_100");
			SetItemText(m_miView_Zoom_150, Properties.Resources.Item_View_Zoom_Factor_Text, "15_0");
			SetItemText(m_miView_Zoom_200, Properties.Resources.Item_View_Zoom_Factor_Text, "_200");
			SetItemText(m_miView_Zoom_300, Properties.Resources.Item_View_Zoom_Factor_Text, "_300");
			SetItemText(m_miView_Zoom_400, Properties.Resources.Item_View_Zoom_Factor_Text, "_400");
		}

		/*-------------------------------------------------------------------------------------------------------------
		** SetViewItemsImages Method
		**
		** Sets the images of the 'View' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetViewItemsImages() {
			// 'Page Layout'
			SetItemImage(m_miView_PageLayout, RibbonViewTab.RibbonItem.TXITEM_PrintLayout.ToString());

			// 'Draft'
			SetItemImage(m_miView_Draft, RibbonViewTab.RibbonItem.TXITEM_Draft.ToString());

			// 'Table Gridlines'
			SetItemImage(m_miView_TableGridlines, RibbonViewTab.RibbonItem.TXITEM_ShowTableGridlines.ToString());

			// 'Bookmark Markers'
			SetItemImage(m_miView_BookmarkMarkers, RibbonViewTab.RibbonItem.TXITEM_ShowBookmarkMarkers.ToString());

			// 'Text Frame Marker Lines'
			SetItemImage(m_miView_TextFrameMarkerLines, RibbonViewTab.RibbonItem.TXITEM_ShowTextFrameMarkersLines.ToString());

			// 'Drawing Marker Lines'
			SetItemImage(m_miView_DrawingMarkerLines, RibbonViewTab.RibbonItem.TXITEM_ShowDrawingFrameMarkersLines.ToString());

			// 'Control Chars'
			SetItemImage(m_miView_ControlChars, RibbonViewTab.RibbonItem.TXITEM_ShowControlChars.ToString());

			// 'Editable Regions'
			SetItemImage(m_miView_EditableRegions, RibbonPermissionsTab.RibbonItem.TXITEM_HighlightEditableRegions.ToString());

			// 'Tracked Changes'
			SetItemImage(m_miView_TrackedChanges, RibbonProofingTab.RibbonItem.TXITEM_TrackChanges.ToString());

			// 'Comments'
			SetItemImage(m_miView_Comments, RibbonProofingTab.RibbonItem.TXITEM_CommentsViewMode.ToString());

			// 'Zoom'
			SetItemImage(m_miView_Zoom, RibbonViewTab.RibbonItem.TXITEM_ZoomFactor.ToString());

			// 'Right to Left Layout'
			m_miView_RightToLeftLayout.Icon = GetSmallIcon("TXTextControl.Words.Images.RightToLeft_Small.svg");
		}
	}
}
