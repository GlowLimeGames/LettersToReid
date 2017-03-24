/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class MapItemBehaviour : MapObjectBehaviour
{
    ItemController items;
    MemoryBehvior memory;

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
        memory = GetComponent<MemoryBehvior>();
        if(memory)
        {
            memory.SetMemory(GetItem);
        }
	}

    void OnMouseUp()
    {
        if(GetItem.IsCollectible)
        {
            items.CollectItem(this);
            if(memory)
            {
                memory.Collect();
            }
        }
    }

}
