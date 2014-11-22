/*
 * @author Ekzaryan Daniil 
 * @2014
 * @website http://refwarlock.blogspot.ru 
 */
using System.Windows;

namespace WhiteSpaceInterpretator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow()
        {
            InitializeComponent();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            if(InputTextBox.Text.Length>0)
                Close();
            else
            {
                MessageBox.Show("Введите символ или число");
            }
        }
    }
}
