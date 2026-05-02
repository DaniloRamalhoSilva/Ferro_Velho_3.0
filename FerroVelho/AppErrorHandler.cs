using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerroVelho
{
    internal static class AppErrorHandler
    {
        public static void Register()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowFriendlyMessage(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowFriendlyMessage(e.ExceptionObject as Exception);
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            ShowFriendlyMessage(e.Exception);
        }

        private static void ShowFriendlyMessage(Exception exception)
        {
            try
            {
                MessageBox.Show(
                    BuildFriendlyMessage(exception),
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch
            {
            }
        }

        private static string BuildFriendlyMessage(Exception exception)
        {
            exception = Unwrap(exception);

            if (exception == null)
            {
                return "Ocorreu um erro inesperado. Tente novamente.";
            }

            if (ContainsException<TaskCanceledException>(exception) || ContainsException<TimeoutException>(exception))
            {
                return "A API demorou para responder. Verifique a conexão e tente novamente.";
            }

            if (ContainsException<HttpRequestException>(exception) ||
                ContainsException<WebException>(exception) ||
                ContainsException<SocketException>(exception))
            {
                return "Não foi possível conectar à API. Verifique se o servidor está aberto e tente novamente.";
            }

            string message = exception.Message;
            if (string.IsNullOrWhiteSpace(message) || IsTechnicalMessage(message))
            {
                return "Ocorreu um erro inesperado. Tente novamente. Se o problema continuar, chame o suporte.";
            }

            return message;
        }

        private static Exception Unwrap(Exception exception)
        {
            while (exception is TargetInvocationException && exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            if (exception is AggregateException aggregate && aggregate.InnerExceptions.Count == 1)
            {
                return Unwrap(aggregate.InnerExceptions[0]);
            }

            return exception;
        }

        private static bool ContainsException<TException>(Exception exception)
            where TException : Exception
        {
            while (exception != null)
            {
                if (exception is TException)
                {
                    return true;
                }

                exception = exception.InnerException;
            }

            return false;
        }

        private static bool IsTechnicalMessage(string message)
        {
            return message.IndexOf("Object reference", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   message.IndexOf("Referência de objeto", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   message.IndexOf("NullReferenceException", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   message.IndexOf("HTTP request", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   message.IndexOf("stack trace", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
