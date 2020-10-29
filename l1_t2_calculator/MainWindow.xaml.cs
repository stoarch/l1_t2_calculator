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
    /// 7. При нажатии на СЕ стирается только активный регистр
    /// 8. При нажатии на удалить (<) активный регистр уменьшается на 1 цифру 
    /// </summary>
    public partial class MainWindow : Window
    {
        enum CalculatorState
        {
            Unknown,
            InputFirstNumber,
            InputSecondNumber,
            CalculateResult
        }

        const int MAX_ACTIVE_DIGITS = 7;
        const float EPS = 1e-3f;

        float activeRegisterValue = 0.0f;
        float resultRegisterValue = 0.0f;
        string historyValue = "";

        int activeRegisterDigits = 0;
        private CalculatorState calculatorState = CalculatorState.InputFirstNumber;
        private string currentOperationName;
        private Func<float, float> currentOperationFun;
        private Func<float, float> prevOperationFunction;
        private string prevOperationName;

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
            textRegister.Text = activeRegisterValue.ToString(); 
        }

        private void ShowResultRegister()
        {
            textRegister.Text = resultRegisterValue.ToString(); 
        }

        private void RefreshCalculatorView()
        {
            ShowActiveRegister();
            ShowHistory();
        }

        private void ShowHistory()
        {
            textHistory.Text = historyValue;
        }
        #endregion

        #region Calculator model
        /// <summary>
        /// Сдвинуть активный регистр на 1 порядок (*10) и добавить 
        /// данное значение
        /// </summary>
        /// <param name="value">Значение к добавление</param>
        private void GrowActiveRegisterDigitsBy(int value) //TODO: Move Calculator: activeRegister -> Register class
        {
            if(activeRegisterDigits == MAX_ACTIVE_DIGITS)
            {
                MessageBox.Show($"Maximum digits {MAX_ACTIVE_DIGITS}");
                return;
            }

            if((Math.Abs(activeRegisterValue) < EPS )&&(value == 0))
            {
                return;
            }

            activeRegisterDigits += 1;

            activeRegisterValue *= 10;
            activeRegisterValue += value;
        }

        /// <summary>
        /// Уменьшить активный регистр на 1 цифру (делить на 10 нацело)
        /// </summary>
        void ShrinkActiveRegisterDigitsByOne() //TODO: Move Calculator: activeRegister -> Register class
        {
            if(activeRegisterDigits == 0)
            {
                return;
            }

            if(Math.Abs(activeRegisterValue) < EPS)
            {
                return;
            }

            activeRegisterValue /= 10.0F;
            activeRegisterValue = (float)Math.Floor((double)activeRegisterValue);

            activeRegisterDigits -= 1;
        }

        /// <summary>
        /// Очистить все регистры и историю (0 поставить в активный и результат регистры)
        /// </summary>
        void ClearCalculator() //TODO: Move -> Calculator class
        {
            ClearActiveRegister();
            resultRegisterValue = 0;
            historyValue = "";
            SetCalculatorState(CalculatorState.InputFirstNumber);
        }

        void ClearActiveRegister() //TODO: Move -> Register (active)
        {
            activeRegisterValue = 0;
            activeRegisterDigits = 0;
        }

        void SetCalculatorState( CalculatorState newState)
        {
            calculatorState = newState;
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
            GrowActiveRegisterDigitsBy(value);
            ShowActiveRegister();
        }

        /// <summary>
        /// Уменьшить регистр на одну цифру (разделить на 10 цело) и показать его
        /// </summary>
        private void ShrinkActiveRegisterAndShow()
        {
            ShrinkActiveRegisterDigitsByOne();
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
            ShowActiveRegister();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            ShrinkActiveRegisterAndShow();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ApplyAddition();
        }

        private void ApplyAddition()
        {
            currentOperationName = "+";
            currentOperationFun = (res) => res += activeRegisterValue;

            ApplyOperationToActiveRegisterAndView(currentOperationName, currentOperationFun);
        }

        private void ApplyOperationToActiveRegisterAndView(string operationName, Func<float, float> operationFunction)
        {
            try
            {
                if (calculatorState == CalculatorState.InputFirstNumber)
                {
                    SetCalculatorState(CalculatorState.InputSecondNumber);

                    CopyActiveToResultRegister();

                    SetHistoryToValueWithOperation(activeRegisterValue, operationName);

                    prevOperationFunction = operationFunction;
                }
                else if (calculatorState == CalculatorState.InputSecondNumber)
                {
                    CalculateResultFromOperation(operationName, prevOperationFunction);

                    prevOperationFunction = operationFunction;
                }

                //Show previous register when we can enter new from scratch
                activeRegisterDigits = 0; //clear calculator
                activeRegisterValue = 0;

                ShowHistory();
            }catch(Exception e)
            {
                ShowErrorMessage(e.Message);
            }
        }

        private void CalculateResultFromOperation(string operationName, Func<float, float> operationFunction)
        {
            ApplyOperationToResultRegister(operationFunction);

            AppendHistoryWithValueAndOperation(activeRegisterValue, operationName);

            ShowResultRegister();
        }

        private void CalculateResultTotal(Func<float, float> operationFunction)
        {
            try
            {
                ApplyOperationToResultRegister(operationFunction);

                ShowTotalInHistory();

                activeRegisterValue = resultRegisterValue;
                resultRegisterValue = 0;

                ShowActiveRegister();
            }catch(Exception e)
            {
                ShowErrorMessage(e.Message);
            }
        }

        private void ShowTotalInHistory()
        {
            historyValue += $"{activeRegisterValue} = {resultRegisterValue}";
        }

        private void ApplyOperationToResultRegister(Func<float, float> operation)
        {
            resultRegisterValue = operation(resultRegisterValue);
        }

        private void AppendHistoryWithValueAndOperation(float value, string operation)
        {
            historyValue += $"{value} {operation} ";
        }

        private void SetHistoryToValueWithOperation(float value, string operation)
        {
            historyValue = $"{value} {operation}";
        }

        private void CopyActiveToResultRegister()
        {
            resultRegisterValue = activeRegisterValue;
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            ApplyCalculation();
        }

        private void ApplyCalculation()
        {
            if (calculatorState == CalculatorState.InputFirstNumber)
            {
                return;
            }

            CalculateResultTotal(currentOperationFun);

            SetCalculatorState(CalculatorState.InputFirstNumber);

            ShowHistory();
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            ApplySubstraction();
        }

        private void ApplySubstraction()
        {
            currentOperationName = "-";
            currentOperationFun = (res) => res -= activeRegisterValue;

            ApplyOperationToActiveRegisterAndView(currentOperationName, currentOperationFun);
        }

        private void btnMul_Click(object sender, RoutedEventArgs e)
        {
            ApplyMultiplication();
        }

        private void ApplyMultiplication()
        {
            currentOperationName = "*";
            currentOperationFun = (res) => res *= activeRegisterValue;

            ApplyOperationToActiveRegisterAndView(currentOperationName, currentOperationFun);
        }

        private void btnDivision_Click(object sender, RoutedEventArgs e)
        {
            ApplyDivision();
        }

        private void ApplyDivision()
        {
            currentOperationName = "/";
            currentOperationFun = (res) =>
            {
                if (activeRegisterValue == 0)
                {
                    ShowErrorMessage("Division by zero"); ; ;
                    throw new DivideByZeroException();
                }

                return res /= activeRegisterValue;
            };

            ApplyOperationToActiveRegisterAndView(currentOperationName, currentOperationFun);
        }

        private void ShowErrorMessage(string message)
        {
            textRegister.Text = message;
        }

        private void btnPoint_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Implement floating points
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                ClearCalculator();
                RefreshCalculatorView();
            }
            else if(e.Key == Key.Enter)
            {
                ApplyCalculation();
            }
            else if(e.Key == Key.Add)
            {
                ApplyAddition();
            }
            else if(e.Key == Key.Subtract)
            {
                ApplySubstraction();
            }
            else if(e.Key == Key.Multiply)
            {
                ApplyMultiplication();
            }
            else if(e.Key == Key.Divide)
            {
                ApplyDivision();
            }
            else if(e.Key == Key.Back)
            {
                ShrinkActiveRegisterAndShow();
            }
        }

    }
}
