/*-------------------------------------------------------------------------------------------------------------
** MainWindow_FormatMenuItem_Click.cs File
**
** Description: Provides all Click handlers associated with 'Format' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System;
using System.Windows;
using System.Windows.Controls;
using TXTextControl.WPF;
using TXTextControl.WPF.Drawing;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** Format_Character_Click Handler
		**
		** Invokes the built-in dialog box for setting fonts and character attributes.
		** 
		** Item: 'Character...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Character_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.FontDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_Paragraph_Click Handler
		**
		** Invokes the built-in dialog box for setting the formatting attributes of a paragraph.
		** 
		** Item: 'Paragraph...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Paragraph_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.ParagraphFormatDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_ParagraphStructureLevels_CurrentParagraphStyle_Level_Click Handler
		**
		** Sets the structure level of the specified paragraph style (see ParagraphStructureLevels_SubmenuOpened 
		** handler). The level is determined by the Tag property value of the clicked item.
		** 
		** Item: Each item of the 'Paragraph Style [Current Praragraph Style]' drop down menu ('Paragraph Style 
		**		 [Current Praragraph Style]' is an item of the 'Paragraph Structure Levels' drop down menu)
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_ParagraphStructureLevels_CurrentParagraphStyle_Level_Click(object sender, RoutedEventArgs e) {
			// Enforce license exception if paragraph styles are not supported by the current product level.
			if (m_plTXLicense < VersionInfo.ProductLevel.Enterprise) {
				m_txTextControl.ParagraphStyles.GetItem("[Normal]");
			}
			// Get the paragraph style.
			ParagraphStyle psParagraphStyle = m_miFormat_ParagraphStructureLevels_CurrentParagraphStyle.Tag as ParagraphStyle;
			// Set structure level to the paragraph style.
			psParagraphStyle.ParagraphFormat.StructureLevel = int.Parse((e.Source as MenuItem).Tag.ToString());
			// Apply the set level to the document.
			psParagraphStyle.Apply();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_ParagraphStructureLevels_AddToParagraph_Level_Click Handler
		**
		** Sets the structure level to the all selected paragraphs.
		** 
		** Item: Each item of the 'Add to Paragraph' drop down menu ('Add to Paragraph' is an item of the 
		**		 'Paragraph Structure Levels' drop down menu)
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_ParagraphStructureLevels_AddToParagraph_Level_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.InputFormat.StructureLevel = int.Parse((e.Source as MenuItem).Tag.ToString());
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_Styles_Click Handler
		**
		** Invokes the built-in dialog box for creating, deleting and modifying formatting styles.
		** 
		** Item: 'Styles...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Styles_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.FormattingStylesDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_ArabicNumbers_Click Handler
		**
		** Determines whether there is a numbered or structured list with arabic numbers at the current input position. 
		** 
		** Item: '1, 2, 3' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_ArabicNumbers_Click(object sender, RoutedEventArgs e) {
			SetNumberedList(m_miFormat_BulletsAndNumbering_ArabicNumbers.IsChecked, NumberFormat.ArabicNumbers);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_CapitalLetters_Click Handler
		**
		** Determines whether there is a numbered or structured list with capital letters at the current input position. 
		** 
		** Item: 'A, B, C' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_CapitalLetters_Click(object sender, RoutedEventArgs e) {
			SetNumberedList(m_miFormat_BulletsAndNumbering_CapitalLetters.IsChecked, NumberFormat.CapitalLetters);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_Letters_Click Handler
		**
		** Determines whether there is a numbered or structured list with small letters at the current input position. 
		** 
		** Item: 'a, b, c' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_Letters_Click(object sender, RoutedEventArgs e) {
			SetNumberedList(m_miFormat_BulletsAndNumbering_Letters.IsChecked, NumberFormat.Letters);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_RomanNumbers_Click Handler
		**
		** Determines whether there is a numbered or structured list with capital roman numbers at the current input 
		** position. 
		** 
		** Item: 'I, II, III, IV' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_RomanNumbers_Click(object sender, RoutedEventArgs e) {
			SetNumberedList(m_miFormat_BulletsAndNumbering_RomanNumbers.IsChecked, NumberFormat.RomanNumbers);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_SmallRomanNumbers_Click Handler
		**
		** Determines whether there is a numbered or structured list with small roman numbers at the current input 
		** position. 
		** 
		** Item: 'i, ii, iii, iv' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_SmallRomanNumbers_Click(object sender, RoutedEventArgs e) {
			SetNumberedList(m_miFormat_BulletsAndNumbering_SmallRomanNumbers.IsChecked, NumberFormat.SmallRomanNumbers);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_AsStructuredList_Click Handler
		**
		** Determines whether the current list is a numbered or a structured list.
		** 
		** Item: 'As structured List' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_AsStructuredList_Click(object sender, RoutedEventArgs e) {
			// Check whether a numbered list is currently set at the current input position.
			bool? bIsNumberedList = m_txTextControl.InputFormat.NumberedList;
			if (bIsNumberedList.HasValue && bIsNumberedList.Value) {
				// In this case change that list to a structured list.
				m_txTextControl.InputFormat.StructuredList = true;
				return;
			}
			// Check whether a structured list is currently set at the current input position.
			bool? bIsStructuredList = m_txTextControl.InputFormat.StructuredList;
			if (bIsStructuredList.HasValue && bIsStructuredList.Value) {
				// In this case change that list to a numbered list.
				m_txTextControl.InputFormat.NumberedList = true;
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_Bullets_Click Handler
		**
		** Determines whether the current list is a bulleted list.
		** 
		** Item: 'Bullets' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_Bullets_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.InputFormat.BulletedList = m_miFormat_BulletsAndNumbering_Bullets.IsChecked;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_IncreaseLevel_Click Handler
		**
		** Increases the list format level and the indent.
		** 
		** Item: 'Increase Level' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_IncreaseLevel_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.Selection.ListFormat.Level += 1;
			m_txTextControl.Selection.IncreaseIndent();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_DecreaseLevel_Click Handler
		**
		** Decreases the list format level and the indent.
		** 
		** Item: 'Decrease Level' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_DecreaseLevel_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.Selection.ListFormat.Level -= 1;
			m_txTextControl.Selection.DecreaseIndent();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_BulletsAndNumbering_Properties_Click Handler
		**
		** Invokes the built-in dialog box for setting formatting attributes of bulleted and numbered lists.
		** 
		** Item: 'Properties...' of the 'Bullets and Numbering' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_BulletsAndNumbering_Properties_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.ListFormatDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_Image_Click Handler
		**
		** Invokes the built-in dialog box for setting attributes of the selected image. 
		** 
		** Item: 'Image...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Image_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.ImageAttributesDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_TextFrame_Click Handler
		**
		** Invokes the built-in dialog box for setting attributes of the selected text frame.
		** 
		** Item: 'Text Frame...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_TextFrame_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.TextFrameAttributesDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_Shape_Click Handler
		**
		** Invokes the built-in dialog box for alter the layout settings, the size and the text distances of the 
		** selected drawing frame. If the drawing frame is activated, a dialog is opened to format the selected shapes 
		** of the drawing canvas.
		** 
		** Item: 'Shape...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Shape_Click(object sender, RoutedEventArgs e) {
			// Check whether a drawing frame is activated.
			DataVisualization.DrawingFrame dfDrawingFrame = m_txTextControl.Drawings.GetActivatedItem();
			if (dfDrawingFrame != null) {
				// In that case open the format shapes dialog.
				TXDrawingControl txdDrawingControl = dfDrawingFrame.Drawing as TXDrawingControl;
				txdDrawingControl.FormatShapesDialog();
			}
			else {
				// Otherwise the layout dialog for the selected drawing frame is opened.
				m_txTextControl.DrawingLayoutDialog();
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Frame_Barcode_Click Handler
		**
		** Invokes the built-in dialog box for alter the layout settings, the size and the text distances of the 
		** selected barcode. 
		** 
		** Item: 'Barcode...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Frame_Barcode_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.BarcodeLayoutDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Frame_HeadersAndFooters_Click Handler
		**
		** Invokes the second tab of the built-in section dialog for specifying headers and footers.
		** 
		** Item: 'Headers and Footers...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Frame_HeadersAndFooters_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.SectionFormatDialog(1);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_PageNumberField_Click Handler
		**
		** Opens a dialog box to alter the formatting and numbering attributes of the page number field.
		** 
		** Item: 'Page Number...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_PageNumberField_Click(object sender, RoutedEventArgs e) {
			HeaderFooter hfHeaderFooter = m_txTextControl.TextParts.GetItem() as HeaderFooter;
			PageNumberField pnfCurrentPageNumberField = hfHeaderFooter.PageNumberFields.GetItem();
			pnfCurrentPageNumberField.PageNumberDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_FormFields_Edit_Click Handler
		**
		** Opens a dialog box to format the current form field.
		** 
		** Item: 'Edit' of the 'Form Fields' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_FormFields_Edit_Click(object sender, RoutedEventArgs e) {
			FormField ffFormField = m_txTextControl.FormFields.GetItem();
			ffFormField.FormFieldDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_FormFields_EnableFormValidation_Click Handler
		**
		** Sets a value indicating whether Conditional Instructions are applied to form fields when the EditMode 
		** property is set to EditMode.ReadAndSelect and TextControl.DocumentPermissions.ReadOnly to true. 
		** 
		** Item: 'Enable Form Validation' of the 'Form Fields' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_FormFields_EnableFormValidation_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.IsFormFieldValidationEnabled = m_miFormat_FormFields_EnableFormValidation.IsChecked;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_FormFields_ManageConditionalInstructions_Click Handler
		**
		** Opens a dialog box to add, edit or delete Conditional Instructions inside the document.
		** 
		** Item: 'Manage Conditional Instructions...' of the 'Form Fields' drop down menu
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_FormFields_ManageConditionalInstructions_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.ManageConditionalInstructionsDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_Hyperlink_Click Handler
		**
		** Opens a built-in dialog box for editing the HypertextLink or DocumentLink at the current text input 
		** position.
		** 
		** Item: 'Hyperlink...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Hyperlink_Click(object sender, RoutedEventArgs e) {
			HyperlinkDialog dlgHyperlink = new HyperlinkDialog(m_txTextControl) { Owner = this };
			dlgHyperlink.ShowDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_Bookmark_Click Handler
		**
		** Opens a built-in dialog box for editing the DocumentTarget at the current text input position.
		** 
		** Item: 'Bookmark...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Bookmark_Click(object sender, RoutedEventArgs e) {
			BookmarkDialog dlgEditBookmark = new BookmarkDialog(m_txTextControl, m_txTextControl.DocumentTargets.GetItem()) { Owner = this };
			dlgEditBookmark.ShowDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_TableOfContents_Click Handler
		**
		** Invokes the built-in dialog box for formatting a table of contents. 
		** 
		** Item: 'Table of Contents...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_TableOfContents_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.TableOfContentsDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_Columns_Click Handler
		**
		** Invokes the third tab of the built-in section dialog for specifying the attributes of the page columns.
		** 
		** Item: 'Columns...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Columns_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.SectionFormatDialog(2);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_PageBorders_Click Handler
		**
		** Invokes the fourth tab of the built-in section dialog for specifying page borders
		** 
		** Item: 'Page Borders...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_PageBorders_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.SectionFormatDialog(3);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_PageColor_Click Handler
		**
		** Invokes the built-in dialog box for setting the page color.
		** 
		** Item: 'Page Color...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_PageColor_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.PageColorDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_Tabs_Click Handler
		**
		** Invokes the built-in dialog box for setting tabs.
		** 
		** Item: 'Tabs...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Tabs_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.TabDialog();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Format_Language_Click Handler
		**
		** Invokes the built-in dialog box for setting the language of the selected text.
		** 
		** Item: 'Language...'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Format_Language_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.LanguageDialog();
		}
	}
}
