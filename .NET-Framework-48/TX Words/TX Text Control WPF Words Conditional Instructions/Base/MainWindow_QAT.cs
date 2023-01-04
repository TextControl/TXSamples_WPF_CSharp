/*-------------------------------------------------------------------------------------------------------------
** MainWindow_QAT.cs File
**
** Description:
**      Creates an undo and a redo button and adds these button plus references to the [current user], Save,  
**      Open, New and Print buttons to the Quick Access Toolbar.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using TXTextControl.WPF;

namespace TXTextControl.Words {
	partial class MainWindow {

        /*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

        /*-------------------------------------------------------------------------------------------------------------
        ** SetQATItemsDesign Method
        ** Creates an undo and a redo button and adds these button plus references to the [current user], Save, Open, 
        ** New and Print buttons to the Quick Access Toolbar.
        **-----------------------------------------------------------------------------------------------------------*/
        private void SetQATItemsDesign() {
            SetQATItemDesign(ResourceProvider.FileMenuItem.TXITEM_Save.ToString(), m_rbtnSaveQAT,"1");
            SetQATItemDesign(ResourceProvider.FileMenuItem.TXITEM_Open.ToString(), m_rbtnOpenQAT, "2");
            SetQATItemDesign(ResourceProvider.FileMenuItem.TXITEM_New.ToString(), m_rbtnNewQAT, "3");
            SetQATItemDesign(ResourceProvider.GeneralItem.TXITEM_Undo.ToString(), m_rbtnUndoQAT, "4");
            SetQATItemDesign(ResourceProvider.GeneralItem.TXITEM_Redo.ToString(), m_rbtnRedoQAT, "5");
            SetQATItemDesign(ResourceProvider.FileMenuItem.TXITEM_Print.ToString(), m_rbtnPrintQAT, "6");
        }

        /*-------------------------------------------------------------------------------------------------------
        ** SetQATItemDesign Method
        ** Sets the icons, text, key tip and tool tip for a specific QAT RibbonButton.
        **
        ** Parameters:
        **      resourceID:     The id that is used to identify the corresponding texts and icons.
        **      menuItem:		The ribbon menu item to update.
        **      keyTip:         The key tip to set.
        **-----------------------------------------------------------------------------------------------------*/
        private void SetQATItemDesign(string resourceID, RibbonButton menuItem, string keyTip) {
            menuItem.Name = resourceID;
            menuItem.SmallImageSource = ResourceProvider.GetSmallIcon(resourceID, this);
            menuItem.LargeImageSource = ResourceProvider.GetLargeIcon(resourceID, this);
            menuItem.KeyTip = keyTip;

            menuItem.Label = ResourceProvider.GetText(resourceID);
            menuItem.ToolTipTitle = ResourceProvider.GetToolTipTitle(resourceID);
            menuItem.ToolTipDescription = ResourceProvider.GetToolTipDescription(resourceID);
        }


        /*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

        /*-------------------------------------------------------------------------------------------------------------
        ** Undo_Click Handler
        ** Invokes the TextControl Undo method to undo the last action.
        **-----------------------------------------------------------------------------------------------------------*/
        private void Undo_Click(object sender, RoutedEventArgs e) {
            m_txTextControl.Undo();
        }

        /*-------------------------------------------------------------------------------------------------------------
        ** Undo_ToolTip_Opening Handler
        ** Sets the tool tip of the Undo button when the tool tip is opening. The tool tip shows the undo action
        ** that is performed when the Undo button is clicked.
        **-----------------------------------------------------------------------------------------------------------*/
        private void Undo_ToolTip_Opening(object sender, ToolTipEventArgs e) {
            string strUndoActionName = m_txTextControl.UndoActionName;
            m_rbtnUndoQAT.ToolTipDescription = !string.IsNullOrEmpty(strUndoActionName) ?
                strUndoActionName :
                ResourceProvider.GetToolTipDescription(ResourceProvider.GeneralItem.TXITEM_Undo.ToString());
        }

        /*-------------------------------------------------------------------------------------------------------------
        ** Redo_Click Handler
        ** Invokes the TextControl Redo method to redo the last action.
        **-----------------------------------------------------------------------------------------------------------*/
        private void Redo_Click(object sender, RoutedEventArgs e) {
            m_txTextControl.Redo();
        }

        /*-------------------------------------------------------------------------------------------------------------
        ** Redo_ToolTip_Opening Handler
        ** Sets the tool tip of the Redo button when the tool tip is opening. The tool tip shows the redo action
        ** that is performed when the Redo button is clicked.
        **-----------------------------------------------------------------------------------------------------------*/
        private void Redo_ToolTip_Opening(object sender, ToolTipEventArgs e) {
            string strRedoActionName = m_txTextControl.RedoActionName;
            m_rbtnRedoQAT.ToolTipDescription = !string.IsNullOrEmpty(strRedoActionName) ?
                strRedoActionName :
                ResourceProvider.GetToolTipDescription(ResourceProvider.GeneralItem.TXITEM_Redo.ToString());
        }
    }
}
