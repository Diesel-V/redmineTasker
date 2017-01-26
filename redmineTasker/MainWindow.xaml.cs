using System;
using System.IO;
using System.Text.RegularExpressions;
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
        private int _port = 80; //https 443
    
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            authTypeComboBox.SelectionChanged += authTypeComboBox_SelectionChanged;
            portTextBox.Text = _port.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void authTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loginLabel.Visibility = loginTextBox.Visibility = authTypeComboBox.SelectedIndex == 1 ? Visibility.Hidden : Visibility.Visible;
            passwordLabel.Margin = authTypeComboBox.SelectedIndex == 1 ? new Thickness(10, 176, 0, 0) : new Thickness(10, 199, 0, 0);
            passwordTextBox.Margin = authTypeComboBox.SelectedIndex == 1 ? new Thickness(142, 179, 10, 0) : new Thickness(142, 207, 10, 0);
            Application.Current.MainWindow.Height = authTypeComboBox.SelectedIndex == 1 ? 280 : 310;
            passwordLabel.Content = authTypeComboBox.SelectedIndex == 0 ? "Пароль:" : "Ключ:";
        }

        private void UriCombine()
        {
            var qwe = new UriBuilder
            {
                Scheme = ((ContentControl) schemaComboBox.SelectedItem).Content.ToString(),
                Host = hostTextBox.Text,
                Port = int.Parse(_port.ToString()),
                Path = urlPathTextBox.Text
            };
            redmineAddressTextBox.Text = qwe.ToString();
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
            if (string.IsNullOrEmpty(passwordTextBox.Password))
            {
                MessageBox.Show(this, "Не заполненно поле авторизации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                loginTextBox.Focus();
                return;
            }
            var credentialClass = new RedmineCredentials();
            if ((string)authTypeComboBox.SelectionBoxItem == "Api Key")
            {

                credentialClass.ApiKeyCredentials = new ApiKeyCredentials { ApiKey = passwordTextBox.Password };
                credentialClass.LoginPasswordCredentials = null;
            }
            else
            {
                credentialClass.ApiKeyCredentials = null;
                credentialClass.LoginPasswordCredentials = new LoginPasswordCredentials
                {
                    UserName = loginTextBox.Text,
                    UserPassword = passwordTextBox.Password
                };
            }
            
            var createTaskWindow = new CreateTaskWindow(credentialClass, redmineUri, this);
            createTaskWindow.Show();            
        }

        private void hostTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UriCombine();
        }

        private void portTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
            UriCombine();
        }

        private void urlPathTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UriCombine();
        }

        private void portTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int port;
            if (int.TryParse(((TextBox)sender).Text, out port))
            {
                _port = port;
            }
            portTextBox.Text = _port.ToString();
        }        
    }
}
