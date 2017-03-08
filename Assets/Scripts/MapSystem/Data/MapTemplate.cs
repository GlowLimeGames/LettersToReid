/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class MapTemplate : Tuning<MapTemplate>
{
    #region Instances Accessors 

    public MapUnit[] Units
    {
        get
        {
            return units;
        }
    }
    public MapTile[] Tiles
    {
        get
        {
            return tiles;
        }
    }
    public MapItem[] Items
    {
        get
        {
            return items;
        }
    }

    #endregion

    protected override string fileName 
    {
        get 
        {
            return MapGlobal.MAP_TEMPLATE;
        }
    }

    [SerializeField]
    MapUnit[] units;
    [SerializeField]
    MapTile[] tiles;
    [SerializeField]
    MapItem[] items;

    Dictionary<string, MapData> mapLookup = new Dictionary<string, MapData>();

    public bool TryGetData(string key, out MapData data)
    {
        if(mapLookup.TryGetValue(key, out data))
        {
            // Return a copy of the template: not the template itself
            data = data.Copy();
            return true;
        }
        else 
        {
            return false;
        }
    }

    protected override void init()
    {
        base.init();
        addMapDataToLookup(units);
        addMapDataToLookup(tiles);
        addMapDataToLookup(items);
    }

    void addMapDataToLookup(MapData[] data)
    {
        foreach(MapData dataObj in data)
        {
            mapLookup.Add(dataObj.Key, dataObj);
        }
    }
}
