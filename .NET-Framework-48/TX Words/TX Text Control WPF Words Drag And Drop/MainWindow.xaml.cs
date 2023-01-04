/*-------------------------------------------------------------------------------------------------------------
** MainWindow.xaml.cs File
**
** Description:
**     Implements TX Text Control Words.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Ribbon;
using TXTextControl.WPF;

namespace TXTextControl.Words {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : RibbonWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly string m_strFilesDirectory = @"Files\";


		/*-------------------------------------------------------------------------------------------------------------
        ** C O N S T R U C T O R
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** MainWindow Constructor
        **-----------------------------------------------------------------------------------------------------------*/
		public MainWindow() {
			InitializeComponent();
			// Get and set saved application settings.
			LoadRecentFiles();
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------
        ** SetAppMenuDesign Method
        ** Updates the design of the menu items.
        **-----------------------------------------------------------------------------------------------------*/
		private void SetAppMenuDesign() {
			SetMenuItemDesign(ResourceProvider.FileMenuItem.TXITEM_New.ToString(), m_rmiNew);
			SetMenuItemDesign(ResourceProvider.FileMenuItem.TXITEM_Open.ToString(), m_rmiOpen);
			SetMenuItemDesign(ResourceProvider.FileMenuItem.TXITEM_Save.ToString(), m_rmiSave);
			SetMenuItemDesign(ResourceProvider.FileMenuItem.TXITEM_SaveAs.ToString(), m_rmiSaveAs);
			SetMenuItemDesign(ResourceProvider.FileMenuItem.TXITEM_Print.ToString(), m_rsmiPrint);
			SetMenuItemDesign(ResourceProvider.FileMenuItem.TXITEM_Print.ToString(), m_rbtnPrint);
			SetMenuItemDesign(ResourceProvider.FileMenuItem.TXITEM_PrintQuick.ToString(), m_rbtnPrintQuick);
			SetMenuItemDesign(ResourceProvider.FileMenuItem.TXITEM_About.ToString(), m_rtbtnAbout, Properties.Resources.AboutButton_ToolTip_Description);

			// Set Recent Files overview label text
			m_rgcRecentFiles.Header = Properties.Resources.ApplicationMenu_ResentFiles;
		}

		/*-------------------------------------------------------------------------------------------------------
        ** SetRibbonButtonDesign Method
        ** Sets the icons, text, key tip and tool tip for a specific RibbonButton.
        **
        ** Parameters:
        **      resourceID:     The id that is used to identify the corresponding texts and icons.
        **      menuItem:		The ribbon menu item to update.
        **      args:           Optional strings that are used to format the displayed texts.
        **-----------------------------------------------------------------------------------------------------*/
		private void SetMenuItemDesign(string resourceID, RibbonMenuItem menuItem, params string[] args) {
			menuItem.Name = resourceID;
			menuItem.ImageSource = ResourceProvider.GetLargeIcon(resourceID, this);
			menuItem.KeyTip = ResourceProvider.GetKeyTip(resourceID);

			if (args.Length > 0) {
				menuItem.Header = string.Format(ResourceProvider.GetText(resourceID), args);
				menuItem.ToolTipTitle = string.Format(ResourceProvider.GetToolTipTitle(resourceID), args);
				menuItem.ToolTipDescription = string.Format(ResourceProvider.GetToolTipDescription(resourceID), args);
			}
			else {
				menuItem.Header = ResourceProvider.GetText(resourceID);
				menuItem.ToolTipTitle = ResourceProvider.GetToolTipTitle(resourceID);
				menuItem.ToolTipDescription = ResourceProvider.GetToolTipDescription(resourceID);
			}
		}

		/*-------------------------------------------------------------------------------------------------------
        ** SetAppMenuBehavior Method
        ** Connects all necessary handlers to the application menu items.
        **-----------------------------------------------------------------------------------------------------*/
		private void SetAppMenuBehavior() {
			// Common
			m_txTextControl.Changed += TextControl_Changed; // Updates the internal 'is dirty document' flag

			// New:
			m_rmiNew.Click += New_Click;

			// Open:
			m_rmiOpen.Click += Open_Click;

			// Save:
			m_rmiSave.Click += Save_Click;

			// Save As:
			m_rmiSaveAs.Click += SaveAs_Click;

			// Print:
			m_rsmiPrint.Click += Print_Click; // Print(Split Button)
			m_rbtnPrint.Click += Print_Click; // Print (Drop Down Button)
			m_rbtnPrintQuick.Click += PrintQuick_Click; // Print Quick (Drop Down Button)
			m_txTextControl.PropertyChanged += TextControl_PropertyChanged_Print; // Add handler for the print buttons Enable state handling
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------
        ** MainWindow_Loaded Handler 
        ** Sets the requested behavior for all added controls.
        **-----------------------------------------------------------------------------------------------------*/
		private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
			// Main Window:
			UpdateMainWindowCaption(); // Set caption

			// Application Menu items:
			SetAppMenuDesign();
			SetAppMenuBehavior();

			// QAT:
			SetQATItemsDesign();

			// Sidebars:
			SetSidebarBehavior();

			// Mini Toolbar
			m_txTextControl.ShowMiniToolbar = MiniToolbarButton.LeftButton | MiniToolbarButton.RightButton;

			// Contextual Tabs:
			SetContextualTabsBehavior();

			// Drag & Drop
			SetDragAndDropBehavior();

			// Toolbars:
			SetRulerBarsDesign();
			SetStatusBarDesign();

			// About:
			UpdateAboutSidebar();
		}

		/*-------------------------------------------------------------------------------------------------------
        ** TextControl_Loaded_MainWindow Handler 
        ** Sets the focus to the TextControl.
        **-----------------------------------------------------------------------------------------------------*/
		private void TextControl_Loaded_MainWindow(object sender, RoutedEventArgs e) {
			m_txTextControl.Focus();
		}

		/*-------------------------------------------------------------------------------------------------------------
		** MainWindow_Closing Handler
		** Saves the recent files to the Properties.Settings.Default.RecentFiles property.
		**-----------------------------------------------------------------------------------------------------------*/
		private void MainWindow_Closing(object sender, CancelEventArgs e) {
			// Save the recent files to the Properties.Settings.Default.RecentFiles property
			SaveRecentFiles();
		}
	}
}
