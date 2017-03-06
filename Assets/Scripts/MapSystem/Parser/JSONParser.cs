/*
 * Author(s): Isaiah Mann
 * Description: Parses JSON files
 * Usage: [no notes]
 */

using UnityEngine;

public class JSONParser : Parser
{
    public T ParseJSONFromResources<T>(string fileName) 
    {
        string json = GetTextAssetInResources(JSONPath(fileName)).text;
        return ParseJSON<T>(json);
    }

    public T ParseJSON<T>(string json) 
    {
        return JsonUtility.FromJson<T>(json);
    }

    public T ParseJSONOverwriteFromResources<T>(string fileName, T target) 
    {
        string json = GetTextAssetInResources(JSONPath(fileName)).text;
        return ParseJSONOverwrite<T>(json, target);
    }

    public T ParseJSONOverwrite<T>(string json, T target)
    {
        JsonUtility.FromJsonOverwrite(json, target);
        return target;
    }

}
