namespace DevHobby.GPTizza.Util;

public class ModelSettings
{
    public string TextModelName { get; set; } = string.Empty;
    public string ImageModelName { get; set; } = string.Empty;
    public string AudioModelName { get; set; } = string.Empty;
    public string OPENAI_API_KEY { get; set; } = string.Empty;
    public string SPEECH_KEY { get; set; } = string.Empty;
    public string SPEECH_REGION { get; set; } = string.Empty;
    public string SPEECH_Voice { get; set; } = string.Empty;
    public string VISION_KEY { get; set; } = string.Empty;
    public string VISION_ENDPOINT { get; set; } = string.Empty;
}
