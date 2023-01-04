/*------------------------------------------------------------------------------------------------
** program:			TX Text Control Simple Sample
** description:	A Simple Sample to show the basic functionality of TX Text Control.						
**
** copyright:		© Text Control GmbH
**----------------------------------------------------------------------------------------------*/
using System.Windows;

namespace Simple {
    // Interaction logic for Window1.xaml
    public partial class WindowMain : Window {

        public WindowMain() {
            InitializeComponent();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) {
            textControl1.Load();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {
            textControl1.Save();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            textControl1.SectionFormatDialog(0);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
     
        private void MenuItem_Click_4(object sender, RoutedEventArgs e) {
            textControl1.Cut();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e) {
            textControl1.Copy();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e) {
            textControl1.Paste();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e) {
            textControl1.FontDialog();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e) {
            textControl1.ParagraphFormatDialog();
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e) {
            textControl1.SectionFormatDialog(2);
        }

        private void textControl1_Loaded(object sender, RoutedEventArgs e) {
            textControl1.Focus();
        }
        
    }
}
