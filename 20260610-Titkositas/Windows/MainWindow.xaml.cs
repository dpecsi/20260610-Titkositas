using _20260610_Titkositas.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20260610_Titkositas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CryptionButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenForCryptionWindow fileOpenForCryptionWindow = new FileOpenForCryptionWindow();
            fileOpenForCryptionWindow.Owner = this;
            fileOpenForCryptionWindow.Password = this.PasswordTextBox.Text;
            fileOpenForCryptionWindow.ShowDialog();
        }

        private void DecryptionButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenForDecryptionWindow fileOpenForDecryptionWindow = new FileOpenForDecryptionWindow();
            fileOpenForDecryptionWindow.Owner = this;
            fileOpenForDecryptionWindow.Password = this.PasswordTextBox.Text;
            fileOpenForDecryptionWindow.ShowDialog();
        }
    }
}