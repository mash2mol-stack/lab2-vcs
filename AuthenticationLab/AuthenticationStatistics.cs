
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthenticationLab
{
    public static class AuthenticationStatistics
    {
        public static void PrintSummary(
            List<AuthenticationRecord> history)
        {
            if (history == null || history.Count == 0)
            {
                Console.WriteLine("История аутентификации пуста.");
                return;
            }

            int total = history.Count;
            int successful = history.Count(x => x.Success);
            int failed = total - successful;

            double successPercent =
                (double)successful / total * 100;

            Console.WriteLine();
            Console.WriteLine("=== СТАТИСТИКА АУТЕНТИФИКАЦИИ ===");
            Console.WriteLine("Всего попыток: " + total);
            Console.WriteLine("Успешных попыток: " + successful);
            Console.WriteLine("Неуспешных попыток: " + failed);
            Console.WriteLine(
                "Процент успешных попыток: " +
                successPercent.ToString("F2") + "%");
            Console.WriteLine("================================");
        }
    }
}
