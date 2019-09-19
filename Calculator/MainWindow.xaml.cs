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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            plusMinusButton.Click += PlusMinusButton_Click;
            percentButton.Click += PercentButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            equalButtonPressed = false;
            resultLabel.Content = "0";
        }

        private void PlusMinusButton_Click(object sender, RoutedEventArgs e)
        {
            equalButtonPressed = false;
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = -lastNumber;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            equalButtonPressed = false;
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber /= 100;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        calculationResult = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        calculationResult = SimpleMath.Subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        calculationResult = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        calculationResult = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                    default:
                        break;
                }

                resultLabel.Content = calculationResult.ToString();
                equalButtonPressed = true;
            }
        }

        private void DecimalPointButton_Click(object sender, RoutedEventArgs e)
        {
            equalButtonPressed = false;

            if (!resultLabel.Content.ToString().Contains(","))
            {
                resultLabel.Content = $"{resultLabel.Content},";
            }
        }        

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            if (equalButtonPressed)
            {
                equalButtonPressed = false;
                resultLabel.Content = "0";
            }

            if (resultLabel.Content.ToString() == "+" ||
                resultLabel.Content.ToString() == "-" ||
                resultLabel.Content.ToString() == "*" ||
                resultLabel.Content.ToString() == "/")
                resultLabel.Content = "";

            /*
            int selectedValue = 0;

            if (sender == zeroButton)
                selectedValue = 0;
            else if (sender == oneButton)
                selectedValue = 1;
            else if (sender == twoButton)
                selectedValue = 2;
            else if (sender == threeButton)
                selectedValue = 3;
            else if (sender == fourRosesButton)
                selectedValue = 4;
            else if (sender == fiveButton)
                selectedValue = 5;
            else if (sender == sixButton)
                selectedValue = 6;
            else if (sender == sevenButton)
                selectedValue = 7;
            else if (sender == eightButton)
                selectedValue = 8;
            else if (sender == nineButton)
                selectedValue = 9;
            */

            int selectedValue = int.Parse(((System.Windows.Controls.ContentControl)sender).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            equalButtonPressed = false;

            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                if (sender == addButton)
                {
                    selectedOperator = SelectedOperator.Addition;
                    resultLabel.Content = "+";
                }
                else if (sender == subtractButton)
                {
                    selectedOperator = SelectedOperator.Subtraction;
                    resultLabel.Content = "-";
                }
                else if (sender == multiplyButton)
                {
                    selectedOperator = SelectedOperator.Multiplication;
                    resultLabel.Content = "*";
                }
                else if (sender == divideButton)
                {
                    selectedOperator = SelectedOperator.Division;
                    resultLabel.Content = "/";
                }
            }
        }

        double lastNumber, calculationResult;
        SelectedOperator selectedOperator;
        bool equalButtonPressed = false;    // For resetting the display (when true).
    }   // public partial class MainWindow : Window

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add(double x, double y)
        {
            return x + y;
        }

        public static double Subtract(double x, double y)
        {
            return x - y;
        }

        public static double Multiply(double x, double y)
        {
            return x * y;
        }

        public static double Divide(double x, double y)
        {
            if (y == 0)
            {
                MessageBox.Show("Division by zero is not supported", "Invalid argument", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

            return x / y;
        }
    }
}
