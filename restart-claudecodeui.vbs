' Restart Claude Code UI silently (no console windows)
Set shell = CreateObject("WScript.Shell")

' Kill any existing process on port 3001
shell.Run "pwsh -NoProfile -Command ""netstat -ano | Select-String '0.0.0.0:3001' | Select-String 'LISTENING' | ForEach-Object { Stop-Process -Id ([int]($_.split()[-1])) -Force -ErrorAction SilentlyContinue }""", 0, True

' Wait a moment for port release
WScript.Sleep 2000

' Start fresh watchdog silently (no cmd window at all)
CreateObject("Shell.Application").ShellExecute "C:\Program Files\nodejs\node.exe", "C:\Users\Administrator\claudecodeui\watch-restart.js", "C:\Users\Administrator\claudecodeui", "", 0
