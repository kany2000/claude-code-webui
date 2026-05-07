' Restart Claude Code UI silently (no console windows)
Set shell = CreateObject("WScript.Shell")

' Kill any process listening on port 3001
shell.Run "powershell -NoProfile -Command ""netstat -ano | Select-String '0.0.0.0:3001' | Select-String 'LISTENING' | ForEach-Object { Stop-Process -Id ([int]($_.split()[-1])) -Force -ErrorAction SilentlyContinue }""", 0, True

' Wait for port to release
WScript.Sleep 2000

' Start fresh watchdog silently
shell.CurrentDirectory = "C:\Users\Administrator\claudecodeui"
shell.Run """C:\Program Files\nodejs\node.exe"" watch-restart.js", 0, False
