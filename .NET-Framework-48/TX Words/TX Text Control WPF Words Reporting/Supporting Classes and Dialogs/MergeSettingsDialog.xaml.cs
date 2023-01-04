using System.Text.RegularExpressions;
using System.Windows;

namespace TXTextControl.Words {
	/// <summary>
	/// Interaction logic for MergeSettingsDialog.xaml
	/// </summary>
	public partial class MergeSettingsDialog : Window {
		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S 
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Regex m_rgxPositivNumber = new Regex(@"^[1-9]+\d*$");


		/*-------------------------------------------------------------------------------------------------------------
		** C O N S T R U C T O R
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** MergeSettingsDialog Constructor
		** Creates a dialog to determine the settings for the following merge process.
		**-----------------------------------------------------------------------------------------------------------*/
		public MergeSettingsDialog() {
			InitializeComponent();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** P R O P E R T I E S 
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** MaxRecords Property
		** Returns the number of files that should be merged. If the "Merge all records" check box is checked, the
		** property returns int:MaxValue
		**-----------------------------------------------------------------------------------------------------------*/
		internal int MaxRecords { get { return m_chbxMergeAllRecords.IsChecked.Value ? int.MaxValue : int.Parse(m_tbxNumberOfRecords.Text); } }

		/*-------------------------------------------------------------------------------------------------------------
		** MergeIntoSingleFile Property
		** Returns a value indicating whether all created files should be merged into a single file.
		**-----------------------------------------------------------------------------------------------------------*/
		internal bool MergeIntoSingleFile { get { return m_chbxMergeIntoSingleDocument.IsChecked.Value; } }

		/*-------------------------------------------------------------------------------------------------------------
		** RemoveEmptyBlocks Property
		** Returns a value indicating whether or not the content of empty merge blocks should be removed from the 
		** template.
		**-----------------------------------------------------------------------------------------------------------*/
		internal bool RemoveEmptyBlocks { get { return m_chbxBlocks.IsChecked.Value; } }

		/*-------------------------------------------------------------------------------------------------------------
		** RemoveEmptyFields Property
		** Returns a value indicating whether or not empty fields should be removed from the template.
		**-----------------------------------------------------------------------------------------------------------*/
		internal bool RemoveEmptyFields { get { return m_chbxFields.IsChecked.Value; } }

		/*-------------------------------------------------------------------------------------------------------------
		** RemoveEmptyLines Property
		** Returns a value indicating whether or not text lines which are empty after merging should be removed from 
		** the template.
		**-----------------------------------------------------------------------------------------------------------*/
		internal bool RemoveEmptyLines { get { return m_chbxLines.IsChecked.Value; } }

		/*-------------------------------------------------------------------------------------------------------------
		** RemoveEmptyImages Property
		** Returns a value indicating whether or not images which don't have merge data should be removed from the 
		** template.
		**-----------------------------------------------------------------------------------------------------------*/
		internal bool RemoveEmptyImages { get { return m_chbxImages.IsChecked.Value; } }

		/*-------------------------------------------------------------------------------------------------------------
		** RemoveTrailingWhitespace Property
		** Returns a value indicating whether trailing whitespace should be removed before saving a document. 
		**-----------------------------------------------------------------------------------------------------------*/
		internal bool RemoveTrailingWhitespace { get { return m_chbxTrailingWhitespace.IsChecked.Value; } }


		/*-------------------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** OK_Click Handler
		** Closes the dialog with DialogResult.OK when clicked.
		**-----------------------------------------------------------------------------------------------------------*/
		private void OK_Click(object sender, RoutedEventArgs e) {
			this.DialogResult = true;
			this.Close();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** MergeAllRecords_CheckedChanged Handler
		** Enables/Disables the "Number of records:" label and text box when the "Merge all records" check box
		** was checked.
		**-----------------------------------------------------------------------------------------------------------*/
		private void MergeAllRecords_CheckedChanged(object sender, RoutedEventArgs e) {
			m_lblNumberOfRecords.IsEnabled =
			m_tbxNumberOfRecords.IsEnabled = !m_chbxMergeAllRecords.IsChecked.Value;
			m_btnOK.IsEnabled = m_chbxMergeAllRecords.IsChecked.Value || m_tbxNumberOfRecords.Text.Length > 0;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** NumberOfRecords_PreviewTextInput Handler
		** Validates the text input: Only positive numbers are allowed.
		**-----------------------------------------------------------------------------------------------------------*/
		private void NumberOfRecords_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) {
			e.Handled = !m_rgxPositivNumber.IsMatch(e.Text);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** NumberOfRecords_TextChanged Handler
		** Disables the OK button if no number is set.
		**-----------------------------------------------------------------------------------------------------------*/
		private void NumberOfRecords_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
			if (m_btnOK != null) {
				m_btnOK.IsEnabled = m_tbxNumberOfRecords.Text.Length > 0;
			}
		}
	}
}
