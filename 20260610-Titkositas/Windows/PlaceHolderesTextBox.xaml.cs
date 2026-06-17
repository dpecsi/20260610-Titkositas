using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20260610_Titkositas.Windows
{
    /// <summary>
    /// Interaction logic for PlaceHolderesTextBox.xaml
    /// </summary>
    public partial class PlaceHolderesTextBox : UserControl
    {
        public PlaceHolderesTextBox()
        {
            InitializeComponent();
        }

        private void UserInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UserInput.Text == string.Empty)
            {
                UserInput.Visibility = Visibility.Collapsed;
                PlaceholderInput.Visibility = Visibility.Visible;
            }
        }

        private void PlaceholderInput_GotFocus(object sender, RoutedEventArgs e)
        {
            UserInput.Visibility = Visibility.Visible;
            PlaceholderInput.Visibility = Visibility.Collapsed;
            UserInput.Focus();
        }
    }
}
