/*
 * Author(s): Isaiah Mann
 * Description: Generic: Tuning file
 * Usage: [no notes]
 */

using UnityEngine;

public abstract class Tuning<T> where T : Tuning<T>, new()
{
    #region Static Accessors

    public static T Get
    {
        get
        {
            if(!hasInstance)
            {
                _instance = loadFromFilePath();
                _instance.init();
            }
            return _instance;
        }
    }

    #endregion

    static bool hasInstance
    {
        get
        {
            return _instance != null;
        }
    }

    static T _instance;

    protected abstract string fileName
    {
        get;
    }

    static T loadFromFilePath()
    {
        JSONParser parser = new JSONParser();
        T instance = new T();
        return parser.ParseJSONOverwriteFromResources<T>(instance.fileName, instance);
    }

    protected virtual void init()
    {
        // NOTHING
    }

}
