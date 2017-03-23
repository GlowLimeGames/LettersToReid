/*
 * Authors: Martha Hollister, Isaiah Mann
 * Description: Controls a single memory in game
 */

using UnityEngine;

using k = MapGlobal;

public class MemoryBehvior : MonoBehaviourExtended
{
    Memory memory
    {
        get
        {
            return controller.GetMemory(memoryId.ToString());
        }
    }

    int memoryId
    {
        get
        {
            return int.Parse(memoryDescriptor.DelegateStr(tuning.IdDelegate));
        }
    }

    MemoryController controller;
    SpriteRenderer sRenderer;
    MapTuning tuning;

    [SerializeField]
    Sprite closedMemory;
    [SerializeField]
    Sprite openMemory;

    MapItem memoryDescriptor;

    public void SetMemory(MapItem mem)
    {
        this.memoryDescriptor = mem;
        updateMemoryDisplay();
    }

    public void Collect()
    {
        controller.CollectMemory(memoryId);
        updateMemoryDisplay();
    }

    #region MonoBehaviourExtended Overrides

    protected override void setReferences()
    {
        base.setReferences();
        sRenderer = GetComponentInChildren<SpriteRenderer>();
        tuning = MapTuning.Get;
        controller = MemoryController.Instance;
    }
   
    #endregion

    void updateMemoryDisplay()
    {
        if(!sRenderer)
        {
            Debug.LogError("Sprite Renderer is null");
            return;
        }
        if(!controller)
        {
            Debug.LogError("MemoryController is null");
            return;
        }

        if(controller.MemoryDiscovered(memoryId))
        {
            sRenderer.sprite = openMemory;
        }
        else
        {
            sRenderer.sprite = closedMemory;
        }
    }

}
