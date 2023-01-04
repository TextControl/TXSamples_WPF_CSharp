/*-------------------------------------------------------------------------------------------------------------
** MainWindow_InsertMenuItem_DropDownOpening.cs File
**
** Description: Provides all SubmenuOpened handlers associated with 'Insert' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** Insert_SubmenuOpened Handler
		**
		** Updates the IsEnabled state of 'Insert' drop down menu items.
		** 
		** Item: 'Insert'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Insert_SubmenuOpened(object sender, RoutedEventArgs e) {
			bool bCanEdit = m_txTextControl.CanEdit;
			// 'File...'
			m_miInsert_File.IsEnabled = bCanEdit;

			// 'Image...'
			m_miInsert_Image.IsEnabled = bCanEdit;

			// 'Text Frame'
			m_miInsert_TextFrame.IsEnabled = bCanEdit;

			// 'Shape'	
			m_miInsert_Shape.IsEnabled = bCanEdit;

			// 'Barcode'
			m_miInsert_Barcode.IsEnabled = bCanEdit;

			if (m_plTXLicense >= VersionInfo.ProductLevel.Professional) {
				PageCollection colPages = m_txTextControl.GetPages();
				HeaderFooter hfHeaderFooter = m_txTextControl.TextParts.GetItem() as HeaderFooter;
				Page pgPage = colPages != null ? colPages.GetItem() : null;

				// 'Header'
				m_miInsert_Header_Insert.IsEnabled = colPages != null;
				m_miInsert_Header_Remove.IsEnabled = bCanEdit && pgPage != null && pgPage.Header != null;

				// 'Footer'
				m_miInsert_Footer_Insert.IsEnabled = colPages != null;
				m_miInsert_Footer_Remove.IsEnabled = bCanEdit && pgPage != null && pgPage.Footer != null;

				// 'Page Number'
				m_miInsert_PageNumber.IsEnabled = bCanEdit && hfHeaderFooter != null;

				if (hfHeaderFooter != null) {
					PageNumberField pnfPageNumberField = hfHeaderFooter.PageNumberFields.GetItem();
					m_miInsert_PageNumber_Insert.IsEnabled = pnfPageNumberField == null;
					m_miInsert_PageNumber_Delete.IsEnabled = pnfPageNumberField != null;
				}
			}

			if (m_plTXLicense >= VersionInfo.ProductLevel.Enterprise) {
				// 'Form Fields'
				m_miInsert_FormField_TextFormField.IsEnabled =
				m_miInsert_FormField_CheckBox.IsEnabled =
				m_miInsert_FormField_ComboBox.IsEnabled =
				m_miInsert_FormField_DropDownList.IsEnabled =
				m_miInsert_FormField_DateFormField.IsEnabled = bCanEdit && m_txTextControl.FormFields.CanAdd;
				m_miInsert_FormField_Delete.IsEnabled = bCanEdit && m_txTextControl.FormFields.GetItem() != null;
			}

			// 'Symbol'
			m_miInsert_Symbol.IsEnabled = bCanEdit;

			if (m_plTXLicense >= VersionInfo.ProductLevel.Professional) {
				// 'Hyperlink...'
				m_miInsert_Hyperlink.IsEnabled = bCanEdit && (m_txTextControl.HypertextLinks.CanAdd || m_txTextControl.DocumentLinks.CanAdd);

				// 'Bookmark...'
				DocumentTargetCollection colDocumentTargets = m_txTextControl.DocumentTargets;
				m_miInsert_Bookmark_Insert.IsEnabled = bCanEdit && colDocumentTargets.CanAdd;
				m_miInsert_Bookmark_Delete.IsEnabled = bCanEdit && colDocumentTargets.Count != 0;
				m_miInsert_Bookmark.IsEnabled = this.m_miInsert_Bookmark_Insert.IsEnabled || this.m_miInsert_Bookmark_Delete.IsEnabled;
			}

			if (m_plTXLicense >= VersionInfo.ProductLevel.Enterprise) {
				// 'Table of Contents'
				bool bInsideTOC = m_txTextControl.TablesOfContents.GetItem() != null;
				m_miInsert_TableOfContents_Insert.IsEnabled = bCanEdit && !bInsideTOC;
				m_miInsert_TableOfContents_Delete.IsEnabled =
				m_miInsert_TableOfContents_Update.IsEnabled = bCanEdit && bInsideTOC;
				m_miInsert_TableOfContents.IsEnabled = bCanEdit;
			}

			// 'Columns'
			m_miInsert_Columns_One.IsEnabled =
			m_miInsert_Columns_Two.IsEnabled =
			m_miInsert_Columns_MoreColumns.IsEnabled = bCanEdit;

			// 'Page Breaks'
			m_miInsert_PageBreaks.IsEnabled = bCanEdit;

			// 'Section Breaks'
			m_miInsert_SectionBreaks.IsEnabled = bCanEdit;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Insert_Columns_SubmenuOpened Handler
		**
		** Updates the checked state of 'Columns' drop down menu items.
		** 
		** Item: 'Columns'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Insert_Columns_SubmenuOpened(object sender, RoutedEventArgs e) {
			if (m_plTXLicense >= VersionInfo.ProductLevel.Professional) {
				// Get the number of columns
				Section secCurrentSection = m_txTextControl.Sections.GetItem();
				int iColumns = secCurrentSection != null ? secCurrentSection.Format.Columns : -1;
				// Check the items.
				m_miInsert_Columns_One.IsChecked = iColumns == 1;
				m_miInsert_Columns_Two.IsChecked = iColumns == 2;
				m_miInsert_Columns_MoreColumns.IsChecked = iColumns > 2;
			}
		}
	}
}
