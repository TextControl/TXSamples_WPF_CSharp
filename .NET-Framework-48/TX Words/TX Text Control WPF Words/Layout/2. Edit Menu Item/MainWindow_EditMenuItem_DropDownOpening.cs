/*-------------------------------------------------------------------------------------------------------------
** MainWindow_EditMenuItem_DropDownOpening.cs File
**
** Description: Provides all SubmenuOpened handlers associated with 'Edit' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** Edit_SubmenuOpened Handler
		**
		** Updates the IsEnabled state and texts of 'Edit' drop down menu items.
		** 
		** Item: 'Edit'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Edit_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Get some current TextControl states
			bool bCanCopy = m_txTextControl.CanCopy;
			bool bCanEdit = m_txTextControl.CanEdit;
			EditMode emEditMode = m_txTextControl.EditMode;

			// 'Undo'
			UpdateUndoText();
			m_miEdit_Undo.IsEnabled = m_txTextControl.CanUndo;

			// 'Redo'
			UpdateRedoText();
			m_miEdit_Redo.IsEnabled = m_txTextControl.CanRedo;

			// 'Cut'
			m_miEdit_Cut.IsEnabled = bCanCopy && bCanEdit;

			// 'Copy'
			m_miEdit_Copy.IsEnabled = bCanCopy;

			// 'Paste'
			m_miEdit_Paste.IsEnabled = m_txTextControl.CanPaste;

			// 'Select All'
			m_miEdit_SelectAll.IsEnabled = bCanEdit || emEditMode == EditMode.ReadAndSelect;

			// 'Replace...'
			m_miEdit_Replace.IsEnabled = bCanEdit;

			// 'Protect Document'
			m_miEdit_ProtectDocument.IsChecked = emEditMode == EditMode.ReadAndSelect || emEditMode == EditMode.ReadOnly;

			// 'Tracked Changes'
			m_miEdit_TrackedChanges.IsEnabled = bCanEdit;

			// 'Comments'
			m_miEdit_Comments.IsEnabled = bCanEdit;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Edit_Permissions_SubmenuOpened Handler
		**
		** Updates the IsEnabled and checked state of 'Permissions' drop down menu items.
		** 
		** Item: 'Permissions'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Edit_Permissions_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Get current document permissions
			DocumentPermissions dpDocumentPermissions = m_txTextControl.DocumentPermissions;

			// Check the 'Permissions' drop down items

			// Because formatting is not allowed in a read only document even if the corresponding document
			// permissions are set, the corresponding items are unchecked when the DocumentPermissions.ReadOnly
			// is set to true.
			bool bIsReadOnly = dpDocumentPermissions.ReadOnly;
			m_miEdit_Permissions_AllowFormatting.IsChecked = !bIsReadOnly && dpDocumentPermissions.AllowFormatting;
			m_miEdit_Permissions_AllowFormattingStyles.IsChecked = !bIsReadOnly && dpDocumentPermissions.AllowFormattingStyles;
			m_miEdit_Permissions_AllowPrinting.IsChecked = dpDocumentPermissions.AllowPrinting;
			m_miEdit_Permissions_AllowCopy.IsChecked = dpDocumentPermissions.AllowCopy;
			m_miEdit_Permissions_AllowEditingFormFields.IsChecked = dpDocumentPermissions.AllowEditingFormFields;
			m_miEdit_Permissions_ReadOnly.IsChecked = bIsReadOnly;

			// Set the enable states of the 'Permissions' drop down items
			bool bIsProtectedDocument = m_miEdit_ProtectDocument.IsChecked;
			m_miEdit_Permissions_AllowFormatting.IsEnabled =
			m_miEdit_Permissions_AllowFormattingStyles.IsEnabled = !bIsReadOnly && !bIsProtectedDocument;
			m_miEdit_Permissions_AllowPrinting.IsEnabled =
			m_miEdit_Permissions_AllowCopy.IsEnabled =
			m_miEdit_Permissions_AllowEditingFormFields.IsEnabled =
			m_miEdit_Permissions_ReadOnly.IsEnabled = !bIsProtectedDocument;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Edit_EditableRegions_Add_SubmenuOpened Handler
		**
		** Updates the IsEnabled state of 'Add' drop down menu items.
		** 
		** Item: 'Add' of the 'Editable Regions' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Edit_EditableRegions_Add_SubmenuOpened(object sender, RoutedEventArgs e) {
			if (m_plTXLicense >= VersionInfo.ProductLevel.Professional) {
				// Set the IsEnabled states of the 'Add' drop down items
				EditableRegionCollection colEditableRegions = m_txTextControl.EditableRegions;
				EditableRegion[] rerCurrentEditableRegions = colEditableRegions.GetItems();
				// The 'For [Current User]' item is IsEnabled if the current user is signed in and 
				// no editable region was defined for the current user at the input position.
				m_miEdit_EditableRegions_Add_ForCurrentUser.IsEnabled = m_uiCurrentUser != null && m_uiCurrentUser.IsSignedIn && (rerCurrentEditableRegions == null || GetEditableRegion(m_strUserName, rerCurrentEditableRegions) == null);
				// The 'For Everyone' item is IsEnabled if no corresponding editable region was
				// at the current input position.
				m_miEdit_EditableRegions_Add_ForEveryone.IsEnabled = rerCurrentEditableRegions == null || GetEditableRegion("", rerCurrentEditableRegions) == null;
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Edit_EditableRegions_Remove_SubmenuOpened Handler
		**
		** Updates the IsEnabled state of 'Remove' drop down menu items.
		** 
		** Item: 'Remove' of the 'Editable Regions' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Edit_EditableRegions_Remove_SubmenuOpened(object sender, RoutedEventArgs e) {
			if (m_plTXLicense >= VersionInfo.ProductLevel.Professional) {
				// Set the IsEnabled states of the 'Remove' drop down items
				EditableRegionCollection colEditableRegions = m_txTextControl.EditableRegions;
				EditableRegion[] rerCurrentEditableRegions = colEditableRegions.GetItems();
				// The 'For [Current User]' item is IsEnabled if the current user is signed in and 
				// an editable region was defined for the current user at the input position.
				m_miEdit_EditableRegions_Remove_ForCurrentUser.IsEnabled = m_uiCurrentUser != null && m_uiCurrentUser.IsSignedIn && rerCurrentEditableRegions != null && GetEditableRegion(m_strUserName, rerCurrentEditableRegions) != null;
				// The 'For Everyone' item is IsEnabled if a corresponding editable region was
				// at the current input position.
				m_miEdit_EditableRegions_Remove_ForEveryone.IsEnabled = rerCurrentEditableRegions != null && GetEditableRegion("", rerCurrentEditableRegions) != null;
			}
		}
	}
}
