/*-------------------------------------------------------------------------------------------------------------
** MainWindow_ContextualTabs.cs File
**
** Description:
**     Handles showing/hiding contextual tabs.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System;
using TXTextControl.DataVisualization;

namespace TXTextControl.Words {
	partial class MainWindow {

		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** SetContextualTabsBehavior Method
        ** Sets the header of the contextual tabs and adds all necessary handlers to the TextControl.
        **-----------------------------------------------------------------------------------------------------------*/
		private void SetContextualTabsBehavior() {
			// Frame Tools:
			m_ctgFrameTools.Header = Properties.Resources.ContextualTabGroup_FrameTools; // Set Frame Tools header

			// Connect all necessary handlers to act as a Frame Tools group
			m_txTextControl.FrameSelected += TextControl_FrameSelected;
			m_txTextControl.FrameDeselected += TextControl_FrameDeselected;
			m_txTextControl.DrawingActivated += TextControl_DrawingActivated;
			m_txTextControl.DrawingDeactivated += TextControl_DrawingDeactivated;

			// Table Tools:
			m_ctgTableTools.Header = Properties.Resources.ContextualTabGroup_TableTools; // Set Table Tools header

			// Connect all necessary handlers to act as a Table Tools group
			m_txTextControl.InputPositionChanged += TextControl_InputPositionChanged;
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
	    ** TextControl_InputPositionChanged Handler
	    ** Checks whether the input position is in a table and makes the table layout tab visible.
	    **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_InputPositionChanged(object sender, EventArgs e) {
			m_ctgTableTools.Visibility = m_txTextControl.Tables.GetItem() != null ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
		}

		/*-------------------------------------------------------------------------------------------------------------
	    ** TextControl_FrameSelected Handler
	    ** A frame has been selected. In this case make the frame layout tab visible.
	    **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_FrameSelected(object sender, FrameEventArgs e) {
			// Show the Frame Tools group
			m_ctgFrameTools.Visibility = System.Windows.Visibility.Visible;
		}

		/*-------------------------------------------------------------------------------------------------------------
	    ** TextControl_FrameDeselected Handler
	    ** Makes the frame layout tab invisible. When a new frame is selected, the FrameSelected event of the
	    ** new frame occurs before the FrameDeselected event of the old frame. Therefore it must be checked
	    ** whether a new frame is selected. When a drawing is activated, the tab must also remain visible.
	    **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_FrameDeselected(object sender, FrameEventArgs e) {
			if (m_txTextControl.Frames.GetItem() == null && m_txTextControl.Drawings.GetActivatedItem() == null) {
				// If no frame is selected and no drawing is activated, hide the Frame Tools group
				m_ctgFrameTools.Visibility = System.Windows.Visibility.Collapsed;
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
	    ** TextControl_DrawingActivated Handler
	    ** When a drawing is activated, the contained shapes can also be formatted with the RibbonFrameLayoutTab.
	    **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_DrawingActivated(object sender, DrawingEventArgs e) {
			// Show the Frame Tools group if the drawing is activated.
			m_ctgFrameTools.Visibility = System.Windows.Visibility.Visible;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** TextControl_DrawingDeactivated Handler
        ** Makes the frame layout tab invisible. When a frame is selected or another drawing is activated
        ** the tab must remain visible.
        **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_DrawingDeactivated(object sender, DrawingEventArgs e) {
			if (m_txTextControl.Frames.GetItem() == null && m_txTextControl.Drawings.GetActivatedItem() == null) {
				// Hide the Frame Tools group if the drawing is deactivated and no other frame is selected.
				m_ctgFrameTools.Visibility = System.Windows.Visibility.Collapsed;
			}
		}
	}
}
