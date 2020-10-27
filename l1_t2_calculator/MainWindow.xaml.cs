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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        float activeRegister = 0.0f;
        float resultRegister = 0.0f;
        string history = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            GrowNumberBy(1);
            ShowActiveRegister();
        }

        private void ShowActiveRegister()
        {
            textActiveRegister.Text = activeRegister.ToString(); 
        }

        /// <summary>
        /// Сдвинуть активный регистр на 1 порядок (*10) и добавить 
        /// данное значение
        /// </summary>
        /// <param name="value">Значение к добавление</param>
        private void GrowNumberBy(int value)
        {
            activeRegister *= 10;
            activeRegister += value;
        }
    }
}
