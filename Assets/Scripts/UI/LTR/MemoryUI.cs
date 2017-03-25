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
    MemoryController memories;

    [SerializeField]
    Text memoryText;

    [SerializeField]
    ScrollRect scroll;

    [SerializeField]
    CanvasGroup memoryDisplayCanvas;

    [SerializeField]
    Text memoriesCollectedDisplay;

    [SerializeField]
    string memoriesCollectedFormat = "{0}/{1} Memories";

    bool mouseIsDown;

    public void DisplayMemory(Memory mem)
    {
        Show();
        memoryText.text = mem.Body;
    }

    public override void Show()
    {
        toggleCanvasGroup(memoryDisplayCanvas, true);
        scroll.verticalNormalizedPosition = 1;
    }
        
    public override void Hide()
    {
        toggleCanvasGroup(memoryDisplayCanvas, false);
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

    #region MonoBehaviourExtended Overrides

    protected override void setReferences()
    {
        base.setReferences();
        memoryDisplayCanvas = GetComponent<CanvasGroup>();
    }

    protected override void fetchReferences()
    {
        base.fetchReferences();
        memories = MemoryController.Instance;
        memories.SubscribeToMemoryCollected(updateMemoriesCollected);
        updateMemoriesCollected();
    }

    protected override void cleanupReferences()
    {
        base.cleanupReferences();
        memories.UnsubscribeFromMemoryCollected(updateMemoriesCollected);
    }

    #endregion

    void updateMemoriesCollected()
    {
        memoriesCollectedDisplay.text = string.Format(
            memoriesCollectedFormat,
            memories.MemoryDiscoveredCount,
            memories.TotalMemoryCount);

    }

}
