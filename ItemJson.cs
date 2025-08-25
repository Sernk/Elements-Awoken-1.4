using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ElementsAwoken;

/// <summary>
/// Thanks to skyzephire for filling out the file
/// </summary>
public class ItemJson
{
    public List<string> Materials { get; set; }

    public static ItemJson LoadFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<ItemJson>(json);
    }
}