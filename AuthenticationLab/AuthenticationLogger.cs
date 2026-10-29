
using Serilog;

namespace AuthenticationLab
{
    public static class AuthenticationLogger
    {
        public static void LogAuthenticationResult(
            string method,
            bool success)
        {
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
