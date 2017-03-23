/*
 * Author(s): Isaiah Mann
 * Description: Letters to Reid game save file
 * Usage: [no notes]
 */

using System.Collections.Generic;

[System.Serializable]
public class LTRGameSave : GameSave
{
    #region Instance Accessors

    public Memory[] DiscoveredMemories
    {
        get
        {
            return this.discoveredMemories.ToArray();
        }
    }

    #endregion

    List<Memory> discoveredMemories = new List<Memory>();
    Dictionary<int, Memory> memLookup = new Dictionary<int, Memory>();

    public void AddDiscoveredMemory(Memory mem)
    {
        discoveredMemories.Add(mem);
        try 
        {
            memLookup.Add(mem.id, mem);
        }
        catch
        {
            UnityEngine.Debug.LogErrorFormat("Unable to add memory {0} to lookup", mem.id);
        }
    }

    public bool MemoryDiscovered(Memory mem)
    {
        return discoveredMemories.Contains(mem);
    }

    public bool MemoryDiscovered(int memId)
    {
        return memLookup.ContainsKey(memId);
    }

    public bool TryGetMemory(int memId, out Memory mem)
    {
        return memLookup.TryGetValue(memId, out mem);
    }

}
