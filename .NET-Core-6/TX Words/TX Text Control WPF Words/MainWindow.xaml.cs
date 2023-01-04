using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using TXTextControl.WPF;

namespace TXTextControl.Words {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/
		private VersionInfo.ProductLevel m_plTXLicense;
		private readonly ResourceManager m_rmResources = null;
		private bool m_bRestartApplication = false;

		// File Info
		// Values that are updated when opening, creating or saving a document
		private string m_strActiveDocumentName = Properties.Resources.MainWindow_Caption_Untitled; // The document's name is '[Untitled]' on default.
		private string m_strActiveDocumentPath = null; // The path of the active document.
		private StreamType m_stLastLoadedType = StreamType.RichTextFormat; // The StreamType that was last used to load a document. If no document has been loaded so far, RichtTextFormat is used. 
		private StreamType m_stLastSavedType = StreamType.RichTextFormat; // The StreamType that was last used to load a document. If no document has been loaded so far, RichtTextFormat is used. 
		private StreamType m_stActiveDocumentType = StreamType.RichTextFormat; // The StreamType that was last used to load or save the current document.
		private string m_strUserPasswordPDF = string.Empty; // Tthe password for the user when the document is reopened.
		private string m_strCssFileName = null; //The path and filename of a CSS file belonging to a HTML document.
		private CssSaveMode m_svCssSaveMode = CssSaveMode.None; // Specifies how to save stylesheet data with a HTML document.
		private bool m_bIsUnknownDocument = true; // A flag that indicates whether or not the active document is loaded/saved or created (unknown)		
		private bool m_bIsDirtyDocument = false; // A flag that indicates whether or not the document is 'dirty'


		/*-------------------------------------------------------------------------------------------------------------
        ** C O N S T R U C T O R
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** MainWindow Constructor
        **-----------------------------------------------------------------------------------------------------------*/
		public MainWindow() {
			InitializeComponent();
			// Create the ResourceManager instance.
			Type tpMainWindow = this.GetType();
			m_rmResources = new ResourceManager(tpMainWindow.Namespace + ".Properties.Resources", tpMainWindow.Assembly);

			// Get and set saved application settings.
			LoadRightToLeftSettings();
			LoadKnownUserSettings();
			LoadRecentFiles();
		}


		/*-------------------------------------------------------------------------------------------------------------
		** M E T H O D S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** UpdateMainWindowCaption Method
        ** Updates the application caption with the name of the active document and the product name.
        **-----------------------------------------------------------------------------------------------------------*/
		private void UpdateMainWindowCaption() {
			this.Title = m_strActiveDocumentName + (m_bIsDirtyDocument ? "*" : "") + " - " + Properties.Resources.MainWindow_Caption_Product;
		}

		/*-------------------------------------------------------------------------------------------------------
        ** SetItemsDesign Method
        ** Sets the texts and images of all menu items.
        **-----------------------------------------------------------------------------------------------------*/
		private void SetItemsDesign() {
			// 'File'
			SetFileItemsTexts();
			SetFileItemsImages();

			// 'Edit'
			SetEditItemsTexts();
			SetEditItemsImages();

			// 'View'
			SetViewItemsTexts();
			SetViewItemsImages();

			// 'Insert'
			SetInsertItemsTexts();
			SetInsertItemsImages();

			// 'Format'
			SetFormatItemsTexts();
			SetFormatItemsImages();

			// 'Table'
			SetTableItemsImages();
		}

