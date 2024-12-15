using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace WPFView
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }
        private void ValidationNameTextBox(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только буквы (русские и английские)
            if (!Regex.IsMatch(e.Text, @"^[а-яА-Яa-zA-Z\s\b]+$"))
            {
                e.Handled = true; // Блокируем ввод
            }
        }
        private void ValidationGroupTextBox(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только буквы (русские и английские)
            if (!Regex.IsMatch(e.Text, @"^[а-яА-Яa-zA-Z\s\b0-9]+$"))
            {
                e.Handled = true; // Блокируем ввод
            }
        }
    }
}
