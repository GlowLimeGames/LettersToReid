/*
 * Author(s): Isaiah Mann
 * Description: Loads sprites
 * Usage: [no notes]
 */

using System.IO;

using UnityEngine;

using k = MapGlobal;

public class SpriteLoader : Loader
{
    const string DEFAULT_PATH = k.SPRITES_DIR;

    bool usesCustomPath
    {
        get
        {
            return !string.IsNullOrEmpty(customDirPath);
        }
    }

    string dir 
    {
        get
        {
            if(usesCustomPath)
            {
                return customDirPath;
            }
            else
            {
                return DEFAULT_PATH;
            }
        }
    }

    string customDirPath = string.Empty;


    public SpriteLoader(string customDirInResources = null)
    {
        this.customDirPath = customDirInResources;
    }

    public void SetCustomPath(string customDir)
    {
        this.customDirPath = customDir;
    }

    public void ClearCustomPath()
    {
        this.customDirPath = string.Empty;
    }

    public Sprite Load(string spriteName)
    {
        return Resources.Load<Sprite>(getPath(spriteName));
    }

    string getPath(string spriteName)
    {
        return Path.Combine(this.dir, spriteName);
    }

}
