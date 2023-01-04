/*-------------------------------------------------------------------------------------------------------------
** MainWindow_FormatMenuItem.cs File
**
** Description: Provides methods to set the layout of the 'Format' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows.Controls;
using TXTextControl.WPF;

namespace TXTextControl.Words {
	partial class MainWindow {

		/*-------------------------------------------------------------------------------------------------------------
		** M E T H O D S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** SetFormatItemsTexts Method
		**
		** Sets the texts of the 'Format' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetFormatItemsTexts() {
			// 'Paragraph Structure Levels'
			// Set texts of the 'Paragraph Style: [Current Paragraph Style]' item's 'Level' drop down items.
			for (int i = 1; i < m_miFormat_ParagraphStructureLevels_CurrentParagraphStyle.Items.Count; i++) {
				// Get item.
				MenuItem miLevel = m_miFormat_ParagraphStructureLevels_CurrentParagraphStyle.Items[i] as MenuItem;
				// Create accelerator string.
				string strLevel = i < 10 ? "_" + i : "1_0";
				// Set text.
				SetItemText(miLevel, Properties.Resources.Item_Format_ParagraphStructureLevels_CurrentParagraphStyle_Level_Text, strLevel);
			}

			// Set texts of the 'Add to Paragraph' item's 'Level' drop down items.
			for (int i = 1; i < m_miFormat_ParagraphStructureLevels_AddToParagraph.Items.Count; i++) {
				// Get item.
				MenuItem miLevel = m_miFormat_ParagraphStructureLevels_AddToParagraph.Items[i] as MenuItem;
				// Create accelerator string.
				string strLevel = i < 10 ? "_" + i : "1_0";
				// Set text.
				SetItemText(miLevel, Properties.Resources.Item_Format_ParagraphStructureLevels_AddToParagraph_Level_Text, strLevel);
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** SetFormatItemsImages Method
		**
		** Sets the images of the 'Format' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetFormatItemsImages() {
			// 'Character...'
			SetItemImage(m_miFormat_Character, RibbonFormattingTab.RibbonItem.TXITEM_ChangeCase.ToString());

			// 'Paragraph...'
			SetItemImage(m_miFormat_Paragraph, RibbonFormattingTab.RibbonItem.TXITEM_ControlChars.ToString());

			// 'Styles...'
			SetItemImage(m_miFormat_Styles, RibbonFormattingTab.RibbonItem.TXITEM_StyleName.ToString());

			// 'Paragraph Structure Levels'
			SetItemImage(m_miFormat_ParagraphStructureLevels, RibbonReferencesTab.RibbonItem.TXITEM_TOCMinimumStructureLevel.ToString());
			SetItemImage(m_miFormat_ParagraphStructureLevels_CurrentParagraphStyle, RibbonFormattingTab.RibbonItem.TXITEM_StyleName.ToString());
			SetItemImage(m_miFormat_ParagraphStructureLevels_AddToParagraph, RibbonFormattingTab.RibbonItem.TXITEM_ControlChars.ToString());

			// 'Bullets and Numbering'
			SetItemImage(m_miFormat_BulletsAndNumbering, RibbonFormattingTab.RibbonItem.TXITEM_NumberedList.ToString());
			SetItemImage(m_miFormat_BulletsAndNumbering_AsStructuredList, RibbonFormattingTab.RibbonItem.TXITEM_StructuredList.ToString());
			SetItemImage(m_miFormat_BulletsAndNumbering_Bullets, RibbonFormattingTab.RibbonItem.TXITEM_BulletedList.ToString());
			SetItemImage(m_miFormat_BulletsAndNumbering_IncreaseLevel, RibbonFormattingTab.RibbonItem.TXITEM_IncreaseIndent.ToString());
			SetItemImage(m_miFormat_BulletsAndNumbering_DecreaseLevel, RibbonFormattingTab.RibbonItem.TXITEM_DecreaseIndent.ToString());
			SetItemImage(m_miFormat_BulletsAndNumbering_Properties, RibbonFormattingTab.RibbonItem.TXITEM_NumberedList.ToString());

			// 'Image...'
			SetItemImage(m_miFormat_Image, RibbonInsertTab.RibbonItem.TXITEM_InsertImage.ToString());

			// 'Text Frame...'
			SetItemImage(m_miFormat_TextFrame, RibbonInsertTab.RibbonItem.TXITEM_InsertTextFrame.ToString());

			// 'Shape...'
			SetItemImage(m_miFormat_Shape, RibbonInsertTab.RibbonItem.TXITEM_InsertShape.ToString());

			// 'Barcode...'
			SetItemImage(m_miFormat_Barcode, RibbonInsertTab.RibbonItem.TXITEM_InsertBarcode.ToString());

			// 'Headers and Footers...'
			SetItemImage(m_miFormat_HeadersAndFooters, RibbonInsertTab.RibbonItem.TXITEM_InsertHeader.ToString());

			// 'Page Number...'
			SetItemImage(m_miFormat_PageNumberField, RibbonInsertTab.RibbonItem.TXITEM_InsertPageNumber.ToString());

			// 'Form Fields'
			SetItemImage(m_miFormat_FormFields, RibbonFormFieldsTab.RibbonItem.TXITEM_InsertComboBoxField.ToString());
			SetItemImage(m_miFormat_FormFields_Edit, RibbonFormFieldsTab.RibbonItem.TXITEM_InsertComboBoxField.ToString());
			SetItemImage(m_miFormat_FormFields_EnableFormValidation, RibbonFormFieldsTab.RibbonItem.TXITEM_EnableFormValidation.ToString());
			SetItemImage(m_miFormat_FormFields_ManageConditionalInstructions, RibbonFormFieldsTab.RibbonItem.TXITEM_ManageConditionalInstructions.ToString());

			// 'Hyperlink...'
			SetItemImage(m_miFormat_Hyperlink, RibbonInsertTab.RibbonItem.TXITEM_InsertHyperlink.ToString());

			// 'Bookmark...'
			SetItemImage(m_miFormat_Bookmark, RibbonInsertTab.RibbonItem.TXITEM_InsertBookmark.ToString());

			// 'Table of Contents...'
			SetItemImage(m_miFormat_TableOfContents, RibbonReferencesTab.RibbonItem.TXITEM_ModifyTableOfContents.ToString());

			// 'Columns...'
			SetItemImage(m_miFormat_Columns, RibbonPageLayoutTab.RibbonItem.TXITEM_Columns.ToString());

			// 'Page Borders...'
			SetItemImage(m_miFormat_PageBorders, RibbonPageLayoutTab.RibbonItem.TXITEM_PageBorders.ToString());

			// 'Page Color...'
			SetItemImage(m_miFormat_PageColor, RibbonPageLayoutTab.RibbonItem.TXITEM_PageColor.ToString());

			// 'Tabs...'
			SetItemImage(m_miFormat_Tabs, RibbonFormattingTab.RibbonItem.TXITEM_EditTabs.ToString());

			// 'Language...'
			SetItemImage(m_miFormat_Language, RibbonProofingTab.RibbonItem.TXITEM_SetLanguage.ToString());
		}
	}
}
