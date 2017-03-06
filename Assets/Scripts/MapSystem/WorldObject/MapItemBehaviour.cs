/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class MapItemBehaviour : MapObjectBehaviour
{
    public MapItem GetItem 
    {
        get 
        {
            return this.Descriptor as MapItem;
        }
    }

}
