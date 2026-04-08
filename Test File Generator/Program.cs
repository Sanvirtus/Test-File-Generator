using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Test_File_Generator;

public static class Program
{
    public static void Main(string[] args)
    {
        var currentDriveInfo = GetCurrentDriveInfo();
        var safeLimitBytes = (long)(currentDriveInfo.AvailableFreeSpace * 0.9);
        var safeLimitGb = safeLimitBytes / 1024.0 / 1024.0 / 1024.0;

        double targetGb = 0;

        while (true)
        {
            Display.PrintHeader(currentDriveInfo, safeLimitGb);

            var input = Console.ReadLine()?.Replace(',', '.');

            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out targetGb))
            {
                if (targetGb >= 0.1 && targetGb <= safeLimitGb)
                {
                    break;
                }
            }
        }

        var fileName = GetFileName(targetGb);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        Display.PrintOutputFileName(fileName);

        var targetSizeBytes = (long)(targetGb * 1024 * 1024 * 1024);

        var stopwatch = Stopwatch.StartNew();

        ZeroAllocationFileGenerator.GenerateFile(filePath, targetSizeBytes);

        stopwatch.Stop();

        Display.PrintSummary(stopwatch, targetSizeBytes, filePath);
    }

    private static DriveInfo GetCurrentDriveInfo()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        return new DriveInfo(Path.GetPathRoot(currentDirectory)!);
    }

    private static string GetFileName(double targetGb)
    {
        var timestamp = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");

        return $"{targetGb:F1}GB_{timestamp}.txt";
    }
}