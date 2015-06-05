using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Crystal_Injector {

    class Crystal {

        private const string version = "0.1";

        private const string gitHubPage = "https://github.com/Gawkat/Crystal-Injector";

        private static int processID = 0; // PID of Process
        private static string processName = null; // Name of Process
        private static string dllPath = null; // Path, includes nameofdll.dll

        // DLLImports
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationgFlags, IntPtr lpThreadId);

        // Privileges
        private const int ProcessCreateThread = 0x0002;
        private const int ProcessQueryInformation = 0x0400;
        private const int ProcessVMOperation = 0x0008;
        private const int ProcessVMWrite = 0x0020;
        private const int ProcessVMRead = 0x0010;

        // Memory Allocation
        private const uint MemoryCommit = 0x00001000;
        private const uint MemoryReserve = 0x00002000;
        private const uint PageReadWrite = 4;

        public Process[] getProcesses() {
            Process[] processList = Process.GetProcesses();
            return processList;
        }

        public int inject(int processID, string dll) {
            if (processID != 0 && dll != null) {
                // Target process
                Process targetProcess = Process.GetProcessById(getProcessID());

                // Get handle of process
                IntPtr procHandle = OpenProcess(ProcessCreateThread | ProcessQueryInformation | ProcessVMOperation | ProcessVMWrite | ProcessVMRead, false, targetProcess.Id);

                // Get address of LoadLibraryA
                IntPtr loadLibraryAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

                // Allocate memory on target process
                IntPtr allocateMemoryAdress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((getdllPath().Length + 1) * Marshal.SizeOf(typeof(char))), MemoryCommit | MemoryReserve, PageReadWrite);

                // Write name of dll in process
                UIntPtr bytesWritten;
                WriteProcessMemory(procHandle, allocateMemoryAdress, Encoding.Default.GetBytes(getdllPath()), (uint)((getdllPath().Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);

                // Create thread to call LoadLibraryA
                CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddress, allocateMemoryAdress, 0, IntPtr.Zero);
            }
            return 0;
        }

        public string getVersion() {
            return version;
        }

        public string getGitHubPage() {
            return gitHubPage;
        }

        public void setProcessID(int pid) {
            processID = pid;
        }

        public int getProcessID() {
            return processID;
        }

        public void setProcessName(string name) {
            processName = name;
        }

        public string getProcessName() {
            return processName;
        }

        public void setdllPath(string path) {
            dllPath = path;
        }

        public string getdllPath() {
            return dllPath;
        }

    }
}
