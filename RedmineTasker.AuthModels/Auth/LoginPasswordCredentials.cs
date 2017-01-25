namespace RedmineTasker.AuthModels.Auth
{
    /// <summary>
    /// класс для хранения связки пароля и пользователя, с шифрованием данных
    /// </summary>
    public class LoginPasswordCredentials
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
