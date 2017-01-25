using System;
using System.Windows;
using System.Windows.Controls;
using redmineTasker.Forms;
using RedmineTasker.AuthModel.Auth;

namespace redmineTasker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void authTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            passwdLabel.Visibility = passwdTextBox.Visibility = authTypeComboBox.SelectedIndex == 0 ? Visibility.Hidden : Visibility.Visible;
            Application.Current.MainWindow.Height = authTypeComboBox.SelectedIndex == 0 ? 167 : 195;
            loginLabel.Content = authTypeComboBox.SelectedIndex == 1 ? "Имя пользователя:": "Ключ:";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri redmineUri;
            if (!Uri.TryCreate(redmineAddressTextBox.Text, UriKind.Absolute, out redmineUri))
            {
                MessageBox.Show(this, "Неправильно задан адрес Redmine", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                redmineAddressTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(loginTextBox.Text))
            {
                MessageBox.Show(this, "Не заполненно поле авторизации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                loginTextBox.Focus();
                return;
            }
            var credentialClass = new RedmineCredentials();
            if ((string)authTypeComboBox.SelectionBoxItem == "Api Key")
            {

                credentialClass.ApiKeyCredentials = new ApiKeyCredentials { ApiKey = loginTextBox.Text };
                credentialClass.LoginPasswordCredentials = null;
            }
            else
            {
                credentialClass.ApiKeyCredentials = null;
                credentialClass.LoginPasswordCredentials = new LoginPasswordCredentials
                {
                    UserName = loginTextBox.Text,
                    UserPassword = passwdTextBox.Text
                };
            }
            
            var createTaskWindow = new CreateTaskWindow(credentialClass, redmineUri, this);
            createTaskWindow.Show();            
        }
    }
}
