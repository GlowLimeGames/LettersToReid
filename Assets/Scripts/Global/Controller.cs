/*
 * Author(s): Isaiah Mann
 * Description: Generic controller class
 * Usage: [no notes]
 */

using UnityEngine;

public abstract class MController : MonoBehaviour 
{
    protected TravelController travel;
    protected MapTuning tuning;

    protected virtual void Awake()
    {
        tuning = MapTuning.Get;
    }

    protected virtual void Start()
    {
        travel = TravelController.Instance;
    }

    protected object getDelegate(MapObjectBehaviour obj, string delegateId)
    {
        return obj.Descriptor.DelegateValue(delegateId);
    }

    protected string getDelegateStr(MapObjectBehaviour obj, string delegateId)
    {
        return obj.Descriptor.DelegateStr(delegateId);
    }

}
