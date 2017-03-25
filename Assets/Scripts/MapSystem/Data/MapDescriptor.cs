/*
 * Author(s): Isaiah Mann
 * Description: Describes a map in pure data that can be loaded in
 * Usage: [no notes]
 */

using UnityEngine;

using k = MapGlobal;

[System.Serializable]
public class MapDescriptor 
{
	const string DEFAULT_BACKGROUND = k.DEFAULT_BACKGROUND;

    #region Instance Acccessors 

    public string BackgroundSprite
    {
        get
        {
            return backgroundSprite;
        }
    }

    public GameObject[,][] Map 
    {
        get 
        {
            return _map;
        }
        private set
        {
            _map = value;
        }
        
    }

    public string MapName 
    {
        get;
        private set;
    }

    #endregion

    [SerializeField]
    string backgroundSprite;
   
    GameObject[,][] _map;

    #region Constructors 

    public MapDescriptor(string mapName, GameObject[,][] map)
    {
        this.MapName = mapName;
        this.Map = map;
    }
        
    #endregion

	public void SetBackgroundToDefault()
	{
		this.backgroundSprite = DEFAULT_BACKGROUND;
	}

}
