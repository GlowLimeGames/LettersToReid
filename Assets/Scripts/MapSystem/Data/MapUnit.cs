/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

[System.Serializable]
public class MapUnit : MapData
{
    #region Instance Accessors 

    public bool IsPlayer
    {
        get
        {
            return isPlayer;
        }
    }
    public bool IsHostile
    {
        get
        {
            return isHostile;
        }
    }

    #endregion

    [SerializeField]
    bool isPlayer;
    [SerializeField]
    bool isHostile;

}
