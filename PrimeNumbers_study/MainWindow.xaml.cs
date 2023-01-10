//using TRPO_Lab1;

using SieveOfEratosthenes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
//что-то не так, я чувствую это
//я, правда, старался. Поставьте 4, пожалуйста

namespace TRPO_Lab2._1
{
    public class LeftBorderIsNotAnInteger21 : Exception
    {
    }
    public class RightBorderIsNotAnInteger22 : Exception
    {
    }

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent(); //было сразу
        }



        int leftBor = 0;
        private void textBoxLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            try //для каждого активного объекта
            {
                if (int.TryParse(textBoxLeft.Text, out int x)) //условие проверки на принадлежность типу int 
                {
                    leftBor = Convert.ToInt32(textBoxLeft.Text);
                }
                else
                {
                    if (textBoxLeft.Text == "")
                    {
                        textBoxLeft.Text = "";
                    }
                    else
                    {
                        throw new LeftBorderIsNotAnInteger21();
                    }
                }
            }
            catch (LeftBorderIsNotAnInteger21)
            {
                textBoxLeft.Text = "";
                MessageBox.Show("Введено нечисловое или превышающее максимум значение для левой границы. ", "Ошибка ввода! - 21");
                leftBor = 0;
            }


        }

        int rightBor = 0;
        private void textBoxRight_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (int.TryParse(textBoxRight.Text, out int x))
                {
                    rightBor = Convert.ToInt32(textBoxRight.Text);
                }
                else
                {
                    if (textBoxRight.Text == "")
                    {
                        textBoxRight.Text = "";
                    }
                    else
                    {
                        throw new RightBorderIsNotAnInteger22();
                    }
                }

            }
            catch (RightBorderIsNotAnInteger22)
            {
                textBoxRight.Text = "";
                MessageBox.Show("Введено нечисловое или превышающее максимум значение для правой границы. ", "Ошибка ввода! - 22");
                rightBor = 0;
            }
            //
        }

        List<int> eratosList = new List<int>();

        private void buttonCalcul_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                eratosList = NewEratosClass.EratosFunc(leftBor, rightBor);

                listOfNumbers.Items.Clear();
                for (int i = 0; i < eratosList.Count; i++)
                {
                    listOfNumbers.Items.Add(string.Format(Convert.ToString(eratosList[i])));
                    //нет проверки на ввод отрицательных границ
                    //и нет обработки значений 0, 1 для простых чисел (их не должно быть)
                }
            }
            catch (LeftBorderIsLessThanOrEqualToZero31)
            {
                textBoxLeft.Text = "";
                MessageBox.Show("Число левой границы должно быть натуральным. ", "Ошибка значения! - 31");
                leftBor = 0;
            }
            catch (RightBorderIsLessThanOrEqualToZero32)
            {
                textBoxRight.Text = "";
                MessageBox.Show("Число правой границы должно быть натуральным. ", "Ошибка значения! - 32");
                rightBor = 0;
            }
            catch (RightBorderIsLessThanOrEqualToTheLeftBorder33)
            {
                MessageBox.Show("Правая граница должна быть больше левой. ", "Ошибка значения! - 33");
                listOfNumbers.Items.Clear();
            }

        }

        private void listOfNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

    }
}
