/*-------------------------------------------------------------------------------------------------------------
** MainWindow.xaml.cs File
**
** Description:
**      Sample project that is related to the 'Howto: Mail Merge -> Sample: Simple Mail Merge' 
**		article inside the 'Windows Presentation Foundation User's Guide'.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace MailMerge_Simple {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		/*-------------------------------------------------------------------------------------------------------------
        ** M E M B E R   V A R I A B L E S
        **-----------------------------------------------------------------------------------------------------------*/
		private DataSet m_dsAddresses;
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
        ** Creates the addresses data set, adds an item for each database field to the 'Add' item drop down and 
        ** loads the 'Instructions.tx' sample template.
        **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_Loaded(object sender, RoutedEventArgs e) {
			// Create a new DataSet and load the XML file.
			m_dsAddresses = new DataSet();
			m_dsAddresses.ReadXml(@"Files\Data.xml");

			// Create a new ToolStripMenuItem for each database field.
			foreach (DataColumn dataColumn in m_dsAddresses.Tables[0].Columns) {
				MenuItem mnuItem = new MenuItem();
				mnuItem.Header = dataColumn.ColumnName;

				mnuItem.Click += new RoutedEventHandler(DatabaseFieldItem_Click);
				m_miAdd.Items.Add(mnuItem);
			}

			// Initialize a MailMerge instance.
			m_mmMailMerge = new TXTextControl.DocumentServer.MailMerge();
			m_mmMailMerge.TextComponent = m_txTextControl;

			// Load the 'Instructions.tx' sample template.
			m_txTextControl.Selection.Load(@"Files\Instructions.tx", TXTextControl.StreamType.InternalUnicodeFormat);

			// Set focus to the TextControl.
			m_txTextControl.Focus();
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** 'Application Fields' Drop Down
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** ApplicationFields_DropDownOpening Handler
        ** Sets the enabled state of the 'Add' and 'Properties' items when the 'Application Fields' 
        ** drop down is opening.
        **-----------------------------------------------------------------------------------------------------------*/
		private void ApplicationFields_DropDownOpening(object sender, RoutedEventArgs e) {
			m_miProperties.IsEnabled = m_txTextControl.ApplicationFields.GetItem() == null ? false : true;
			m_miAdd.IsEnabled = m_txTextControl.ApplicationFields.CanAdd;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** DatabaseFieldItem_Click Handler
        ** Creates with the text of the clicked database field item a new TXTextControl.DocumentServer.Fields.MergeField  
        ** and adds it to TextControl.
        **-----------------------------------------------------------------------------------------------------------*/
		private void DatabaseFieldItem_Click(object sender, RoutedEventArgs e) {
			MenuItem tmiClickedItem = (MenuItem)sender;
			InsertMergeField(tmiClickedItem.Header.ToString());
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** Properties_Click Handler
        ** Creates with the text of the clicked database field item a new TXTextControl.DocumentServer.Fields.MergeField  
        ** and adds it to TextControl.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Properties_Click(object sender, RoutedEventArgs e) {
			TXTextControl.DocumentServer.Fields.MergeField mfMergeField = new TXTextControl.DocumentServer.Fields.MergeField(m_txTextControl.ApplicationFields.GetItem());
			mfMergeField.ShowDialog(this);
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** 'Mail Merge' Drop Down
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** MailMerge_DropDownOpening Handler
        ** Sets the enabled state of the 'Merge' item when the 'Mail Merge' drop down is opening.
        **-----------------------------------------------------------------------------------------------------------*/
		private void MailMerge_DropDownOpening(object sender, RoutedEventArgs e) {
			m_miMerge.IsEnabled = m_txTextControl.ApplicationFields.Count > 0;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** Merge_Click Handler
        ** Use the MailMerge instance to merge the data into the application fields.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Merge_Click(object sender, RoutedEventArgs e) {
			m_mmMailMerge.Merge(m_dsAddresses.Tables[0], true);
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** InsertMergeField Method
        ** Creates with the text of the clicked database field item a new TXTextControl.DocumentServer.Fields.MergeField  
        ** and adds it to TextControl.
        **
        ** Parameters:
        **		name:		The name of the merge field that is created and added to the TextControl.
        **-----------------------------------------------------------------------------------------------------------*/
		private void InsertMergeField(string name) {
			// Create a new TXTextControl.DocumentServer.Fields.MergeField
			// and add it to TextControl.
			TXTextControl.DocumentServer.Fields.MergeField mfMergeField = new TXTextControl.DocumentServer.Fields.MergeField();
			mfMergeField.Name = name;
			mfMergeField.Text = "{ " + name + " }";
			mfMergeField.ApplicationField.HighlightMode = TXTextControl.HighlightMode.Activated;
			mfMergeField.ApplicationField.DoubledInputPosition = true;

			m_txTextControl.ApplicationFields.Add(mfMergeField.ApplicationField);
		}
	}
}
