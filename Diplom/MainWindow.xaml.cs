﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
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
    ///
    public partial class MainWindow : Window
    {
        public void ReplaceContentWithDictionaryValues(Dictionary<string, string> dictionary, DependencyObject parent)
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var frameworkElement = child as FrameworkElement;

                if (frameworkElement != null)
                {
                    if (frameworkElement is Label label)
                    {
                        string content = label.Content.ToString();
                        foreach (var key in dictionary.Keys)
                        {
                            if (content.Contains(key))
                            {
                                content = content.Replace(key, dictionary[key]);
                            }
                        }
                        label.Content = content;
                    }
                    else if (frameworkElement is Button button)
                    {
                        string content = button.Content.ToString();
                        foreach (var key in dictionary.Keys)
                        {
                            if (content.Contains(key))
                            {
                                content = content.Replace(key, dictionary[key]);
                            }
                        }
                        button.Content = content;
                    }
                    else if (frameworkElement is ListBox listBox)
                    {
                        foreach (ListBoxItem listBoxItem in listBox.Items)
                        {
                            string content = listBoxItem.Content.ToString();
                            foreach (var key in dictionary.Keys)
                            {
                                if (content.Contains(key))
                                {
                                    content = content.Replace(key, dictionary[key]);
                                }
                            }
                            listBoxItem.Content = content;
                        }
                    }
                }

                ReplaceContentWithDictionaryValues(dictionary, child); // Recursive call to handle children of children
            }
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Application.Current.MainWindow = this;
            double factor = Application.Current.MainWindow.Width / 850; // You can change the 800 value as per your requirement

            // Find the ResourceDictionary that contains the styles you want to update
            ResourceDictionary dict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source?.ToString().EndsWith("MyResourceDictionary.xaml") == true);
            if (dict != null)
            {
                foreach (Style style in dict.Values.OfType<Style>())
                {
                    foreach (Setter setter in style.Setters.OfType<Setter>())
                    {
                        if (setter.Property == Control.FontSizeProperty)
                        {
                            setter.Value = 12 * factor; // You can change the 16 value as per your requirement
                        }
                    }
                }
            }
        }
        private int counter = 1;
        private string[] allHints = { "Выберите ВСЕ условия перед Стартом (Кликнуть по нужному тексту в списке)", "Метод 1 и Метод 2 собирают запрос в окно parcel", "Метод 3 собирает c# код для обработки", "Для ввода десятичных значений используйте \".\" на Numpad"};
        private Dictionary<string, string> ruTOengDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> engTOruDictionary = new Dictionary<string, string>();
        private bool langFlag = false;
        private string errorMsg = "\"Ошибка!\nВыберите ВСЕ условия перед стартом! \n(Кликнуть по нужному тексту в списке)";
        private byte methodnum = 1;
        void fillDictionaries()
        {
            ruTOengDictionary.Add("Метод 3", "Method 3");
            engTOruDictionary.Add("Method 3", "Метод 3");
            ruTOengDictionary.Add("Метод 2", "Method 1");
            engTOruDictionary.Add("Method 2", "Метод 1");
            ruTOengDictionary.Add("Метод 1", "Method 1");
            engTOruDictionary.Add("Method 1", "Метод 1");
            ruTOengDictionary.Add("Полезное ископаемое", "Useful component");
            engTOruDictionary.Add("Useful component", "Полезное ископаемое");
            ruTOengDictionary.Add("Значение", "Value");
            engTOruDictionary.Add("Value", "Значение");
            ruTOengDictionary.Add("Условие", "Condition");
            engTOruDictionary.Add("Condition", "Условие");
            ruTOengDictionary.Add("верно", "true");
            engTOruDictionary.Add("true", "верно");
            ruTOengDictionary.Add("Иначе", "Else");
            engTOruDictionary.Add("Else", "Иначе");
            ruTOengDictionary.Add("больше", "more");
            engTOruDictionary.Add("more", "больше");
            ruTOengDictionary.Add("или", "or");
            engTOruDictionary.Add("ore", "руду");
            engTOruDictionary.Add("or", "или");
            ruTOengDictionary.Add("меньше", "less");
            engTOruDictionary.Add("less", "меньше");
            ruTOengDictionary.Add("равно", "equal");
            engTOruDictionary.Add("equal", "равно");
            ruTOengDictionary.Add("не", "non");
            engTOruDictionary.Add("non", "не");
            ruTOengDictionary.Add("Старт", "Start");
            engTOruDictionary.Add("Start", "Старт");
            ruTOengDictionary.Add("Копировать", "Copy");
            engTOruDictionary.Add("Copy", "Копировать");
            ruTOengDictionary.Add("Внимание", "Warning");
            engTOruDictionary.Add("Warning", "Внимание");
            ruTOengDictionary.Add("выводит", "generates");
            engTOruDictionary.Add("generates", "выводит");
            ruTOengDictionary.Add("код", "code");
            engTOruDictionary.Add("code", "код");
            ruTOengDictionary.Add("Добавить", "Add");
            engTOruDictionary.Add("Add", "Добавить");
            ruTOengDictionary.Add("Убрать", "Remove");
            engTOruDictionary.Add("Remove", "Убрать");
            ruTOengDictionary.Add("руду", "ore");
            //ruTOengDictionary.Add("", "");
            //engTOruDictionary.Add("", "");

        }
        public MainWindow()
        {
            InitializeComponent();
            fillDictionaries();
            this.SizeChanged += MainWindow_SizeChanged;
        }

        //Simple Check for Textbox values to make sure input is correct
        private void isNumber(object sender, KeyEventArgs e)
        {
            //Check if it is spacebar key
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            //check if it is not number or control keys (delete, backspace, left, right, delete) or decimal (numpad".")
            e.Handled = !checkBothNumberAndNumpad(e.Key) && !checkControlKeys(e.Key) && !checkDecimalKey(e.Key);
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

        private bool checkDecimalKey(Key inKey)
        {
            return inKey == Key.Decimal;
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
                case "неравно":
                    return "!=";
                //break;
                case "more":
                    return ">";
                //break;
                case "more or equal":
                    return ">=";
                //break;
                case "less":
                    return "<";
                //break;
                case "less or equal":
                    return "<=";
                //break;
                case "equal":
                    return "==";
                //break;
                case "nonequal":
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
            Application.Current.MainWindow.Width = 950;
            Application.Current.MainWindow.Height = 450;
            ResultGrid.Margin = new Thickness(18, 285, 0, 0);
            ResultGrid.Width = 900;
            Note.Visibility = Visibility.Collapsed;
            Copy.Margin = new Thickness(815, 35, 0, 0);
            resultQuery.Width = 800;
            Clear();
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
            Application.Current.MainWindow.Width = 1575;
            Application.Current.MainWindow.Height = 450;
            ResultGrid.Margin = new Thickness(18, 285, 0, 0);
            ResultGrid.Width = 1525;
            Note.Visibility = Visibility.Collapsed;
            Copy.Margin = new Thickness(1435, 35, 0, 0);
            resultQuery.Width = 1420;
            Clear();
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
            Application.Current.MainWindow.Width = 1575;
            Application.Current.MainWindow.Height = 450;
            ResultGrid.Margin = new Thickness(18, 285, 0, 0);
            ResultGrid.Width = 1525;
            Note.Visibility = Visibility.Visible;
            Copy.Margin = new Thickness(1435, 35, 0, 0);
            resultQuery.Width = 1420;
            Clear();
        }
        //Clearing values before swithcing between them:
        private void Clear()
        {
            foreach (var control in firstMethod.Children)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is ListBox)
                {
                    ListBox listBox = (ListBox)control;
                    listBox.UnselectAll();
                }
            }
            foreach (var control in secondMethod.Children)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is ListBox)
                {
                    ListBox listBox = (ListBox)control;
                    listBox.UnselectAll();
                }
            }
            foreach (var control in thirdMethod.Children)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is ListBox)
                {
                    ListBox listBox = (ListBox)control;
                    listBox.UnselectAll();
                }
            }
            while (counter>1)
            {
                DeactivateOreGrid(counter);
                counter--;
            }
            menuGrid.Width = Application.Current.MainWindow.Width;
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
                    resultQuery.Text ="";
                    break;
            }

        }
        //Methods realisation 
        private string firstM()
        {
            //

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
                return errorMsg;
            }
        }
        private string secondM()
        {
            //
            try
            {
                //
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
                    "\"" + oreResult1M2.Text + "\"",//0
                    "\"" + oreResult2M2.Text + "\"",//1
                    "\"" + oreResult3M2.Text + "\"",//2
                    "\"" + oreResult4M2.Text + "\"",//3
                    "\"" + oreResult5M2.Text + "\"",//4
                    "\"" + oreResult6M2.Text + "\"",//5
                };
                string stroka = makeAnExpression(valueOne, valuesTwo[4], selectedListBoxItem_TextValues[4], results[4], results[5]);
                for (int i = valuesTwo.Length - 2; i > -1; i--)
                {
                    stroka = makeAnExpression(valueOne, valuesTwo[i], selectedListBoxItem_TextValues[i], results[i], stroka);
                }
                //
                return stroka;
            }
            catch (NullReferenceException e)
            {
                resultQuery.Foreground = Brushes.Red;
                return errorMsg;
            }
        }

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
                    Add_Ore_Value[i] = GetInput[1];//
                    Add_Ore_Condition[i] = ConditionStringToSymbol(GetInput[2]);

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
                string Method = "bool ConditionChecker(string symbol, double x, double y)\n{\n" + "\t" + "switch (symbol)\n{\n" + "\t\t" + "case \">\": return x > y" + ";\n" + "\t\t" + "case \">=\": return x >= y" + ";\n" + "\t\t" + "case \"<\": return x < y" + ";\n" + "\t\t" + "case \"<=\": return x <= y" + ";\n" + "\t\t" + "case \"==\": return x == y" + ";\n" + "\t\t" + "case \"!=\": return x != y" + ";\n" + "\t}\t\nreturn false;\n}\n";

                string mainIni = "//Input Data\n" + "\tdouble mainOreValue = " + "context.N(\"" + oreNameM3.Text + "\")" + ";\n" + "\tstring[] gradeOre = new string[] " + grades + ";\n" + "\tdouble[] mainOreControlValues = new double[] " + mControlV + ";\n" + "\tstring[] mainConditions = new string[] " + mConditions + ";\n";
                string addIni = "int counter = " + counter + ";\n" + "\tdouble[] addOresValues = new double[] " + addNames + ";\n" + "\tdouble[] addOresControlValues = new double[] " + addValues + ";\n" + "\tstring[] addConditions = new string[] " + addConditions + ";\n";

                string mainControl = "int k = 0;\n" + "do\n{\n" + "\tk++;\n" + "}while" + "(!(ConditionChecker(mainConditions[k], mainOreValue, mainOreControlValues[k]))&&(k<(mainOreControlValues.Length-1)))" + ";\n" + "if ((k == (mainOreControlValues.Length - 1))&&!(ConditionChecker(mainConditions[k], mainOreValue, mainOreControlValues[k]))) k++;\n";
                string addControl = "for (int i = 0; i < counter; i++)\n{\n" + "\tif (ConditionChecker(addConditions[i], addOresValues[i], addOresControlValues[i]))k--;\n" + "}\n";
                string output = "if(k<0)k=0;\nreturn gradeOre[k];";
                //
                //Assembling Code
                string ModelMaket = "using System;\nusing System.Collections.Generic;\nusing System.Text;\nusing System.Linq;\nusing Alastri.Scripting;\nusing Alastri.BlockModel.Engine.CustomVariables;\n\npublic class OreType : ITextCustomVariable\n{\n\tprivate " + Method + "\n\tpublic string GetText(CustomVariablesContext context)\n\t{\n\t\t" + mainIni + addIni + mainControl + addControl + output + "\n\t}\n}";


                return ModelMaket;
            }
            catch (NullReferenceException e)
            {
                resultQuery.Foreground = Brushes.Red;
                return errorMsg;
            }
        }
        private string[] GetAddOresInput(int i = 0)
        {

            string OreName;
            string OreValue;//string->-TO->double later
            string OreCondition;


            OreName = ReturnTextBoxText("AON_", i);
            OreValue = ReturnTextBoxText("AOV_", i);
            OreCondition = ReturnListBoxItemText("AOC_", i);
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
                thirdMethod.Height = thirdMethod.Height + 75;
                switch (i)
                {

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


        private void DelOreBtn_Click(object sender, RoutedEventArgs e)
        {
            DeactivateOreGrid(counter);
            counter--;
        }
        private void DeactivateOreGrid(int i = 1)
        {
            if (i >= 1)
            {
                AddOreBtn.Margin = new Thickness(0, (i - 1) * 75, 0, 0);
                DelOreBtn.Margin = new Thickness(135, (i - 1) * 75, 0, 0);
                ResultGrid.Margin = new Thickness(18, 160 + (i - 1) * 75, 0, 0);
                Application.Current.MainWindow = this;
                //Application.Current.MainWindow.Height = this.Height - 75;//makes height negative, remained height doesn't seem to affect the work and I don't wanna spend time to fix it :P
                thirdMethod.Height = thirdMethod.Height - 75;
                switch (i - 1)
                {

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
        private void changeCurrentHint(object sender, MouseEventArgs e)
        {
            int k = 0;
            if(sender is Button)
            {
                if ((sender as Button).Name == "BtnM3") k = 2;
                if ((sender as Button).Name == "BtnM2" || (sender as Button).Name == "BtnM1") k = 1;
            }
            if (sender is ListBox)
            {
                k = 0;
            }
            if (sender is TextBox)
            {
                k = 3;
            }
            currentHint.Content = allHints[k];
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (!langFlag)
            {
                string[] newAllHints = { "Select ALL conditions before Start (Click on element in list)", "Method 1 and Method 2 build parcel", "Method 3 build c# code", "For input of decimals use \".\" on Numpad" };
                allHints = newAllHints;
                errorMsg = "\"Error!\nSelect All conditions before start! \n(Click on element in list)";
                Application.Current.MainWindow.Title = "Generator of useful components\'s grades";
                langBtn.Content = "eng";
                ReplaceContentWithDictionaryValues(ruTOengDictionary, mainGrid);
                currentHint.Content = "changed language to " + "english";
                langFlag=!langFlag;
            }
            else
            {
                string[] newAllHints = { "Выберите ВСЕ условия перед стартом (Кликнуть по нужному тексту в списке)", "Метод 1 и Метод 2 собирают запрос в окно parcel", "Метод 3 собирает c# код для обработки", "Для ввода десятичных значений используйте \".\" на Numpad"};
                allHints = newAllHints;
                errorMsg = "\"Ошибка!\nВыберите ВСЕ условия перед стартом! \n(Кликнуть по нужному тексту в списке)";
                Application.Current.MainWindow.Title = "Генератор сортов полезного ископаемого";
                langBtn.Content = "ru";
                ReplaceContentWithDictionaryValues(engTOruDictionary, mainGrid);
                currentHint.Content = "поменял язык на " + "русский";//may also make this for several languages, i.e.: use index for switch instead of bool for if else?
                langFlag = !langFlag;
            }
        }
    }
}
