/*-------------------------------------------------------------------------------------------------------------
** MainWindow_Reporting_Layout.cs File
**
** Description:
**		Sets the layout of the added application menu's sample template button, the RibbonReportingTab's  
**		'Database Sample' button,  Merge' group and the reporting "Result" tab.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows.Controls.Ribbon;
using System.Windows.Media.Imaging;
using TXTextControl.WPF;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
        ** M E M B E R   V A R I A B L E S
        **-----------------------------------------------------------------------------------------------------------*/
		// RibbonReportingTab
		private RibbonMenuItem m_rmiSampleDatabaseButton = null;
		private readonly RibbonGroup m_rgMerge = new RibbonGroup();
		private RibbonButton m_rbtnMergeAndExport = null;

		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** Application Menu
		**-----------------------------------------------------------------------------------------------------------*/
		/*-------------------------------------------------------------------------------------------------------------
		** SetOpenSampleTemplateButtonDesign Method
		** Sets the image source of the 'Open Sample Template' menu button.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetOpenSampleTemplateButtonDesign() {
			m_rmbtnOpenSampleTemplate.ImageSource = GetLargeIcon(@"TXTextControl.Words.Images.OpenSampleTemplate_Large.svg");
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** RibbonReportingTab
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** AddSampleDatabaseButton Method
        ** Creates a ribbon button that loads the sampled database when clicked. That button is added to the drop 
        ** down menu of the 'Select Data Source' button.
        **-----------------------------------------------------------------------------------------------------------*/
		private void AddSampleDatabaseButton() {
			// Create the ribbon button
			m_rmiSampleDatabaseButton = new RibbonMenuItem() {
				Header = Properties.Resources.RibbonReportingTab_LoadSampleDatabase,
				ImageSource = GetSmallIcon(@"TXTextControl.Words.Images.SampleDatabase_Small.svg")
			};

			// Add tool tips
			m_rmiSampleDatabaseButton.ToolTipTitle = Properties.Resources.RibbonReportingTab_LoadSampleDatabase_ToolTip_Title;
			m_rmiSampleDatabaseButton.ToolTipDescription = Properties.Resources.RibbonReportingTab_LoadSampleDatabase_ToolTip_Description;

			// Add the handler that loads the sampled database when clicked
			m_rmiSampleDatabaseButton.Click += SampleDatabaseButton_Click;

			// Add the ribbon button to the drop down menu of the 'Select Data Source' button.
			RibbonSplitButton rsbtnSelectDataSource = m_rtRibbonReportingTab.FindName(RibbonReportingTab.RibbonItem.TXITEM_DataSource.ToString()) as RibbonSplitButton;
			rsbtnSelectDataSource.Items.Insert(3, m_rmiSampleDatabaseButton);
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** AddMergeGroup Method
        ** Creates a ribbon group with a ribbon button that starts merging files and switches to the 'Result' tab when 
        ** clicked.
        **-----------------------------------------------------------------------------------------------------------*/
		private void AddMergeGroup() {
			// Create the icons for the ribbon group and ribbon button.
			BitmapSource bmpSmallIcon = GetSmallIcon(@"TXTextControl.Words.Images.MergeAndExport_Small.svg");
			BitmapSource bmpLargeIcon = GetLargeIcon(@"TXTextControl.Words.Images.MergeAndExport_Large.svg");

			// Set ribbon group design
			m_rgMerge.SmallImageSource = bmpSmallIcon;
			m_rgMerge.LargeImageSource = bmpLargeIcon;
			m_rgMerge.Header = Properties.Resources.Merge;
			m_rgMerge.KeyTip = Properties.Resources.Merge_KeyTip;

			// Add a ribbon button that starts merging files and switches to the 'Result' tab when clicked.
			AddMergeAndExportButton(m_rgMerge, bmpSmallIcon, bmpLargeIcon);

			// Add the ribbon group to the ribbon tab
			m_rtRibbonReportingTab.Items.Add(m_rgMerge);

			// The group's enabled state depends on the IsMergingPossible property value of the 
			// RibbonReportingTab's DataSourceManager 
			m_rgMerge.IsEnabled = m_rtRibbonReportingTab.DataSourceManager.IsMergingPossible;
			m_rtRibbonReportingTab.DataSourceManager.IsMergingPossibleChanged += DataSourceManager_IsMergingPossibleChanged;
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** AddMergeAndExportButton Method
        ** Creates a ribbon button that starts merging files and switches to the 'Result' tab when clicked. That 
        ** ribbon button is added to the specified ribbon group. 
        **
        ** Parameters:
        **      ribbonGroup:    The ribbon group where to add the created ribbon button.
        **      smallIcon:      The bitmap that is used as the ribbon button's small icon.
        **      largeIcon:      The bitmap that is used as the ribbon button's large icon.
        **-----------------------------------------------------------------------------------------------------------*/
		private void AddMergeAndExportButton(RibbonGroup ribbonGroup, BitmapSource smallIcon, BitmapSource largeIcon) {

			// Create the ribbon button
			m_rbtnMergeAndExport = new RibbonButton() {
				Label = Properties.Resources.MergeAndExport,
				SmallImageSource = smallIcon,
				LargeImageSource = largeIcon,
				KeyTip = Properties.Resources.MergeAndExport_KeyTip
			};

			// Add tool tips
			m_rbtnMergeAndExport.ToolTipTitle = Properties.Resources.MergeAndExport_ToolTip_Title;
			m_rbtnMergeAndExport.ToolTipDescription = Properties.Resources.MergeAndExport_ToolTip_Description;

			// Add the handler that starts merging files and switches to the 'Result' tab when clicked.
			m_rbtnMergeAndExport.Click += MergeAndExport_Click;

			// Add the ribbon button to the ribbon group
			ribbonGroup.Items.Add(m_rbtnMergeAndExport);
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** 'Reporting' ContextualTabGroup and 'Result' RibbonTab
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** SetMergeResultsTabDesign Method
		** Creates groups and sets the design of the reporting 'Result' tab.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetMergeResultsTabDesign() {
			// Set the images of the 'Result' group items
			m_rgMergeResultsTab_ResultGroup.SmallImageSource =
			m_rbtnExitMergeResultsTab.SmallImageSource = ResourceProvider.GetSmallIcon(ResourceProvider.FileMenuItem.TXITEM_Exit.ToString(), this);
			m_rgMergeResultsTab_ResultGroup.LargeImageSource =
			m_rbtnExitMergeResultsTab.LargeImageSource = ResourceProvider.GetLargeIcon(ResourceProvider.FileMenuItem.TXITEM_Exit.ToString(), this);

			// Set the images of the 'Navigate' group items 
			m_rgNavigateGroup.SmallImageSource =
			m_rbtnNextRecord.SmallImageSource = ResourceProvider.GetSmallIcon(ResourceProvider.GeneralItem.TXITEM_NavigateToNext.ToString(), this);
			m_rgNavigateGroup.LargeImageSource =
			m_rbtnNextRecord.LargeImageSource = ResourceProvider.GetLargeIcon(ResourceProvider.GeneralItem.TXITEM_NavigateToNext.ToString(), this);
			m_rbtnFirstRecord.SmallImageSource = ResourceProvider.GetSmallIcon(ResourceProvider.GeneralItem.TXITEM_NavigateToFirst.ToString(), this);
			m_rbtnFirstRecord.LargeImageSource = ResourceProvider.GetLargeIcon(ResourceProvider.GeneralItem.TXITEM_NavigateToFirst.ToString(), this);
			m_rbtnPreviousRecord.SmallImageSource = ResourceProvider.GetSmallIcon(ResourceProvider.GeneralItem.TXITEM_NavigateToPrevious.ToString(), this);
			m_rbtnPreviousRecord.LargeImageSource = ResourceProvider.GetLargeIcon(ResourceProvider.GeneralItem.TXITEM_NavigateToPrevious.ToString(), this);
			m_rbtnLastRecord.SmallImageSource = ResourceProvider.GetSmallIcon(ResourceProvider.GeneralItem.TXITEM_NavigateToLast.ToString(), this);
			m_rbtnLastRecord.LargeImageSource = ResourceProvider.GetLargeIcon(ResourceProvider.GeneralItem.TXITEM_NavigateToLast.ToString(), this);

			// Set the images of the 'Export' group items 
			m_rgExportGroup.SmallImageSource =
			m_rbtnExportMergeResult.SmallImageSource = GetSmallIcon(@"TXTextControl.Words.Images.MergeAndExport_Small.svg");
			m_rgExportGroup.LargeImageSource =
			m_rbtnExportMergeResult.LargeImageSource = GetLargeIcon(@"TXTextControl.Words.Images.MergeAndExport_Large.svg");
		}
	}
}
