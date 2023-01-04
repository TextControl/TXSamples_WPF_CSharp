using System.Windows;
using System.Windows.Controls;

namespace TXTextControl.Words {
	/// <summary>
	/// Interaction logic for ExportMergeResultDialog.xaml
	/// </summary>
	public partial class ExportMergeResultDialog : Window {
		/*------------------------------------------------------------------------------------------------
			** Class FormatItem
			** Represents the 'Format' combo box item. It provides the displayed text, the format extension
			** and the TXTextControl.StreamType to use.
			**----------------------------------------------------------------------------------------------*/
		internal class FormatItem {
			// Member Variables
			private string m_strFormat;
			private string m_strExtension;
			private StreamType m_stStreamType;

			// Constructor
			internal FormatItem(string displayedText, string extension, StreamType streamType) {
				m_strFormat = displayedText;
				m_strExtension = extension;
				m_stStreamType = streamType;
			}

			// Properties
			internal string Extension { get { return m_strExtension; } }

			internal StreamType StreamType { get { return m_stStreamType; } }

			// Overridden Methods.
			public override string ToString() {
				return string.Format(m_strFormat, m_strExtension);
			}
		}


		/*------------------------------------------------------------------------------------------------
		** C O N S T R U C T O R
		**----------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** ExportMergeResultDialog Constructor
		** Creates a dialog to export the results of the merge process.
		**-----------------------------------------------------------------------------------------------------------*/
		public ExportMergeResultDialog() {
			InitializeComponent();


			// Add format items.
			m_cmbxFormat.Items.Add(new FormatItem(Properties.Resources.ExportMergeResultDialog_Format_RTF, ".rtf", StreamType.RichTextFormat));
			m_cmbxFormat.Items.Add(new FormatItem(Properties.Resources.ExportMergeResultDialog_Format_HTML,".html", StreamType.HTMLFormat));
			m_cmbxFormat.Items.Add(new FormatItem(Properties.Resources.ExportMergeResultDialog_Format_DOCX,".docx", StreamType.SpreadsheetML));
			m_cmbxFormat.Items.Add(new FormatItem(Properties.Resources.ExportMergeResultDialog_Format_DOC,".doc", StreamType.MSWord));
			m_cmbxFormat.Items.Add(new FormatItem(Properties.Resources.ExportMergeResultDialog_Format_PDF,".pdf", StreamType.AdobePDFA));
			m_cmbxFormat.Items.Add(new FormatItem(Properties.Resources.ExportMergeResultDialog_Format_TXT,".txt", StreamType.PlainText));
			m_cmbxFormat.Items.Add(new FormatItem(Properties.Resources.ExportMergeResultDialog_Format_TX,".tx", StreamType.InternalFormat));

			// Select the PDF item.
			m_cmbxFormat.SelectedIndex = 4;
		}


		/*------------------------------------------------------------------------------------------------
		** P R O P E R T I E S
		**----------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** Directory Property
		** Gets or sets the directoy where to export the created files of the merge process. 
		**-----------------------------------------------------------------------------------------------------------*/
		internal string Directory { get { return m_tbxDirectory.Text.Trim(); } set { m_tbxDirectory.Text = value; } }

		/*-------------------------------------------------------------------------------------------------------------
		** FilePrefix Property
		** Gets or sets the prefix string that is used when exporting the created files of the merge process.
		**-----------------------------------------------------------------------------------------------------------*/
		internal string FilePrefix { get { return m_tbxFilePrefix.Text.Trim(); } set { m_tbxFilePrefix.Text = System.IO.Path.GetFileNameWithoutExtension(value); } }

		/*-------------------------------------------------------------------------------------------------------------
		** Format Property
		** Gets the document format that is used when exporting the created files of the merge process.
		**-----------------------------------------------------------------------------------------------------------*/
		internal FormatItem Format { get { return m_cmbxFormat.SelectedItem as FormatItem; } }

		/*-------------------------------------------------------------------------------------------------------------
		** openDirectory Property
		** Gets a value indicating whether the directory where the merged files are exported should be openeded.
		**-----------------------------------------------------------------------------------------------------------*/
		internal bool openDirectory { get { return m_chbxopenDirectory.IsChecked.Value; } }


		/*------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**----------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** Directory_TextChanged Handler
		** Handles the enabled state of the OK button.
		**-----------------------------------------------------------------------------------------------------------*/
		private void Directory_TextChanged(object sender, TextChangedEventArgs e) {
			m_btnOK.IsEnabled = this.Directory.Length > 0;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** OK_Click Handler
		** If the specified directory path exists, close the dialog.
		**-----------------------------------------------------------------------------------------------------------*/
		private void OK_Click(object sender, RoutedEventArgs e) {
			if (!System.IO.Directory.Exists(this.Directory)) {
				MessageBox.Show(Properties.Resources.MessageBox_ExportMergeResultDialog_DirectoryDoesNotExist_Text, Properties.Resources.MessageBox_ExportMergeResultDialog_DirectoryDoesNotExist_Caption,MessageBoxButton.OK,MessageBoxImage.Error);
			}
			else {
				this.DialogResult = true;
				this.Close();
			}
		}
	}
}
