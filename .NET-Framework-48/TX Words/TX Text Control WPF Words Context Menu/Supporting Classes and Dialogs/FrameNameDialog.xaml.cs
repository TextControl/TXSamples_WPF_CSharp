/*-------------------------------------------------------------------------------------------------------------
** FrameNameDialog.xaml.cs File
**
** Description:
**     Provides a dialog to to edit the name of a frame.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;

namespace TXTextControl.Words {
	/// <summary>
	/// Interaction logic for FrameNameDialog.xaml
	/// </summary>
	public partial class FrameNameDialog : Window {
		/*-------------------------------------------------------------------------------------------------------------
        ** C O N S T R U C T O R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** FrameNameDialog Constructor
        ** Opens the dialog to edit the name of a frame.
        **
        ** Parameters:
        **      frameName:   The current name of the frame.
        **-----------------------------------------------------------------------------------------------------------*/
		internal FrameNameDialog(string frameName) {
			InitializeComponent();
			m_tbxFrameName.Text = frameName; // Set the text box text.
			m_tbxFrameName.Focus();
		}


		/*-------------------------------------------------------------------------------------------------------------
		** P R O P E R T I E S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** FrameName Property
        ** Returns the text of the frame name text box.
        **-----------------------------------------------------------------------------------------------------------*/
		internal string FrameName { get { return m_tbxFrameName.Text; } }


		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** OK_Click Handler
        ** Closes the dialog by setting the DialogResult property to true.
        **-----------------------------------------------------------------------------------------------------------*/
		private void OK_Click(object sender, RoutedEventArgs e) {
			this.DialogResult = true;
		}
	}
}
