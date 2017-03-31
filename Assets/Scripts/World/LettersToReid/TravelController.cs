/*
 * Author(s): Isaiah Mann
 * Description: Controls the player's travel between maps
 * Usage: [no notes]
 */

using System.Collections.Generic;

using UnityEngine;

public class TravelController : SingletonController<TravelController>
{
    PortalList allPortals;

    public bool InTransit
    {
        get
        {
            return !string.IsNullOrEmpty(destination);
        }
    }

    #region MonoBehaviourExtended Overrides

    protected override void setReferences()
    {
        base.setReferences();
        allPortals = PortalList.Get;
    }

    #endregion

    public string GetDestinationID()
    {
        // The destination id is the origin
        if(this.origin.Contains("Map"))
        {
            return this.origin.Replace("Map", string.Empty);
        }
        else
        {
            return this.destination;
        }
    }

    string origin = string.Empty;
    string destination = string.Empty;

    public string GetMapFromPortal(string portalId)
    {
        return allPortals.GetMap(portalId);
    }

    public void BeginTravel(string start, string end)
    {
        this.origin = start;
        this.destination = end;
    }
        
    public void CompleteTravel()
    {
        origin = string.Empty;
        destination = string.Empty;
    }

}

[System.Serializable]
public class PortalList : Tuning<PortalList>
{    
    public string[] Map1;
    public string[] Map2;
    public string[] Map3;
    public string[] Map4;
    public string[] Map5;
    public string[] Map6;
    public string[] Map7;
    public string[] Map8;
    public string[] Map9;
    public string[] Map10;

    Dictionary<string, string> portalToMapLookup;

    public string GetMap(string portalId)
    {
        string map;
        if(portalToMapLookup.TryGetValue(portalId, out map))
        {
            return map;
        }
        else
        {
            return string.Empty;
        }
            
    }

    protected override string fileName 
    {
        get 
        {
            return "PortalList";
        }
    }

    protected override void init()
    {
        base.init();
        string[][] allMaps = new string[][]{Map1, Map2, Map3, Map4, Map5, Map6, Map7, Map8, Map9, Map10};
        portalToMapLookup = new Dictionary<string, string>();
        for(int i = 0; i < allMaps.Length; i++)
        {
            foreach(string portal in allMaps[i])
            {
                portalToMapLookup[portal] = "Map" + (i+1);
            }
        }
    }

}
