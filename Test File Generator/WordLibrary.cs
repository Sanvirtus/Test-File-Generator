using System;

namespace Test_File_Generator;

public static class WordLibrary
{
    private static ReadOnlySpan<byte> Apple => "Apple is a classic fruit"u8;
    private static ReadOnlySpan<byte> Something => "Something something something dark side"u8;
    private static ReadOnlySpan<byte> Cherry => "Cherry is the best for pies"u8;
    private static ReadOnlySpan<byte> Banana => "Banana is yellow and curved"u8;
    private static ReadOnlySpan<byte> Dragonfruit => "Dragonfruit looks like an alien"u8;
    private static ReadOnlySpan<byte> Elderberry => "Elderberry is good for your health"u8;
    private static ReadOnlySpan<byte> Fig => "Fig is surprisingly sweet"u8;
    private static ReadOnlySpan<byte> Grape => "Grape comes in many colors"u8;
    private static ReadOnlySpan<byte> Honeydew => "Honeydew is a refreshing melon"u8;
    private static ReadOnlySpan<byte> Kiwi => "Kiwi is small and fuzzy"u8;
    private static ReadOnlySpan<byte> Lemon => "Lemon is very sour"u8;
    private static ReadOnlySpan<byte> Mango => "Mango is the king of fruits"u8;
    private static ReadOnlySpan<byte> Test => "I haven't come up with a name or color for this fruit"u8;

    public static ReadOnlySpan<byte> DotSpace => ". "u8;
    public static ReadOnlySpan<byte> NewLine => "\n"u8;

    public static ReadOnlySpan<byte> GetRandomWord()
    {
        return Random.Shared.Next(13) switch
        {
            0 => Apple,
            1 => Something,
            2 => Cherry,
            3 => Banana,
            4 => Dragonfruit,
            5 => Elderberry,
            6 => Fig,
            7 => Grape,
            8 => Honeydew,
            9 => Kiwi,
            10 => Lemon,
            11 => Mango,
            _ => Test
        };
    }
}