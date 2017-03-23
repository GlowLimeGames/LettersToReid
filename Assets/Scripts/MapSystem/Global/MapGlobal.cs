/*
 * Author(s): Isaiah Mann
 * Description: Global constants for the map system
 * Usage: [no notes]
 */

using System.IO;

public class MapGlobal : Global
{
    public const string MAP_DATA = "MapData";
    public const string MAP_TUNING = "MapTuning";
    public const string LTR_TUNING = "LTRTuning";
    public const string MAP_TEMPLATE = "MapTemplate";
	public const string DEFAULT_BACKGROUND = "Background";
    public const string MAP_KEY = "MP";
    public const string MEMORY_KEY = "M";

    const string MAP_SCENE_NAME_FORMAT = "Map{0}";

    #region Static Accessors

    public static string MAP_DATA_PATH
    {
        get
        {
            return Path.Combine(JSON_DIR, MAP_DATA);
        }
    }

    public static string MAP_TUNING_PATH
    {
        get
        {
            return Path.Combine(JSON_DIR, MAP_TUNING);
        }
    }

    #endregion

    public static bool IsMemory(string objKey)
    {
        return objKey.Equals(MEMORY_KEY);
    }

    public static string GetMapSceneName(object mapId)
    {
        return string.Format(MAP_SCENE_NAME_FORMAT, mapId);
    }

}
