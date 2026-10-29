using System;

namespace AuthenticationLab
{
    public class FingerprintAuthenticator : IAuthenticator
    {
        public bool Authenticate(Credentials credentials)
        {
            Console.WriteLine("[FINGERPRINT] Проверка отпечатка пальца...");

            if (credentials.Fingerprint == "FP-001")
            {
                Console.WriteLine("[FINGERPRINT] Аутентификация успешна.");
                return true;
            }

            Console.WriteLine("[FINGERPRINT] Отпечаток не распознан.");
            return false;
        }
    }
}