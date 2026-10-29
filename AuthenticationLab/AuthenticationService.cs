using System;
using System.Collections.Generic;

namespace AuthenticationLab
{
    public class AuthenticationService
    {
        private readonly List<IAuthenticator> _authenticators;

        public AuthenticationService(List<IAuthenticator> authenticators)
        {
            _authenticators = authenticators;
        }

        public bool Authenticate(Credentials credentials)
        {
            Console.WriteLine();
            Console.WriteLine("=== НАЧАЛО АУТЕНТИФИКАЦИИ ===");

            foreach (IAuthenticator authenticator in _authenticators)
            {
                if (authenticator.Authenticate(credentials))
                {
                    Console.WriteLine("=== АУТЕНТИФИКАЦИЯ УСПЕШНА ===");
                    return true;
                }
            }

            Console.WriteLine("=== АУТЕНТИФИКАЦИЯ НЕ ПРОЙДЕНА ===");
            return false;
        }
    }
}