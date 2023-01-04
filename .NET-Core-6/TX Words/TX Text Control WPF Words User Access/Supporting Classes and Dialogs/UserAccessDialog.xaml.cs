/*-------------------------------------------------------------------------------------------------------------
** UserAccessDialog.xaml.cs File
**
** Description:
**     Provides a dialog to create a new user account, sign in a user or edit a user account.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;

namespace TXTextControl.Words {
	/// <summary>
	/// Interaction logic for UserAccessDialog.xaml
	/// </summary>
	public partial class UserAccessDialog : Window {
		/*-------------------------------------------------------------------------------------------------------------
        ** E N U M S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** DialogBehaviors Enum
        ** Represents three kinds of behavior how the dialog might act.
        **-----------------------------------------------------------------------------------------------------------*/
		internal enum DialogBehaviors {
			CreateAccount,      // The dialog is opened to create a new user account.
			SignIn,             // The dialog is opened to sign in a user.
			AccountSettings     // The dialog is opened to edit the signed in user's account.
		}


		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/

		private readonly UserInfo m_uiUserInfo = null;
		private readonly string m_strUserName = null;
		private readonly DialogBehaviors m_dbDialogBehavior = DialogBehaviors.CreateAccount;
		private bool m_bDeletePassword = false;


		/*-------------------------------------------------------------------------------------------------------------
        ** C O N S T R U C T O R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** UserAccessDialog Constructor
        ** Opens the dialog to sign in a user or to edit the signed in user's account settings.
        **
        ** Parameters:
        **      userInfo:   The UserInfo instance of the user to be handled.
        **-----------------------------------------------------------------------------------------------------------*/
		internal UserAccessDialog(UserInfo userInfo) {
			InitializeComponent();
			m_uiUserInfo = userInfo;

			// Set the user name
			m_lblUserName.Content = m_strUserName = m_uiUserInfo.Name;

			// Check whether to sign in the user or edit its account settings.
			if (m_uiUserInfo.IsSignedIn) {
				// Edit user account settings.
				m_dbDialogBehavior = DialogBehaviors.AccountSettings;

				// Update controls texts
				this.Title = Properties.Resources.UserAccessDialog_AccountSettings_Caption;

				m_lblPassword.Content = Properties.Resources.UserAccessDialog_AccountSettings_OldPassword;
				m_lblNewPassword.Content = Properties.Resources.UserAccessDialog_AccountSettings_NewPassword;
				m_lblConfirmPassword.Content = Properties.Resources.UserAccessDialog_AccountSettings_ConfirmPassword;

				// Buttons
				m_btnDelete.Visibility = Visibility.Visible;
			}
			else {
				// Otherwise the user is known but not signed in.
				m_dbDialogBehavior = DialogBehaviors.SignIn;

				// Hide 'New password' and 'Confirm password' controls
				m_lblNewPassword.Visibility =
				m_tbxNewPassword.Visibility =
				m_lblConfirmPassword.Visibility =
				m_tbxConfirmPassword.Visibility = Visibility.Collapsed;

				// Update controls texts
				this.Title = Properties.Resources.UserAccessDialog_SignIn_Caption;
				m_lblPassword.Content = Properties.Resources.UserAccessDialog_SignIn_Password;
			}
			m_tbxPassword.Focus();
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** UserAccessDialog Constructor
        ** Opens the dialog to create a user account.
        **
        ** Parameters:
        **      userName:   The name of the user to create an account for.
        **-----------------------------------------------------------------------------------------------------------*/
		public UserAccessDialog(string userName) {
			InitializeComponent();

			// Set the user name
			m_lblUserName.Content = m_strUserName = userName;

			// A new account should be created.
			m_dbDialogBehavior = DialogBehaviors.CreateAccount;

			// Hide password controls
			m_lblPassword.Visibility =
			m_tbxPassword.Visibility = Visibility.Collapsed;

			// Update control texts
			this.Title = Properties.Resources.UserAccessDialog_CreateAccount_Caption;
			m_lblNewPassword.Content = Properties.Resources.UserAccessDialog_CreateAccount_NewPassword;
			m_lblConfirmPassword.Content = Properties.Resources.UserAccessDialog_CreateAccount_ConfirmPassword;
			m_tbxNewPassword.Focus();
		}


		/*-------------------------------------------------------------------------------------------------------------
		** P R O P E R T I E S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** UserInfo Property
        ** Returns an instance of a UserInfo class that represents the signed in user.
        **-----------------------------------------------------------------------------------------------------------*/
		internal UserInfo UserInfo {
			get
			{
				switch (m_dbDialogBehavior) {
					case DialogBehaviors.SignIn:
						m_uiUserInfo.IsSignedIn = true;
						return m_uiUserInfo;
					default:
						return new UserInfo(m_strUserName, m_tbxConfirmPassword.Password);
				}
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** DeletePassword Property
        ** Returns a value whether the password should be deleted.
        **-----------------------------------------------------------------------------------------------------------*/
		internal bool DeletePassword { get { return m_bDeletePassword; } }


		/*-------------------------------------------------------------------------------------------------------------
		** H A N D L E R S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** Password_TextChanged Handler
        ** Updates the IsEnabled states of the dialog controls when the text of a text box changed.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Password_TextChanged(object sender, RoutedEventArgs e) {
			EnableControls();
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** Delete_Click Handler
        ** Asks the user whether his user account should be deleted. In that case and if the 
        ** password is correct, the dialog is closed.
        **-----------------------------------------------------------------------------------------------------------*/
		private void Delete_Click(object sender, RoutedEventArgs e) {
			// Ask the user whether the current user account should be deleted.
			if (MessageBox.Show(this, Properties.Resources.MessageBox_UserAccessDialogDeleteAccount_Text, this.Title, MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK) {
				// Validate the password of the current signed in user.
				if (!m_uiUserInfo.ValidatePassword(m_tbxPassword.Password)) {
					MessageBox.Show(this, Properties.Resources.MessageBox_UserAccessDialogIncorrectPassword_Text, this.Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return;
				}
				// Close the dialog
				m_bDeletePassword = true;
				this.Close();
			}
		}

		/*-------------------------------------------------------------------------------------------------------------
        ** OK_Click Handler
        ** Validates the password when the OK button is clicked.
        **-----------------------------------------------------------------------------------------------------------*/
		private void OK_Click(object sender, RoutedEventArgs e) {
			// Validate the password of the current signed in user or the user to sign in.
			if (m_uiUserInfo != null && !m_uiUserInfo.ValidatePassword(m_tbxPassword.Password)) {
				// The password is not correct.
				MessageBox.Show(this, Properties.Resources.MessageBox_UserAccessDialogIncorrectPassword_Text, this.Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			this.DialogResult = true;
		}


		/*-------------------------------------------------------------------------------------------------------------
		** M E T H O D S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
        ** EnableControls Method
        ** Updates the IsEnabled states of the dialog controls when the text of a text box changed.
        **-----------------------------------------------------------------------------------------------------------*/
		private void EnableControls() {
			switch (m_dbDialogBehavior) {
				case DialogBehaviors.CreateAccount:
					// Enable/Disable the confirm password text 
					if (!(m_tbxConfirmPassword.IsEnabled = m_tbxNewPassword.Password.Length > 0)) {
						m_tbxConfirmPassword.Password = ""; // Reset the confirm password text box if it is disabled.
					}
					// Enable/Disable the OK button.
					m_btnOK.IsEnabled = m_tbxConfirmPassword.IsEnabled && m_tbxNewPassword.Password == m_tbxConfirmPassword.Password;
					break;
				case DialogBehaviors.SignIn:
					// Enable/Disable the OK button.
					m_btnOK.IsEnabled = m_tbxPassword.Password.Length > 0;
					break;
				case DialogBehaviors.AccountSettings:
					// Enable/Disable the confirm password text 
					if (!(m_tbxConfirmPassword.IsEnabled = m_tbxNewPassword.Password.Length > 0)) {
						m_tbxConfirmPassword.Password = "";  // Reset the confirm password text box if it is disabled.
					}
					// Enable/Disable the OK and Delete button.
					m_btnOK.IsEnabled = (m_btnDelete.IsEnabled = m_tbxPassword.Password.Length > 0) && m_tbxConfirmPassword.IsEnabled && m_tbxNewPassword.Password == m_tbxConfirmPassword.Password;
					break;
			}
		}
	}
}
