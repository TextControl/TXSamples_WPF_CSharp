﻿/*-------------------------------------------------------------------------------------------------------------
** MainWindow_AppMenu_Save.cs File
**
** Description:
**      Handles saving the current document.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Windows;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/
		private StreamType m_stLastSavedType = StreamType.RichTextFormat; // The StreamType that was last used to load a document. If no document has been loaded so far, RichtTextFormat is used. 


		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** Save_Click Handler
        ** Invokes the Save method to save a document by saving it at the same location where it was loaded 
        ** before.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Save_Click(object sender, RoutedEventArgs e) {
			Save(m_strActiveDocumentPath);
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** SaveAs_Click Handler
        ** Invokes the Save method to save a document by using the internal TextControl 'Save' dialog.
        **-----------------------------------------------------------------------------------------------------------*/
		private void SaveAs_Click(object sender, RoutedEventArgs e) {
			Save(null);
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** Save Method
        ** Saves the current document as a file by using the TextControl Save dialog or using the path where the
        ** the active document was loaded from.
        **
        ** Parameters:
        **              savePath: The path where to save the active document. If that parameter is null  
        **                        or an empty string, the TextControl Save dialog is opened to save the 
        **                        document.
        **
        ** Return value:    False if the document was not saved. Otherwise true.
        **-----------------------------------------------------------------------------------------------------------*/
		private bool Save(string savePath) {
			// Create settings to determine some save parameters and get information about the document
			// when it is saved.
			SaveSettings svsSaveSettings = CreateSaveSettings();

			// Check whether a file path is specified where the document should be loaded.
			if (string.IsNullOrEmpty(savePath)) {
				// If no such path is determined, the TextControl Save dialog is opened. In that dialog 
				// all file formats can be chosen that are provided by the TXTextControl.StreamType enumeration.
				// Furthermore the DialogSettings EnterPassword, StylesheetOptions and SaveSelection are set.
				if (m_txTextControl.Save(StreamType.All, svsSaveSettings,
					SaveSettings.DialogSettings.EnterPassword |
					SaveSettings.DialogSettings.StylesheetOptions |
					SaveSettings.DialogSettings.SaveSelection) == WPF.DialogResult.Cancel) {
					return false;
				}
			}
			else {
				// Save the document at the same location (and with the same format) where it was loaded
				// before.
				svsSaveSettings.CssSaveMode = m_svCssSaveMode; // Set the stored css save mode.
				svsSaveSettings.CssFileName = m_strCssFileName;  // Set the stored css file name.
				svsSaveSettings.UserPassword = m_strUserPasswordPDF; // Set the stored user password.
				m_txTextControl.Save(m_strActiveDocumentPath, m_stActiveDocumentType, svsSaveSettings);
			}

			// The document is saved. Now:
			UpdateCurrentDocumentInfo(svsSaveSettings);  // Set information about the saved document.       
			AddRecentFile(m_strActiveDocumentPath); // Add the document to the recent files list.
			UpdateMainWindowCaption();  // Update the caption of the application's main window.
			UpdateSaveEnabledState(); // Update the enabled state of the Save button.
			return true;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** CreateSaveSettings Method
        ** Creates and returns an object of type TXTextControl.SaveSettings that is used to save a document and
        ** provide information about the document after it was saved.
        **
        ** Return value:    The created SaveSettings object.
        **-----------------------------------------------------------------------------------------------------------*/
		private SaveSettings CreateSaveSettings() {
			SaveSettings svsSaveSettings = new SaveSettings {
				LastModificationDate = DateTime.Now,
				ReportingMergeBlockFormat = ReportingMergeBlockFormat.SubTextParts,
				DefaultStreamType = m_stLastSavedType
			};
			return svsSaveSettings;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** UpdateCurrentDocumentInfo Method
        ** Updates some variables that provide information about the current active document. In this case 
        ** these information are updated by the save settings of the saved document.
        **
        ** Parameters:
        **              saveSettings:   The save settings that provide the information about the saved 
        **                              document.
        **-----------------------------------------------------------------------------------------------------------*/
		private void UpdateCurrentDocumentInfo(SaveSettings saveSettings) {
			m_strActiveDocumentPath = saveSettings.SavedFile;
			m_stActiveDocumentType = m_stLastSavedType = saveSettings.SavedStreamType;
			m_strUserPasswordPDF = saveSettings.UserPassword;
			m_strCssFileName = saveSettings.CssFileName;
			m_svCssSaveMode = saveSettings.CssSaveMode;
			m_bIsDirtyDocument = false;
			m_bIsUnknownDocument = false;
			m_strActiveDocumentName = Path.GetFileName(m_strActiveDocumentPath);
		}
	}
}
