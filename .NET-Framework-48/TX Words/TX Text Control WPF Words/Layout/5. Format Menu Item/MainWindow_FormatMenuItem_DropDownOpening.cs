/*-------------------------------------------------------------------------------------------------------------
** MainWindow_FormatMenuItem_DropDownOpening.cs File
**
** Description: Provides all SubmenuOpened handlers associated with 'Format' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;
using System.Windows.Controls;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** Format_SubmenuOpened Handler
		**
		** Updates the IsEnabled state of 'Format' drop down menu items.
		** 
		** Item: 'Format'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_SubmenuOpened(object sender, RoutedEventArgs e) {
			bool bCanEdit = m_txTextControl.CanEdit;
			FrameBase fbFrame = bCanEdit ? m_txTextControl.Frames.GetItem() : null;

			// 'Character...', 
			m_miFormat_Character.IsEnabled = m_txTextControl.CanCharacterFormat;

			// 'Paragraph...'
			m_miFormat_Paragraph.IsEnabled = m_txTextControl.CanParagraphFormat;

			// 'Styles...'
			m_miFormat_Styles.IsEnabled = m_txTextControl.CanStyleFormat;

			// 'Image...'
			m_miFormat_Image.IsEnabled = fbFrame is Image;

			// 'Text Frame...'
			m_miFormat_TextFrame.IsEnabled = fbFrame is TextFrame;

			if (m_plTXLicense >= VersionInfo.ProductLevel.Professional) {
				// 'Shape...'
				m_miFormat_Shape.IsEnabled = EnableShapeItem(fbFrame);
			}

			// 'Barcode...'
			m_miFormat_Barcode.IsEnabled = fbFrame is DataVisualization.BarcodeFrame;

			// 'Headers and Footers...'
			m_miFormat_HeadersAndFooters.IsEnabled = bCanEdit;

			if (m_plTXLicense >= VersionInfo.ProductLevel.Professional) {
				// 'Page Number...'
				HeaderFooter hfHeaderFooter = m_txTextControl.TextParts.GetItem() as HeaderFooter;
				m_miFormat_PageNumberField.IsEnabled = bCanEdit && hfHeaderFooter != null && hfHeaderFooter.PageNumberFields.GetItem() != null;
			}

			if (m_plTXLicense >= VersionInfo.ProductLevel.Professional) {
				// 'Hyperlink...'
				HypertextLinkCollection colHyperTextLinks = m_txTextControl.HypertextLinks;
				m_miFormat_Hyperlink.IsEnabled = bCanEdit && (colHyperTextLinks.GetItem() != null || m_txTextControl.DocumentLinks.GetItem() != null);

				// 'Bookmark...'
				DocumentTargetCollection colDocumentTargets = m_txTextControl.DocumentTargets;
				m_miFormat_Bookmark.IsEnabled = bCanEdit && colDocumentTargets.GetItem() != null;
			}

			if (m_plTXLicense >= VersionInfo.ProductLevel.Enterprise) {
				// 'Table of Contents...'
				bool bInsideTOC = m_txTextControl.TablesOfContents.GetItem() != null;
				m_miFormat_TableOfContents.IsEnabled = bCanEdit && bInsideTOC;
			}

			// 'Columns...'
			m_miFormat_Columns.IsEnabled = bCanEdit;

			// 'Page Borders...'
			m_miFormat_PageBorders.IsEnabled = bCanEdit;

			// 'Page Color...'
			m_miFormat_PageColor.IsEnabled = bCanEdit;

			// 'Tabs...'
			m_miFormat_Tabs.IsEnabled = bCanEdit;

			// 'Language...'
			m_miFormat_Language.IsEnabled = bCanEdit;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_ParagraphStructureLevels_SubmenuOpened Handler
		**
		** Updates the text of the '[Current Paragraph Style]' item.
		** 
		** Item: 'Paragraph Structure Levels'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_ParagraphStructureLevels_SubmenuOpened(object sender, RoutedEventArgs e) {
			if (m_plTXLicense >= VersionInfo.ProductLevel.Enterprise) {
				// Get the current style name
				string strStyleName = m_txTextControl.InputFormat.StyleName;

				// Determine current paragraph style
				ParagraphStyle psCurrentStyle = m_txTextControl.ParagraphStyles.GetItem(strStyleName);

				// If no paragraph style could be determined, use the default "[Normal]" style.
				if (psCurrentStyle == null) {
					strStyleName = "[Normal]";
					psCurrentStyle = m_txTextControl.ParagraphStyles.GetItem(strStyleName);
				}

				// Provide the paragraph style by using the item's Tag property.
				m_miFormat_ParagraphStructureLevels_CurrentParagraphStyle.Tag = psCurrentStyle;

				// Display the paragraph style name as item text.
				m_miFormat_ParagraphStructureLevels_CurrentParagraphStyle.Header = string.Format(Properties.Resources.Item_Format_ParagraphStructureLevels_CurrentParagraphStyle_Text, strStyleName);
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_ParagraphStructureLevels_CurrentParagraphStyle_SubmenuOpened Handler
		**
		** Updates the checked and IsEnabled state of the '[Current Paragraph Style]' drop down menu items.
		** 
		** Item: '[Current Paragraph Style]' of the 'Paragraph Structure Levels' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_ParagraphStructureLevels_CurrentParagraphStyle_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Get the corresponding paragraph style.
			ParagraphStyle psParagraphStyle = m_miFormat_ParagraphStructureLevels_CurrentParagraphStyle.Tag as ParagraphStyle;
			if (psParagraphStyle != null) {
				// Get name and structure level of that style.
				string strStyleName = psParagraphStyle.Name;
				int iStructureLevel = psParagraphStyle.ParagraphFormat.StructureLevel;

				// The strucure levels of the table of contents styles ("TOC_Title" and "TOC_Level") cannot be edited.
				bool bCanEdit = m_txTextControl.CanEdit && !(strStyleName == "TOC_Title" || strStyleName.StartsWith("TOC_Level"));

				// Step through all structure level drop down items and handle their IsEnabled and Check properties.
				foreach (MenuItem item in m_miFormat_ParagraphStructureLevels_CurrentParagraphStyle.Items) {
					item.IsEnabled = bCanEdit;
					item.IsChecked = int.Parse(item.Tag.ToString()) == iStructureLevel;
				}
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_ParagraphStructureLevels_AddToParagraph_SubmenuOpened Handler
		**
		** Updates the checked and IsEnabled state of the 'Add to Paragaph' drop down menu items.
		** 
		** Item: 'Add to Paragaph' of the 'Paragraph Structure Levels' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_ParagraphStructureLevels_AddToParagraph_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Check whether the items should be IsEnabled.
			bool bCanEdit = m_txTextControl.CanEdit;

			// Get the current paragraph's structure level.
			int? iStructureLevel = m_txTextControl.InputFormat.StructureLevel;
			iStructureLevel = iStructureLevel.HasValue ? iStructureLevel.Value : -1;

			// Step through all structure level drop down items and handle their IsEnabled and Check properties.
			foreach (MenuItem item in m_miFormat_ParagraphStructureLevels_AddToParagraph.Items) {
				item.IsEnabled = bCanEdit;
				item.IsChecked = int.Parse(item.Tag.ToString()) == iStructureLevel;
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_SubmenuOpened Handler
		**
		** Updates the IsEnabled and checked state of 'Bullets and Numbering' drop down menu items.
		** 
		** Item: 'Bullets and Numbering'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Get list format
			ListFormat lfListFormat = m_txTextControl.Selection.ListFormat;

			// Check list format type
			bool bIsList = lfListFormat.Type != ListType.None;
			bool bIsBulleted = lfListFormat.Type == ListType.Bulleted;
			bool bIsStructured = lfListFormat.Type == ListType.Structured;

			// Get number format
			NumberFormat fnNumberFormat = lfListFormat.NumberFormat;
			bool bCanCharacterFormat = m_txTextControl.CanCharacterFormat;

			// Set items IsEnabled states
			m_miFormat_BulletsAndNumbering_ArabicNumbers.IsEnabled =                     // '1, 2, 3'		
			m_miFormat_BulletsAndNumbering_CapitalLetters.IsEnabled =                    // 'A, B, C'
			m_miFormat_BulletsAndNumbering_Letters.IsEnabled =                           // 'a, b, c'
			m_miFormat_BulletsAndNumbering_RomanNumbers.IsEnabled =                      // 'I, II, III, IV'
			m_miFormat_BulletsAndNumbering_SmallRomanNumbers.IsEnabled =                 // 'i, ii, iii, iv'
			m_miFormat_BulletsAndNumbering_AsStructuredList.IsEnabled =                  // 'As structured List'
			m_miFormat_BulletsAndNumbering_Bullets.IsEnabled = bCanCharacterFormat;      // 'Bullets'

			// 'Increase Level'
			m_miFormat_BulletsAndNumbering_IncreaseLevel.IsEnabled = bIsList && bCanCharacterFormat;

			// 'Decrease Level'
			m_miFormat_BulletsAndNumbering_DecreaseLevel.IsEnabled = bIsList && bCanCharacterFormat && m_txTextControl.Selection.ListFormat.Level >= 2;

			// 'Properties...'
			m_miFormat_BulletsAndNumbering_Properties.IsEnabled = bCanCharacterFormat;

			// Set items IsChecked states
			m_miFormat_BulletsAndNumbering_ArabicNumbers.IsChecked = bIsList && !bIsBulleted && fnNumberFormat == NumberFormat.ArabicNumbers;            // '1, 2, 3'
			m_miFormat_BulletsAndNumbering_CapitalLetters.IsChecked = bIsList && !bIsBulleted && fnNumberFormat == NumberFormat.CapitalLetters;          // 'A, B, C'
			m_miFormat_BulletsAndNumbering_Letters.IsChecked = bIsList && !bIsBulleted && fnNumberFormat == NumberFormat.Letters;                        // 'a, b, c'
			m_miFormat_BulletsAndNumbering_RomanNumbers.IsChecked = bIsList && !bIsBulleted && fnNumberFormat == NumberFormat.RomanNumbers;              // 'I, II, III, IV'
			m_miFormat_BulletsAndNumbering_SmallRomanNumbers.IsChecked = bIsList && !bIsBulleted && fnNumberFormat == NumberFormat.SmallRomanNumbers;    // 'i, ii, iii, iv'
			m_miFormat_BulletsAndNumbering_AsStructuredList.IsChecked = bIsStructured;   // 'As structured List'
			m_miFormat_BulletsAndNumbering_Bullets.IsChecked = bIsBulleted;              // 'Bullets'
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_FormFields_SubmenuOpened Handler
		**
		** Updates the IsEnabled and checked state of 'Form Fields' drop down menu items.
		** 
		** Item: 'Form Fields'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_FormFields_SubmenuOpened(object sender, RoutedEventArgs e) {
			if (m_plTXLicense >= VersionInfo.ProductLevel.Enterprise) {
				bool bCanEdit = m_txTextControl.CanEdit;
				// 'Form Fields...'
				FormFieldCollection colFormFields = m_txTextControl.FormFields;
				m_miFormat_FormFields_Edit.IsEnabled = bCanEdit && colFormFields.GetItem() != null;

				// 'Form Validation'
				m_miFormat_FormFields_EnableFormValidation.IsEnabled = bCanEdit && colFormFields.Count > 0;
				m_miFormat_FormFields_EnableFormValidation.IsChecked =
				m_miFormat_FormFields_ManageConditionalInstructions.IsEnabled = m_txTextControl.IsFormFieldValidationEnabled;
			}
		}
	}
}
