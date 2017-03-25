using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
/*
 * Author: Anisha Pai, Isaiah Mann
 * Description: Allows json to store multiple memories in dictionary format
*/

[System.Serializable]
public class MemoryGroup : SerializableDataCollection<Memory> {

    public Dictionary<int, string> memoryLookup {
        get {
            if (_lookup == null) {
                this._lookup = generateLookup(this.mmrs);
            }
            return _lookup;
        }
    }

    Dictionary<int, string> _lookup;
    public Memory[] mmrs;

    Dictionary<int, string> generateLookup(Memory[] mmrs) {
        Dictionary<int, string> lookup = new Dictionary<int, string>();

        // puts ids and memory bodies into memoryLookup
        foreach (Memory mmr in mmrs)
            lookup.Add(mmr.id, mmr.Body);
    
       return lookup;
    }

}
