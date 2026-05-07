using System;
using System.Runtime.InteropServices;
using System.Text;

class Launcher {
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern bool CreateProcess(
        string lpApplicationName,
        StringBuilder lpCommandLine,
        IntPtr lpProcessAttributes,
        IntPtr lpThreadAttributes,
        bool bInheritHandles,
        uint dwCreationFlags,
        IntPtr lpEnvironment,
        string lpCurrentDirectory,
        ref STARTUPINFO lpStartupInfo,
        out PROCESS_INFORMATION lpProcessInformation
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CloseHandle(IntPtr hObject);

    struct STARTUPINFO {
        public int cb;
        public IntPtr lpReserved;
        public IntPtr lpDesktop;
        public string lpTitle;
        public int dwX;
        public int dwY;
        public int dwXSize;
        public int dwYSize;
        public int dwXCountChars;
        public int dwYCountChars;
        public int dwFillAttribute;
        public int dwFlags;
        public short wShowWindow;
        public short cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    struct PROCESS_INFORMATION {
        public IntPtr hProcess;
        public IntPtr hThread;
        public int dwProcessId;
        public int dwThreadId;
    }

    const uint DETACHED_PROCESS = 0x00000008;
    const uint CREATE_NO_WINDOW = 0x08000000;
    const uint STARTF_USESHOWWINDOW = 0x0001;
    const uint SW_HIDE = 0;

    static void Main(string[] args) {
        if (args.Length < 2) return;

        // Build command line: "executable" "arg2" "arg3" ...
        StringBuilder cmd = new StringBuilder();
        cmd.Append('"').Append(args[0]).Append('"');
        for (int i = 2; i < args.Length; i++) {
            cmd.Append(" \"").Append(args[i].Replace("\"", "\\\"")).Append('"');
        }

        STARTUPINFO si = new STARTUPINFO();
        si.cb = Marshal.SizeOf(typeof(STARTUPINFO));
        si.dwFlags = (int)STARTF_USESHOWWINDOW;
        si.wShowWindow = (short)SW_HIDE;

        PROCESS_INFORMATION pi;

        if (CreateProcess(
            null,
            cmd,
            IntPtr.Zero,           // process security
            IntPtr.Zero,            // thread security
            false,                  // inherit handles
            DETACHED_PROCESS | CREATE_NO_WINDOW,
            IntPtr.Zero,            // environment (inherit)
            args[1],                // working directory
            ref si,
            out pi
        )) {
            // Close handles immediately — we don't need to track the process
            CloseHandle(pi.hProcess);
            CloseHandle(pi.hThread);
        }
    }
}
