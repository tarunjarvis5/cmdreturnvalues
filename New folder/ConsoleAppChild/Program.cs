

//using System.Diagnostics;
//using System.Runtime.InteropServices;
//using System.Text;

//namespace Program
//{

//    public class Program
//    {
//        static void AttachToNewConsole()
//        {
//            // Attach to an existing console or create a new one
//            IntPtr stdOutHandle = GetStdHandle(STD_OUTPUT_HANDLE);
//            IntPtr stdErrHandle = GetStdHandle(STD_ERROR_HANDLE);

//            if (stdOutHandle != IntPtr.Zero && stdErrHandle != IntPtr.Zero)
//            {
//                // Redirect standard output and error to the new console
//                using (var stdOutWriter = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true })
//                using (var stdErrWriter = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true })
//                {
//                    Console.SetOut(stdOutWriter);
//                    Console.SetError(stdErrWriter);
//                }
//            }
//        }

//        [DllImport("kernel32", SetLastError = true)]
//        private static extern bool AttachConsole(int dwProcessId);



//        [DllImport("kernel32.dll", SetLastError = true)]
//        static extern IntPtr GetStdHandle(int nStdHandle);

//        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
//        static extern bool WriteConsole(IntPtr hConsoleOutput, byte[] lpBuffer, int nNumberOfCharsToWrite, out int lpNumberOfCharsWritten, IntPtr lpReserved);

//        const int STD_OUTPUT_HANDLE = -11;

//        const int STD_ERROR_HANDLE = -12;


//        public static void Maxxin(string[] args)
//        {
//            AttachConsole(int.Parse(args[0]));

//            writer();



//            try
//            {
//                Console.WriteLine("Hello, World!" + args[0]);

//                Console.WriteLine("number 1");
//                int x = 3;

//                Console.WriteLine("number 2");
//                int y = 0;

//                Console.WriteLine("result is " + (x / y).ToString());

//                Console.WriteLine("End");




//            }
//            catch (Exception ex)
//            {
//                Environment.ExitCode = 6969;

//            }

//        }

//        public static void writer()
//        {
//            // Create a StreamWriter to write to Console.Out and Console.Error
//var consoleOutWriter = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
//            var consoleErrorWriter = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true };

//            Console.SetOut(consoleOutWriter);
//            Console.SetError(consoleErrorWriter);

//        }


//        //------------------------------------------


//        public static void Main(string[] args)
//        {
//            // Get the process ID of the target process
//            int targetProcessId = int.Parse(args[0]); // Replace with the target process ID

//            // Get the handle of the target process
//            IntPtr targetProcessHandle = Process.GetProcessById(targetProcessId).Handle;

//            // Get the standard output handle of the target process
//            IntPtr targetStdOutputHandle = GetStdHandle(STD_OUTPUT_HANDLE);

//            // Duplicate the standard output handle of the target process for the current process
//            IntPtr duplicateStdOutputHandle;
//            bool success = DuplicateHandle(targetProcessHandle, targetStdOutputHandle,
//                GetCurrentProcess(), out duplicateStdOutputHandle,
//                0, false, 2);

//            if (!success)
//            {
//                Console.WriteLine("Failed to duplicate handle. Error code: " + Marshal.GetLastWin32Error());
//                return;
//            }

//            // Write to the standard output of the target process using the duplicated handle
//            string message = "Hello from the current process!";
//            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(message);
//            uint bytesWritten;
//            success = WriteFile(duplicateStdOutputHandle, bytes, (uint)bytes.Length, out bytesWritten, IntPtr.Zero);

//            if (!success)
//            {
//                Console.WriteLine("Failed to write to file. Error code: " + Marshal.GetLastWin32Error());
//                return;
//            }

//            Console.WriteLine("Message successfully written to target process's standard output.");

//            // Close the duplicated handle
//            CloseHandle(duplicateStdOutputHandle);



//            try
//            {
//                Console.WriteLine("Hello, World!" + args[0]);

//                Console.WriteLine("number 1");
//                int x = 3;

//                Console.WriteLine("number 2");
//                int y = 0;

//                Console.WriteLine("result is " + (x / y).ToString());

//                Console.WriteLine("End");




//            }
//            catch (Exception ex)
//            {
//                Environment.ExitCode = 6969;

//            }

//        }

//        [DllImport("kernel32.dll", SetLastError = true)]
//        static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle,
//        IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle,
//        uint dwDesiredAccess, bool bInheritHandle, uint dwOptions);

//        [DllImport("kernel32.dll", SetLastError = true)]
//        static extern IntPtr GetCurrentProcess();

//        [DllImport("kernel32.dll", SetLastError = true)]
//        static extern bool CloseHandle(IntPtr hObject);



//        [DllImport("kernel32.dll", SetLastError = true)]
//        static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer,
//            uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten,
//            IntPtr lpOverlapped);


//    }
//}
