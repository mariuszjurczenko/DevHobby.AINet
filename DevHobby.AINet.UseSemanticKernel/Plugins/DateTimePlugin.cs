using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace DevHobby.AINet.UseSemanticKernel.Plugins;

public class DateTimePlugin
{
    [KernelFunction]
    [Description("Pobiera aktualną datę i godzinę w formacie UTC")]
    public string GetCurrentDateAndTime()
    {
        return DateTime.UtcNow.ToString("R");
    }
}
