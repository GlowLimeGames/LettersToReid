/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class MapUnitBehaviour : MapObjectBehaviour
{
    public  MapUnit GetUnit 
    {
        get 
        {
            return this.Descriptor as MapUnit;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
		if(GetUnit.IsSolid)
		{
			Destroy(GetComponent<BoxCollider2D>());
			// Need a round collider for the character:
			gameObject.AddComponent<CapsuleCollider2D>();
		}
        if(GetUnit.IsPlayer)
        {
            ensureRef<PlayerController>();
        }
    }

}
