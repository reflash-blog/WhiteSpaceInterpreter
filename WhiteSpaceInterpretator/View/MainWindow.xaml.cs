/*
 * @author Ekzaryan Daniil 
 * @2014
 * @website http://refwarlock.blogspot.ru 
 */

using System.IO;
using System.Windows;
using System.Xml;
using WhiteSpaceInterpretator.ViewModel;

namespace WhiteSpaceInterpretator.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Stream xshd_stream = File.OpenRead(@"CustomHighlighting.xshd");
            XmlTextReader xshd_reader = new XmlTextReader(xshd_stream);
            TextEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(xshd_reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
            this.DataContext = new MainWindowContext();
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
