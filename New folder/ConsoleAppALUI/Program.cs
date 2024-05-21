
using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.Reflection;
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
                        process.StartInfo.RedirectStandardInput = true;



                        //process.StartInfo.CreateNoWindow = true; // Hide console window
                        process.Start();


                        var z = process.StandardOutput.BaseStream;



                        // Use reflection to get the SafeFileHandle from the FileStream
                        PropertyInfo safeFileHandleProperty = typeof(FileStream).GetProperty("SafeFileHandle", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                        // Access the SafeFileHandle for StandardOutput
                        SafeFileHandle childStdOutSafeHandle = (SafeFileHandle)safeFileHandleProperty.GetValue(process.StandardOutput.BaseStream);






                        //// Use reflection to get the SafeFileHandle from the FileStream
                        //var safeFileHandlxeProperty = typeof(SafeProcessHandle).GetProperty("SafeFileHandle");
                        //SafeFileHandle childStdInSafeHandle = null;
                        //var childStdInSassfeHandle = (SafeFileHandle)safeFileHandleProperty.GetValue(process.Handle);

                        //childStdInSassfeHandle.WriteLine("aaaaaaaraa");

                        try
                        {
                            // Use the SafeFileHandle as needed, here an example for StandardInput
                            using (var fileStream = new FileStream(childStdOutSafeHandle, FileAccess.Write))
                            using (var streamWriter = new StreamWriter(fileStream))
                            {
                                streamWriter.AutoFlush = true;
                                streamWriter.WriteLine("Hello from parent process!");
                            }

                            //TextWriter streamWriter = new StreamWriter(fileStream);
                            //streamWriter.WriteLine("Bello");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("this : " + ex);
                        }

                        id = process.Id.ToString();
                        // Synchronous read of output streams
                        string a = process.StandardOutput.ReadToEnd();
                        string b = process.StandardError.ReadToEnd().ToString();

                        Console.WriteLine("this : " + a);

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
                        process.StartInfo.Arguments = $"";
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