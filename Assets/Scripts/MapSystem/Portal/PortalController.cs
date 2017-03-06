/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections.Generic;

using UnityEngine;

public class PortalController : MController
{
    Dictionary<string, MapObjectBehaviour> activePortals = new Dictionary<string, MapObjectBehaviour>();

    // TODO: Solve the portal loop (two portals that are gatways to each other)
    public void HandlePortalEnter(MapObjectBehaviour target, MapObjectBehaviour portal)
    {
        if(hasDestination(portal))
        {
            MapObjectBehaviour destination = GetDestination(portal);
            Transform targetTrans = target.transform;
            float preserveZPos = targetTrans.position.z;
            Vector3 destPos = destination.transform.position;
            destPos.z = preserveZPos;
            targetTrans.position = destPos;
        }
    }

    public void TrackPortal(MapObjectBehaviour mapObject)
    {
        string id = getDelegateStr(mapObject, tuning.IdDelegate);
        if(id != null)
        {
            if(activePortals.ContainsKey(id))
            {
                activePortals[id] = mapObject;
            }
            else
            {
                activePortals.Add(id, mapObject);
            }
        }
    }

    public void ClearActivePortals()
    {
        activePortals.Clear();
    }

    public MapObjectBehaviour GetDestination(MapObjectBehaviour gateway)
    {
        string destinationId = getDelegateStr(gateway, tuning.DestinationDelegate);
        MapObjectBehaviour destination;
        if(tryGetPortal(destinationId, out destination))
        {
            return destination;
        }
        else
        {
            Debug.LogErrorFormat("Destination with id {0} was not found", destinationId);
            return null;
        }
    }

    bool tryGetPortal(string portalId, out MapObjectBehaviour obj)
    {
        return activePortals.TryGetValue(portalId, out obj);
    }

    bool hasDestination(MapObjectBehaviour obj)
    {
        return obj.Descriptor.HasDelegate(tuning.DestinationDelegate);
    }

}
