using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        //using an enum option to keep track of the current selected math operator
        SelectedOperator mathOperator;

        public MainWindow()
        {
            InitializeComponent();

            //set up event listeners for each button
            ACButton.Click += ACButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentButton.Click += PercentButton_Click;
            equalsButton.Click += EqualsButton_Click;

        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            //checks and parses the string into a double if the string contains only charactors allowed in a double
            if (double.TryParse(ResultLable.Content.ToString(), out newNumber))
            {
                switch (mathOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Substraction(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.divide(lastNumber, newNumber);
                        break;
                }

                ResultLable.Content = result.ToString();
            }
        }

        //divides the given number by 100 for a percentage value
        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            //checks and parses the string into a double if the string contains only charactors allowed in a double
            if (double.TryParse(ResultLable.Content.ToString(), out tempNumber))
            {
                //changes number from negative to positive or positive to negative
                tempNumber /= 100;
                
                if(lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                }

                ResultLable.Content = tempNumber.ToString();
            }
        }

        //converts given number to / from negative / positive
        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            //checks and parses the string into a double if the string contains only charactors allowed in a double
            if (double.TryParse(ResultLable.Content.ToString(), out lastNumber))
            {
                //changes number from negative to positive or positive to negative
                lastNumber *= -1;
                ResultLable.Content = lastNumber.ToString();
            }
        }

        //resets the resultLables value to 0
        private void ACButton_Click(object sender, RoutedEventArgs e)
        {
            ResultLable.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            //checks and parses the string into a double if the string contains only charactors allowed in a double
            if (double.TryParse(ResultLable.Content.ToString(), out lastNumber))
            {
                //changes number from negative to positive or positive to negative
                ResultLable.Content = lastNumber.ToString();
                //reset resultLable number to 0, to get second number
                ResultLable.Content = "0";
            }

            //sets the mathOperator depending on what button was pressed
            if (sender == multiplyButton) mathOperator = SelectedOperator.Multiplication;
            else if (sender == divideButton) mathOperator = SelectedOperator.Division;
            else if (sender == plusButton) mathOperator = SelectedOperator.Addition;
            else if (sender == subtractButton) mathOperator = SelectedOperator.Subtraction;

        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            //check that the current number does not have a decimal before adding another
            if (!ResultLable.Content.ToString().Contains("."))
            {
                ResultLable.Content += ".";
            }
        }

        private void Number_Button_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = 0;

            //sets the selectedValue to the button number 
            if (sender == zeroButton) selectedValue = 0;
            else if (sender == oneButton) selectedValue = 1;
            else if (sender == twoButton) selectedValue = 2;
            else if (sender == threeButton) selectedValue = 3;
            else if (sender == fourButton) selectedValue = 4;
            else if (sender == fiveButton) selectedValue = 5;
            else if (sender == sixButton) selectedValue = 6;
            else if (sender == sevenButton) selectedValue = 7;
            else if (sender == eightButton) selectedValue = 8;
            else if (sender == nineButton) selectedValue = 9;

            //if the number is zero, replace with selected number
            if (ResultLable.Content.ToString() == "0")
            {
                ResultLable.Content = selectedValue;
            }
            //else add number to the end
            else
            {
                ResultLable.Content += selectedValue.ToString();
            }
            //else ResultLable.Content = $"{ResultLable.Content}7";
        }


    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }


    public class SimpleMath
    {
        public static double Add(double number1, double number2)
        {
            return number1 + number2;
        }

        public static double Substraction(double number1, double number2)
        {
            return number1 - number2;
        }

        public static double multiply(double number1, double number2)
        {
            return number1 * number2;
        }

        public static double divide(double number1, double number2)
        {
            if(number2 == 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return number1 / number2;
        }
    }
}
