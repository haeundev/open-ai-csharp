using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace WhisperTest;

using OpenAI.GPT3;

public class WhisperClient
{
    //string wavFile = "C:/Users/haeun/OneDrive/바탕 화면/보험계약자의 고의로 인한 손해는 대인배상에서 보상하지 않습니다.wav";
    string wavFile = "D:/Source/OpenSource/OpenAI/WhisperTest/WhisperTest/harvard.wav";

    
    
    public byte[] GetAudioBytes(string audioFilePath)
    {
        var bytes = File.ReadAllBytes(audioFilePath);
        
        return bytes;
    }

    public async Task Run()
    {
        
        try
        {
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = Keys.ApiKey
            });
            

            var cancellationToken = new CancellationToken();

            var audioBytes = GetAudioBytes(wavFile);
            var response = await openAiService.CreateTranslation(new AudioCreateTranscriptionRequest
            {
                //Prompt = "",
                ResponseFormat = StaticValues.AudioStatics.ResponseFormat.VerboseJson,
                Language = "en",
                File = audioBytes,
                FileName = wavFile,
                Model = Models.WhisperV1,
                //Temperature = 0.2f // https://platform.openai.com/docs/api-reference/audio/create#audio/create-temperature
            }, cancellationToken);

            if (response.Successful)
            {
                Console.WriteLine(string.Join("/n", response.Text));
            }
            else
            {
                if (response.Error == null)
                {
                    throw new Exception("Unknown Error");
                }

                Console.WriteLine($"{response.Error.Code}: {response.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}


/*
 *  public static async Task RunSimpleAudioCreateTranslationTest(IOpenAIService sdk)
    {
        ConsoleExtensions.WriteLine("Audio Create Translation Testing is starting:", ConsoleColor.Cyan);

        try
        {
            ConsoleExtensions.WriteLine("Audio Create Translation Test:", ConsoleColor.DarkCyan);

            const string fileName = "multilingual.mp3";
            var sampleFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{fileName}");

            ConsoleExtensions.WriteLine($"Uploading file {fileName}", ConsoleColor.DarkCyan);
            var audioResult = await sdk.Audio.CreateTranslation(new AudioCreateTranscriptionRequest
            {
                FileName = fileName,
                File = sampleFile,
                Model = Models.WhisperV1,
                ResponseFormat = StaticValues.AudioStatics.ResponseFormat.VerboseJson
            });

            if (audioResult.Successful)
            {
                Console.WriteLine(string.Join("/n", audioResult.Text));
            }
            else
            {
                if (audioResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }

                Console.WriteLine($"{audioResult.Error.Code}: {audioResult.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
 */