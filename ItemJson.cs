using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ElementsAwoken;

/// <summary>
/// Thanks to skyzephire for filling out the file
/// </summary>
public class ItemJson
{
	public List<string> TheNecessaryItemsToOpen_400 { get; set; }
    public List<string> TheNecessaryItemsToOpen_100 { get; set; }
    public List<string> TheNecessaryItemsToOpen_75 { get; set; }
    public List<string> TheNecessaryItemsToOpen_50 { get; set; }
    public List<string> TheNecessaryItemsToOpen_25 { get; set; }
    public List<string> TheNecessaryItemsToOpen_20 { get; set; }
    public List<string> TheNecessaryItemsToOpen_15 { get; set; }
    public List<string> TheNecessaryItemsToOpen_10 { get; set; }
    public List<string> TheNecessaryItemsToOpen_5 { get; set; }
    public List<string> TheNecessaryItemsToOpen_3 { get; set; }
    /// <summary>
    /// Remove
    /// </summary>
    public List<string> TheNecessaryItemsToOpen { get; set; }

    public static ItemJson LoadFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<ItemJson>(json);
    }
}