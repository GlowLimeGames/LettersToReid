using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.IO;
/* Author: Anisha Pai
* Description: Retrieves a memory with method getMemory(int id)
*/

public class MemoriesParser: MonoBehaviour {

    public TextAsset jsonMemory;
    MemoryGroup allMemories;

    public void Awake() {

        jsonMemory = (TextAsset)Resources.Load("jsonMemories") as TextAsset;
        allMemories = JsonUtility.FromJson<MemoryGroup>(jsonMemory.text);
    }

    public String getMemory(int id) {

        if (allMemories.memoryLookup.ContainsKey(id))
            return allMemories.memoryLookup[id];
        else
            return "Memory not found. Choose an id number that is an integer between 1 and " + allMemories.memoryLookup.Count;
       
    }

    public int getLength() {
        return allMemories.memoryLookup.Count;
    }
}
