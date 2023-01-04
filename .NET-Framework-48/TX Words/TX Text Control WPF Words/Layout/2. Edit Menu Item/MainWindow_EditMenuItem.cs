/*-------------------------------------------------------------------------------------------------------------
** MainWindow_EditMenuItem.cs File
**
** Description: Provides methods to set the layout of the 'Edit' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using TXTextControl.WPF;
namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** SetEditItemsTexts Method
		**
		** Sets the texts of the 'Edit' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetEditItemsTexts() {
			// 'Editable Regions'
			SetItemText(m_miEdit_EditableRegions_Add_ForCurrentUser, m_strUserName);
			SetItemText(m_miEdit_EditableRegions_Remove_ForCurrentUser, m_strUserName);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** SetEditItemsImages Method
		**
		** Sets the images of the 'Edit' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetEditItemsImages() {
			// 'Undo'
			SetItemImage(m_miEdit_Undo, ResourceProvider.GeneralItem.TXITEM_Undo.ToString());

			// 'Redo'
			SetItemImage(m_miEdit_Redo, ResourceProvider.GeneralItem.TXITEM_Redo.ToString());

			// 'Cut'
			SetItemImage(m_miEdit_Cut, RibbonFormattingTab.RibbonItem.TXITEM_Cut.ToString());

			// 'Copy'
			SetItemImage(m_miEdit_Copy, RibbonFormattingTab.RibbonItem.TXITEM_Copy.ToString());

			// 'Paste'
			SetItemImage(m_miEdit_Paste,RibbonFormattingTab.RibbonItem.TXITEM_Paste.ToString());

			// 'Select All'
			SetItemImage(m_miEdit_SelectAll, RibbonFormattingTab.RibbonItem.TXITEM_SelectAll.ToString());

			// 'Find...'
			SetItemImage(m_miEdit_Find, RibbonFormattingTab.RibbonItem.TXITEM_Find.ToString());

			// 'Replace...'
			SetItemImage(m_miEdit_Replace, RibbonFormattingTab.RibbonItem.TXITEM_Replace.ToString());

			// 'Permissions'
			SetItemImage(m_miEdit_Permissions, RibbonPermissionsTab.RibbonItem.TXITEM_ReadOnly.ToString());
			SetItemImage(m_miEdit_Permissions_AllowFormatting, RibbonPermissionsTab.RibbonItem.TXITEM_AllowFormatting.ToString());
			SetItemImage(m_miEdit_Permissions_AllowFormattingStyles, RibbonPermissionsTab.RibbonItem.TXITEM_AllowFormattingStyles.ToString());
			SetItemImage(m_miEdit_Permissions_AllowPrinting, RibbonPermissionsTab.RibbonItem.TXITEM_AllowPrinting.ToString());
			SetItemImage(m_miEdit_Permissions_AllowCopy, RibbonPermissionsTab.RibbonItem.TXITEM_AllowCopy.ToString());
			SetItemImage(m_miEdit_Permissions_AllowEditingFormFields, RibbonPermissionsTab.RibbonItem.TXITEM_FillInFormFields.ToString());
			SetItemImage(m_miEdit_Permissions_ReadOnly, RibbonPermissionsTab.RibbonItem.TXITEM_ReadOnly.ToString());

			// 'Editable Regions'
			SetItemImage(m_miEdit_EditableRegions, RibbonPermissionsTab.RibbonItem.TXITEM_HighlightEditableRegions.ToString());

			// 'Protect Document'
			SetItemImage(m_miEdit_ProtectDocument, RibbonPermissionsTab.RibbonItem.TXITEM_EnforceProtection.ToString());

			// 'Tracked Changes'
			SetItemImage(m_miEdit_TrackedChanges, RibbonProofingTab.RibbonItem.TXITEM_TrackChanges.ToString());
			SetItemImage(m_miEdit_TrackedChanges_TrackChanges, RibbonProofingTab.RibbonItem.TXITEM_TrackChanges.ToString());
			SetItemImage(m_miEdit_TrackedChanges_ReviewTrackedChanges, RibbonProofingTab.RibbonItem.TXITEM_TrackedChanges.ToString());

			// 'Comments'
			SetItemImage(m_miEdit_Comments, RibbonProofingTab.RibbonItem.TXITEM_EditComment.ToString());
			SetItemImage(m_miEdit_Comments_AddComment, RibbonProofingTab.RibbonItem.TXITEM_AddComment.ToString());
			SetItemImage(m_miEdit_Comments_ReviewComments, RibbonProofingTab.RibbonItem.TXITEM_Comments_Sidebars.ToString());
		}
	}
}
