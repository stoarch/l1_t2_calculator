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
    /// 5. При нажатии 0 происходит сдвиг регистра (на 10), но только когда он > 0
    /// 6. При нажатии на С стирается всё состояние калькулятора (активный регистр, результат
    ///     регистр и история)
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

        #region Calculator view model
        private void ShowActiveRegister()
        {
            textActiveRegister.Text = activeRegister.ToString(); 
        }

        private void RefreshCalculatorView()
        {
            ShowActiveRegister();
            textHistory.Text = history;
        }
        #endregion

        #region Calculator model
        /// <summary>
        /// Сдвинуть активный регистр на 1 порядок (*10) и добавить 
        /// данное значение
        /// </summary>
        /// <param name="value">Значение к добавление</param>
        private void GrowNumberBy(int value) // Move -> Register class
        {
            if(activeRegisterDigits == MAX_ACTIVE_DIGITS)
            {
                MessageBox.Show($"Maximum digits {MAX_ACTIVE_DIGITS}");
                return;
            }

            if((activeRegister == 0)&&(value == 0))
            {
                return;
            }

            activeRegisterDigits += 1;

            activeRegister *= 10;
            activeRegister += value;
        }

        /// <summary>
        /// Очистить все регистры и историю (0 поставить в активный и результат регистры)
        /// </summary>
        void ClearCalculator() // Move -> Calculator class
        {
            ClearActiveRegister();
            activeRegisterDigits = 0;
            resultRegister = 0;
            history = "";
        }

        void ClearActiveRegister() // Move -> Register (active)
        {
            activeRegister = 0;
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

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(3);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(4);
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(5);
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(6);
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(7);
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(8);
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(9);
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            GrowAndShowBy(0);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            ClearCalculator();
            RefreshCalculatorView();
        }

        private void btnCE_Click(object sender, RoutedEventArgs e)
        {
            ClearActiveRegister();
            RefreshCalculatorView();
        }
    }
}
