using System;
using System.Net;
using System.Threading;
using FBL_ATM_POSTING.NAVWS;
using System.IO;

namespace FBL_ATM_POSTING
{
    class Program
    {
        public static fbl Cft = new fbl
        {
            UseDefaultCredentials = false,
            PreAuthenticate = true,
            Credentials =
                new NetworkCredential(
                    FBL_ATM_POSTING.Properties.Settings.Default.LoginUser,
                    FBL_ATM_POSTING.Properties.Settings.Default.Password,
                    ""),
            Timeout = -1
        };


        public static class Logger
        {
            private static readonly object _lock = new object();
            private static readonly string logFile = @"C:\Logs\atmposting.log";


            public static void Log(string level, string message, Exception ex = null)
            {
                lock (_lock)
                {
                    string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
                    if (ex != null) logLine += $" → {ex.Message}";

                    File.AppendAllText(logFile, logLine + Environment.NewLine);
                }
            }

        }


        static void Main(string[] args)
        {
            Console.WriteLine(" ATM Posting Service started. Running every 10 seconds...");

            while (true)
            {
                try
                {
                    string response = Cft.PostTrans();
                    Console.WriteLine($"{DateTime.Now} INFO: {response}");
                    Logger.Log("Success", $"INFO: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{DateTime.Now} Error: {ex.Message}");
                    Logger.Log("ERROR", $"Error: {ex.Message}", ex);
                }

                Thread.Sleep(10000);
            }
        }
    }
}
