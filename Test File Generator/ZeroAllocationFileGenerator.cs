using System;
using System.Buffers.Text;
using System.IO;
using Test_File_Generator;

public static class ZeroAllocationFileGenerator
{
    private const int MaxRandomNumber = int.MaxValue;
    private const int Blocks = 64;
    private const int BytesInBlock = 1024;

    public static void GenerateFile(string filePath, long targetSizeBytes)
    {
        var buffer = new byte[Blocks * BytesInBlock];
        var span = buffer.AsSpan();
        var offset = 0;
        long totalWritten = 0;

        long lineCountStatistics = 0;
        var minNumberStatistics = int.MaxValue;
        var maxNumberStatistics = 0;

        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 0))
        {
            var lastPercentage = -1;

            while (totalWritten < targetSizeBytes)
            {
                if (span.Length - offset < 256)
                {
                    fileStream.Write(buffer, 0, offset);
                    totalWritten += offset;
                    offset = 0;

                    var currentPercentage = (int)((totalWritten * 100) / targetSizeBytes);
                    if (currentPercentage != lastPercentage)
                    {
                        Console.Write($"\rProgress: {currentPercentage}%");
                        lastPercentage = currentPercentage;
                    }
                }

                var randomNumber = Random.Shared.Next(1, MaxRandomNumber);

                lineCountStatistics++;
                if (randomNumber < minNumberStatistics) minNumberStatistics = randomNumber;
                if (randomNumber > maxNumberStatistics) maxNumberStatistics = randomNumber;

                Utf8Formatter.TryFormat(randomNumber, span.Slice(offset), out var bytesWritten);
                offset += bytesWritten;

                WordLibrary.DotSpace.CopyTo(span.Slice(offset));
                offset += WordLibrary.DotSpace.Length;

                var word = WordLibrary.GetRandomWord();
                word.CopyTo(span.Slice(offset));
                offset += word.Length;

                WordLibrary.NewLine.CopyTo(span.Slice(offset));
                offset += WordLibrary.NewLine.Length;
            }

            if (offset > 0)
            {
                fileStream.Write(buffer, 0, offset);
            }
        }

        Display.PrintLineBlueText("\n\nSummary");
        Console.WriteLine($"- Lines Generated: {lineCountStatistics}");
        Console.WriteLine($"- Min Number: {minNumberStatistics}");
        Console.WriteLine($"- Max Number: {maxNumberStatistics}");
    }
}