/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class MemoryController : SingletonController<MemoryController>
{
    #region Instance Accessors

    public int MemoryDiscoveredCount
    {
        get
        {
            return save.GetSave.DiscoveredMemoryCount;
        }
    }

    public int TotalMemoryCount
    {
        get
        {
            return data.TotalMemoryCount;   
        }
    }

    #endregion

	[SerializeField]
	string memoryPathInResources;

    LTRSaveController save;
	MemoryDatabase data;
    MonoAction onMemoryCollected;

    #region MonoBehaviourExtended Overrides 

	protected override void setReferences()
	{
		base.setReferences();
		data = new MemoryDatabase(parseMemories(memoryPathInResources));
	}

    protected override void fetchReferences()
    {
        base.fetchReferences();
        save = LTRSaveController.GetInstance;
    }

    #endregion

    public bool MemoryDiscovered(int memId)
    {
        return save.MemoryDiscovered(memId);
    }

	public Memory GetMemory(string id)
	{
		return data.GetMemory(id);	
	}

    public void CollectMemory(int memId)
    {
        CollectMemory(GetMemory(memId.ToString()));
    }

	public void CollectMemory(Memory mem)
	{
		data.CollectMemory(mem);
        save.FindMemory(mem);
        callOnMemoryCollected();
	}

    public void SubscribeToMemoryCollected(MonoAction act)
    {
        onMemoryCollected += act;
    }

    public void UnsubscribeFromMemoryCollected(MonoAction act)
    {
        onMemoryCollected -= act;
    }

    void callOnMemoryCollected()
    {
        if(onMemoryCollected != null)
        {
            onMemoryCollected();
        }
    }

	Memory[] parseMemories(string path)
	{
		MemoryList list	= JsonUtility.FromJson<MemoryList>(Resources.Load<TextAsset>(path).text);
		return list.Memories;
	}
        
}

[Serializable]
public class MemoryDatabase
{
    #region Instance Accessors

    public int TotalMemoryCount
    {
        get
        {
            return allMemories.Count;
        }
    }

    #endregion

	Dictionary<string, Memory> allMemories;
	List<Memory> collectedMemories;

	public MemoryDatabase(Memory[] mems)
	{
		allMemories = new Dictionary<string, Memory>();
		collectedMemories = new List<Memory>();
		foreach(Memory mem in mems)
		{
			allMemories.Add(mem.id.ToString(), mem);
		}
	}

	public Memory[] GetCollectedMemories()
	{
		return collectedMemories.ToArray();
	}

	public bool MemoryIsCollected(Memory mem)
	{
		return collectedMemories.Contains(mem);
	}

	public Memory GetMemory(string id)
	{
		Memory mem;
		if(allMemories.TryGetValue(id, out mem))
		{
			return mem;
		}
		else
		{
			Debug.LogErrorFormat("Memory {0} not found. Returning null", id);
			return null;
		}
	}

	public void CollectMemory(Memory memory)
	{
		if(!collectedMemories.Contains(memory))
		{
			collectedMemories.Add(memory);
		}
	}

}

[Serializable]
public class MemoryList
{
	public Memory[] Memories;
}
