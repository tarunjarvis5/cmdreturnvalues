//using Microsoft.Win32.SafeHandles;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;

//namespace ConsoleAppChild
//{
//    internal class Class2
//    {
//        [DllImport("user32.dll")]
//        static extern IntPtr GetForegroundWindow();

//        [DllImport("user32.dll")]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        static extern bool SetForegroundWindow(IntPtr hWnd);

//        [DllImport("user32.dll", SetLastError = true)]
//        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

//        [DllImport("kernel32.dll",
//            EntryPoint = "GetStdHandle",
//            SetLastError = true,
//            CharSet = CharSet.Auto,
//            CallingConvention = CallingConvention.StdCall)]
//        private static extern IntPtr GetStdHandle(nint d, nint handle);

//        [DllImport("kernel32.dll",
//            EntryPoint = "GetStdHandle",
//            SetLastError = true,
//            CharSet = CharSet.Auto,
//            CallingConvention = CallingConvention.StdCall)]
//        private static extern IntPtr GetStdHandle(nint handle);

//        [DllImport("kernel32", SetLastError = true)]
//        private static extern bool AttachConsole(int dwProcessId);

//        [DllImport("kernel32.dll",
//            EntryPoint = "AllocConsole",
//            SetLastError = true,
//            CharSet = CharSet.Auto,
//            CallingConvention = CallingConvention.StdCall)]
//        private static extern int AllocConsole();

//        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
//        static extern bool FreeConsole();

//        private const int STD_OUTPUT_HANDLE = -11;
//        private const int STD_ERROR_HANDLE = -12;
//        private static bool _consoleAttached = false;
//        private static IntPtr consoleWindow;

//        [STAThread]
//        static void Main(string[] args)
//        {
//            int prId = 16360;

//            if (args != null && args.Length != 0)
//            {
//                prId = int.Parse(args[0]);
//            }

//            //consoleWindow = GetForegroundWindow();
//            //GetWindowThreadProcessId(consoleWindow, out prId);
//            Process process = Process.GetProcessById(prId);


//            if (process.ProcessName == "cmd")
//            {
//                //bool a = AttachConsole(prId);
//                {
//                    _consoleAttached = true;
//                    IntPtr stdHandle = GetStdHandle(-11 , process.SafeHandle.DangerousGetHandle()); // must be error dunno why
//                    SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
//                    FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
//                    Encoding encoding = Encoding.ASCII;
//                    StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
//                    standardOutput.AutoFlush = true;
//                    standardOutput.WriteLine("ola");

//                    //Console.SetOut(standardOutput);
//                    Console.WriteLine(" was launched from a console window and will redirect output to it.");
//                }
//            }
//            // ... do whatever, use console.writeline or debug.writeline
//            // if you started the app with /debug from a console
//            Cleanup();
//        }

//        private static void Cleanup()
//        {
//            try
//            {
//                if (_consoleAttached)
//                {
//                    SetForegroundWindow(consoleWindow);
//                    //SendKeys.SendWait("{ENTER}");
//                    FreeConsole();
//                }
//            }
//            catch (Exception ex)
//            {
//            }
//        }

//    }
//}


////using System;
////using System.Diagnostics;
////using System.Runtime.InteropServices;

////class Program
////{
////    // Import the necessary WinAPI functions
////    [DllImport("kernel32.dll")]
////    static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

////    [DllImport("kernel32.dll")]
////    static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

////    [DllImport("kernel32.dll")]
////    static extern bool CloseHandle(IntPtr hObject);

////    [DllImport("kernel32.dll")]
////    static extern IntPtr GetCurrentProcess();

////    [DllImport("kernel32.dll")]
////    static extern IntPtr GetStdHandle(int nStdHandle);

////    [DllImport("kernel32.dll")]
////    static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, uint dwDesiredAccess, bool bInheritHandle, uint dwOptions);

////    [StructLayout(LayoutKind.Sequential)]
////    struct PROCESS_BASIC_INFORMATION
////    {
////        public IntPtr Reserved1;
////        public IntPtr PebBaseAddress;
////        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
////        public IntPtr[] Reserved2;
////        public IntPtr UniqueProcessId;
////        public IntPtr Reserved3;
////    }

////    [StructLayout(LayoutKind.Sequential)]
////    struct UNICODE_STRING
////    {
////        public ushort Length;
////        public ushort MaximumLength;
////        public IntPtr Buffer;
////    }

////    [StructLayout(LayoutKind.Sequential)]
////    struct PROCESS_PARAMETERS
////    {
////        public uint MaximumLength;
////        public uint Length;
////        public uint Flags;
////        public uint DebugFlags;
////        public IntPtr ConsoleHandle;
////        public uint ConsoleFlags;
////        public IntPtr StandardInput;
////        public IntPtr StandardOutput;
////        public IntPtr StandardError;
////    }

