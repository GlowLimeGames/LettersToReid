/*
 * Author: Isaiah Mann
 * Description: Guards against player getting outside the area of the map by walking past a just traveled through portal
 */

using UnityEngine;

public class PortalSafetyNet : MonoBehaviourExtended
{
    Collider2D targetPortal;

    public void SetTargetPortal(Collider2D portal)
    {
        this.targetPortal = portal;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if(player)
        {
            player.CompleteTravel();
            player.OnTriggerEnter2D(this.targetPortal);
        }
    }

}
