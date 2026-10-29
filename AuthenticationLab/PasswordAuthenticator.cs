using System;

namespace AuthenticationLab
{
    public class PasswordAuthenticator : IAuthenticator
    {
        public bool Authenticate(Credentials credentials)
        {
            Console.WriteLine("[PASSWORD] Проверка логина и пароля...");

            if (credentials.Login == "maria" &&
                credentials.Password == "1234")
            {
                Console.WriteLine("[PASSWORD] Аутентификация успешна.");
                return true;
            }

            Console.WriteLine("[PASSWORD] Неверный логин или пароль.");
            return false;
        }
    }
}