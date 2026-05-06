' Restart Claude Code UI silently
Dim shell
Set shell = CreateObject("WScript.Shell")

' Kill server on port 3001
shell.Run "cmd /c for /f ""tokens=5"" %a in ('netstat -ano ^| findstr /C:""0.0.0.0:3001"" ^| findstr ""LISTENING""') do taskkill /f /pid %a >nul 2>&1", 0, True

' Wait for port to release
WScript.Sleep 2000

' Start fresh watchdog silently
shell.Run "cmd /c cd /d C:\Users\Administrator\claudecodeui && node watch-restart.js", 0, False
