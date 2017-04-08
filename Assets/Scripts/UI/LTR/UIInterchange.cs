/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System;
using System.Collections.Generic;

public class UIInterchange : SingletonController<UIInterchange>
{
    Dictionary<Type, LTRUITemplate> interfaces;

    protected override void setReferences()
    {
        base.setReferences();
        interfaces = new Dictionary<Type, LTRUITemplate>();
    }

    public void RegisterUI(Type type, LTRUITemplate ui)
    {
        if(interfaces.ContainsKey(type))
        {
            interfaces[type] = ui;
        }
        else
        {
            interfaces.Add(type, ui);
        }
    }

    public void DisplayMemory(Memory mem)
    {
        LTRUITemplate ui;
        if(getInterface(typeof(MemoryUI), out ui))
        {
            (ui as MemoryUI).DisplayMemory(mem);
        }
    }

    public void ToggleMemoryDisplay(Memory mem)
    {
        LTRUITemplate ui;
        if(getInterface(typeof(MemoryUI), out ui))
        {
            MemoryUI memoryUI = ui as MemoryUI;
            if(memoryUI.IsOpen)
            {
                memoryUI.Hide();
            }
            else
            {
                memoryUI.DisplayMemory(mem);
            }
        }
    }

    bool getInterface(Type type, out LTRUITemplate ui)
    {
        return interfaces.TryGetValue(type, out ui);
    }

}
