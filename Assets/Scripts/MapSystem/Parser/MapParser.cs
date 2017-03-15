/*
 * Author(s): Isaiah Mann
 * Description: Parses the game world from a collection of strings
 * Usage: [no notes]
 */

using System.Collections.Generic;

using UnityEngine;

public class MapParser : Parser
{
    
    string joinKey = DEFAULT_JOIN_KEY;
    string delegateKey = DEFAULT_DELEGATE_KEY;
    string delegateSeparatorKey = DEFAULT_DELEGATE_SEPARATOR_KEY;

    MapTemplate template;

    Dictionary<string, GameObject> bufferedPrefabs = new Dictionary<string, GameObject>();

    protected MapTuning tuning
    {
        get
        {
            return MapTuning.Get;   
        }
    }

    #region Constructors 

    public MapParser()
    {
        MapTuning tuning = MapTuning.Get;
        joinKey = tuning.JoinKey;
        delegateKey = tuning.DelegateKey;
        delegateSeparatorKey = tuning.DelegateSeparator;
        template = MapTemplate.Get;
    }

    #endregion

    public MapDescriptor ParseWorld(string mapName, string[,] worldKeys) 
    {
        int width = worldKeys.GetLength(0);
        int height = worldKeys.GetLength(1);
        GameObject[,][] world = new GameObject[width, height][];
        for(int x = 0; x < width; x++) 
        {
            for(int y = 0; y < height; y++) 
            {
                string keysInCell = worldKeys[x, y];
                if(string.IsNullOrEmpty(keysInCell))
                {
                    world[x, y] = new GameObject[0];
                }
                else
                {
                    string[] keys = worldKeys[x, y].Split(joinKey.ToCharArray());
                    world[x, y] = parseObjectsAtPosition(keys, x, y);
                }
            }
        }
        MapDescriptor descriptor = new MapDescriptor(mapName, world);
        JSONParser metaDataParser = new JSONParser();
		try 
		{
        	metaDataParser.ParseJSONOverwriteFromResources(getMetaFileName(mapName), descriptor);
		}
		catch
		{
			descriptor.SetBackgroundToDefault();
		}
        return descriptor;
    }

    GameObject[] parseObjectsAtPosition(string[] keys, int x, int y) 
    {
        GameObject[] objectsAtPosition = new GameObject[keys.Length];
        for(int i = 0; i < objectsAtPosition.Length; i++) 
        {
            string key;
            bool hasDelegates = false;
            string[] keyPlusDelegates = null;

            if(keys[i].Contains(delegateKey))
            {
                hasDelegates = true;
                keyPlusDelegates = keys[i].Split(delegateKey.ToCharArray());
                key = keyPlusDelegates[0];
            }
            else 
            {
                key = keys[i];
            }
            objectsAtPosition[i] = getPrefabFromKey(key);
            MapObjectBehaviour behaviour = objectsAtPosition[i].GetComponent<MapObjectBehaviour>();
            MapData descriptor;
            if(behaviour && template.TryGetData(key, out descriptor))
            {
                behaviour.AssignDescriptor(descriptor);
                if(hasDelegates && keyPlusDelegates != null)
                {
                    string[] delegateVals = keyPlusDelegates[1].Split(delegateSeparatorKey.ToCharArray());
                    descriptor.SetDelegates(descriptor.Delegates, delegateVals);
                }
            }
        }
        return objectsAtPosition;
    }

    GameObject getPrefabFromKey(string key) 
    {
        GameObject obj;
        if(!bufferedPrefabs.TryGetValue(key, out obj))
        {
            obj = Resources.Load<GameObject>(PrefabsPath(key));
            bufferedPrefabs.Add(key, obj);
        }
        if(!obj)
        {
            Debug.LogErrorFormat("Key {0} is null", key);
        }
        return invisibleClone(obj);
    }

    GameObject invisibleClone(GameObject obj)
    {
        GameObject invisible = Object.Instantiate(obj);
        invisible.hideFlags = HideFlags.HideInHierarchy;
        invisible.SetActive(false);
        return invisible;
    }

    string getMetaFileName(string mapName)
    {
        return string.Format("{0}{1}", mapName, tuning.MetaSuffix);
    }

}
