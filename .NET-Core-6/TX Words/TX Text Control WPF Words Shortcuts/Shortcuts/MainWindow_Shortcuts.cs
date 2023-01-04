/*-------------------------------------------------------------------------------------------------------------
** MainWindow_Shortcuts.cs File
**
** Description:
**     Handles shortcuts.
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows.Input;

namespace TXTextControl.Words {
	partial class MainWindow {

		/*-------------------------------------------------------------------------------------------------------------
        ** H A N D L E R S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
	    ** TextControl_KeyDown Handler
	    ** Implement Shortcuts for the TextControl.
	    **-----------------------------------------------------------------------------------------------------------*/
		private void TextControl_KeyDown(object sender, KeyEventArgs e) {
			switch (e.Key) {
				case Key.Insert:   // Toggle insertion mode					
					if (Keyboard.Modifiers == ModifierKeys.None) {
						ToggleInsertionMode();
					}
					break;

				case Key.A:        // Ctrl + A: Select all
					if (Keyboard.Modifiers == ModifierKeys.Control) {
						m_txTextControl.SelectAll();
					}
					break;

				case Key.S:        // Ctrl + S: Save
					if (Keyboard.Modifiers == ModifierKeys.Control) {
						Save(m_strActiveDocumentPath);
					}
					break;

				case Key.O:        // Ctrl + O: Open
					if (Keyboard.Modifiers == ModifierKeys.Control) {
						Open();
					}
					break;

				case Key.F:        // Ctrl + F: Search
					if (Keyboard.Modifiers == ModifierKeys.Control) {
						m_txTextControl.Find();
					}
					break;

				case Key.P:        // Ctrl + P: Print
					if (Keyboard.Modifiers == ModifierKeys.Control) {
						if (m_txTextControl.CanPrint) {
							m_txTextControl.Print(m_strActiveDocumentName, true);
						}
					}
					break;
			}
		}


		/*-------------------------------------------------------------------------------------------------------------
        ** M E T H O D S
        **-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** SetShortCutBehavior Method
		** Adds all necessary handlers to implement short cut behavior.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetShortCutBehavior() {
			m_txTextControl.KeyDown += TextControl_KeyDown; // Handles shortcuts
		}

		/*-------------------------------------------------------------------------------------------------------------
		** ToggleInsertionMode Method
		** Switch the TextControl's insertion mode between overwrite and insert.
		**-----------------------------------------------------------------------------------------------------------*/
		private void ToggleInsertionMode() {
			m_txTextControl.InsertionMode
				= m_txTextControl.InsertionMode == TXTextControl.InsertionMode.Insert
				? TXTextControl.InsertionMode.Overwrite
				: TXTextControl.InsertionMode.Insert;
		}
	}
}
