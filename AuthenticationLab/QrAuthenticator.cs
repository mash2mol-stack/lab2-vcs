using System;

namespace AuthenticationLab
{
    public class QrAuthenticator : IAuthenticator
    {
        public bool Authenticate(Credentials credentials)
        {
            Console.WriteLine("[QR] Проверка QR-кода...");

            if (credentials.QrCode == "QR-2026")
            {
                Console.WriteLine("[QR] Аутентификация успешна.");
                return true;
            }

            Console.WriteLine("[QR] QR-код недействителен.");
            return false;
        }
    }
}