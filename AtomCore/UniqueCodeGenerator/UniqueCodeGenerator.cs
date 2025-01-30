namespace AtomCore.UniqueCodeGenerator;

public static class UniqueCodeGenerator
{
    private static string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    /// <summary>
    /// Enter your pattern. For example:
    /// XXXX-XXXX (output: A1B3-XWT7)
    /// XXXXX-XXX (output: AAO6-HF9)
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string Generate(string pattern)
    {
        string upperCasePattern = pattern.ToUpper();

        bool isPatternCorrect = IsPatternEnteredCorrect(upperCasePattern);

        if (!isPatternCorrect) throw new Exception($"Entered pattern {pattern} is not correct");

        var result = "";
        Random random = new Random();
        for (int i = 0; i < upperCasePattern.Length; i++)
        {
            if (upperCasePattern[i] == 'X')
                result += charSet[random.Next(charSet.Length)];
            else result += upperCasePattern[i];
        }

        return result;
    }

    private static bool IsPatternEnteredCorrect(string pattern)
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            char character = pattern[i];

            if (character != 88 && character != 45)
                return false;
        }

        return true;
    }
}