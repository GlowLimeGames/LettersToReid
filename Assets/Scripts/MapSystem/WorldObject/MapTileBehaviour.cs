/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class MapTileBehaviour : MapObjectBehaviour
{
    public MapTile GetTile 
    {
        get 
        {
            return this.Descriptor as MapTile;
        }
    }

    #region MapObjectBehaviour Overrides 

    public override void Initialize()
    {
        base.Initialize();
        // Double check to prevent adding two box colliders to the same object
        if(GetTile.IsClimbable && !gameObject.GetComponent<BoxCollider2D>())
        {
            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    #endregion

}
