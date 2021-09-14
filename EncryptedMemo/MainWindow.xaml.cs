using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EncryptedMemo
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow: MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();


            if (Settings.Default.MainWindowStat == null) return;
            this.Top = Settings.Default.MainWindowStat.Top;
            this.Left = Settings.Default.MainWindowStat.Left;
            this.Width = Settings.Default.MainWindowStat.Width;
            this.Height = Settings.Default.MainWindowStat.Height;
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            _loginCtrl = new LoginControl();
            _loginCtrl.ButtonCancel.Click += ButtonCancelOnClick;
            _loginCtrl.ButtonLogin.Click += ButtonLoginOnClick;
            _loginCtrl.Loaded += FocusPass;
            _customDialog = new CustomDialog { Name = "PASS", Content = _loginCtrl };
            await this.ShowMetroDialogAsync(_customDialog);
        }
        
        private void LoadMainContent(string pass)
        {
            _mwvm = new MainWindowViewModel("pass");
            this.DataContext = _mwvm;
        }

        MainWindowViewModel _mwvm;

        private void ForcusTxt(object sender, EventArgs e)
        {
            MemoTxt.Focus();
        }

        public void TaskTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            _mwvm.Memo.SaveData();
        }
        private void TaskWindow_Closed(object sender, EventArgs e)
        {
            Settings.Default.MainWindowStat = new WindowStat { Height = Height, Width = Width, Left = Left, Top = Top };
            Settings.Default.Save();
        }


        #region LoginCheck
        private CustomDialog _customDialog;
        private LoginControl _loginCtrl;

        private void ButtonLoginOnClick(object sender, RoutedEventArgs e)
        {
            var pass = _loginCtrl.PasswordBox1.Password;
            if (string.IsNullOrEmpty(pass)) return;
            this.HideMetroDialogAsync(_customDialog);
            LoadMainContent(pass);
        }
        private void ButtonCancelOnClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void FocusPass(object sender, EventArgs e)
        {
            _loginCtrl.PasswordBox1.Focus();
        }

        #endregion
    }
}
