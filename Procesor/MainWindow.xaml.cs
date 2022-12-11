using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Procesor
{
    public partial class MainWindow : Window
    {
        char[] memory;
        int counter;

        public MainWindow()
        {
            InitializeComponent();
            tbAH.MaxLength = 2;
            tbAL.MaxLength = 2;
            tbBH.MaxLength = 2;
            tbBL.MaxLength = 2;
            tbCH.MaxLength = 2;
            tbCL.MaxLength = 2;
            tbDH.MaxLength = 2;
            tbDL.MaxLength = 2;

            counter = 0;
            memory = new char[65536];
        }


        #region Event Clicks Handlers

        private void btnNOT_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 1)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc jedna wartosc rejestru");
                }

                var result = HandleNotOperation(checkedCheckboxes[0]);
                var tbValue = GetTextbox(checkedCheckboxes[0]);
                tbValue.Text = result.ToString("X");
                RefreshMemory(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnINC_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 1)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc jedna wartosc rejestru");
                }

                var result = HandleIncOperation(checkedCheckboxes[0]);
                var tbValue = GetTextbox(checkedCheckboxes[0]);
                tbValue.Text = result.ToString("X");
                RefreshMemory(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDEC_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 1)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc jedna wartosc rejestru");
                }

                var result = HandleDecOperation(checkedCheckboxes[0]);
                var tbValue = GetTextbox(checkedCheckboxes[0]);
                tbValue.Text = result.ToString("X");
                RefreshMemory(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMOV_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 2)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc dwie wartoci rejestru");
                }

                var tbValue1 = GetTextbox(checkedCheckboxes[0]);
                var tbValue2 = GetTextbox(checkedCheckboxes[1]);

                var answer = MessageBox.Show($"(Tak) {tbValue1.Text} => {tbValue2.Text} (Nie) {tbValue2.Text} => {tbValue1.Text}", "Pytanie", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (answer == MessageBoxResult.Yes)
                {
                    tbValue1.Text = tbValue2.Text;
                }
                else if (answer == MessageBoxResult.No)
                {
                    tbValue2.Text = tbValue1.Text;
                }
                RefreshMemory(Convert.ToInt32(tbValue1.Text));
                RefreshMemory(Convert.ToInt32(tbValue2.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 2)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc dwie wartoci rejestru");
                }

                var tbValue1 = GetTextbox(checkedCheckboxes[0]);
                var tbValue2 = GetTextbox(checkedCheckboxes[1]);

                HandleAddOperation(tbValue1, tbValue2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSUB_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 2)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc dwie wartoci rejestru");
                }

                var tbValue1 = GetTextbox(checkedCheckboxes[0]);
                var tbValue2 = GetTextbox(checkedCheckboxes[1]);

                HandleSubOperation(tbValue1, tbValue2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAND_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 2)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc dwie wartoci rejestru");
                }

                var tbValue1 = GetTextbox(checkedCheckboxes[0]);
                var tbValue2 = GetTextbox(checkedCheckboxes[1]);

                HandleAndOperation(tbValue1, tbValue2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOR_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 2)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc dwie wartoci rejestru");
                }

                var tbValue1 = GetTextbox(checkedCheckboxes[0]);
                var tbValue2 = GetTextbox(checkedCheckboxes[1]);

                HandleOrOperation(tbValue1, tbValue2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnXOR_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 2)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc dwie wartoci rejestru");
                }

                var tbValue1 = GetTextbox(checkedCheckboxes[0]);
                var tbValue2 = GetTextbox(checkedCheckboxes[1]);

                HandleXorOperation(tbValue1, tbValue2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnXCHG_Click(object sender, RoutedEventArgs e)
        {
            var checkedCheckboxes = GetCheckedCheckboxes();

            try
            {
                if (checkedCheckboxes.Count != 2)
                {
                    throw new Exception("Do wykonania tej operacji potrzebujesz zaznaczyc dwie wartoci rejestru");
                }

                var tbValue1 = GetTextbox(checkedCheckboxes[0]);
                var tbValue2 = GetTextbox(checkedCheckboxes[1]);
                var temp = tbValue1.Text;
                tbValue1.Text = tbValue2.Text;
                tbValue2.Text = temp;
                RefreshMemory(Convert.ToInt32(tbValue1.Text));
                RefreshMemory(Convert.ToInt32(tbValue2.Text));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Getters

        private CheckBox GetCheckedCheckbox()
        {
            if ((bool)cbAH.IsChecked)
            {
                return cbAH;
            }
            else if((bool)cbAL.IsChecked)
            {
                return cbAL;
            }
            else if ((bool)cbBH.IsChecked)
            {
                return cbBH;
            }
            else if ((bool)cbBL.IsChecked)
            {
                return cbBL;
            }
            else if ((bool)cbCH.IsChecked)
            {
                return cbCH;
            }
            else if ((bool)cbCL.IsChecked)
            {
                return cbCL;
            }
            else if ((bool)cbDH.IsChecked)
            {
                return cbDH;
            }
            else if ((bool)cbDL.IsChecked)
            {
                return cbDL;
            }
            else
            {
                return null;
            }
        }

        private List<CheckBox> GetCheckedCheckboxes()
        {
            List<CheckBox> results = new List<CheckBox>();

            if ((bool)cbAH.IsChecked)
            {
                results.Add(cbAH);
            }
            if ((bool)cbAL.IsChecked)
            {
                results.Add(cbAL);
            }
            if ((bool)cbBH.IsChecked)
            {
                results.Add(cbBH); 
            }
            if ((bool)cbBL.IsChecked)
            {
                results.Add(cbBL);
            }
            if ((bool)cbCH.IsChecked)
            {
                results.Add(cbCH);
            }
            if ((bool)cbCL.IsChecked)
            {
                results.Add(cbCL);
            }
            if ((bool)cbDH.IsChecked)
            {
                results.Add(cbDH);
            }
            if ((bool)cbDL.IsChecked)
            {
                results.Add(cbDL);
            }

            return results;
        }

        private TextBox GetTextbox(CheckBox checkBox)
        {
            var name = checkBox.Name.Substring(2, 2);

            if (name == "AH")
            {
                return tbAH;
            }
            else if (name == "AL")
            {
                return tbAL;
            }
            else if (name == "BH")
            {
                return tbBH;
            }
            else if (name == "BL")
            {
                return tbBL;
            }
            else if (name == "CH")
            {
                return tbCH;
            }
            else if (name == "CL")
            {
                return tbCL;
            }
            else if (name == "DH")
            {
                return tbDH;
            }
            else if (name == "DL")
            {
                return tbDL;
            }
            else
            {
                return null;
            }
        }

        private int GetSingleValue(CheckBox checbkBox)
        {
            try
            {
                if ((bool)cbAH.IsChecked)
                {
                    return Convert.ToInt32(tbAH.Text, 16);
                }
                else if ((bool)cbAL.IsChecked)
                {
                    return Convert.ToInt32(tbAL.Text, 16);
                }
                else if ((bool)cbBH.IsChecked)
                {
                    return Convert.ToInt32(tbBH.Text, 16);
                }
                else if ((bool)cbBL.IsChecked)
                {
                    return Convert.ToInt32(tbBL.Text, 16);
                }
                else if ((bool)cbCH.IsChecked)
                {
                    return Convert.ToInt32(tbCH.Text, 16);
                }
                else if ((bool)cbCL.IsChecked)
                {
                    return Convert.ToInt32(tbCL.Text, 16);
                }
                else if ((bool)cbDH.IsChecked)
                {
                    return Convert.ToInt32(tbDH.Text, 16);
                }
                else if ((bool)cbDL.IsChecked)
                {
                    return Convert.ToInt32(tbDL.Text, 16);
                }
                else
                {
                    return -1;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Musisz wpisac wartosc HEX!", "Blad");
                return -1;
            }
           
        }

        #endregion

        #region Operations

        private int HandleNotOperation(CheckBox checbkBox)
        {
            var value = GetSingleValue(checbkBox);
            var result = Convert.ToString(value, 2).ToCharArray();

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '0') result[i] = '1';
                else result[i] = '0';
            }

            var temp = new string(result);
            var trueResult = Convert.ToInt32(temp, 2);

            return trueResult;
        }

        private int HandleIncOperation(CheckBox checbkBox)
        {
            var value = GetSingleValue(checbkBox);
            var result = Convert.ToInt32(value);
            result++;

            return result;
        }

        private int HandleDecOperation(CheckBox checbkBox)
        {
            var value = GetSingleValue(checbkBox);
            var result = Convert.ToInt32(value);
            result--;

            return result;
        }

        private void HandleAddOperation(TextBox tb1, TextBox tb2)
        {
            try
            {
                var tb1Dec = Convert.ToInt32(tb1.Text, 16);
                var tb2Dec = Convert.ToInt32(tb2.Text, 16);
                var result = tb1Dec + tb2Dec;


                lbLeftNumber.Content = Convert.ToString(tb1Dec, 2);
                lbRightNumber.Content = Convert.ToString(tb2Dec, 2);
                lbOperation.Content = "ADD";

                if (result > 255)
                {
                    MessageBox.Show("Wynik dodawania dal wartosc wieksza niz 255!", "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
                    lbResult.Content = "Blad!";
                }
                else
                {
                    lbResult.Content = Convert.ToString(result, 2);
                    RefreshMemory(result);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Potrzeba wartosci HEX", "Błąd");
            }
        }

        private void HandleSubOperation(TextBox tb1, TextBox tb2)
        {
            try
            {
                var tb1Dec = Convert.ToInt32(tb1.Text, 16);
                var tb2Dec = Convert.ToInt32(tb2.Text, 16);
                var result = tb1Dec - tb2Dec;

                lbLeftNumber.Content = Convert.ToString(tb1Dec, 2);
                lbRightNumber.Content = Convert.ToString(tb2Dec, 2);
                lbOperation.Content = "SUB";

                if (result > 255 || result < 0)
                {
                    MessageBox.Show("Wynik odejmowania dal wartosc wieksza niz 255 lub mniejsza niz 0!", "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
                    lbResult.Content = "Blad!";
                }
                else
                {
                    lbResult.Content = Convert.ToString(result, 2);
                    RefreshMemory(result);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Potrzeba wartosci HEX", "Błąd");
            }

        }

        private void HandleAndOperation(TextBox tb1, TextBox tb2)
        {
            try
            {
                var tb1Dec = Convert.ToInt32(tb1.Text, 16);
                var tb2Dec = Convert.ToInt32(tb2.Text, 16);
                var result = tb1Dec & tb2Dec;
                RefreshMemory(result);

                lbLeftNumber.Content = Convert.ToString(tb1Dec, 2);
                lbRightNumber.Content = Convert.ToString(tb2Dec, 2);
                lbOperation.Content = "AND";

                lbResult.Content = Convert.ToString(result, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Potrzeba wartosci HEX", "Błąd");
            }

        }

        private void HandleOrOperation(TextBox tb1, TextBox tb2)
        {
            try
            {
                var tb1Dec = Convert.ToInt32(tb1.Text, 16);
                var tb2Dec = Convert.ToInt32(tb2.Text, 16);
                var result = tb1Dec | tb2Dec;
                RefreshMemory(result);

                lbLeftNumber.Content = Convert.ToString(tb1Dec, 2);
                lbRightNumber.Content = Convert.ToString(tb2Dec, 2);
                lbOperation.Content = "OR";

                lbResult.Content = Convert.ToString(result, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Potrzeba wartosci HEX", "Błąd");
            }

        }

        private void HandleXorOperation(TextBox tb1, TextBox tb2)
        {
            try
            {
                var tb1Dec = Convert.ToInt32(tb1.Text, 16);
                var tb2Dec = Convert.ToInt32(tb2.Text, 16);
                var result = tb1Dec ^ tb2Dec;
                RefreshMemory(result);

                lbLeftNumber.Content = Convert.ToString(tb1Dec, 2);
                lbRightNumber.Content = Convert.ToString(tb2Dec, 2);
                lbOperation.Content = "XOR";

                lbResult.Content = Convert.ToString(result, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Potrzeba wartosci HEX", "Błąd");
            }
            
        }

        private void RefreshMemory(int value)
        {
            var bytes = Convert.ToString(value, 2).ToCharArray();

            for (int i = 0; i < bytes.Length; i++, counter++)
            {
                if (counter > memory.Length - 1)
                {
                    MessageBox.Show("Pamiec pelna!", "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                memory[counter] = bytes[i];
            
            }

            for (int i = 0; i < counter; i++)
            {
                tbMemory.Text += $"{memory[i]} ";
            }
        }

        #endregion
    }
}
