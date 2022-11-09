using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Diplom
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            firstMethod.Visibility = Visibility.Visible;
            secondMethod.Visibility = Visibility.Collapsed;
            thirdMethod.Visibility = Visibility.Collapsed;
            BtnM1.BorderBrush = new SolidColorBrush(Colors.LightSkyBlue);
            BtnM2.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM3.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            firstMethod.Visibility=Visibility.Collapsed;
            secondMethod.Visibility=Visibility.Visible;
            thirdMethod.Visibility=Visibility.Collapsed;
            BtnM1.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM2.BorderBrush = new SolidColorBrush(Colors.LightSkyBlue);
            BtnM3.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            firstMethod.Visibility= Visibility.Collapsed;
            secondMethod.Visibility = Visibility.Collapsed;
            thirdMethod.Visibility = Visibility.Visible;
            BtnM1.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM2.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM3.BorderBrush = new SolidColorBrush(Colors.LightSkyBlue);
        }
    }
}
