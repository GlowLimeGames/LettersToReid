/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryTest : MonoBehaviour 
{
	ItemController item;

	void Start()
	{
		item = ItemController.Instance;
		MapTemplate temp = MapTemplate.Get;
		MapData mem;
		temp.TryGetData("M", out mem);
		mem.SetDelegates(new string[]{"id"}, new object[]{4});
		GetComponent<MapItemBehaviour>().AssignDescriptor(mem);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			item.CollectItem(GetComponent<MapItemBehaviour>());
		}
	}

}
