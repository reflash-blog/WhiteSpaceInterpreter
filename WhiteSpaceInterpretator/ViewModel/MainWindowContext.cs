/*
 * @author Ekzaryan Daniil 2014
 * @website http://refwarlock.blogspot.ru 
 */

using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Utils;
using Microsoft.Win32;
using WhiteSpaceInterpretator.Controller;

namespace WhiteSpaceInterpretator.ViewModel
{
    class MainWindowContext : INotifyPropertyChanged
    {
        public MainWindowContext()
        {
            Document = new TextDocument();

        }
        private TextDocument _document = null;
        public TextDocument Document
        {
            get { return this._document; }
            set
            {
                if (this._document != value)
                {
                    this._document = value;
                    RaisePropertyChanged("Document");
                }
            }
        }

        private string _output = null;
        public string Output
        {
            get { return this._output; }
            set { _output = value; RaisePropertyChanged("Output"); }
        }

        #region OpenCommand
        RelayCommand _openCommand = null;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                {
                    _openCommand = new RelayCommand((p) => OnOpen(p), (p) => CanExecuteOpenCommand(p));
                }

                return _openCommand;
            }
        }

        private bool CanExecuteOpenCommand(object parameter)
        {
            return true;
        }


        public void OnOpen(object parameter)
        {
            var dlg = new OpenFileDialog();
            if (!dlg.ShowDialog().GetValueOrDefault()) return;
            using (var fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = FileReader.OpenStream(fs, Encoding.UTF8))
                {
                    Document = new TextDocument(reader.ReadToEnd());
                }
            }
        }
        #endregion

        #region InterpreteCommand
        RelayCommand _interpreteCommand = null;
        public ICommand InterpreteCommand
        {
            get
            {
                if (_interpreteCommand == null)
                {
                    _interpreteCommand = new RelayCommand((p) => OnInterprete(p), (p) => CanExecuteInterpreteCommand(p));
                }

                return _interpreteCommand;
            }
        }

        private bool CanExecuteInterpreteCommand(object parameter)
        {
            return true;
        }


        public async void OnInterprete(object parameter)
        {
            var interpretator = new WhitespaceInterpretator();
            await interpretator.Execute(Document.Text);
            Output += interpretator.output;
        }


        #endregion

        #region AboutCommand
        RelayCommand _aboutCommand = null;
        public ICommand AboutCommand
        {
            get
            {
                if (_aboutCommand == null)
                {
                    _aboutCommand = new RelayCommand((p) => OnAbout(p), (p) => CanExecuteAboutCommand(p));
                }

                return _aboutCommand;
            }
        }

        private bool CanExecuteAboutCommand(object parameter)
        {
            return true;
        }


        public void OnAbout(object parameter)
        {
            MessageBox.Show(
                "О программе\nПрограмма интерпретирует исходные файлы языка WhiteSpace ");
        }
        #endregion

        #region HelpCommand
        RelayCommand _helpCommand = null;
        public ICommand HelpCommand
        {
            get
            {
                if (_helpCommand == null)
                {
                    _helpCommand = new RelayCommand((p) => OnHelp(p), (p) => CanExecuteHelpCommand(p));
                }

                return _helpCommand;
            }
        }

        private bool CanExecuteHelpCommand(object parameter)
        {
            return true;
        }


        public void OnHelp(object parameter)
        {

        }
        #endregion

        #region SaveCommand
        RelayCommand _saveCommand = null;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand((p) => OnSave(p), (p) => CanExecuteSaveCommand(p));
                }

                return _saveCommand;
            }
        }

        private bool CanExecuteSaveCommand(object parameter)
        {
            return true;
        }


        public void OnSave(object parameter)
        {
            
        }
        #endregion   
  

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
