using System.Windows;
/*-------------------------------------------------------------------------------------------------------------
** MainWindow.xaml.cs File
**
** Description:
**      Sample project that is related to the 'Howto: Use Hypertext Links -> Step 1: Inserting a Hypertext 
**		Link' article inside the 'Windows Presentation Foundation User's Guide'.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
namespace Step1 {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		/*-------------------------------------------------------------------------------------------------------------
		** C O N S T R U C T O R
		**-----------------------------------------------------------------------------------------------------------*/
		public MainWindow() {
			InitializeComponent();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** InsertHyperlink_Click Handler
        ** Creates an object of type HypertextLink that references to the Text Control Web Site and inserts that
        ** link into the current input position of the TextControl.
        **-----------------------------------------------------------------------------------------------------------*/
		private void InsertHyperlink_Click(object sender, RoutedEventArgs e) {
			TXTextControl.HypertextLink hlLink = new TXTextControl.HypertextLink("Text Control Web Site", "http://www.textcontrol.com");
			m_txTextControl.HypertextLinks.Add(hlLink);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** SaveAs_Click Handler
		** Opens the built-in save dialog to save the document as an HTML file.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SaveAs_Click(object sender, RoutedEventArgs e) {
			m_txTextControl.Save(TXTextControl.StreamType.HTMLFormat);
		}
	}
}
