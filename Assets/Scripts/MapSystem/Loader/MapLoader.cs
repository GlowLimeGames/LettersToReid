/*
 * Author(s): Isaiah Mann
 * Description: Spawns the map
 * Usage: [no notes]
 */

using UnityEngine;

public class MapLoader : Loader
{
    public void CreateWorld(MapDescriptor descriptor, MapController map, PortalController portals, CameraController camera, Transform parent)
    {
        GameObject[,][] worldTemplate = descriptor.Map;
        for(int x = 0; x < worldTemplate.GetLength(0); x++) 
        {
            for(int y = 0; y < worldTemplate.GetLength(1); y++)
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
                    }


                }
            }
        }
        Sprite background = new SpriteLoader().Load(descriptor.BackgroundSprite);
        camera.SetBackground(background);
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
