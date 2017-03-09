/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MemoryUI : LTRUITemplate, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    Text memoryText;

    [SerializeField]
    ScrollRect scroll;

    bool mouseIsDown;

    public void DisplayMemory(Memory mem)
    {
        Show();
        memoryText.text = mem.Body;
    }

    public override void Show()
    {
        scroll.verticalNormalizedPosition = 1;
        base.Show();
    }
        
    void Update()
    {
        if(isVisible && !mouseIsDown && Input.GetMouseButtonDown(0))
        {
            Hide();
        }
    }

    public void OnPointerDown(PointerEventData pData)
    {
        this.mouseIsDown = true;
    }

    public void OnPointerUp(PointerEventData pData)
    {
        this.mouseIsDown = false;
    }

}
