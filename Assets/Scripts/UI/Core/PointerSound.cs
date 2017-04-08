using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExampleClass : MonoBehaviour, IPointerEnterHandler {

	
	public void OnPointerEnter(PointerEventData eventData)
	{
		EventController.Event ("ui_hover");
		
	}
}