////    const uint PROCESS_QUERY_INFORMATION = 0x0400;
////    const uint PROCESS_VM_READ = 0x0010;
////    const uint PROCESS_DUP_HANDLE = 0x0040;
////    const uint DUPLICATE_SAME_ACCESS = 0x00000002;
////    const int STD_INPUT_HANDLE = -10;
////    const int STD_OUTPUT_HANDLE = -11;
////    const int STD_ERROR_HANDLE = -12;

////    static void Main(string[] args)
////    {
////        int targetProcessId = 16360; // Replace with the actual PID of the target process

////        // Open a handle to the target process
////        IntPtr hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, true, targetProcessId);
////        if (hProcess == IntPtr.Zero)
////        {
////            Console.WriteLine("Failed to open target process.");
////            return;
////        }

////        try
////        {
////            // Read the PEB address from the target process
////            PROCESS_BASIC_INFORMATION pbi;
////            int bytesReturned;
////            bool success = NtQueryInformationProcess(hProcess, 0, out pbi, Marshal.SizeOf(typeof(PROCESS_BASIC_INFORMATION)), out bytesReturned);
////            if (!success)
////            {
////                Console.WriteLine("Failed to query process information.");
////                return;
////            }

////            // Read the PROCESS_PARAMETERS structure from the target process
////            PROCESS_PARAMETERS pp;
////            byte[] buffer = new byte[Marshal.SizeOf(typeof(PROCESS_PARAMETERS))];
////            IntPtr address = IntPtr.Add(pbi.PebBaseAddress, 0x10); // Offset to ProcessParameters
////            success = ReadProcessMemory(hProcess, address, buffer, buffer.Length, out bytesReturned);
////            if (!success)
////            {
////                Console.WriteLine("Failed to read process memory.");
////                return;
////            }

////            // Marshal the buffer to the PROCESS_PARAMETERS structure
////            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
////            try
////            {
////                pp = (PROCESS_PARAMETERS)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PROCESS_PARAMETERS));
////            }
////            finally
////            {
////                handle.Free();
////            }

////            // Duplicate the standard handles into the current process
////            IntPtr hInput, hOutput, hError;
////            //success = DuplicateHandle(hProcess, pp.StandardInput, GetCurrentProcess(), out hInput, 0, false, DUPLICATE_SAME_ACCESS)
////            //       && DuplicateHandle(hProcess, pp.StandardOutput, GetCurrentProcess(), out hOutput, 0, false, DUPLICATE_SAME_ACCESS)
////            //       && DuplicateHandle(hProcess, pp.StandardError, GetCurrentProcess(), out hError, 0, false, DUPLICATE_SAME_ACCESS);

////            success = DuplicateHandle(hProcess, pp.StandardOutput, GetCurrentProcess(), out hOutput, 0, false, DUPLICATE_SAME_ACCESS);

////            if (!success)
////            {
////                Console.WriteLine("Failed to duplicate handles.");
////                return;
////            }

////            // Use the duplicated handles as needed
////            //Console.WriteLine("Standard input handle: " + hInput);
////            Console.WriteLine("Standard output handle: " + hOutput);
////            //Console.WriteLine("Standard error handle: " + hError);
////        }
////        finally
////        {
////            CloseHandle(hProcess);
////        }
////    }

////    // Import the NtQueryInformationProcess function
////    [DllImport("ntdll.dll")]
////    static extern bool NtQueryInformationProcess(IntPtr ProcessHandle, int ProcessInformationClass, out PROCESS_BASIC_INFORMATION ProcessInformation, int ProcessInformationLength, out int ReturnLength);
////}


//using Microsoft.Win32.SafeHandles;
//using System;
//using System.Diagnostics;
//using System.Runtime.InteropServices;
//using System.Text;

//class Program
//{
//    [DllImport("ntdll.dll")]
//    static extern int NtQueryInformationProcess(IntPtr ProcessHandle, int ProcessInformationClass, IntPtr ProcessInformation, int ProcessInformationLength, out int ReturnLength);

//    // Import required WinAPI functions
//    [DllImport("kernel32.dll")]
//    static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

//    [DllImport("kernel32.dll")]
//    static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

//    [DllImport("kernel32.dll")]
//    static extern bool CloseHandle(IntPtr hObject);

//    [StructLayout(LayoutKind.Sequential)]
//    struct PROCESS_BASIC_INFORMATION
//    {
//        public IntPtr Reserved1;
//        public IntPtr PebBaseAddress;
//        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
//        public IntPtr[] Reserved2;
//        public IntPtr UniqueProcessId;
//        public IntPtr Reserved3;
//    }