		/*-------------------------------------------------------------------------------------------------------
        ** SetItemText Method
        ** Sets the text of the specified item.
        **
		** Parameters:
		**      item:		The item where to set the text.
		**		args:		Optional: If no string is set, only the resource text that corresponds to the item's
		**					name is set. If one string is set, the resource text is formatted with that string.
		**					If two strings are set, the first string is the text to set and the second string
		**					is the format value.
        **-----------------------------------------------------------------------------------------------------*/
		private void SetItemText(MenuItem item, params string[] args) {
			string strText;
			string strFormat;
			switch (args.Length) {
				case 0: // Only the resource text that corresponds to the item's name is set.
					strText = m_rmResources.GetString("Item_" + item.Name.Substring(4) + "_Text");
					strFormat = "";
					break;
				case 1: // The resource text is formatted.
					strText = m_rmResources.GetString("Item_" + item.Name.Substring(4) + "_Text");
					strFormat = args[0];
					break;
				case 2: // Two strings are set: the first string is the text to set and the second string is the format value.
					strText = args[0];
					strFormat = args[1];
					break;
				default:
					return;

			}

			// Set the text.
			item.Header = string.Format(strText, strFormat);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** SetItemImage Method
		** Creates an image that correspond to the referenced image id. That image is set as the item's icon.
		**
		** Parameters:
		**      item:		The item where to set the created image.
		**		iamgeID:	The id of the image to create.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetItemImage(MenuItem item, string imageID) {
			item.Icon = new System.Windows.Controls.Image() { Source = ResourceProvider.GetSmallIcon(imageID, this) };
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** GetSmallIcon Method
        ** Creates a small bitmap icon from an embedded SVG resource.
        **
        ** Parameters:
        **      path:   The path to the embedded SVG resource.
        **
        ** Returns:     The created small bitmap as System.Windows.Controls.Image.
        **-----------------------------------------------------------------------------------------------------------*/
		private System.Windows.Controls.Image GetSmallIcon(string path) {
			Assembly asm = Assembly.GetExecutingAssembly();
			System.Windows.Controls.Image img = null;

			using (Stream sStream = asm.GetManifestResourceStream(path)) {
				img = new System.Windows.Controls.Image() { Source = ResourceProvider.GetSmallIcon(sStream, this) };
			}
			return img;
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------
        ** MainWindow_Loaded Handler 
        ** Sets the requested behavior for all added controls.
        **-----------------------------------------------------------------------------------------------------*/
		private void MainWindow_Loaded(object sender, RoutedEventArgs e) {

			// Items
			SetItemsDesign();
			CreateShapeAndBarcodeItems();

			// Main Window
			UpdateMainWindowCaption(); // Set caption

			// Drag & Drop
			SetDragAndDropBehavior();

			// Open Hyperlink
			SetOpenHyperlinkBehavior();

			// Shortcuts
			SetShortCutBehavior();

			// Tool Bars
			SetButtonBar();
			SetRulerBarsDesign();
			SetStatusBarDesign();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** TextControl_Changed Handler
		** Updates the 'Is Dirty Document' flag to true if the document changed.
		**-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_Changed(object sender, EventArgs e) {
			if (m_bIsDirtyDocument != (m_bIsDirtyDocument = true)) {
				// Update caption and save items enabled state.
				UpdateMainWindowCaption();
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** TextControl_Loaded_Version Handler
		** Gets the TextControl license level and sets the focus to the TextControl.
		**-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_Loaded_MainWindow(object sender, RoutedEventArgs e) {
			m_plTXLicense = m_txTextControl.GetVersionInfo().Level;
			m_txTextControl.Focus();
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** MainWindow_Closing Handler
        ** Invokes the SaveDirtyDocumentOnExit method to handle dirty documents. If the method returns false, the 
        ** closing of the application will be canceled. If the window closing is not canceled, the recent files
        ** are saved to the Properties.Settings.Default.RecentFiles property.
        **-----------------------------------------------------------------------------------------------------------*/
		private void MainWindow_Closing(object sender, CancelEventArgs e) {
			if (!(e.Cancel = !SaveDirtyDocumentOnExit())) {
				// Save the recent files to the Properties.Settings.Default.RecentFiles property
				SaveRecentFiles();
				// Save the know users to the Properties.Settings.Default.KnownUsers property
				SaveKnownUserSettings();
				if (m_bRestartApplication) {
					System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
				}
			}
		}


	}
}
