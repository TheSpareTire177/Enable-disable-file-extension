# Enable/Disable File Extension

This tool adds an option in your context menu to enable or disable files by changing their file extensions. It is designed to be user-friendly and efficient for managing file states in your projects.

#### This tool works by adding registry keys to your registry. After running the executable, you can delete it and all associated files.
### !!MAKE SURE TO RUN AS ADMIN!!

## Features
- **Enable/Disable Files:** Quickly toggle files between active and inactive states by changing their extensions (e.g., `file.txt` to `file.txt.disabled`).
- **Context Menu Integration:** Right-click on a file to enable or disable it directly from the context menu.
- **Batch Operations:** Select and toggle multiple files at once for streamlined workflows.
- **Safe File Handling:** Prevents accidental overwrites and ensures files are safely renamed.

## Uses
- **Temporarily Disable Files:** Deactivate configuration files, scripts, or resources without deleting them.
- **Testing and Debugging:** Switch between different versions of files or configurations easily.
- **Prevent Accidental Use:** Disable sensitive or deprecated files to avoid unintended usage.
- **Project Maintenance:** Manage large numbers of files efficiently with batch operations.

## How It Works
1. **Right-click** on a file in your file explorer.
2. Select **Enable** or **Disable** from the context menu.
3. The file's extension will be changed to reflect its state (e.g., `.disabled` appended to disable).
4. To re-enable, repeat the process and the original extension will be restored.

## Example
- Disabling a file: `config.json` → `config.json.disabled`
- Enabling a file: `config.json.disabled` → `config.json`

---

## How to Compile and Run (Windows)

1. Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed (version 9.0 or later).
2. Open a terminal (PowerShell or Command Prompt).
3. Navigate to the project directory:
   ```powershell
   cd path\to\Enable-disable-file-extension
   ```
4. Build the project:
   ```powershell
   dotnet build
   ```
5. Run the application:
   ```powershell
   dotnet run --project Enabler.cs
   ```

---

The latest precompiled version is always available in the [releases](https://github.com/TheSpareTire177/Enable-disable-file-extension/releases) section.
