using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Automation;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.InputDevices;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.ListViewItems;
using TestStack.White.UIItems.WindowItems;

using TRPO_Lab2._1;

using static System.Net.Mime.MediaTypeNames;
using Application = TestStack.White.Application;
//using static System.Net.Mime.MediaTypeNames;

//так же к решению надо добавить модуль библиотеки и ссылку на него
//(долго думал, почему же не работает вычисление - фейспалм)

namespace UI_Lab9._1_Test
{
    [TestClass]
    public class UnitTest1
    {
        string path = @"D:\Krisher\Учёба\3 курс\ТРПО\Лаб 9\UI_Lab9.1_Test\UI_Lab9.1_Test\TRPO_Lab2.1.exe";

        [TestMethod]
        public void TestMethod1()
        {
            //var applicationDirectory = TestContext.;

            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            if (window == null)
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsTrue(true);
            }
            application.Close();
        }

        [TestMethod]
        public void BoxLeft_input()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");

            textBox1.BulkText = "626";
            //System.Diagnostics.Debug.WriteLine(textBox1.Text);

            if (textBox1.Text != "626")
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsTrue(true);
            }
            application.Close();
        }

        [TestMethod]
        public void BoxRight_input()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox2 = window.Get<TextBox>("textBoxRight");

            textBox2.BulkText = "9000";

            int expected = 9000;

            Assert.AreEqual(expected, Convert.ToInt32(textBox2.Text));
            application.Close();
        }

        [TestMethod]
        public void TestButtonCalc_clic() //пока не совсем понимаю что тут надо
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            Button button = window.Get<Button>("buttonCalcul");

            //System.Diagnostics.Debug.WriteLine($"что покажет button.Enabled {button.Enabled}\n"); //true
            //System.Diagnostics.Debug.WriteLine($"что покажет window.IsFocussed {window.IsFocussed} - 1"); //true
            //button.Focus();
            //System.Diagnostics.Debug.WriteLine($"что покажет window.IsFocussed {window.IsFocussed} - 2\n"); //false
            //System.Diagnostics.Debug.WriteLine($"----что покажет button.IsFocussed {button.IsFocussed}\n"); //true

            Assert.IsTrue(button.Enabled);
            //Assert.IsTrue(button.IsFocussed); //button.IsFocussed
            application.Close();
        }

        [TestMethod]
        public void TabSwitch_focus()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            TextBox textBox2 = window.Get<TextBox>("textBoxRight");
            Button button = window.Get<Button>("buttonCalcul");
            ListBox listBox = window.Get<ListBox>("listOfNumbers");

            //System.Diagnostics.Debug.WriteLine($"window - {window.IsFocussed}");
            //System.Diagnostics.Debug.WriteLine($"textBox1 - {textBox1.IsFocussed}");
            //System.Diagnostics.Debug.WriteLine($"textBox2 - {textBox2.IsFocussed}");
            //System.Diagnostics.Debug.WriteLine($"button - {button.IsFocussed}");
            //System.Diagnostics.Debug.WriteLine($"listBox - {listBox.IsFocussed}\n");

            textBox1.Focus();
            if (textBox1.IsFocussed == false)
            {
                System.Diagnostics.Debug.WriteLine($"TB1");
                Assert.Fail();
            }
            else
            {
                textBox1.BulkText = "2"; //не переходит на лист бокс, если там ничего нет
                Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
                Thread.Sleep(50);
            }

            if (textBox2.IsFocussed == false)
            {
                System.Diagnostics.Debug.WriteLine($"TB2");
                Assert.Fail();
            }
            else
            {
                textBox2.BulkText = "4"; //не переходит на лист бокс, если там ничего нет
                Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
                Thread.Sleep(50);
            }

            if (button.IsFocussed == false)
            {
                System.Diagnostics.Debug.WriteLine($"button");
                Assert.Fail();
            }
            else
            {
                button.Click(); //не переходит на лист бокс, если там ничего нет
                Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
                Thread.Sleep(50);
            }

            //listBox.IsSelected listBox.IsSelected(0)
            //listBox.IsFocussed
            //System.Diagnostics.Debug.WriteLine($"listBox.IsFocussed = {listBox.IsFocussed}\n");
            //ListItem listIt = window.Get<ListItem>("listOfNumbers");
            //if (listBox.Item(0).IsFocussed == false)
            //{
            //    System.Diagnostics.Debug.WriteLine($"listBox");
            //    Assert.Fail();
            //    //не переходит на лист бокс, если там ничего нет
            //}
            //else
            //{
            //    Assert.IsTrue(true);
            //    //настроив свойство TabIndex в приложении получилось
            //}

            Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);

            if (textBox1.IsFocussed == false)
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsTrue(true); //когда вернулись на textbox для левой границы
            }
            application.Close();
        }

        [TestMethod]
        public void EmptyTabSwitch_Focus()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            TextBox textBox2 = window.Get<TextBox>("textBoxRight");
            Button button = window.Get<Button>("buttonCalcul");

            textBox1.Focus();
            if (textBox1.IsFocussed == false)
            {
                Assert.Fail();
            }
            else
            {
                Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
                Thread.Sleep(50);
            }

            if (textBox2.IsFocussed == false)
            {
                Assert.Fail();
            }
            else
            {
                Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
                Thread.Sleep(50);
            }

            if (button.IsFocussed == false)
            {
                Assert.Fail();
            }
            else
            {
                Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
                Thread.Sleep(50);
            }

            if (textBox1.IsFocussed == false)
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsTrue(true);
            }
            application.Close();
        }

        [TestMethod]
        public void ListOfNumbs_Clear()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            TextBox textBox2 = window.Get<TextBox>("textBoxRight");
            Button button = window.Get<Button>("buttonCalcul");
            ListBox listBox = window.Get<ListBox>("listOfNumbers");

            textBox1.BulkText = "2";
            textBox2.BulkText = "35";
            button.Click();

            int firstAmt = listBox.Items.Count;
            //System.Diagnostics.Debug.WriteLine($"listBox.Items.Count - {listBox.Items.Count}\n");

            textBox1.BulkText = "2";
            textBox2.BulkText = "35";
            button.Click();

            int secondAmt = listBox.Items.Count;

            Assert.AreEqual(firstAmt, secondAmt);

            application.Close();
        }

        [TestMethod]
        public void ResultOfProg_correct()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            TextBox textBox2 = window.Get<TextBox>("textBoxRight");
            Button button = window.Get<Button>("buttonCalcul");
            ListBox listBox = window.Get<ListBox>("listOfNumbers");

            //Keyboard.Instance.Enter("TAB");
            List<int> results = new List<int>();
            textBox1.Focus();
            for (int i = 1; i < 7; i++)
            {
                //var forA = i + 2;
                //textBox1.BulkText = Convert.ToString($"{forA}"); //ругается, вроде
                Keyboard.Instance.Enter($"{2 + i}");

                Thread.Sleep(200);

                Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);

                //var forB = i + 20;
                //textBox2.BulkText = Convert.ToString($"{forB}"); //точно ругается
                Keyboard.Instance.Enter($"{(2 + i)}");
                Keyboard.Instance.Enter($"{(0)}");

                Thread.Sleep(200);

                //Keyboard.Instance.Enter("TAB"); //если таб, а не через фокус, то ругается
                button.Focus();

                button.Click();

                Thread.Sleep(200);

                Keyboard.Instance.Enter("TAB");
                Thread.Sleep(200);
                Keyboard.Instance.Enter("TAB");
                Thread.Sleep(200);

                textBox1.Text = ""; //если не очистить, то записывается к уже введенному
                textBox2.Text = "";

                textBox1.Focus();

                results.Add(listBox.Items.Count);
            }

            Assert.AreEqual(17, results[5]);
            application.Close();
        }

        // Интеграционное тестирование

        [TestMethod] //1 31
        public void EmptyBorders_CLick()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            Button button = window.Get<Button>("buttonCalcul");

            button.Click();

            Thread.Sleep(20);

            Window mesBox = window.ModalWindow("Ошибка значения! - 31");

            Assert.IsNotNull(mesBox);

            application.Close();
        }

        [TestMethod] //2 32
        public void EmptyRightBorder_Click()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            Button button = window.Get<Button>("buttonCalcul");

            textBox1.BulkText = "10";
            button.Click();

            Thread.Sleep(20);

            Window mesBox = window.ModalWindow("Ошибка значения! - 32");

            Assert.IsNotNull(mesBox);

            application.Close();
        }

        [TestMethod] //3 31
        public void EmptyLeftBorder_Click()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox2 = window.Get<TextBox>("textBoxRight");
            Button button = window.Get<Button>("buttonCalcul");

            textBox2.BulkText = "10";
            button.Click();

            Thread.Sleep(20);

            Window mesBox = window.ModalWindow("Ошибка значения! - 31");

            Assert.IsNotNull(mesBox);

            application.Close();
        }

        [TestMethod] //4 21
        public void BigLeftBorder_Instantly()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");

            //textBox1.BulkText = "ввв";
            textBox1.Focus();
            Keyboard.Instance.Enter($"9");
            Keyboard.Instance.Enter($"9");
            Keyboard.Instance.Enter($"9");
            Keyboard.Instance.Enter($"9");
            Keyboard.Instance.Enter($"9");
            Keyboard.Instance.Enter($"9");
            Keyboard.Instance.Enter($"9");
            Keyboard.Instance.Enter($"9");
            Keyboard.Instance.Enter($"9");
            Keyboard.Instance.Enter($"9");
            //button.Click();
            //button.Focus();

            Thread.Sleep(20);

            Window mesBox = window.ModalWindow("Ошибка ввода! - 21");

            Assert.IsNotNull(mesBox);

            application.Close();
        }

        [TestMethod] //5 22
        public void BigRigthBorder_Instantly()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            TextBox textBox2 = window.Get<TextBox>("textBoxRight");

            textBox1.BulkText = "100";
            textBox2.Focus();
            //textBox2.BulkText = "3000000000";
            Keyboard.Instance.Enter($"8");
            Keyboard.Instance.Enter($"8");
            Keyboard.Instance.Enter($"8");
            Keyboard.Instance.Enter($"8");
            Keyboard.Instance.Enter($"8");
            Keyboard.Instance.Enter($"8");
            Keyboard.Instance.Enter($"8");
            Keyboard.Instance.Enter($"8");
            Keyboard.Instance.Enter($"8");
            Keyboard.Instance.Enter($"8");

            Thread.Sleep(20);

            Window mesBox = window.ModalWindow("Ошибка ввода! - 22");

            Assert.IsNotNull(mesBox);

            application.Close();
        }

        [TestMethod] //6 21
        public void LetterLeftBorder_Instantly()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");

            textBox1.Focus();
            //textBox1.BulkText = "gg"; //такая ситуация...
            //окно об ошибке появляется. модальное окно
            //но C# его как будто не видит
            //даже List<Window> modalWindows = window.ModalWindows(); не воспринимает его
            //получилось только через имитацию нажатия клавиши
            //но почему, я не понял =(
            Keyboard.Instance.Enter($"G");

            Thread.Sleep(20);

            Window mesBox = window.ModalWindow("Ошибка ввода! - 21");

            Assert.IsNotNull(mesBox);

            application.Close();
        }

        [TestMethod] //7 22
        public void LetterRightBorder_Instantly()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            TextBox textBox2 = window.Get<TextBox>("textBoxRight");

            textBox1.BulkText = "100";
            textBox2.Focus();
            Keyboard.Instance.Enter($"F");
            //textBox2.BulkText = "FFF";

            Thread.Sleep(20);

            Window mesBox = window.ModalWindow("Ошибка ввода! - 22");

            Assert.IsNotNull(mesBox);

            application.Close();
        }

        [TestMethod] //8 31
        public void NegativeLeftBorder_Instantly()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            Button button = window.Get<Button>("buttonCalcul");

            textBox1.BulkText = "-100";
            button.Click();

            Thread.Sleep(20);

            Window mesBox = window.ModalWindow("Ошибка значения! - 31");

            Assert.IsNotNull(mesBox);

            application.Close();
        }

        [TestMethod] //9 32
        public void NegativeRightBorder_Click()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            TextBox textBox2 = window.Get<TextBox>("textBoxRight");
            Button button = window.Get<Button>("buttonCalcul");

            textBox1.BulkText = "100";
            textBox2.BulkText = "-100";
            button.Click();

            Thread.Sleep(20);

            Window mesBox = window.ModalWindow("Ошибка значения! - 32");

            Assert.IsNotNull(mesBox);

            application.Close();
        }

        [TestMethod] //10 33
        public void BigLeft_SmallRight_Click()
        {
            Application application = Application.Launch(path);
            Window window = application.GetWindow("Простые числа", InitializeOption.NoCache);

            TextBox textBox1 = window.Get<TextBox>("textBoxLeft");
            TextBox textBox2 = window.Get<TextBox>("textBoxRight");
            Button button = window.Get<Button>("buttonCalcul");

            textBox1.BulkText = "9000";
            textBox2.BulkText = "30";
            button.Click();

            Thread.Sleep(20);

            //Window mesBox1 = application.GetWindow("Ошибка значения!", InitializeOption.NoCache); //не работают
            // Window mesBox2 = Desktop.Instance.Windows().Find(obj => obj.Title.Contains("Ошибка значения!")); 

            //List<Window> modalWindows = window.ModalWindows(); //список всех модальных окон, принадлежащих окну

            Window mesBox = window.ModalWindow("Ошибка значения! - 33"); //модальное окно с заголовком "Ошибка значения!"

            //mesBox.Get(SearchCriteria.ByControlType(ControlType.Pane).AndByText("FooObject.cs")); //не знаю что это
            //mesBox.MdiChild(SearchCriteria.ByControlType(ControlType.Pane).AndByText("FooObject.cs"));

            if (mesBox == null)
            {
                Assert.Fail();
            }
            else
            {
                // Assert.IsTrue(true);
                Assert.Fail();

            }

            application.Close();
        }

    }
}
