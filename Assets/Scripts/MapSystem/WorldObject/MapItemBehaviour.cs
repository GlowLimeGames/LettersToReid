/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class MapItemBehaviour : MapObjectBehaviour
{
    public MapItem GetItem 
    {
        get 
        {
            return this.Descriptor as MapItem;
        }
    }

	public override void Initialize()
	{
		base.Initialize();
		if(GetItem.Collectible)
		{
			BoxCollider2D coll;
			if(!(coll = GetComponent<BoxCollider2D>()))
			{
				coll = gameObject.AddComponent<BoxCollider2D>();
			}
			coll.isTrigger = true;
		}
	}

}
