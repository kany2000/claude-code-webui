' Start Claude Code Web UI silently (no console window)
Set shell = CreateObject("WScript.Shell")
shell.CurrentDirectory = "C:\Users\Administrator\claudecodeui"
shell.Run """C:\Program Files\nodejs\node.exe"" watch-restart.js", 0, False
