using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Text;

namespace DevHobby.AINet.UseOpenAI;

public static class ChatCompletions
{
    public static void SimpleChat(string modelName)
    {
        ChatClient client = new(modelName, Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        ChatCompletion completion = client.CompleteChat("Cześć, świecie sztucznej inteligencji");

        Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
    }

    public static void SimpleChatUsingOpenAIClient(string modelName)
    {
        OpenAIClient openAiClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        ChatClient chatClient = openAiClient.GetChatClient(modelName);

        ChatCompletion completion = chatClient.CompleteChat("Cześć, świecie sztucznej inteligencji");

        Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
    }

    public async static Task SimpleChatAsync(string modelName)
    {
        ChatClient client = new(modelName, Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        ChatCompletion completion = await client.CompleteChatAsync("Cześć, świecie sztucznej inteligencji");

        Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
    }

    public async static Task SimpleChatStreamingAsync(string modelName)
    {
        ChatClient client = new(modelName, Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = 
            client.CompleteChatStreamingAsync("Powiedz 'Witaj, cyfrowy wszechświecie sztucznej inteligencji! ' 20 razy");

        await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
        {
            foreach (ChatMessageContentPart contentPart in completionUpdate.ContentUpdate)
            {
                Console.Write(contentPart.Text);
            }
        }   
    }
  
    public async static Task SimpleChatUsingOpenAIClientWithMessages(string modelName)
    {
        OpenAIClient openAiClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        ChatClient chatClient = openAiClient.GetChatClient(modelName);

        ChatMessage[] chatMessages =
        [
            new SystemChatMessage("Jesteś pomocnym asystentem, masz dużą wiedzę na temat programowania w C#."),
            new UserChatMessage("Cześć, czy możesz mi pomóc?"),
            new AssistantChatMessage("Oczywiście, drogi aspirujący programisto C#! Co mogę dla ciebie zrobić?"),
            new UserChatMessage("Czy możesz mi podać listę 10 najpopularniejszych bibliotek i frameworków używanych w ekosystemie .NET?"),
        ];

        AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = chatClient.CompleteChatStreamingAsync(chatMessages);

        await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
        {
            foreach (ChatMessageContentPart contentPart in completionUpdate.ContentUpdate)
            {
                Console.Write(contentPart.Text);
            }
        }
    }

    public static void CreateCourseDescription(string modelName)
    {
        OpenAIClient openAiClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        ChatClient chatClient = openAiClient.GetChatClient(modelName);

        var systemPrompt = "Jesteś pomocnym asystentem, który tworzy opisy kursów w sklepie internetowym";

        Console.WriteLine("Wprowadź kurs:");
        var userPrompt = Console.ReadLine();
        Console.WriteLine("Właśnie nad tym pracuję...");

        ChatCompletion completion = chatClient.CompleteChat(
        [
            new SystemChatMessage(systemPrompt),
            new AssistantChatMessage("Mogę pomóc w tworzeniu opisów kursów. Co mogę dla Ciebie zrobić?"),
            new UserChatMessage(userPrompt)
        ]);

        Console.WriteLine($"[{completion.Role}]: {completion.Content[0].Text}");
    }

    public static void CreateCourseName(string modelName)
    {
        OpenAIClient openAiClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        ChatClient chatClient = openAiClient.GetChatClient(modelName);

        var systemPrompt = "Jesteś pomocnym asystentem, który tworzy lepsze nazwy kursów w sklepie internetowym. Zwrócisz tylko jedną sugestię nazwy.";

        var userPromptSample =
                """
                Opis kursu: C# Podstawy
                Słowa kluczowe: C# od podstaw, Typy danych C#, Pętle i warunki C#, Klasy i obiekty C#
                Nazwy kursów: C# Podstawy Programowania, C# od Zera do Programisty, C# Podstawy – Fundamenty Programowania Bez Bólu Głowy

                Opis kursu: C# Obiektówka
                Słowa kluczowe: Klasy i obiekty C#, Dziedziczenie C#, Polimorfizm C#, Hermetyzacja w C#
                Nazwy kursów: Zrozum Obiektowość – C# w Stylu Profesjonalnym, Obiektowy C# – Kurs, po którym piszesz jak senior, C# Obiektowo i Skutecznie – Praktyka zamiast Teorii
                """;

        Console.WriteLine("Wprowadź opis kursu:");
        var userPrompt = Console.ReadLine();
        Console.WriteLine("Właśnie nad tym pracuję...");

        ChatCompletion completion = chatClient.CompleteChat(
        [
            new SystemChatMessage(systemPrompt),
            new UserChatMessage(userPromptSample),
            new AssistantChatMessage("Mogę pomóc w tworzeniu nazw kursów. Co mogę dla Ciebie zrobić??"),
            new UserChatMessage(userPrompt)
        ]);

        Console.WriteLine($"[{completion.Role}]: {completion.Content[0].Text}");
    }

    public static void CreateFancyAndPoshCourseDescription(string modelName)
    {
        OpenAIClient openAiClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        ChatClient chatClient = openAiClient.GetChatClient(modelName);

        var systemPrompt = "Jesteś pomocnym asystentem, który tworzy opisy kursów w sklepie internetowym. Używasz bardzo eleganckiego i wymyślnego języka.";

        Console.WriteLine("Wprowadź opis kursu:");
        var userPrompt = Console.ReadLine();
        Console.WriteLine("Właśnie nad tym pracuję...");

        ChatCompletion completion = chatClient.CompleteChat(
        [
            new SystemChatMessage(systemPrompt),
            new AssistantChatMessage("Mogę pomóc w tworzeniu opisów kursów. Co mogę dla Ciebie zrobić??"),
            new UserChatMessage(userPrompt)
        ]);

        Console.WriteLine($"[{completion.Role}]: {completion.Content[0].Text}");
    }

    public static void CreateCourseChapters(string modelName)
    {
        OpenAIClient openAiClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        ChatClient chatClient = openAiClient.GetChatClient(modelName);

        var systemPrompt =
            """
            Jesteś pomocnym asystentem, który tworzy rozdziały do kursu. 
            Dane zostaną zwrócone w postaci arkusza kalkulacyjnego z 3 kolumnami.
            Nazwa rozdziału | Opis | Skrócony opis 
            Pokaż listę zarówno w języku polskim, jak i angielskim.
            """;

        Console.WriteLine("Wprowadź nazwę kursu:");
        var userPrompt = Console.ReadLine();
        Console.WriteLine("Właśnie nad tym pracuję...");

        ChatCompletion completion = chatClient.CompleteChat(
        [
            new SystemChatMessage(systemPrompt),
            new AssistantChatMessage("Mogę pomóc w stworzeniu listy rozdziałów na kurs. Co mogę dla Ciebie zrobić??"),
            new UserChatMessage(userPrompt)
        ]);

        Console.WriteLine($"[{completion.Role}]: {completion.Content[0].Text}");
    }

    public static void CreateCourseDescriptionWithOptions(string modelName)
    {
        var completionOptions = new ChatCompletionOptions
        {
            MaxOutputTokenCount = 200,
            Temperature = 0.5f,
            FrequencyPenalty = 0.0f,
            PresencePenalty = 0.0f,
        };

        OpenAIClient openAiClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        ChatClient chatClient = openAiClient.GetChatClient(modelName);

        var systemPrompt = "Jesteś pomocnym asystentem, który tworzy opisy kursów w sklepie internetowym. Zwrócisz odpowiedź w formacie JSON.";

        Console.WriteLine("Wprowadź kurs:");
        var userPrompt = Console.ReadLine();
        Console.WriteLine("Właśnie nad tym pracuję...");

        ChatCompletion completion = chatClient.CompleteChat(
        [
            new SystemChatMessage(systemPrompt),
            new AssistantChatMessage("Mogę pomóc w tworzeniu opisów kursów. Co mogę dla Ciebie zrobić?"),
            new UserChatMessage(userPrompt)
        ], completionOptions);

        Console.WriteLine($"[{completion.Role}]: {completion.Content[0].Text}");
    }

    public async static Task OpenEndedChatStreamingAsync(string modelName)
    {
        OpenAIClient openAiClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        ChatClient chatClient = openAiClient.GetChatClient(modelName);

        List<ChatMessage> chatHistory =
        [
            new SystemChatMessage("Jesteś pomocnym asystentem, masz dużą wiedzę na temat programowania w C#."),
            new AssistantChatMessage("Wiem dużo o programowania w C#. W czym mogę ci dzisiaj pomóc?"),
        ];

        Console.WriteLine($"\n[ASYSTENT]: {chatHistory[^1].Content[0].Text}\n"); 

        while (true)
        {        
            Console.Write("Ty: ");
            var input = Console.ReadLine()!;

            if (string.IsNullOrEmpty(input))
            {
                break;
            }
            chatHistory.Add(new UserChatMessage(input));

            Console.Write("\n[ASYSTENT]: ");
            var fullAssistantResponse = new StringBuilder();

            await foreach (var update in chatClient.CompleteChatStreamingAsync(chatHistory))
            {
                foreach (ChatMessageContentPart contentUpdatePart in update.ContentUpdate)
                {
                    Console.Write(contentUpdatePart.Text);
                    fullAssistantResponse.Append(contentUpdatePart.Text);
                }
            }
            Console.WriteLine();

            chatHistory.Add(new AssistantChatMessage(fullAssistantResponse.ToString()));
        }
    }
}
