using System;

namespace AuthenticationLab
{
    public class TokenAuthenticator : IAuthenticator
    {
        public bool Authenticate(Credentials credentials)
        {
            Console.WriteLine("[TOKEN] Проверка токена...");

            if (credentials.Token == "TOKEN-2026")
            {
                Console.WriteLine("[TOKEN] Аутентификация успешна.");
                return true;
            }

            Console.WriteLine("[TOKEN] Токен недействителен.");
            return false;
        }
    }
}