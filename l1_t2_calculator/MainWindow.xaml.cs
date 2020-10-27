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

namespace l1_t2_calculator
{
    /// <summary>
    /// Калькулятор: 
    /// 1. Имеет активный регистр, где происходит ввод активного числа
    ///     с которым мы будем работать
    /// 2. Имеет регистр результат - куда будет сохраняться результат
    ///     текущей операции
    /// 3. Имеет регистр истории - куда пишется выражение введённое пользователем
    ///     до нажатия кнопки "=" (вычислить)
    /// 4. Активный регистр ограничен 7 символами
    /// </summary>
    public partial class MainWindow : Window
    {
        const int MAX_ACTIVE_DIGITS = 7;

        float activeRegister = 0.0f;
        float resultRegister = 0.0f;
        string history = "";
        int activeRegisterDigits = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(1);
        }

        private void ShowActiveRegister()
        {
            textActiveRegister.Text = activeRegister.ToString(); 
        }

        #region Calculator model
        /// <summary>
        /// Сдвинуть активный регистр на 1 порядок (*10) и добавить 
        /// данное значение
        /// </summary>
        /// <param name="value">Значение к добавление</param>
        private void GrowNumberBy(int value)
        {
            if(activeRegisterDigits == MAX_ACTIVE_DIGITS)
            {
                MessageBox.Show($"Maximum digits {MAX_ACTIVE_DIGITS}");
                return;
            }

            activeRegisterDigits += 1;

            activeRegister *= 10;
            activeRegister += value;
        }

        #endregion

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(2);
        }

        /// <summary>
        /// Увеличить активный регистр
        /// </summary>
        /// <param name="value"></param>
        private void GrowAndShowBy(int value)
        {
            GrowNumberBy(value);
            ShowActiveRegister();
        }
    }
}
