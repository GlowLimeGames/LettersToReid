using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
/* 
 * Author: Anisha Pai
 * Description: Allows json to store multiple memories in dictionary format
*/

[System.Serializable]
public class MemoryGroup : SerializableDataCollection<Memory>
{

    public Dictionary<int, string> memoryLookup;
    public Memory[] mmrs;

   public void start() {
       
        List<Memory> allMemories = new List<Memory>(mmrs);
        memoryLookup = new Dictionary<int, string>();

        // puts ids and memory bodies into memoryLookup
        foreach(Memory mmr in mmrs) 
            memoryLookup.Add(mmr.id, mmr.Body);
        
}

}
