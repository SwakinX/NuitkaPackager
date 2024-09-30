using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace NuitkaPackager;

public class ControlInfo
{
    public string? Command
    {
        get; set;
    }
    public Func<bool>? Condition
    {
        get; set;
    }
}
public static class GetCommand
{
    private static List<ControlInfo> GenerateControlTable()
    {
        return new()
    {
            new() {
                Command = "--module",
                Condition = () => AppConfig.IsModule
            },
            new() {
                Command = "--onefile",
                Condition = () => AppConfig.IsOneFile
            },
            new() {
                Command = "--standalone",
                Condition = () => AppConfig.IsSandalone
            },
            new() {
                Command = "--remove-output",
                Condition = () => AppConfig.IsRemoveOutput
            },
            new() {
                Command = "--windows-icon-from-ico=" + AppConfig.IconPath,
                Condition = () => AppConfig.IsEnableIcon
            },
            new() {
                Command = "--output-dir=" + AppConfig.OutputPath,
                Condition = () => AppConfig.IsEnableOut
            },
            new() {
                Command = "--mingw64",
                Condition = () => AppConfig.IsMingw64
            },
            new() {
                Command = "--assume-yes-for-downloads",
                Condition = () => AppConfig.IsaYesfdownload
            },
            new() {
                Command = "--windows-uac-admin",
                Condition = () => AppConfig.IsAdmin
            },
            new() {
                Command = "--show-memory",
                Condition = () => AppConfig.IsShowMemory
            },

            new() {
                Command = "--disable-console",
                Condition = () => AppConfig.IsDisableConsole
            },

            new() {
                Command = "--follow-imports",
                Condition = () => AppConfig.IsFollowImports
            },
            new() {
                Command = GetIncludes("--include-package=" , AppConfig.IncludePackage),
                Condition = () => !string.IsNullOrEmpty(AppConfig.IncludePackage)
            },
            new() {
                Command =  GetIncludes("--nofollow-import-to=" , AppConfig.NofollowImport),
                Condition = () => !string.IsNullOrEmpty(AppConfig.NofollowImport)
            },
            new() {
                Command =  GetIncludes("--include-module=" , AppConfig.IncludeModule),
                Condition = () => !string.IsNullOrEmpty(AppConfig.IncludeModule)
            },
            new() {
                Command = "--company-name=" + AppConfig.CompanyName,
                Condition = () => !string.IsNullOrEmpty(AppConfig.CompanyName)
            },
            new() {
                Command = "--product-name=" + AppConfig.ProductName,
                Condition = () => !string.IsNullOrEmpty(AppConfig.ProductName)
            },
            new() {
                Command = "--file-description=" + AppConfig.FileDescription,
                Condition = () => !string.IsNullOrEmpty(AppConfig.FileDescription)
            },
            new() {
                Command = "--file-version=" + AppConfig.FileVersion,
                Condition = () => !string.IsNullOrEmpty(AppConfig.FileVersion)
            },
            new() {
                Command = "--product-version=" + AppConfig.ProductVersion,
                Condition = () => !string.IsNullOrEmpty(AppConfig.ProductVersion)
            },
            new() {
                Command = "--copyright=" + AppConfig.Copyright,
                Condition = () => !string.IsNullOrEmpty(AppConfig.Copyright)
            },
            new() {
                Command = "--trademarks=" + AppConfig.Trademarks,
                Condition = () => !string.IsNullOrEmpty(AppConfig.Trademarks)
            },
            new() {
                Command = GetIncludeDirs("--include-data-files=", AppConfig.DataFile),
                Condition = () => AppConfig.IsIncludeDataFile
            },
            new() {
                Command = GetIncludeDirs("--include-data-dirs=", AppConfig.DataDir),
                Condition = () => AppConfig.IsIncludeDataDir
            },
            new() {
                Command = GetIncludeDirs("", AppConfig.CustomSetting),
                Condition = () => AppConfig.IsCustomSetting
            }
        };
    }

    public static string Get()
    {
        var command = " -m nuitka";
        if (AppConfig.IsFullyCustom)
        {
            return command+ GetIncludeDirs("", AppConfig.CustomSetting);
        }
        var controlTable = GenerateControlTable();
        foreach (var controlInfo in controlTable)
        {
            if (!controlInfo.Condition())
            {
                continue;
            }
            command += " " + controlInfo.Command;
        }
        command += " " + GetPlugin();
        command += " --main=" + AppConfig.PyFilePath;
        return command;
    }
    private static string GetPlugin()
    {
        var pluginConditions = new Dictionary<Func<bool>, string>
    {
        { () => AppConfig.IsQt, "pyqt5" },
        { () => AppConfig.IsPyside6, "pyside6" },
        { () => AppConfig.IsTkinter, "tk-inter" },
        { () => AppConfig.IsNumpy, "numpy" },
        { () => AppConfig.IsTorch, "torch" },
        { () => AppConfig.IsTensorflow, "tensorflow" },
        { () => AppConfig.IsAntibloat, "anti-bloat" },
        { () => AppConfig.IsMtiprocessing, "multiprocessing" }
    };

        var plugins = new List<string>();

        foreach (var condition in pluginConditions)
        {
            if (condition.Key())
            {
                plugins.Add(condition.Value);
            }
        }
        if (plugins.Count == 0)
        {
            return "";
        }
        var plugin = string.Join(",", plugins);
        return "--enable-plugins=" + plugin;
    }

    private static readonly char[] separator = ['\r', '\n', '\t', ' ', ','];

    private static string GetIncludeDirs(string ahead, string? str)
    {
        var command = "";
        if (string.IsNullOrEmpty(str)) return "";
        var paths = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        foreach (var path in paths)
        {
            command += " " + ahead + path;
        }
        return command;
    }
    private static string GetIncludes(string ahead, string? str)
    {
        var command = ahead;
        if (string.IsNullOrEmpty(str)) return "";
        var includes = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        foreach (var include in includes)
        {
            command += include + ",";
        }
        command = command.TrimEnd(',');
        return command;
    }
}