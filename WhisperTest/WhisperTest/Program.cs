using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using WhisperTest;




var whisper = new WhisperClient();
await whisper.Run();