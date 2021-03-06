﻿/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections.Generic;

public class MemoryUI : LTRUITemplate, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsOpen
    {
        get
        {
            return this.isOpen;
        }
    }

    MemoryController memories;

    [SerializeField]
    Text memoryText;

    [SerializeField]
    CanvasGroup memoryDisplayCanvas;

    [SerializeField]
    Text memoriesCollectedDisplay;

    [SerializeField]
    Text locationName;

    [SerializeField]
    string memoriesCollectedFormat = "{0}/{1} Memories";

	[SerializeField]
	Sprite[] overlays;

	[SerializeField]
	Image overlayDisplay;

    bool mouseInCanvas;
    bool isOpen;

	Dictionary<string, Sprite> overlayLookup;

    Memory memory;

	protected override void setReferences()
	{
		base.setReferences();
		initOverlayLookup();
	}

    public void SetMap(MapDescriptor map)
    {
        this.locationName.text = map.MapName.Replace("MP-", "Map ");
    }

    public void DisplayMemory(Memory mem)
    {
        Show();
        memoryText.text = mem.Body;
        this.memory = mem;
		setOverlay(mem);
    }

    public override void Show()
    {
        if(memoryDisplayCanvas.alpha == 0)
        {
            toggleCanvasGroup(memoryDisplayCanvas, true);
        }
        isOpen = true;
    }
        
    public override void Hide()
    {
        toggleCanvasGroup(memoryDisplayCanvas, false);
        isOpen = false;
        if(memory != null && !string.IsNullOrEmpty(memory.triggerScene))
        {
            try
            {
                SceneManager.LoadScene(memory.triggerScene);
            }
            catch
            {
                Debug.LogErrorFormat("Scene {0} does not exist", memory.triggerScene);
            }
        }
        memory = null;
    }

    void Update()
    {
        if(isVisible && !mouseInCanvas && Input.GetMouseButtonDown(0))
        {
            Hide();
        }
    }

	void initOverlayLookup()
	{
		overlayLookup = new Dictionary<string, Sprite>();
		foreach(Sprite overlay in overlays) 
		{
			overlayLookup.Add(overlay.name, overlay);
		}
	}

	void setOverlay(Memory mem)
	{
		overlayDisplay.enabled = !string.IsNullOrEmpty(mem.overlay);
		Sprite overlay;
		if(overlayDisplay.enabled && overlayLookup.TryGetValue(mem.overlay, out overlay)) 
		{
			overlayDisplay.sprite = overlay;
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
