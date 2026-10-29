
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Serilog;

namespace AuthenticationLab
{
    // Класс для хранения результата аутентификации
    public class AuthenticationRecord
    {
        public string Method { get; set; }
        public bool Success { get; set; }
        public DateTime Time { get; set; }
    }

    class Program
    {
        // Путь к папке и файлу журнала
        private static readonly string LogDirectory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        private static readonly string LogFile =
            Path.Combine(LogDirectory, "app.log");

        // Путь к файлу с результатами аутентификации
        private static readonly string JsonFile =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "authentication.json"
            );

        // Список результатов всех четырёх тестов
        private static readonly List<AuthenticationRecord> History =
            new List<AuthenticationRecord>();

        static void Main(string[] args)
        {
            ConfigureLogger();

            try
            {
                PasswordAuthenticator passwordAuthenticator =
                    new PasswordAuthenticator();

                FingerprintAuthenticator fingerprintAuthenticator =
                    new FingerprintAuthenticator();

                QrAuthenticator qrAuthenticator =
                    new QrAuthenticator();

                TokenAuthenticator tokenAuthenticator =
                    new TokenAuthenticator();

                List<IAuthenticator> authenticators =
                    new List<IAuthenticator>
                    {
                        passwordAuthenticator,
                        fingerprintAuthenticator,
                        qrAuthenticator,
                        tokenAuthenticator
                    };

                AuthenticationService authenticationService =
                    new AuthenticationService(authenticators);

                // Тест 1 — успешная аутентификация по паролю
                Credentials passwordCredentials = new Credentials
                {
                    Login = "maria",
                    Password = "1234"
                };

                Console.WriteLine("ТЕСТ 1: АУТЕНТИФИКАЦИЯ ПО ПАРОЛЮ");

                bool passwordResult =
                    authenticationService.Authenticate(passwordCredentials);

                SaveAuthentication("Пароль", passwordResult);

                // Тест 2 — переход к QR-коду
                Credentials qrCredentials = new Credentials
                {
                    Login = "wrong",
                    Password = "wrong",
                    Fingerprint = "wrong",
                    QrCode = "QR-2026"
                };

                Console.WriteLine();
                Console.WriteLine("ТЕСТ 2: АУТЕНТИФИКАЦИЯ ПО QR-КОДУ");

                bool qrResult =
                    authenticationService.Authenticate(qrCredentials);

                SaveAuthentication("QR-код", qrResult);

                // Тест 3 — аутентификация по токену
                Credentials tokenCredentials = new Credentials
                {
                    Login = "wrong",
                    Password = "wrong",
                    Fingerprint = "wrong",
                    QrCode = "wrong",
                    Token = "TOKEN-2026"
                };

                Console.WriteLine();
                Console.WriteLine("ТЕСТ 3: АУТЕНТИФИКАЦИЯ ПО ТОКЕНУ");

                bool tokenResult =
                    authenticationService.Authenticate(tokenCredentials);

                SaveAuthentication("Токен", tokenResult);

                // Тест 4 — все способы неуспешны
                Credentials invalidCredentials = new Credentials
                {
                    Login = "wrong",
                    Password = "wrong",
                    Fingerprint = "wrong",
                    QrCode = "wrong",
                    Token = "wrong"
                };

                Console.WriteLine();
                Console.WriteLine("ТЕСТ 4: НЕУСПЕШНАЯ АУТЕНТИФИКАЦИЯ");

                bool invalidResult =
                    authenticationService.Authenticate(invalidCredentials);

                SaveAuthentication("Все способы", invalidResult);

                Console.WriteLine();
                Console.WriteLine("Результаты сохранены в authentication.json");
                Console.WriteLine("Журнал работы сохранён в logs/app.log");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Произошла ошибка в приложении");
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            finally
            {
                Log.Information("Приложение завершило работу");
                Log.CloseAndFlush();
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }

        // Настройка Serilog: вывод в консоль и файл
        private static void ConfigureLogger()
        {
            Directory.CreateDirectory(LogDirectory);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(
                    LogFile,
                    rollingInterval: RollingInterval.Day
                )
                .CreateLogger();

            Log.Information("Приложение запущено");
        }

        // Сохранение результата аутентификации в JSON
        private static void SaveAuthentication(
            string method,
            bool success)
        {
            AuthenticationRecord record = new AuthenticationRecord
            {
                Method = method,
                Success = success,
                Time = DateTime.Now
            };

            History.Add(record);

            string json = JsonConvert.SerializeObject(
                History,
                Formatting.Indented
            );

            File.WriteAllText(JsonFile, json);

            if (success)
            {
                Log.Information(
                    "Аутентификация успешна. Способ: {Method}",
                    method
                );
            }
            else
            {
                Log.Warning(
                    "Аутентификация не пройдена. Способ: {Method}",
                    method
                );
            }
        }
    }
}
