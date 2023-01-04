/*-------------------------------------------------------------------------------------------------------------
** MainWindow_DragAndDrop.cs File
**
** Description:
**     Handles the drag and drop behavior.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.IO;
using System.Windows;

namespace TXTextControl.Words {
	partial class MainWindow {

		/*-------------------------------------------------------------------------------------------------------------
		** E N U M S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** FileType Enum
        ** Two different file types can be handled by TX Text Control: documents and images
        **-----------------------------------------------------------------------------------------------------------*/
		internal enum FileType { Document, Image }


		/*-------------------------------------------------------------------------------------------------------------
		** S U B C L A S S E S 
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** DraggedFileInfo Class
        ** Stores and provides information about the dragged file.
        **-----------------------------------------------------------------------------------------------------------*/
		internal class DraggedFileInfo {
			// Member variables
			private readonly string m_strFilePath;
			private readonly FileType m_ftFileType;
			private readonly StreamType m_stStreamType;

			// Constructors
			internal DraggedFileInfo(string filePath) {
				m_strFilePath = filePath;
				// If no StreamType is set as parameter, the dragged file is an image.
				m_ftFileType = FileType.Image;
			}

			internal DraggedFileInfo(string filePath, StreamType streamType) {
				m_strFilePath = filePath;
				m_stStreamType = streamType;
				// The specified StreamType indicates that the dragged file is a document.
				m_ftFileType = FileType.Document;
			}

			// Properties

			/*-------------------------------------------------------------------------------------------------------
            ** FilePath Property
            ** Returns the file path of the dragged file.
            **-----------------------------------------------------------------------------------------------------*/
			internal string FilePath { get { return m_strFilePath; } }

			/*-------------------------------------------------------------------------------------------------------
            ** FileType Property
            ** Returns the type of the dragged file.
            **-----------------------------------------------------------------------------------------------------*/
			internal FileType FileType { get { return m_ftFileType; } }

			/*-------------------------------------------------------------------------------------------------------
            ** StreamType Property
            ** If the dragged file is a document, this property returns the corresponding StreamType.
            **-----------------------------------------------------------------------------------------------------*/
			internal StreamType StreamType { get { return m_stStreamType; } }
		}


		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/

		private DraggedFileInfo m_dfiFileInfo = null;


		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** TextControl_DragEnter Handler
        ** Gets information about the dragged file.
        **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_DragEnter(object sender, DragEventArgs e) {
			m_dfiFileInfo = CheckDraggedFiles((string[])e.Data.GetData(DataFormats.FileDrop));
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** TextControl_DragOver Handler
        ** Specifies the target drop effect for the drag & drop operation.
        **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_DragOver(object sender, DragEventArgs e) {

			if ((e.AllowedEffects & DragDropEffects.Copy) == DragDropEffects.Copy) {
				e.Effects = DragDropEffects.Copy;
			}
			else if ((e.AllowedEffects & DragDropEffects.Move) == DragDropEffects.Move) {
				e.Effects = DragDropEffects.Move;
			}
			else { e.Effects = DragDropEffects.None; }
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** TextControl_Drop Handler
        ** Inserts the dragged document or image into the TextControl.
        **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_Drop(object sender, DragEventArgs e) {
			// Check whether the dragged file is valid (supported by TX Text Control).
			if (m_dfiFileInfo != null) {
				// Insert the file as document or image.
				switch (m_dfiFileInfo.FileType) {
					case FileType.Document:
						Open(m_dfiFileInfo.FilePath, m_dfiFileInfo.StreamType);
						break;
					case FileType.Image:
						InsertDroppedImage(m_dfiFileInfo.FilePath, e.GetPosition(m_txTextControl));
						break;
				}
				m_dfiFileInfo = null;
			}
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** SetDragAndDropBehavior Method
        ** Adds all necessary handlers to implement a drag and drop behavior.
        **-----------------------------------------------------------------------------------------------------------*/
		private void SetDragAndDropBehavior() {
			m_txTextControl.DragEnter += TextControl_DragEnter; // Gets information about the dragged file.
			m_txTextControl.DragOver += TextControl_DragOver; // Specifies the target drop effect for the drag & drop operation.
			m_txTextControl.Drop += TextControl_Drop; // Inserts the dragged document or image into the TextControl.
		}

		/*-------------------------------------------------------------------------------------------------------------
		** InsertDroppedImage Method
		** Inserts the dropped image into the TextControl
        **
        ** Parameters:
        **      filePath:		The file path of the image to insert.
        **		cursorPosition:	The mouse cursor position inside the TextControl
		**-----------------------------------------------------------------------------------------------------------*/
		private void InsertDroppedImage(string filePath, Point cursorPosition) {
			// Convert mouse cursor inside the TextControl to System.Drawing.Point
			System.Drawing.Point pntCursorPosition = new System.Drawing.Point((int)cursorPosition.X, (int)cursorPosition.Y);

			// Get bounding rectangle of the first character of the paragraph
			// the image was dropped over
			Paragraph parCurrentParagraph = m_txTextControl.Paragraphs.GetItem(pntCursorPosition);
			TextChar tcParagraphStartChar = m_txTextControl.TextChars[parCurrentParagraph.Start];
			System.Drawing.Point pntParagraphLocation = (tcParagraphStartChar != null) ? tcParagraphStartChar.Bounds.Location : System.Drawing.Point.Empty;

			// Get bounding rectangle of the character the image was dropped over
			TextChar tcCursorPositionChar = m_txTextControl.TextChars.GetItem(pntCursorPosition, true);
			System.Drawing.Rectangle recCursorPositionChar = (tcCursorPositionChar != null) ? tcCursorPositionChar.Bounds : System.Drawing.Rectangle.Empty;

			// Calculate image position relative to paragraph position
			System.Drawing.Point pntImagePosition = new System.Drawing.Point(recCursorPositionChar.Right - pntParagraphLocation.X, recCursorPositionChar.Top - pntParagraphLocation.Y);

			// Insert image anchored to paragraph
			TXTextControl.Image imgImage = new TXTextControl.Image() { FileName = filePath };
			m_txTextControl.Images.Add(imgImage, pntImagePosition, parCurrentParagraph.Start, ImageInsertionMode.DisplaceText);
		}

		/*------------------------------------------------------------------------------------------------------------
		** CheckDraggedFiles Method
        ** Determines the file type, stream type (for document files) and file path of the dragged file.
        **
        ** Parameters:
        **      fileList:   The file list where the dragged file is stored.
        **
        ** Return value:    An object of type DraggedFileInfo that stores information about the dragged 
        **                  file. If the file format is not supported by TX Text Control, the method returns
        **                  null.
        **----------------------------------------------------------------------------------------------------------*/
		internal DraggedFileInfo CheckDraggedFiles(string[] fileList) {
			if (fileList != null) {
				// Get first parameter from the list and check if it is a supported file type
				string strFilePath = fileList[0];

				switch (Path.GetExtension(strFilePath).ToLower()) {
					// Documents
					case ".rtf":
						return new DraggedFileInfo(strFilePath, StreamType.RichTextFormat);
					case ".htm":
					case ".html":
						return new DraggedFileInfo(strFilePath, StreamType.HTMLFormat);
					case ".doc":
						return new DraggedFileInfo(strFilePath, StreamType.MSWord);
					case ".docx":
						return new DraggedFileInfo(strFilePath, StreamType.WordprocessingML);
					case ".pdf":
						return new DraggedFileInfo(strFilePath, StreamType.AdobePDF);
					case ".xml":
						return new DraggedFileInfo(strFilePath, StreamType.XMLFormat);
					case ".txt":
						return new DraggedFileInfo(strFilePath, StreamType.PlainText);
					case ".tx":
						return new DraggedFileInfo(strFilePath, StreamType.InternalUnicodeFormat);
					case ".xlsx":
						return new DraggedFileInfo(strFilePath, StreamType.SpreadsheetML);
					// Images
					case ".jpeg":
					case ".jpg":
					case ".tif":
					case ".tiff":
					case ".bmp":
					case ".gif":
					case ".png":
					case ".wmf":
					case ".emf":
					case ".svg":
						return m_txTextControl.CanEdit ? new DraggedFileInfo(strFilePath) : null;
					default:
						return null;
				}
			}
			return null;
		}
	}
}
