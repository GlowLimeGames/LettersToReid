/*
 * Author(s): Isaiah Mann
 * Description: Controls the player's travel between maps
 * Usage: [no notes]
 */

public class TravelController : SingletonController<TravelController>
{
    public bool InTransit
    {
        get
        {
            return !string.IsNullOrEmpty(destinationMap);
        }
    }
     
    public string GetOrigin()
    {
        return this.originMap.Replace("Map", string.Empty);
    }

    string originMap = string.Empty;
    string destinationMap = string.Empty;

    public void BeginTravel(string start, string end)
    {
        this.originMap = start;
        this.destinationMap = end;
    }
        
    public void CompleteTravel()
    {
        originMap = string.Empty;
        destinationMap = string.Empty;
    }

}
