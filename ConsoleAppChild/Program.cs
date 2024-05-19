

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Program
{

    public class Program
    {
        static void AttachToNewConsole()
        {
            // Attach to an existing console or create a new one
            IntPtr stdOutHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            IntPtr stdErrHandle = GetStdHandle(STD_ERROR_HANDLE);

            if (stdOutHandle != IntPtr.Zero && stdErrHandle != IntPtr.Zero)
            {
                // Redirect standard output and error to the new console
                using (var stdOutWriter = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true })
                using (var stdErrWriter = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true })
                {
                    Console.SetOut(stdOutWriter);
                    Console.SetError(stdErrWriter);
                }
            }
        }

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool AttachConsole(int dwProcessId);



        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool WriteConsole(IntPtr hConsoleOutput, byte[] lpBuffer, int nNumberOfCharsToWrite, out int lpNumberOfCharsWritten, IntPtr lpReserved);

        const int STD_OUTPUT_HANDLE = -11;

        const int STD_ERROR_HANDLE = -12;


        public static void Main(string[] args)
        {
            AttachConsole(int.Parse(args[0]));

            writer();



            try
            {
                Console.WriteLine("Hello, World!" + args[0]);

                Console.WriteLine("number 1");
                int x = 3;

                Console.WriteLine("number 2");
                int y = 0;

                Console.WriteLine("result is " + (x / y).ToString());

                Console.WriteLine("End");




            }
            catch (Exception ex)
            {
                Environment.ExitCode = 6969;

            }

        }

        public static void writer()
        {
            // Create a StreamWriter to write to Console.Out and Console.Error
            var consoleOutWriter = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
            var consoleErrorWriter = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true };

            Console.SetOut(consoleOutWriter);
            Console.SetError(consoleErrorWriter);

        }


    }
}
