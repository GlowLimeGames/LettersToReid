/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MemoryUI : LTRUITemplate, IPointerEnterHandler, IPointerExitHandler
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

    bool mouseInCanvas;

    public void DisplayMemory(Memory mem)
    {
        Show();
        memoryText.text = mem.Body;
    }

    public override void Show()
    {
        if(memoryDisplayCanvas.alpha == 0)
        {
            toggleCanvasGroup(memoryDisplayCanvas, true);
            scroll.verticalNormalizedPosition = 1;
        }
    }
        
    public override void Hide()
    {
        toggleCanvasGroup(memoryDisplayCanvas, false);
    }

    void Update()
    {
        if(isVisible && !mouseInCanvas && Input.GetMouseButtonDown(0))
        {
            Hide();
        }
    }

    public void OnPointerEnter(PointerEventData pData)
    {
        mouseInCanvas = true;   
    }

    public void OnPointerExit(PointerEventData pData)
    {
        mouseInCanvas = false;
    }

    #region MonoBehaviourExtended Overrides

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
