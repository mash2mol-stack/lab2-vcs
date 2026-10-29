using System.Collections.Generic;
using AuthenticationLab;
using Xunit;

namespace AuthenticationLab.Tests
{
    public class AuthenticationTests
    {
        [Fact]
        public void Authenticate_WithCorrectPassword_ReturnsTrue()
        {
            var authenticators = new List<IAuthenticator>
            {
                new PasswordAuthenticator()
            };

            var service = new AuthenticationService(authenticators);

            var credentials = new Credentials
            {
                Login = "maria",
                Password = "1234"
            };

            bool result = service.Authenticate(credentials);

            Assert.True(result, "Правильные логин и пароль должны приниматься.");
        }

        [Fact]
        public void Authenticate_WithInvalidCredentials_ReturnsFalse()
        {
            var authenticators = new List<IAuthenticator>
            {
                new PasswordAuthenticator()
            };

            var service = new AuthenticationService(authenticators);

            var credentials = new Credentials
            {
                Login = "wrong",
                Password = "wrong",
                Fingerprint = "wrong",
                QrCode = "wrong",
                Token = "wrong"
            };

            bool result = service.Authenticate(credentials);

            Assert.False(result, "Неверные логин и пароль должны отклоняться.");
        }

        [Fact]
        public void Authenticate_WithCorrectToken_ReturnsTrue()
        {
            var authenticators = new List<IAuthenticator>
            {
                new TokenAuthenticator()
            };

            var service = new AuthenticationService(authenticators);

            var credentials = new Credentials
            {
                Login = "wrong",
                Password = "wrong",
                Fingerprint = "wrong",
                QrCode = "wrong",
                Token = "TOKEN-2026"
            };

            bool result = service.Authenticate(credentials);

            Assert.True(result, "Правильный токен должен приниматься.");
        }
    }
}