using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace DevHobby.AINet.UseSemanticKernel.Plugins;

public class WeatherPlugin
{
    [KernelFunction]
    [Description("Pobiera pogodę na dany dzień i miejsce")]
    public string GetWeather([Description("Data podania pogody na")] string date, [Description("Lokalizacja, w której można sprawdzić pogodę")] string location)
    {
        if (location == "Katowice" || location == "Kraków" || location == "Wrocław" || location == "Warszawa")
            return $"Pogoda w {location} dnia {date} jest słoneczna, a temperatura maksymalna wynosi 28 stopni.";
        else
        {
            return "Przykro mi, nie posiadam takich informacji.";
        }
    }
}
