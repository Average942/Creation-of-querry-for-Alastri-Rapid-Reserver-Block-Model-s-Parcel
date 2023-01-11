using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
        private int counter = 1;
        private Dictionary<int, object> addOres = new Dictionary<int, object>();
        //private List<string[]> aOGeneric = new List<string[]>();
        /*
        private List<string> aON = new List<string>();
        private List<string> aOV = new List<string>();
        private List<string> aOC = new List<string>();*/

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
                //break;
                case "больше или равно":
                    return ">=";
                //break;
                case "меньше":
                    return "<";
                //break;
                case "меньше или равно":
                    return "<=";
                //break;
                case "равно":
                    return "==";
                //break;
                case "неравное":
                    return "!=";
                //break;
                default:
                    return null;
                    //break;
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
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Width = 850;
            Application.Current.MainWindow.Height = 325;
            ResultGrid.Margin = new Thickness(18, 160, 0, 0);
            ResultGrid.Width = 800;
            Note.Visibility = Visibility.Collapsed;
            Copy.Margin = new Thickness(705, 35, 0, 0);
            resultQuery.Width = 700;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            firstMethod.Visibility = Visibility.Collapsed;
            secondMethod.Visibility = Visibility.Visible;
            thirdMethod.Visibility = Visibility.Collapsed;
            BtnM1.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM2.BorderBrush = new SolidColorBrush(Colors.LightSkyBlue);
            BtnM3.BorderBrush = new SolidColorBrush(Colors.Gray);
            methodnum = 2;
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Width = 1325;
            Application.Current.MainWindow.Height = 325;
            ResultGrid.Margin = new Thickness(18, 160, 0, 0);
            ResultGrid.Width = 1275;
            Note.Visibility = Visibility.Collapsed;
            Copy.Margin = new Thickness(1175, 35, 0, 0);
            resultQuery.Width = 1170;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            firstMethod.Visibility = Visibility.Collapsed;
            secondMethod.Visibility = Visibility.Collapsed;
            thirdMethod.Visibility = Visibility.Visible;
            BtnM1.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM2.BorderBrush = new SolidColorBrush(Colors.Gray);
            BtnM3.BorderBrush = new SolidColorBrush(Colors.LightSkyBlue);
            methodnum = 3;
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Width = 1325;
            Application.Current.MainWindow.Height = 400;
            ResultGrid.Margin = new Thickness(18, 235, 0, 0);
            ResultGrid.Width = 1275;
            Note.Visibility = Visibility.Visible;
            Copy.Margin = new Thickness(1175, 35, 0, 0);
            resultQuery.Width = 1170;
        }
        //To decide which method we make
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            resultQuery.Foreground = Brushes.Black;
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
                resultQuery.Foreground = Brushes.Red;
                return "Ошибка!\nВыберите условие перед Стартом";
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
                string stroka = makeAnExpression(valueOne, valuesTwo[4], selectedListBoxItem_TextValues[4], results[4], results[5]);
                for (int i = valuesTwo.Length - 2; i > -1; i--)
                {
                    stroka = makeAnExpression(valueOne, valuesTwo[i], selectedListBoxItem_TextValues[i], results[i], stroka);
                }
                //stroka = makeAnExpression(valueOne, valuesTwo[0])
                return stroka;
            }
            catch (NullReferenceException e)
            {
                resultQuery.Foreground = Brushes.Red;
                return "Ошибка!\nВыберите ВСЕ условия перед Стартом";
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
        private string thirdM()
        {
            try
            {
                string[] Add_Ore_Name = new string[counter];
                string[] Add_Ore_Value = new string[counter];
                string[] Add_Ore_Condition = new string[counter];
                Add_Ore_Name[0] = "N";
                Add_Ore_Value[0] = "N";
                Add_Ore_Condition[0] = "N";
                for (int i = 0; i < counter; i++)
                {
                    string[] GetInput = GetAddOresInput(i);
                    Add_Ore_Name[i] = GetInput[0];
                    Add_Ore_Value[i] = GetInput[1];//Convert.ToDouble(GetInput[1]);
                    Add_Ore_Condition[i] = ConditionStringToSymbol(GetInput[2]);
                    //...if correct, should set input iside arrays then we need to make code with these values and return it
                    /*//Example of Convertation array to string for print
                    string s = "{" + string.Join(", ", mainOreControlValues) + "}";//nums
                    Console.WriteLine(s);
                    s = "{\"" + string.Join("\", \"", gradeOre) + "\"}";//chrs
                    Console.WriteLine(s);*/
                }

                //Get Input:
                string grades = "{\"" + oreResult1M3.Text + "\", \"" + oreResult2M3.Text + "\", \"" + oreResult3M3.Text + "\", \"" + oreResult4M3.Text + "\", \"" + oreResult5M3.Text + "\", \"" + oreResult6M3.Text + "\"}";

                string mControlV = "{" + oreValue1M3.Text + ", " + oreValue2M3.Text + ", " + oreValue2M3.Text + ", " + oreValue4M3.Text + ", " + oreValue5M3.Text + "}";
                string mConditions = "{\"" + ConditionStringToSymbol(((ListBoxItem)MMTL1.SelectedItem).Content.ToString()) + "\", \"" + ConditionStringToSymbol(((ListBoxItem)MMTL2.SelectedItem).Content.ToString()) + "\", \"" + ConditionStringToSymbol(((ListBoxItem)MMTL3.SelectedItem).Content.ToString()) + "\", \"" + ConditionStringToSymbol(((ListBoxItem)MMTL4.SelectedItem).Content.ToString()) + "\", \"" + ConditionStringToSymbol(((ListBoxItem)MMTL5.SelectedItem).Content.ToString()) + "\"}";

                string addNames = "{context.N(\"" + string.Join("\"), context.N(\"", Add_Ore_Name) + "\")}";
                string addValues = "{" + string.Join(", ", Add_Ore_Value) + "}";
                string addConditions = "{\"" + string.Join("\", \"", Add_Ore_Condition) + "\"}";//\n
                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //Code Blocks
                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                string Method = "bool ConditionChecker(string symbol, double x, double y)\n{\n" + "\t" + "switch (symbol)\n{\n" + "\t\t" + "case \">\": return x > y" + ";\n" + "\t\t" + "case \">=\": return x >= y" + ";\n" + "\t\t" + "case \"<\": return x < y" + ";\n" + "\t\t" + "case \"<=\": return x <= y" + ";\n" + "\t\t" + "case \"==\": return x == y" + ";\n" + "\t\t" + "case \"!=\": return x != y" + ";\n" + "\t}\n}\n";

                string mainIni = "//Input Data\n" + "\tdouble mainOreValue = " + "context.N(\"" + oreNameM3.Text + "\")" + ";\n" + "\tstring[] gradeOre = new string[] " + grades + ";\n" + "\tdouble[] mainOreControlValues = new double[] " + mControlV + ";\n" + "\tstring[] mainConditions = new string[] " + mConditions + ";\n";
                string addIni = "int counter = " + counter + ";\n" + "\tdouble[] addOresValues = new double[] " + addNames + ";\n" + "\tdouble[] addOresControlValues = new double[] " + addValues + ";\n" + "\tstring[] addConditions = new string[] " + addConditions + ";\n";

                string mainControl = "int k = 0;\n" + "do\n{\n" + "\tk++;\n" + "}while" + "(!(ConditionChecker(mainConditions[k], mainOreValue, mainOreControlValues[k]))&&(k<(mainOreControlValues.Length-1)))" + ";\n" + "if ((k == (mainOreControlValues.Length - 1))&&!(ConditionChecker(mainConditions[k], mainOreValue, mainOreControlValues[k]))) k++;\n";
                string addControl = "for (int i = 0; i < counter; i++)\n{\n" + "\tif (ConditionChecker(addConditions[i], addOresValues[i], addOresControlValues[i]))k--;\n" + "}\n";
                string output = "return gradeOre[k];";
                //
                //Assembling Code
                string ModelMaket = "using System;\nusing System.Collections.Generic;\nusing System.Text;\nusing System.Linq;\nusing Alastri.Scripting;\nusing Alastri.BlockModel.Engine.CustomVariables;\n\npublic class OreType : ITextCustomVariable\n{\n\tprivate" + Method + "\n\tpublic string GetText(CustomVariablesContext context)\n\t{\n\t\t" + mainIni + addIni + mainControl + addControl + output + "\n\t}\n}";


                return ModelMaket;
            }
            catch (NullReferenceException e)
            {
                resultQuery.Foreground = Brushes.Red;
                return "Ошибка!\nВыберите ВСЕ условия перед Стартом";
            }
        }
        private string[] GetAddOresInput(int i = 0)
        {
            //XmlDocument doc = new XmlDocument();
            //doc.Load("MainWindow.xml");

            //XmlNode grid;
            //XmlNode root = doc.DocumentElement;////<-Can't find file in debug...
            //grid = root.SelectSingleNode("descendant::Grid[@x:Name='AddOre']");
            string OreName;
            string OreValue;//string->-TO->double later
            string OreCondition;

            // oreGrid = grid.SelectSingleNode("descendant::Grid[@x:Name='AOG" + i + "']");
            //XmlNode OreNameI = oreGrid.SelectSingleNode("descendant::TextBox[@x:Name='AON" + i + "']");
            //XmlNode OreValueI = oreGrid.SelectSingleNode("descendant::TextBox[@x:Name='AOV" + i + "']");
            //XmlNode OreConditionI = oreGrid.SelectSingleNode("descendant::ListBox[@x:Name='AOC" + i + "']");
            //XmlNode OreCondList_selected = OreConditionI.SelectSingleNode("descendant::ListBoxItem[@Selected='OnSelected']");
            //

            OreName = ReturnTextBoxText("AON_", i);
            OreValue = ReturnTextBoxText("AOV_", i);
            OreCondition = ReturnListBoxItemText("AOC_", i);
            //OreName = //OreNameI.InnerText;
            //OreValue = OreValueI.InnerText;
            //OreCondition =ConditionStringToSymbol(OreCondList_selected.InnerText);
            string[] group = new string[3] { OreName, OreValue, OreCondition };
            return group;
        }

        private string ReturnTextBoxText(string part, int i)
        {
            object wantedNode = AddOre.FindName(part + i);
            if (wantedNode is TextBox)
            {
                TextBox wantedChild = wantedNode as TextBox;
                string cont = wantedChild.Text;
                return cont;
            }
            return "";
        }
        private string ReturnListBoxItemText(string part, int i)
        {
            /*
            object wantedNode = AddOre.FindName(part + i);
            if (wantedNode is ListBox)
            {
                ListBox wantedChild = wantedNode as ListBox;
                string cont = ((ListBoxItem)wantedChild.SelectedItem).Content.ToString();
                return ConditionStringToSymbol(cont);
            }
            return "";
            */
            switch (i)
            {
                case 0:
                    return (((ListBoxItem)AOC_0.SelectedItem).Content.ToString());
                case 1:
                    return (((ListBoxItem)AOC_1.SelectedItem).Content.ToString());
                case 2:
                    return (((ListBoxItem)AOC_2.SelectedItem).Content.ToString());
                case 3:
                    return (((ListBoxItem)AOC_3.SelectedItem).Content.ToString());
                case 4:
                    return (((ListBoxItem)AOC_4.SelectedItem).Content.ToString());
                case 5:
                    return (((ListBoxItem)AOC_5.SelectedItem).Content.ToString());
                case 6:
                    return (((ListBoxItem)AOC_6.SelectedItem).Content.ToString());
                case 7:
                    return (((ListBoxItem)AOC_7.SelectedItem).Content.ToString());
                case 8:
                    return (((ListBoxItem)AOC_8.SelectedItem).Content.ToString());
                case 9:
                    return (((ListBoxItem)AOC_9.SelectedItem).Content.ToString());
                default:
                    return "";
            }/**/
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, resultQuery.Text);
        }

        private void AddOreBtn_Click(object sender, RoutedEventArgs e)
        {
            //AddOreToMethod3();
            ActivateAddOreGrid(counter);
            counter++;
        }

        private void ActivateAddOreGrid(int i = 1)
        {
            if (i <= 9)
            {
                AddOreBtn.Margin = new Thickness(0, (i + 1) * 75, 0, 0);
                DelOreBtn.Margin = new Thickness(135, (i + 1) * 75, 0, 0);
                ResultGrid.Margin = new Thickness(18, 160 + (i + 1) * 75, 0, 0);
                Application.Current.MainWindow = this;
                Application.Current.MainWindow.Height = this.Height + 75;
                switch (i)
                {
                    /*case 0:
                        AOG_0.Visibility = Visibility.Visible;
                        DelOreBtn.Visibility = Visibility.Visible;
                        break;*/
                    case 1:
                        AOG_1.Visibility = Visibility.Visible;
                        AOG_1.Margin = new Thickness(0, 0 + i * 75, 0, 0);
                        DelOreBtn.Visibility = Visibility.Visible;//
                        break;
                    case 2:
                        AOG_2.Visibility = Visibility.Visible;
                        AOG_2.Margin = new Thickness(0, 0 + i * 75, 0, 0);
                        break;
                    case 3:
                        AOG_3.Visibility = Visibility.Visible;
                        AOG_3.Margin = new Thickness(0, 0 + i * 75, 0, 0);
                        break;
                    case 4:
                        AOG_4.Visibility = Visibility.Visible;
                        AOG_4.Margin = new Thickness(0, 0 + i * 75, 0, 0);
                        break;
                    case 5:
                        AOG_5.Visibility = Visibility.Visible;
                        AOG_5.Margin = new Thickness(0, 0 + i * 75, 0, 0);
                        break;
                    case 6:
                        AOG_6.Visibility = Visibility.Visible;
                        AOG_6.Margin = new Thickness(0, 0 + i * 75, 0, 0);
                        break;
                    case 7:
                        AOG_7.Visibility = Visibility.Visible;
                        AOG_7.Margin = new Thickness(0, 0 + i * 75, 0, 0);
                        break;
                    case 8:
                        AOG_8.Visibility = Visibility.Visible;
                        AOG_8.Margin = new Thickness(0, 0 + i * 75, 0, 0);
                        break;
                    case 9:
                        AOG_9.Visibility = Visibility.Visible;
                        AOG_9.Margin = new Thickness(0, 0 + i * 75, 0, 0);
                        AddOreBtn.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }
        /*
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

            //
            /*aON.Add(addOreNameText.Name);
            aOV.Add(addOreValueText.Name);
            aOC.Add(addOreCondList.Name);*//*
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
        }*/

        private void DelOreBtn_Click(object sender, RoutedEventArgs e)
        {
            DeactivateOreGrid(counter);
            counter--;
        }
        private void DeactivateOreGrid(int i = 1)
        {
            if (i >= 0)
            {
                AddOreBtn.Margin = new Thickness(0, (i - 1) * 75, 0, 0);
                DelOreBtn.Margin = new Thickness(135, (i - 1) * 75, 0, 0);
                ResultGrid.Margin = new Thickness(18, 160 + (i - 1) * 75, 0, 0);
                Application.Current.MainWindow = this;
                Application.Current.MainWindow.Height = this.Height - 75;
                switch (i - 1)
                {
                    /*case 0:
                        AOG_0.Visibility = Visibility.Collapsed;
                        AON_0.Text = "";
                        AOV_0.Text = "";
                        AOC_0.SelectedItem = null;
                        DelOreBtn.Visibility = Visibility.Collapsed;
                        break;*/
                    case 1:
                        AOG_1.Visibility = Visibility.Collapsed;
                        AON_1.Text = "";
                        AOV_1.Text = "";
                        AOC_1.SelectedItem = null;
                        DelOreBtn.Visibility = Visibility.Collapsed;//
                        break;
                    case 2:
                        AOG_2.Visibility = Visibility.Collapsed;
                        AON_2.Text = "";
                        AOV_2.Text = "";
                        AOC_2.SelectedItem = null;
                        break;
                    case 3:
                        AOG_3.Visibility = Visibility.Collapsed;
                        AON_3.Text = "";
                        AOV_3.Text = "";
                        AOC_3.SelectedItem = null;
                        break;
                    case 4:
                        AOG_4.Visibility = Visibility.Collapsed;
                        AON_4.Text = "";
                        AOV_4.Text = "";
                        AOC_4.SelectedItem = null;
                        break;
                    case 5:
                        AOG_5.Visibility = Visibility.Collapsed;
                        AON_5.Text = "";
                        AOV_5.Text = "";
                        AOC_5.SelectedItem = null;
                        break;
                    case 6:
                        AOG_6.Visibility = Visibility.Collapsed;
                        AON_6.Text = "";
                        AOV_6.Text = "";
                        AOC_6.SelectedItem = null;
                        break;
                    case 7:
                        AOG_7.Visibility = Visibility.Collapsed;
                        AON_7.Text = "";
                        AOV_7.Text = "";
                        AOC_7.SelectedItem = null;
                        break;
                    case 8:
                        AOG_8.Visibility = Visibility.Collapsed;
                        AON_8.Text = "";
                        AOV_8.Text = "";
                        AOC_8.SelectedItem = null;
                        break;
                    case 9:
                        AOG_9.Visibility = Visibility.Collapsed;
                        AON_9.Text = "";
                        AOV_9.Text = "";
                        AOC_9.SelectedItem = null;
                        AddOreBtn.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
        /*
private void TextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
{
TextBox txtBx = sender as TextBox;//This looks like it will be needed, so even though code is not working I leave it for the time being
txtBx.Text = "";
}*/
    }
}
