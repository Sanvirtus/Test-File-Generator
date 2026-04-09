using System;
using System.Diagnostics;
using System.IO;

namespace Test_File_Generator;

public static class Display
{
    public static void PrintHeader(DriveInfo driveInfo, double safeLimitGb)
    {
        Console.Clear();
        PrintLineBlueText("Zero-Allocation File Generator");
        PrintDriveInfo(driveInfo);

        Console.Write("- Safe Capacity Limit (-10%): ");
        PrintLineGreenText($"{safeLimitGb:F2} GB");

        Console.Write("\nEnter target file size in GB (");
        PrintGreenText($"Min 0.1GB - Max {safeLimitGb:F2}GB");
        Console.Write("): ");
    }

    public static void PrintOutputFileName(string fileName)
    {
        Console.Write("Output file name: ");
        SetGreenConsoleColor();
        Console.WriteLine($"{fileName}");
        ResetConsoleColor();
    }

    public static void PrintSummary(Stopwatch stopwatch, long targetSizeBytes, string filePath)
    {
        PrintLineBlueText("\nExecution Summary:");

        Console.Write("- Total Time: ");
        PrintLineYellowText($"{stopwatch.Elapsed.TotalSeconds:F2} sec");

        var averageThroughput = (targetSizeBytes / 1024.0 / 1024.0) / stopwatch.Elapsed.TotalSeconds;
        Console.Write("- Average Throughput: ");
        PrintLineYellowText($"{averageThroughput:F2} MB/s");

        Console.Write("- Full Path: ");
        PrintLineYellowText($"{Path.GetFullPath(filePath)}");
        PrintPeakMemoryUsage();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    private static void PrintDriveInfo(DriveInfo driveInfo)
    {
        Console.Write("- Current Drive: ");
        PrintLineYellowText($"{driveInfo.Name}");

        Console.Write("- Drive Format: ");
        PrintLineYellowText($"{driveInfo.DriveFormat}");

        var availableGb = driveInfo.AvailableFreeSpace / 1024.0 / 1024.0 / 1024.0;
        Console.Write("- Free Space: ");
        PrintLineYellowText($"{driveInfo.AvailableFreeSpace} bytes ({availableGb:F2} GB)");
    }

    public static void PrintMemoryUsage()
    {
        using var currentProcess = Process.GetCurrentProcess();

        var managedMemory = GC.GetTotalMemory(forceFullCollection: false);
        var workingSet = currentProcess.WorkingSet64;

        PrintLineBlueText("\nMemory Usage");
        Console.Write("- Managed Heap: ");
        PrintLineYellowText($"{managedMemory / 1024.0 / 1024.0:F2} MB");
        Console.Write("- Working Set:  ");
        PrintLineYellowText($"{workingSet / 1024.0 / 1024.0:F2} MB");

        Console.Write("- GC Gen 0: ");
        PrintLineYellowText($"{GC.CollectionCount(0)}");
        Console.Write("- GC Gen 1: ");
        PrintLineYellowText($"{GC.CollectionCount(1)}");
        Console.Write("- GC Gen 2: ");
        PrintLineYellowText($"{GC.CollectionCount(2)}");
    }

    private static void PrintPeakMemoryUsage()
    {
        using var currentProcess = Process.GetCurrentProcess();

        var peakMemoryBytes = currentProcess.PeakWorkingSet64;
        var peakMemoryMb = peakMemoryBytes / 1024.0 / 1024.0;

        PrintLineBlueText("\nMemory Performance:");
        Console.Write("- Peak Working Set (Max RAM used): ");

        PrintLineYellowText($"{peakMemoryMb:F2} MB");
    }

    public static void PrintLineBlueText(string text)
    {
        SetBlueConsoleColor();
        Console.WriteLine(text);
        ResetConsoleColor();
    }

    private static void PrintGreenText(string text)
    {
        SetGreenConsoleColor();
        Console.Write(text);
        ResetConsoleColor();
    }

    private static void PrintLineGreenText(string text)
    {
        SetGreenConsoleColor();
        Console.WriteLine(text);
        ResetConsoleColor();
    }

    private static void PrintLineYellowText(string text)
    {
        SetYellowConsoleColor();
        Console.WriteLine(text);
        ResetConsoleColor();
    }

    private static void ResetConsoleColor() => Console.ResetColor();

    private static void SetBlueConsoleColor() => SetConsoleColor(ConsoleColor.Blue);

    private static void SetGreenConsoleColor() => SetConsoleColor(ConsoleColor.Green);

    private static void SetYellowConsoleColor() => SetConsoleColor(ConsoleColor.Yellow);

    private static void SetConsoleColor(ConsoleColor color) => Console.ForegroundColor = color;
}