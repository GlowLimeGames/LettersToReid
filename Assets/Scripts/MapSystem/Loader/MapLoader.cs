/*
 * Author(s): Isaiah Mann
 * Description: Spawns the map
 * Usage: [no notes]
 */

using System.IO;

using UnityEngine;

using k = MapGlobal;

public class MapLoader : Loader
{
    // Used to keep player from stepping outside of map
    GameObject portalSafetyNet;

    public MapLoader()
    {
        portalSafetyNet = Resources.Load<GameObject>(Path.Combine(k.PREFABS_DIR, k.PORTAL_SAFETY_NET));
    }

    public void CreateWorld(MapDescriptor descriptor, MapController map, PortalController portals, CameraController camera, Transform parent)
    {
        GameObject[,][] worldTemplate = descriptor.Map;
        int width = worldTemplate.GetLength(0);
        int height = worldTemplate.GetLength(1);
        for(int x = 0; x < width; x++) 
        {
            for(int y = 0; y < height; y++)
            {
                foreach(GameObject mapObj in worldTemplate[x, y])
                {
                    MapObjectBehaviour behaviour = Object.Instantiate(mapObj, new Vector3(x, y), Quaternion.identity).GetComponent<MapObjectBehaviour>();
                    behaviour.gameObject.SetActive(true);
                    behaviour.transform.SetParent(parent);
                    behaviour.AssignDescriptor(mapObj.GetComponent<MapObjectBehaviour>().CopyDescriptor());
                    behaviour.Initialize();
                    if(isPlayer(behaviour))
                    {
                        handleSetupPlayer(behaviour, map);
                    }
                    if(isPortal(behaviour))
                    {
                        handleSetupPortal(behaviour, portals);
                        if(descriptor.EdgeOfMap(x, y))
                        {
                            PortalSafetyNet safetyNet = Object.Instantiate(
                                this.portalSafetyNet, 
                                getSafetyNetPosition(x, y, width, height),
                                Quaternion.identity).GetComponent<PortalSafetyNet>();
                            safetyNet.SetTargetPortal(behaviour.GetComponent<Collider2D>());
                            safetyNet.transform.SetParent(parent);
                        }
                    }
                }
            }
        }
        Sprite background = new SpriteLoader().Load(descriptor.BackgroundSprite);
        camera.SetBackground(background);
    }
        
    Vector3 getSafetyNetPosition(int x, int y, int width, int height)
    {
        if(x == width - 1)
        {
            return new Vector3(width, y);
        }
        else if(x == 0)
        {
            return new Vector3(-1, y);
        }
        else if(y == height - 1)
        {
            return new Vector3(x, height);
        }
        else if(y == 0)
        {
            return new Vector3(x, -1);
        }
        else
        {
            Debug.LogErrorFormat("Invalid position for safety net: ({0}, {1})", x, y);
            return Vector3.zero;
        }
    }

    bool isPlayer(MapObjectBehaviour obj)
    {
        return obj.GetComponent<PlayerController>();
    }

    bool isPortal(MapObjectBehaviour obj)
    {
        return obj.Descriptor.IsPortal;
    }

    void handleSetupPortal(MapObjectBehaviour obj, PortalController portals)
    {
        portals.TrackPortal(obj);
    }

    void handleSetupPlayer(MapObjectBehaviour obj, MapController map)
    {
        PlayerController player = obj.GetComponent<PlayerController>();
        if(player)
        {
            player.Setup(map);
        }
        else
        {
            Debug.LogErrorFormat("{0} does not contain a [PlayerController] instance");
        }
    }

}
