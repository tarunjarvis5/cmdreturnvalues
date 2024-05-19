
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Program
{
    public class Program
    {

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool AttachConsole(int dwProcessId);

        public static void Main()
        {

            try
            {
                string id = string.Empty;
                Task.Run(() =>
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = "cmd.exe";
                        process.StartInfo.Arguments = "";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.RedirectStandardError = true;
                        //process.StartInfo.CreateNoWindow = true; // Hide console window

                        process.Start();
                        id = process.Id.ToString();
                        // Synchronous read of output streams
                        string a = process.StandardOutput.ReadToEnd().ToString();
                        string b = process.StandardError.ReadToEnd().ToString();

                        process.WaitForExit();

                    }
                });

                Console.ReadLine();


                StringBuilder output = new StringBuilder();
                StringBuilder errorOutput = new StringBuilder();

                Task.Run(() =>
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = "D:\\cmdreturncode\\ConsoleAppChild\\bin\\Debug\\net8.0\\ConsoleAppChild.exe";
                        process.StartInfo.Arguments = $"{id}";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.RedirectStandardError = true;
                        //process.StartInfo.CreateNoWindow = true; // Hide console window

                        process.Start();

                        // Synchronous read of output streams
                        string a = process.StandardOutput.ReadToEnd().ToString();
                        string b = process.StandardError.ReadToEnd().ToString();


                        process.WaitForExit();

                    }


                });





            }
            catch (Exception ex)
            {

            }

            Console.ReadLine();

        }



    }
}