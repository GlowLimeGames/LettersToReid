/*
 * Author(s): Isaiah Mann
 * Description: Manages data saving for memories
 * Usage: [no notes]
 */

using System.IO;

using UnityEngine;

public class LTRSaveController : DataController
{
    #region Static Accessors 
   
    public static LTRSaveController GetInstance
    {
        get
        {
            return Instance as LTRSaveController;
        }
    }

    #endregion

    #region Instance Accessors

    public string GetFilePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, saveFileName);
        }
    }

    #endregion

    [SerializeField]
    string saveFileName = "LTRSave.dat";

    LTRGameSave save;

    // Returns true if it's a new memory:
    public bool FindMemory(Memory mem)
    {
        if(save.MemoryDiscovered(mem))
        {
            return false;
        }
        else
        {
            save.AddDiscoveredMemory(mem);
            SaveGame();
            return true;
        }
    }

    public bool MemoryDiscovered(int memId)
    {
        return save.MemoryDiscovered(memId);
    }

    public void LoadGame()
    {
        save = Load() as LTRGameSave;
    }

    public void SaveGame()
    {
        Buffer(save);
        Save();
    }

    #region DataController Overrides

    protected override SerializableData getDefaultFile()
    {
        return new LTRGameSave();
    }

    #endregion

    #region MonoBehaviourExtended Overrides

    protected override void setReferences()
    {
        base.setReferences();
        SetFilePath(GetFilePath);
        LoadGame();
    }

    #endregion

}
