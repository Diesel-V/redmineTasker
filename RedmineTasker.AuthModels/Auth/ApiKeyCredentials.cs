namespace RedmineTasker.AuthModels.Auth
{
    /// <summary>
    /// класс для хранения связки пароля и пользователя, с шифрованием данных
    /// </summary>
    public class ApiKeyCredentials
    {
        public string ApiKey { get; set; }
    }
}
