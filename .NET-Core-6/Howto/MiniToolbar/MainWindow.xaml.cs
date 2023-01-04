using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Media;

/*-------------------------------------------------------------------------------------------------------------
** MainWindow.xaml.cs File
**
** Description:
**		Sample project that is related to the 'Howto: Manipulate the MiniToolbar' article inside
**		the 'Windows Presentation Foundation User's Guide'.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
namespace MiniToolbar {
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
        ** TextControl_Loaded Handler
        ** Load the sample document.
        **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_Loaded(object sender, RoutedEventArgs e) {
			m_txTextControl.Load(@"Files\Sample.tx", TXTextControl.StreamType.InternalFormat);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** TextControl_TextMiniToolbarInitialized Handler
		** Modify the basic structure of the TextMiniToolbar.
		**-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_TextMiniToolbarInitalized(object sender, TXTextControl.WPF.MiniToolbarInitializedEventArgs e) {
			// Ensure that the TextMiniToolbar's table layout group won't be displayed if the input position is inside a table.
			e.MiniToolbar.Container.Children.Remove(e.MiniToolbar.Container.FindName(TXTextControl.WPF.TextMiniToolbar.RibbonItem.TXITEM_TableLayoutGroup.ToString()) as UIElement);

			// Add a ribbon group separator.
			e.MiniToolbar.Container.Children.Add(CreateRibbonGroupSeperator(3));

			// Create and add a ribbon group to the TextMiniToolbar that provides an "Edit Hyperlink" button.
			e.MiniToolbar.Container.ColumnDefinitions.Add(new ColumnDefinition() {
				Width = GridLength.Auto
			});
			e.MiniToolbar.Container.Children.Add(CreateEditHyperlinkGroup(4));
		}

		/*-------------------------------------------------------------------------------------------------------------
		** TextControl_MiniToolbarOpening Handler
		** Update the TextMiniToolbar's content visibility.
		**-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_MiniToolbarOpening(object sender, TXTextControl.WPF.MiniToolbarOpeningEventArgs e) {
			// Check whether the opening mini tool bar is of type TextMiniToolbar
			if (e.MiniToolbar is TXTextControl.WPF.TextMiniToolbar) {
				e.MiniToolbar.Container.Children[2].Visibility = System.Windows.Visibility.Visible; // Ensure that the TextMiniToolbar's Styles group is always shown (even if the input position is inside a table)
				e.MiniToolbar.Container.Children[3].Visibility = // Ensure that the ribbon group's separator and...
				e.MiniToolbar.Container.Children[4].Visibility =            // ... the "Edit Hyperlink" group are displayed if...

				(e.MiniToolbarContext & TXTextControl.ContextMenuLocation.TextField) == TXTextControl.ContextMenuLocation.TextField && // ... the current context is TextField and ...
				m_txTextControl.HypertextLinks.GetItem() != null ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; // ... the text field is of type TXTextControl.HypertextLink.
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
		** EditHyperlink_Click Handler
		** Opens the TXTextControl HyperlinkDialog.
		**-----------------------------------------------------------------------------------------------------------*/
		private void EditHyperlink_Click(object sender, RoutedEventArgs e) {
			TXTextControl.WPF.HyperlinkDialog dlgHyperlinkDialog = new TXTextControl.WPF.HyperlinkDialog(m_txTextControl);
			dlgHyperlinkDialog.Owner = this;
			dlgHyperlinkDialog.ShowDialog();
		}


		/*-------------------------------------------------------------------------------------------------------------
		** M E T H O D S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** CreateRibbonGroupSeperator Method
		** Creates a ribbon group separator.
		**-----------------------------------------------------------------------------------------------------------*/
		protected RibbonSeparator CreateRibbonGroupSeperator(int column) {
			// Create ribbon separator
			RibbonSeparator rsRibbonGroupSeperator = new RibbonSeparator();
			rsRibbonGroupSeperator.LayoutTransform = new RotateTransform(90);
			rsRibbonGroupSeperator.Margin = new Thickness(2);
			rsRibbonGroupSeperator.BorderThickness = new Thickness(5);

			// Set row and column
			Grid.SetRow(rsRibbonGroupSeperator, 0);
			Grid.SetRowSpan(rsRibbonGroupSeperator, 3);
			Grid.SetColumn(rsRibbonGroupSeperator, column);

			return rsRibbonGroupSeperator;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** CreateEditHyperlinkGroup Method
		** Create a Grid that contains an Edit Hyperlink button.
		**
		** Parameters:
		**		column		The column where to add the Grid.
		**
		** Returns:			The created Grid.	
		**-----------------------------------------------------------------------------------------------------------*/
		private Grid CreateEditHyperlinkGroup(int column) {
			// Create a ribbon group (represented by a Grid) that contains... 
			Grid rgEditHyperlinkGroup = new Grid();
			Grid.SetRow(rgEditHyperlinkGroup, 0);
			Grid.SetRowSpan(rgEditHyperlinkGroup, 3);
			Grid.SetColumn(rgEditHyperlinkGroup, column);

			rgEditHyperlinkGroup.ColumnDefinitions.Add(new ColumnDefinition() {
				Width = GridLength.Auto
			});

			// ... a button to open the TextControl Edit HyperlinkDialog
			RibbonButton rbtnEditHyperlinkButton = new RibbonButton() {
				Label = "Edit Hyperlink"
			};
			Grid.SetRow(rbtnEditHyperlinkButton, 0);
			Grid.SetColumn(rbtnEditHyperlinkButton, 0);

			rbtnEditHyperlinkButton.LargeImageSource = TXTextControl.WPF.ResourceProvider.GetLargeIcon(TXTextControl.WPF.RibbonInsertTab.RibbonItem.TXITEM_InsertHyperlink.ToString(), this);
			rbtnEditHyperlinkButton.Click += EditHyperlink_Click;

			// Add the edit hyperlink button to group.
			rgEditHyperlinkGroup.Children.Add(rbtnEditHyperlinkButton);

			return rgEditHyperlinkGroup;
		}
	}
}
