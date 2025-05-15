using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace enabler
{
    class Program
    {
        // Registry path for the custom context menu
        const string RegistryMenuPath = @"*\shell\EnablerMenu";

        static void Main(string[] args)
        {
            // Figure out what to do based on arguments
            if (args.Length == 0)
            {
                // No args: try to add the context menu (needs admin)
                if (IsAdministrator())
                    AddContextMenu();
                else
                    Console.WriteLine("Please run as administrator to modify the registry.");
            }
            // Two args: user wants to enable or disable a file
            else if (args.Length == 2)
            {
                string action = args[0], filePath = args[1];
                // Go to function based on what the user selects
                if (action == "disable")
                    DisableFile(filePath);
                else if (action == "enable")
                    EnableFile(filePath);
                else
                    Console.WriteLine("Unknown action.");
            }
            else
            {
                // Invalid arguments
                Console.WriteLine("Invalid arguments.");
            }
        }

        static void AddContextMenu()
        {
            try
            {
                // Get the path to the current executable
                string exePath = Process.GetCurrentProcess().MainModule?.FileName ?? throw new InvalidOperationException("Unable to determine the executable path.");
                // Create the parent context menu key
                using (var parentKey = Registry.ClassesRoot.CreateSubKey(RegistryMenuPath))
                {
                    parentKey.SetValue("", "Enabler Actions"); // Set menu label
                    parentKey.SetValue("SubCommands", "");

                    // Create 'Disable File' menu item
                    using (var disableKey = parentKey.CreateSubKey(@"shell\DisableFile"))
                    {
                        disableKey.SetValue("", "Disable File");
                        using (var commandKey = disableKey.CreateSubKey("command"))
                            commandKey.SetValue("", $"\"{exePath}\" disable \"%1\"");
                    }
                    // Create 'Enable File' menu item
                    using (var enableKey = parentKey.CreateSubKey(@"shell\EnableFile"))
                    {
                        enableKey.SetValue("", "Enable File");
                        using (var commandKey = enableKey.CreateSubKey("command"))
                            commandKey.SetValue("", $"\"{exePath}\" enable \"%1\"");
                    }
                }
                Console.WriteLine("Context menu items added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add context menu items: {ex.Message}");
            }
        }

        static void DisableFile(string filePath)
        {
            try
            {
                // Check if file is already disabled
                if (filePath.EndsWith(".disabled"))
                {
                    Log("File is already disabled.");
                    return;
                }
                // Check if file exists
                if (!File.Exists(filePath))
                {
                    Log("File does not exist.");
                    return;
                }
                // Rename file to add .disabled extension
                string disabledFilePath = filePath + ".disabled";
                File.Move(filePath, disabledFilePath);
                Log($"File disabled: {filePath} -> {disabledFilePath}");
            }
            catch (Exception ex)
            {
                Log($"Failed to disable file: {ex.Message}");
            }
        }

        static void EnableFile(string filePath)
        {
            try
            {
                // Check if file is actually disabled
                if (!filePath.EndsWith(".disabled"))
                {
                    Log("File is not disabled.");
                    return;
                }
                // Check if file exists
                if (!File.Exists(filePath))
                {
                    Log("File does not exist.");
                    return;
                }
                // Rename file to remove .disabled extension
                string enabledFilePath = filePath.Substring(0, filePath.Length - ".disabled".Length);
                File.Move(filePath, enabledFilePath);
                Log($"File enabled: {filePath} -> {enabledFilePath}");
            }
            catch (Exception ex)
            {
                Log($"Failed to enable file: {ex.Message}");
            }
        }

        static bool IsAdministrator()
        {
            try
            {
                // Check if the current process is running as administrator
                var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                var principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }

        static void Log(string message)
        {
            // Output messages to the console
            Console.WriteLine(message);
        }
    }
}