﻿/*
 * Author(s): Isaiah Mann
 * Description: Describes the behaviour of an object on the map
 * Usage: [no notes]
 */

using UnityEngine;

public abstract class MapObjectBehaviour : MonoBehaviour
{
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    public MapData Descriptor
    {
        get
        {
            return this.descriptor;
        }
    }

    MapData descriptor;

    public void AssignDescriptor(MapData descriptor)
    {
        this.descriptor = descriptor;
    }

    public MapData CopyDescriptor()
    {
        return this.descriptor.Copy();
    }
        
    public virtual void Initialize()
    {
        if(Descriptor.IsSolid || Descriptor.IsPortal)
        {
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            if(Descriptor.IsPortal)
            {
                collider.isTrigger = true;
            }
        }
    }

    protected T ensureRef<T>() where T : Component
    {
        T comp = GetComponent<T>();
        if(comp == null)
        {
            comp = gameObject.AddComponent<T>();
        }
        return comp;
    }
        
}
