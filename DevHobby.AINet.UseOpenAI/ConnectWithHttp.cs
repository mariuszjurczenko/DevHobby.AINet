using Newtonsoft.Json;
using System.Text;

namespace DevHobby.AINet.UseOpenAI;

public static class ConnectWithHttp
{
    private static readonly string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
    
    public async static Task Run()
    {
        string prompt = "Napisz krótką, inspirującą historyjkę o naszym sklepie z kursami C#, pokazującą, jak pomagamy ludziom rozwijać umiejętności programistyczne i spełniać zawodowe marzenia.";

        string response = await CallGpt4ApiAsync(prompt);

        Console.WriteLine("Odpowiedz od GPT-4:");
        Console.WriteLine(response);
    }

    public static async Task<string> CallGpt4ApiAsync(string prompt)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = prompt }
                },
                max_tokens = 200,
                temperature = 0.6
            };

            string jsonContent = JsonConvert.SerializeObject(requestBody);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject(result);
                return jsonResponse.choices[0].message.content;
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenAI API error: {response.StatusCode} - {error}");
            }
        }
    }
}
