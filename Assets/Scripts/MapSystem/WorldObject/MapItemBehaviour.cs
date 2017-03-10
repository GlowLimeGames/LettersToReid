/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class MapItemBehaviour : MapObjectBehaviour
{
    ItemController items;

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
		if(GetItem.IsCollectible)
		{
			BoxCollider2D coll;
			if(!(coll = GetComponent<BoxCollider2D>()))
			{
				coll = gameObject.AddComponent<BoxCollider2D>();
			}
			coll.isTrigger = true;
		}
        this.items = ItemController.Instance;
	}

    void OnMouseUp()
    {
        if(GetItem.IsCollectible)
        {
            items.CollectItem(this);
        }
    }

}
