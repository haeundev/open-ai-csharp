namespace WhisperTest;

public static class Keys
{
    private static string ApiKeyFilePath => @"D:\Source\Keys\OpenAI API Key.txt";

    public static string ApiKey => File.ReadAllText(ApiKeyFilePath);
}