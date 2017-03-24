/*
 * Author: Isaiah Mann
 * 
 */

using UnityEngine.EventSystems;

public class MemoryUIScrollBar : LTRUITemplate, IPointerDownHandler, IPointerUpHandler
{
    MemoryUI parentUI;

    protected override void setReferences()
    {
        base.setReferences();
        parentUI = GetComponentInParent<MemoryUI>();
        if(!parentUI)
        {
            UnityEngine.Debug.LogError("Parent MemoryUI is null");
        }
    }

    public void OnPointerDown(PointerEventData pEvent)
    {
        parentUI.OnPointerDown(pEvent);
    }

    public void OnPointerUp(PointerEventData pEvent)
    {
        parentUI.OnPointerUp(pEvent);
    }

}
