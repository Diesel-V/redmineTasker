namespace RedmineTasker.AuthModel.Auth
{
    /// <summary>
    /// класс для хранения связки пароля и пользователя, с шифрованием данных
    /// </summary>
    public class RedmineCredentials
    {
        public ApiKeyCredentials ApiKeyCredentials { get; set; }
        public LoginPasswordCredentials LoginPasswordCredentials { get; set; }

                
    }
}
