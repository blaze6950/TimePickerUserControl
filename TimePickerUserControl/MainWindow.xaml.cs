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

namespace TimePickerUserControl
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

        private void TimePickerUC_OnValueChanged(object sender, RoutedEventArgs e)
        {
            this.Title = ((TimePickerUC)sender).Value.ToString();
            //MessageBox.Show(((ValueChangedRoutedEventArghs) e).Value.ToString(), "info");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var buf = TimePickerUc.Hours;
        }
    }
}
