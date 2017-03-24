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

    CanvasGroup canvas;

    bool mouseIsDown;

    public void DisplayMemory(Memory mem)
    {
        Show();
        memoryText.text = mem.Body;
    }

    public override void Show()
    {
        toggleCanvasGroup(canvas, true);
        scroll.verticalNormalizedPosition = 1;
        base.Show();
    }
        
    public override void Hide()
    {
        toggleCanvasGroup(canvas, false);
        base.Hide();
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

    protected override void setReferences()
    {
        base.setReferences();
        canvas = GetComponent<CanvasGroup>();
    }

}
