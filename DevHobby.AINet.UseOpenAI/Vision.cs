using OpenAI.Chat;

namespace UseOpenAIFromNET;

public static class Vision
{
    public static void DescribeImage(string modelName)
    {
        ChatClient client = new(modelName, Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        string path = Path.Combine("Images", "images_dog_and_cat.png");
        using Stream stream = File.OpenRead(path);

        BinaryData imageBytes = BinaryData.FromStream(stream);

        List<ChatMessage> messages = [new UserChatMessage(
            ChatMessageContentPart.CreateTextPart("Opisz co widzisz na tym obrazku."),
            ChatMessageContentPart.CreateImagePart(imageBytes, "image/png"))
            ];

        ChatCompletion chatCompletion = client.CompleteChat(messages);

        Console.WriteLine($"[ASYSTENT]:");
        Console.WriteLine($"{chatCompletion.Content[0].Text}");
    }
}
