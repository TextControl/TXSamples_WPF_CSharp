/*-------------------------------------------------------------------------------------------------------------
** MainWindow.xaml.cs File
**
** Description:
**      Sample project that is related to the 'Howto: Mail Merge -> Sample: Mail Merge with Repeating Blocks' 
**		article inside the 'Windows Presentation Foundation User's Guide'.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Data;
using System.Windows;

namespace MailMerge_Blocks {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/
		private DataSet m_dsData;
		private TXTextControl.DocumentServer.MailMerge m_mmMailMerge;


		/*-------------------------------------------------------------------------------------------------------------
        ** C O N S T R U C T O R S
        **-----------------------------------------------------------------------------------------------------------*/
		public MainWindow() {
			InitializeComponent();
		}


		/*-------------------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** TextControl_Loaded Handler
        ** Creates a new data set and loads the 'Template.docx' template.
        **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_Loaded(object sender, RoutedEventArgs e) {
			// Create a new data set and load the XML file
			m_dsData = new DataSet();
			m_dsData.ReadXml(@"Files\Data.xml");

			TXTextControl.LoadSettings lsLoadSettings = new TXTextControl.LoadSettings {
				ApplicationFieldFormat = TXTextControl.ApplicationFieldFormat.MSWord,
				LoadSubTextParts = true
			};

			// Load the 'Template.docx' template
			m_txTextControl.Load(@"Files\Template.docx", TXTextControl.StreamType.WordprocessingML, lsLoadSettings);

			// Initialize a MailMerge instance.
			m_mmMailMerge = new TXTextControl.DocumentServer.MailMerge();
			m_mmMailMerge.TextComponent = m_txTextControl;

			// Set focus to the TextControl.
			m_txTextControl.Focus();
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** Merge_Click Handler
		** Use the MailMerge instance to merge the data.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Merge_Click(object sender, RoutedEventArgs e) {
			m_mmMailMerge.Merge(m_dsData.Tables["orders"], true);
		}
	}
}
