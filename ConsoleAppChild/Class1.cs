using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppChild
{
    internal class Class1
    {

        class ChildProgram
        {
            static async Task Masin(string[] args)
            {
                // Create a StreamWriter to write to redirected output
                using (var stdOutWriter = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true })
                using (var stdErrWriter = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true })
                {
                    // Redirect the output and error streams
                    Console.SetOut(stdOutWriter);
                    Console.SetError(stdErrWriter);

                    // Attach to a new console window for the child process
                    AllocConsole();

                    for (int i = 0; i < 10; i++)
                    {
                        string outputMessage = $"Message {i}";
                        string errorMessage = $"Error {i}";

                        // Write to the redirected output
                        Console.WriteLine(outputMessage);
                        Console.Error.WriteLine(errorMessage);

                        // Write to the new console window
                        WriteToConsole(outputMessage);
                        WriteToConsole(errorMessage, isError: true);

                        // Simulate work
                        await Task.Delay(1000);
                    }
                }
            }

            static void WriteToConsole(string message, bool isError = false)
            {
                // Get the handle for the new console window
                IntPtr consoleHandle = GetStdHandle(isError ? STD_ERROR_HANDLE : STD_OUTPUT_HANDLE);

                // Write the message to the new console window
                byte[] bytes = Encoding.Default.GetBytes(message + Environment.NewLine);
                WriteConsole(consoleHandle, bytes, bytes.Length, out _, IntPtr.Zero);
            }

            // P/Invoke declarations
            [DllImport("kernel32.dll", SetLastError = true)]
            static extern bool AllocConsole();

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern IntPtr GetStdHandle(int nStdHandle);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            static extern bool WriteConsole(IntPtr hConsoleOutput, byte[] lpBuffer, int nNumberOfCharsToWrite, out int lpNumberOfCharsWritten, IntPtr lpReserved);

            private const int STD_OUTPUT_HANDLE = -11;
            private const int STD_ERROR_HANDLE = -12;
        }

    }
}