//    [StructLayout(LayoutKind.Sequential)]
//    struct UNICODE_STRING
//    {
//        public ushort Length;
//        public ushort MaximumLength;
//        public IntPtr Buffer;
//    }

//    [StructLayout(LayoutKind.Sequential)]
//    struct PROCESS_PARAMETERS
//    {
//        public uint MaximumLength;
//        public uint Length;
//        public uint Flags;
//        public uint DebugFlags;
//        public IntPtr ConsoleHandle;
//        public uint ConsoleFlags;
//        public IntPtr StandardInput;
//        public IntPtr StandardOutput;
//        public IntPtr StandardError;
//        public IntPtr StdHandle;
//    }

//    const uint PROCESS_QUERY_INFORMATION = 0x0400;
//    const uint PROCESS_VM_READ = 0x0010;


//    [DllImport("kernel32.dll", SetLastError = true)]
//    static extern bool DuplicateHandle(
//        IntPtr hSourceProcessHandle,
//        IntPtr hSourceHandle,
//        IntPtr hTargetProcessHandle,
//        out IntPtr lpTargetHandle,
//        uint dwDesiredAccess,
//        [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
//        uint dwOptions);

//    static void Main(string[] args)
//    {
//        int targetProcessId = 29144; // Replace with the actual PID of the target process

//        // Open a handle to the target process
//        IntPtr hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, targetProcessId);
//        if (hProcess == IntPtr.Zero)
//        {
//            Console.WriteLine("Failed to open target process.");
//            return;
//        }

//        try
//        {
//            PROCESS_BASIC_INFORMATION pbi;
//            IntPtr pbiPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PROCESS_BASIC_INFORMATION)));
//            int bytesReturned;
//            int sucacess = NtQueryInformationProcess(hProcess, 0, pbiPtr, Marshal.SizeOf(typeof(PROCESS_BASIC_INFORMATION)), out bytesReturned);
//            if (sucacess != 0)
//            {
//                int errorCode = Marshal.GetLastWin32Error();
//                Console.WriteLine($"NtQueryInformationProcess failed with error code {errorCode}");
//                return;
//            }

//            pbi = Marshal.PtrToStructure<PROCESS_BASIC_INFORMATION>(pbiPtr);

//            // Read the PROCESS_PARAMETERS structure from the target process
//            PROCESS_PARAMETERS pp;
//            byte[] buffer = new byte[Marshal.SizeOf(typeof(PROCESS_PARAMETERS))];
//            IntPtr address = IntPtr.Add(pbi.PebBaseAddress, 0x10); // Offset to ProcessParameters
//            bool success = ReadProcessMemory(hProcess, address, buffer, buffer.Length, out bytesReturned);
//            if (!success)
//            {
//                Console.WriteLine("Failed to read process memory.");
//                return;
//            }

//            // Marshal the buffer to the PROCESS_PARAMETERS structure
//            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

//            try
//            {
//                pp = (PROCESS_PARAMETERS)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PROCESS_PARAMETERS));

//            }
//            finally
//            {
//                //handle.Free();
//            }
//            IntPtr ad = pp.StandardOutput;
//            // Print the standard handle values
//            Console.WriteLine("Standard input handle: " + pp.StandardInput);
//            Console.WriteLine("Standard output handle: " + pp.StandardOutput);
//            Console.WriteLine("Standard error handle: " + ad);
//            const uint DUPLICATE_SAME_ACCESS = 0x00000002;
//            // Duplicate the standard output handle
//            IntPtr duplicatedStdOutHandle;
//            success = DuplicateHandle(hProcess, pp.StandardOutput, Process.GetCurrentProcess().Handle, out duplicatedStdOutHandle, 0, false, DUPLICATE_SAME_ACCESS);
//            if (!success)
//            {
//                Console.WriteLine($"Failed to duplicate handle.{Marshal.GetLastWin32Error()}");
//                Console.ReadLine();

//                return;
//            }
//            Console.ReadLine();
//            SafeFileHandle safeFileHandle = new SafeFileHandle(pp.StandardOutput, true);
//            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
//            Encoding encoding = Encoding.UTF8;
//            StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
//            standardOutput.AutoFlush = true;
//            standardOutput.WriteLine("ola");

//            //Console.SetOut(standardOutput);
//            Console.WriteLine(" was launched from a console window and will redirect output to it.");

//        }
//        finally
//        {
//            CloseHandle(hProcess);
//        }

//        Console.ReadLine();
//    }

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

class Program
{
    // Define the structure for shared memory
    [StructLayout(LayoutKind.Sequential)]
    struct SharedData
    {
        public IntPtr StandardOutput;
    }

