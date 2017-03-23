/*
 * Author(s): Isaiah Mann
 * Description: Letters to Reid game save file
 * Usage: [no notes]
 */

using System.Collections.Generic;
using System.Runtime.Serialization;

[System.Serializable]
public class LTRGameSave : GameSave
{
    const string MEM = "MEMORIES";
    const string MEM_LOOKUP = "LOOKUP";
    #region Instance Accessors

    public Memory[] DiscoveredMemories
    {
        get
        {
            return this.discoveredMemories.ToArray();
        }
    }

    #endregion

    List<Memory> discoveredMemories;
    Dictionary<int, Memory> memLookup;

    public LTRGameSave()
    {
        discoveredMemories = new List<Memory>();
        memLookup = new Dictionary<int, Memory>();
    }

    // The special constructor is used to deserialize values.
    public LTRGameSave(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        discoveredMemories = (List<Memory>) info.GetValue(MEM, typeof(List<Memory>));
        memLookup = (Dictionary<int, Memory>) info.GetValue(MEM_LOOKUP, typeof(Dictionary<int, Memory>));
            
    }

    // Implement this method to serialize data. The method is called on serialization.
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(MEM, discoveredMemories);
        info.AddValue(MEM_LOOKUP, memLookup);
    }

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
