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
using System.Xml;
using System.Xml.Linq;

namespace Diplom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Object[] addOres;#
        private int counter = 0;
        private Dictionary<int, object> addOres = new Dictionary<int, object>();
        private byte methodnum = 1;
        public MainWindow()
        {
            
            InitializeComponent();
            //addOres[0] = AOG1;
            //addOres[1] = AOG2;
        }

        //Simple Check for Textbox values to make sure input is correct
        private void isNumber(object sender, KeyEventArgs e)
        {
            //Check if it is spacebar key
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            //check if it is not number or control keys (delete, backspace, left, right, delete)
            e.Handled = !checkBothNumberAndNumpad(e.Key) && !checkControlKeys(e.Key);
            base.OnPreviewKeyDown(e);
        }
        private bool checkBothNumberAndNumpad(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }
        private bool checkControlKeys(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Left || inKey == Key.Right || inKey == Key.Delete;
        }

        

        //This method will return symbol instead of string for comparison
        private string ConditionStringToSymbol(string textValue)
        {

            switch (textValue)
            {
                case "больше":
                    return ">";
                    break;
                case "больше или равно":
                    return ">=";
                    break;
                case "меньше":
                    return "<";
                    break;
                case "меньше или равно":
                    return "<=";
                    break;
                case "равно":
                    return "==";
                    break;
                case "неравное":
                    return "!=";
                    break;
                default:
                    return null;
                    break;
            }
            //return null;
        }
        //This method will cook an expression ala valueOne[oneOfSymbols:'>','>=','<','<=','==','!=']valueTwo?(resultWhenTrue):(resultWhenFalse)
        private string makeAnExpression(string val1, string val2, string textValue, string resTrue, string resFalse)
        {
            return val1 + " " + ConditionStringToSymbol(textValue) + " " + val2 + " ? (" + resTrue + ") : (" + resFalse + ")";
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
                    //resultQuery.Text = thirdM();
                    break;
                default:
                    break;
            }
            
        }
        //Methods realisation 
        private string firstM()
        {
            //string parcel = "N(\"" + oreNameM1.Text + "\") > " + oreValueM1.Text + " ? \"" + firstResultM1.Text + "\" : \"" + secondResultM1.Text + "\"";
            
            try
            {
                string selectedListBoxItem_TextValue = ((ListBoxItem)MethodOneList.SelectedItem).Content.ToString();
                string valueOne = "N(\"" + oreNameM1.Text + "\")";
                string valueTwo = oreValueM1.Text;
                string resultTrue = "\"" + firstResultM1.Text + "\"";
                string resultFalse = "\"" + secondResultM1.Text + "\"";
                string parcel = makeAnExpression(valueOne, valueTwo, selectedListBoxItem_TextValue, resultTrue, resultFalse);
                return parcel;
            }
            catch (NullReferenceException e)
            {
                return "Error "+ "\"NullReferenceException\"" + "!\nSelect Condition before start!!!";
            }
        }
        private string secondM()
        {
            //string parcel = "N(\"" + oreNameM2.Text + "\") > " + oreValue1M2.Text + " ? \"" + oreResult1M2.Text + "\" : " + "N(\"" + oreNameM2.Text + "\") > " + oreValue2M2.Text + " ? \"" + oreResult2M2.Text + "\" : " + "N(\"" + oreNameM2.Text + "\") > " + oreValue3M2.Text + " ? \"" + oreResult3M2.Text + "\" : " + "N(\"" + oreNameM2.Text + "\") > " + oreValue4M2.Text + " ? \"" + oreResult4M2.Text + "\" : " + "N(\"" + oreNameM2.Text + "\") > " + oreValue5M2.Text + " ? \"" + oreResult5M2.Text + "\" : \"" + oreResult6M2.Text + "\"";
            try
            {
                //int[] array3 = { 1, 2, 3, 4, 5, 6 };
                string[] selectedListBoxItem_TextValues = { 
                    ((ListBoxItem)MTL1.SelectedItem).Content.ToString(),
                    ((ListBoxItem)MTL2.SelectedItem).Content.ToString(),
                    ((ListBoxItem)MTL3.SelectedItem).Content.ToString(),
                    ((ListBoxItem)MTL4.SelectedItem).Content.ToString(),
                    ((ListBoxItem)MTL5.SelectedItem).Content.ToString()
                };
                string valueOne = "N(\"" + oreNameM2.Text + "\")";
                string[] valuesTwo = {
                    oreValue1M2.Text,//0
                    oreValue2M2.Text,//1
                    oreValue3M2.Text,//2
                    oreValue4M2.Text,//3
                    oreValue5M2.Text,//4
                };
                string[] results =
                {
                    oreResult1M2.Text,//0
                    oreResult2M2.Text,//1
                    oreResult3M2.Text,//2
                    oreResult4M2.Text,//3
                    oreResult5M2.Text,//4
                    oreResult6M2.Text,//5
                };
                string stroka = makeAnExpression(valueOne, valuesTwo[4], selectedListBoxItem_TextValues[4],results[4], results[5]);
                for (int i=valuesTwo.Length-2; i>-1; i--)
                {
                    stroka = makeAnExpression(valueOne, valuesTwo[i], selectedListBoxItem_TextValues[i], results[i], stroka);
                }
                //stroka = makeAnExpression(valueOne, valuesTwo[0])
                return stroka;
            }
            catch (NullReferenceException e)
            {
                return "Error " + "\"NullReferenceException\"" + "!\nSelect ALL Conditions before start!!!";
            }
        }
        /*private string thirdM()
        {
            string p3 = "N(\"" + oreAdd2NameM3.Text + "\") > " + oreAdd2ValueM3.Text + " ? " + "\"" + oreAddResult2M3.Text + "\" : \"" + oreAddResult1M3.Text + "\"";
            string p2 = "N(\"" + oreAdd2NameM3.Text + "\") > " + oreAdd2ValueM3.Text + " ? " + "\"" + oreAddResult4M3.Text + "\" : \"" + oreAddResult3M3.Text + "\"";
            string p1 = "N(\""+oreAdd1NameM3.Text+ "\") > "+oreAdd1ValueM3.Text+" ? "+(p2)+" : "+(p3);
            string parcel = "N(\"" + oreMainNameM3.Text + "\") > " + oreValue1M3.Text + " ? " +(p1)+" : " + "N(\"" + oreMainNameM3.Text + "\") > " + oreValue2M3.Text + " ? \"" + oreResult2M3.Text + "\" : " + "N(\"" + oreMainNameM3.Text + "\") > " + oreValue3M3.Text + " ? \"" + oreResult3M3.Text + "\" : " + "N(\"" + oreMainNameM3.Text + "\") > " + oreValue4M3.Text + " ? \"" + oreResult4M3.Text + "\" : \""+oreResult5M3.Text + "\"";
            return parcel;
        }*/

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, resultQuery.Text);
        }

        private void AddOreBtn_Click(object sender, RoutedEventArgs e)
        {
            AddOreToMethod3();
            counter++;
        }

        private void AddOreToMethod3()
        {
            Grid addOreGrid = new Grid();
            Label addOreNameLabel = new Label();
            TextBox addOreNameText = new TextBox();
            Label addOreValueLabel = new Label();
            TextBox addOreValueText = new TextBox();
            Label   addOreCondLabel = new Label();
            ListBox addOreCondList = new ListBox();
          //StackPanel stackPanel = new StackPanel();
          //StackPanel stackPanel_list = new StackPanel();
          //ListBoxItem listBoxItem = new ListBoxItem();

            //place items inside listbox
            addOreCondList.Items.Add("больше");
            addOreCondList.Items.Add("больше или равно");
            addOreCondList.Items.Add("меньше");
            addOreCondList.Items.Add("меньше или равно");
            addOreCondList.Items.Add("равно");
            addOreCondList.Items.Add("неравное");
            //fill elemts' properties
            addOreNameLabel.Content = "Руда";
            addOreNameLabel.Height = 25;
            addOreNameLabel.Width = 120;
            addOreNameLabel.Margin = new Thickness(0, 0, 0, 0);
            addOreNameLabel.HorizontalAlignment = HorizontalAlignment.Left;
            addOreNameLabel.VerticalAlignment = VerticalAlignment.Top;
            
            addOreValueLabel.Content = "Значение";
            addOreValueLabel.Height = 25;
            addOreValueLabel.Width = 75;
            addOreValueLabel.Margin = new Thickness(135, 0, 0, 0);
            addOreValueLabel.HorizontalAlignment = HorizontalAlignment.Left;
            addOreValueLabel.VerticalAlignment = VerticalAlignment.Top;

            addOreCondLabel.Content = "Условие";
            addOreCondLabel.Height = 25;
            addOreCondLabel.Width = 120;
            addOreCondLabel.Margin = new Thickness(210,0, 0, 0);
            addOreCondLabel.HorizontalAlignment = HorizontalAlignment.Left;
            addOreCondLabel.VerticalAlignment = VerticalAlignment.Top;

            addOreNameText.Name = "AON" + counter;
            addOreNameText.Height = 25;
            addOreNameText.Width = 120;
            addOreNameText.Margin = new Thickness(0, 35, 0, 0);
            addOreNameText.HorizontalAlignment = HorizontalAlignment.Left;
            addOreNameText.VerticalAlignment = VerticalAlignment.Top;

            addOreValueText.Name = "AOV" + counter;
            addOreValueText.Height = 25;
            addOreValueText.Width = 65;
            addOreValueText.Margin = new Thickness(135, 35, 0, 0);
            addOreValueText.HorizontalAlignment = HorizontalAlignment.Left;
            addOreValueText.VerticalAlignment = VerticalAlignment.Top;
            addOreValueText.PreviewKeyDown += new KeyEventHandler(isNumber);

            addOreCondList.Name = "AOC" + counter;
            addOreCondList.Height = 25;
            addOreCondList.Width = 140;
            addOreCondList.Margin = new Thickness(210, 35, 0, 0);
            addOreCondList.HorizontalAlignment = HorizontalAlignment.Left;
            addOreCondList.VerticalAlignment = VerticalAlignment.Top;

            //step by step place all elements inside
            addOreGrid.Children.Add(addOreNameLabel);
            addOreGrid.Children.Add(addOreNameText);
            addOreGrid.Children.Add(addOreValueLabel);
            addOreGrid.Children.Add(addOreValueText);
            addOreGrid.Children.Add(addOreCondLabel);
            addOreGrid.Children.Add(addOreCondList);
            //set up add Ore Grid then 
            addOreGrid.Name = "AOG" + counter;
            addOreGrid.Margin = new Thickness(0, 30 + 90 * counter, 0, 0);
            addOreGrid.Height = 90;
            //addOreGrid.Width=320;
            addOreGrid.HorizontalAlignment= HorizontalAlignment.Left;
            addOreGrid.VerticalAlignment= VerticalAlignment.Top;
            //then place this stack inside AddOre grid as children
            AddOre.Children.Add(addOreGrid);
            //AddOre.Children.Add();
        }
        /*
private void TextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
{
TextBox txtBx = sender as TextBox;//This looks like it will be needed, so even though code is not working I leave it for the time being
txtBx.Text = "";
}*/
    }
}