    // Import required WinAPI functions
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, uint dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType dwFreeType);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    [DllImport("kernel32.dll")]
    static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

    // Define constants
    const uint PROCESS_VM_OPERATION = 0x0008;
    const uint PROCESS_VM_READ = 0x0010;
    const uint PROCESS_VM_WRITE = 0x0020;
    const uint PROCESS_CREATE_THREAD = 0x0002;
    const uint PROCESS_QUERY_INFORMATION = 0x0400;
    const uint PROCESS_ALL_ACCESS = 0x1F0FFF;
    const uint WAIT_TIMEOUT = 0x00000102;

    // Define the AllocationType and MemoryProtection enums
    [Flags]
    enum AllocationType
    {
        Commit = 0x1000,
        Reserve = 0x2000,
        Decommit = 0x4000,
        Release = 0x8000,
        Reset = 0x80000,
        Physical = 0x400000,
        TopDown = 0x100000,
        WriteWatch = 0x200000,
        LargePages = 0x20000000
    }

    [Flags]
    enum MemoryProtection
    {
        NoAccess = 0x01,
        ReadOnly = 0x02,
        ReadWrite = 0x04,
        WriteCopy = 0x08,
        Execute = 0x10,
        ExecuteRead = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        GuardModifierflag = 0x100,
        NoCacheModifierflag = 0x200,
        WriteCombineModifierflag = 0x400
    }

    static void Main(string[] args)
    {

        Console.WriteLine("aarroooo");

        return;

        // Replace targetProcessId with the actual PID of the target process
        int targetProcessId = 29144;

        IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, targetProcessId);
        if (hProcess == IntPtr.Zero)
        {
            Console.WriteLine("Failed to open target process. Error: " + Marshal.GetLastWin32Error());
            return;
        }

        try
        {
            // Allocate memory in the target process for shared data
            IntPtr sharedMemory = VirtualAllocEx(hProcess, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(SharedData)), AllocationType.Commit, MemoryProtection.ReadWrite);
            if (sharedMemory == IntPtr.Zero)
            {
                Console.WriteLine("Failed to allocate memory in target process. Error: " + Marshal.GetLastWin32Error());
                return;
            }

            // Write the shared data to the target process
            SharedData data = new SharedData();
            data.StandardOutput = IntPtr.Zero; // Placeholder value, will be filled by injected code
            byte[] buffer = new byte[Marshal.SizeOf(typeof(SharedData))];
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                Marshal.StructureToPtr(data, handle.AddrOfPinnedObject(), false);
            }
            finally
            {
                handle.Free();
            }

            int bytesWritten;
            if (!WriteProcessMemory(hProcess, sharedMemory, buffer, (uint)buffer.Length, out bytesWritten))
            {
                Console.WriteLine("Failed to write shared data to target process. Error: " + Marshal.GetLastWin32Error());
                return;
            }

            // Get the address of the injected code
            IntPtr loadLibraryAddr = GetLoadLibraryAddress();
            if (loadLibraryAddr == IntPtr.Zero)
            {
                Console.WriteLine("Failed to get address of LoadLibraryA. Error: " + Marshal.GetLastWin32Error());
                return;
            }

            // Create a remote thread in the target process to execute the injected code
            IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, loadLibraryAddr, sharedMemory, 0, IntPtr.Zero);
            if (hThread == IntPtr.Zero)
            {
                Console.WriteLine("Failed to create remote thread. Error: " + Marshal.GetLastWin32Error());
                return;
            }

            // Wait for the remote thread to finish
            if (WaitForSingleObject(hThread, 5000) == WAIT_TIMEOUT)
            {
                Console.WriteLine("Timeout waiting for remote thread to finish.");
                return;
            }

            // Read the modified shared data from the target process
            buffer = new byte[Marshal.SizeOf(typeof(SharedData))];
            if (!ReadProcessMemory(hProcess, sharedMemory, buffer, (uint)buffer.Length, out bytesWritten))
            {
                Console.WriteLine("Failed to read shared data from target process. Error: " + Marshal.GetLastWin32Error());
                return;
            }

            // Convert the byte array back to SharedData structure
            handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                data = (SharedData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(SharedData));
            }
            finally
            {
                handle.Free();
            }

            // Now 'data.StandardOutput' contains the standard output handle of the target process
            Console.WriteLine("Standard output handle of target process: " + data.StandardOutput);
        }
        finally
        {
            CloseHandle(hProcess);
        }
    }

    static IntPtr GetLoadLibraryAddress()
    {
        // Return the address of LoadLibraryA function in kernel32.dll
        IntPtr hKernel32 = GetModuleHandle("kernel32.dll");
        if (hKernel32 == IntPtr.Zero)
        {
            Console.WriteLine("Failed to get handle of kernel32.dll. Error: " + Marshal.GetLastWin32Error());
            return IntPtr.Zero;
        }
        return GetProcAddress(hKernel32, "LoadLibraryA");
    }
}



