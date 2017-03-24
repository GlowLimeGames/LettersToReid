/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class MapUnitBehaviour : MapObjectBehaviour
{
    public  MapUnit GetUnit 
    {
        get 
        {
            return this.Descriptor as MapUnit;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        if(GetUnit.IsPlayer)
        {
            ensureRef<PlayerController>();
        }
    }

}
