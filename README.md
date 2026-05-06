# Claude Code Web UI

基于 [@siteboon/claude-code-ui](https://github.com/siteboon/claudecodeui) 的 Claude Code 网页界面封装，提供浏览器端的 Claude Code 交互体验。

## 快速开始

```bash
# 安装依赖
npm install

# 启动服务（前台）
npm start
```

Windows 下推荐双击 `start-claudecodeui.vbs` 静默后台启动（无窗口）。

启动后访问 **http://localhost:3001**，默认账号 `admin / admin123`。

## 启动方式

| 方式 | 说明 |
|------|------|
| `start.bat` | 后台启动（最小化窗口） |
| `start-claudecodeui.vbs` | **静默启动**，完全无窗口，适合开机自启 |
| `npm start` | 前台启动（终端可见） |

## 更新与重启

### 检查更新

在 Web UI 的设置页面可以检查并安装更新。

### 重启服务

更新后需要重启才能生效：

```bash
# 1. 先杀掉旧的进程
taskkill /f /fi "WINDOWTITLE eq *claude-code-ui*" 2>nul
taskkill /f /im node.exe 2>nul

# 或查找端口 3001 的进程
netstat -ano | findstr ":3001"
# 记下 PID，然后 taskkill /f /pid <PID>

# 2. 重新启动
# 双击 start-claudecodeui.vbs 或 start.bat
```

如果安装在开机启动目录，也可以重启电脑让它自动运行。

## 常见问题

### 更新失败

如果在 Web UI 中更新失败，通常是网络问题或权限不足。解决方法：

```bash
# 手动强制重新安装
npm install @siteboon/claude-code-ui@latest --force
```

### 端口被占用

如果 3001 端口被其他程序占用：

1. 找到占用进程：`netstat -ano | findstr ":3001"`
2. 杀掉进程：`taskkill /f /pid <PID>`
3. 或修改 `start.bat` 中的端口参数

### GitHub 推送被拒 (GH007)

```text
remote: error: GH007: Your push would publish a private email address.
```

GitHub 开启了"阻止推送暴露私人邮箱"的保护。解决方法：

```bash
# 将 git 邮箱修改为 GitHub 的 noreply 地址
git config user.email "你的ID+用户名@users.noreply.github.com"
git commit --amend --author="用户名 <你的ID+用户名@users.noreply.github.com>" --no-edit
```

### 静默启动不生效

开机启动目录：`%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup`

确保该目录下有 `ClaudeCodeWebUI.vbs` 文件。

## 项目结构

```
claude-code-webui/
├── package.json                # 项目配置
├── start.bat                   # 后台启动脚本
├── start-claudecodeui.bat      # 开机启动脚本
├── start-claudecodeui.vbs      # 静默启动脚本（推荐）
├── project/                    # 工作区目录
└── node_modules/               # 依赖包
```

## 相关链接

- [@siteboon/claude-code-ui](https://www.npmjs.com/package/@siteboon/claude-code-ui) - 底层 UI 包
- [Claude Code](https://claude.ai) - Anthropic 官方 CLI 工具
