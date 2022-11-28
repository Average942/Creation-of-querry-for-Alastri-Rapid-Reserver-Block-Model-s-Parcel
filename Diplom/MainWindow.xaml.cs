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
        private byte methodnum = 1;
        public MainWindow()
        {
            InitializeComponent();
        }
        //Next three methods are for hiding and showing different input fields
        ///based on which button user clicks = which method they need
        ////made realy sloopy but so what? It works :p
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            firstMethod.Visibility = Visibility.Visible;
            secondMethod.Visibility = Visibility.Collapsed;
            thirdMethod.Visibility = Visibility.Collapsed;
            BtnM1.BorderBrush = new SolidColorBrush(Colors.LightSkyBlue);
            BtnM2.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM3.BorderBrush = new SolidColorBrush(Colors.Gray);
            methodnum = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            firstMethod.Visibility=Visibility.Collapsed;
            secondMethod.Visibility=Visibility.Visible;
            thirdMethod.Visibility=Visibility.Collapsed;
            BtnM1.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM2.BorderBrush = new SolidColorBrush(Colors.LightSkyBlue);
            BtnM3.BorderBrush = new SolidColorBrush(Colors.Gray);
            methodnum = 2;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            firstMethod.Visibility= Visibility.Collapsed;
            secondMethod.Visibility = Visibility.Collapsed;
            thirdMethod.Visibility = Visibility.Visible;
            BtnM1.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM2.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM3.BorderBrush = new SolidColorBrush(Colors.LightSkyBlue);
            methodnum = 3;
        }
        //To decide which method we make
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            switch (methodnum)
            {
                case 1:
                    resultQuery.Text = firstM();
                    break;
                case 2:
                    resultQuery.Text = secondM();
                    break;
                case 3:
                    resultQuery.Text = thirdM();
                    break;
                default:
                    break;
            }
        }
        //Methods realisation 
        private string firstM()
        {
            string parcel = "N(\"" + oreNameM1.Text + "\") > " + oreValueM1.Text + " ? \"" + firstResultM1.Text + "\" : \"" + secondResultM1.Text + "\"";
            return parcel;
        }
        private string secondM()
        {
            string parcel = "N(\"" + oreNameM2.Text + "\") > " + oreValue1M2.Text + " ? \"" + oreResult1M2.Text + "\" : " + "N(\"" + oreNameM2.Text + "\") > " + oreValue2M2.Text + " ? \"" + oreResult2M2.Text + "\" : " + "N(\"" + oreNameM2.Text + "\") > " + oreValue3M2.Text + " ? \"" + oreResult3M2.Text + "\" : " + "N(\"" + oreNameM2.Text + "\") > " + oreValue4M2.Text + " ? \"" + oreResult4M2.Text + "\" : " + "N(\"" + oreNameM2.Text + "\") > " + oreValue5M2.Text + " ? \"" + oreResult5M2.Text + "\" : \"" + oreResult6M2.Text + "\"";
            return parcel;
        }
        private string thirdM()
        {
            string p3 = "N(\"" + oreAdd2NameM3.Text + "\") > " + oreAdd2ValueM3.Text + " ? " + "\"" + oreAddResult2M3.Text + "\" : \"" + oreAddResult1M3.Text + "\"";
            string p2 = "N(\"" + oreAdd2NameM3.Text + "\") > " + oreAdd2ValueM3.Text + " ? " + "\"" + oreAddResult4M3.Text + "\" : \"" + oreAddResult3M3.Text + "\"";
            string p1 = "N(\""+oreAdd1NameM3.Text+ "\") > "+oreAdd1ValueM3.Text+" ? "+(p2)+" : "+(p3);
            string parcel = "N(\"" + oreMainNameM3.Text + "\") > " + oreValue1M3.Text + " ? " +(p1)+" : " + "N(\"" + oreMainNameM3.Text + "\") > " + oreValue2M3.Text + " ? \"" + oreResult2M3.Text + "\" : " + "N(\"" + oreMainNameM3.Text + "\") > " + oreValue3M3.Text + " ? \"" + oreResult3M3.Text + "\" : " + "N(\"" + oreMainNameM3.Text + "\") > " + oreValue4M3.Text + " ? \"" + oreResult4M3.Text + "\" : \""+oreResult5M3.Text + "\"";
            return parcel;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, resultQuery.Text);
        }
        /*
private void TextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
{
TextBox txtBx = sender as TextBox;//This looks like it will be needed, so even though code is not working I leave it for the time being
txtBx.Text = "";
}*/
    }
}
