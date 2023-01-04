/*-------------------------------------------------------------------------------------------------------------
** MainWindow.xaml.cs File
**
** Description:
**      Sample project that is related to the 'Howto: Mail Merge - Sample: Mail Merge with Nested 
**		Repeating Blocks' article inside the 'Windows Presentation Foundation User's Guide'.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using Microsoft.Win32;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MailMerge_Nested_Blocks {
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
        ** Creates load the 'Accruals Report.docx' template.
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
			m_txTextControl.Load(@"Files\Accruals Report.docx", TXTextControl.StreamType.WordprocessingML, lsLoadSettings);

			// Initialize a MailMerge instance.
			m_mmMailMerge = new TXTextControl.DocumentServer.MailMerge();
			m_mmMailMerge.TextComponent = m_txTextControl;
			m_mmMailMerge.BlockRowMerged += MailMerge_BlockRowMerged;
			// Set focus to the TextControl.
			m_txTextControl.Focus();
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** Datasource_Click Handler
        ** Get the reference to the 'Data.xml' sample
        **-----------------------------------------------------------------------------------------------------------*/
		private void Datasource_Click(object sender, RoutedEventArgs e) {
			(sender as Button).ContextMenu.IsEnabled = true;
			(sender as Button).ContextMenu.PlacementTarget = (sender as Button);
			(sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
			(sender as Button).ContextMenu.IsOpen = true;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** LoadSampleDatasource_Click Handler
        ** Get the reference to the 'Data.xml' sample
        **-----------------------------------------------------------------------------------------------------------*/
		private void LoadSampleDatasource_Click(object sender, RoutedEventArgs e) {
			// Update the text box.
			m_tbxLoadedDatabaseFile.Tag = @"Files\Data.xml";
			m_tbxLoadedDatabaseFile.Text = "Data.xml";
			m_tmiCreateReport.IsEnabled = true;
			m_pthRightArraow.Fill = new SolidColorBrush(Colors.Black);
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** LoadXML_Click Handler
        ** Create and open an OpenFileDialog to get the reference to an XML database.
        **-----------------------------------------------------------------------------------------------------------*/
		private void LoadXML_Click(object sender, RoutedEventArgs e) {
			// Create and open an OpenFileDialog to load an XML database.
			OpenFileDialog dlgLoadXML = new OpenFileDialog();
			dlgLoadXML.Filter = "XML Database | *.xml";
			dlgLoadXML.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

			if (dlgLoadXML.ShowDialog(this) == true) {
				// Update the text box.
				m_tbxLoadedDatabaseFile.Tag = dlgLoadXML.FileName;
				m_tbxLoadedDatabaseFile.Text = dlgLoadXML.SafeFileName;
				m_tmiCreateReport.IsEnabled = true;
				m_pthRightArraow.Fill = new SolidColorBrush(Colors.Black);
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** CreateReport_Click Handler
        ** Merge the template with the data source.
        **-----------------------------------------------------------------------------------------------------------*/
		private void CreateReport_Click(object sender, RoutedEventArgs e) {
			try {
				// Load the XML file.
				DataSet dsData = new DataSet();
				dsData.ReadXml(m_tbxLoadedDatabaseFile.Tag as string, XmlReadMode.Auto);

				// Add the relations for the main block and its child blocks.
				DataRelation relCompanyEmployee = new DataRelation("company_employee",
					 dsData.Tables["company"].Columns["company_number"],
					 dsData.Tables["employee"].Columns["company_number"]);

				DataRelation relEmployeeSick = new DataRelation("employee_sick",
					 dsData.Tables["employee"].Columns["employee_number"],
					 dsData.Tables["sick"].Columns["employee_number"]);

				DataRelation relEmployeeVacation = new DataRelation("employee_vacation",
					 dsData.Tables["employee"].Columns["employee_number"],
					 dsData.Tables["vacation"].Columns["employee_number"]);

				dsData.Relations.Add(relCompanyEmployee);
				dsData.Relations.Add(relEmployeeSick);
				dsData.Relations.Add(relEmployeeVacation);

				// Update the progress bar.
				m_pbProgress.Maximum = dsData.Tables["employee"].Rows.Count;

				// Merge.
				m_mmMailMerge.Merge(dsData.Tables["company"], true);

				// Reset the progress bar.
				m_pbProgress.Value = 0;
			} catch (Exception exc) {
				MessageBox.Show(this, exc.Message);
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** MailMerge_BlockRowMerged Handler
		** Update the progress bar when the 'employee' merge block is handled.
		**-----------------------------------------------------------------------------------------------------------*/
		private void MailMerge_BlockRowMerged(object sender, TXTextControl.DocumentServer.MailMerge.BlockRowMergedEventArgs e) {
			if (e.MergeBlockName == "employee") {
				m_pbProgress.Value++;
			}
		}
	}
}
