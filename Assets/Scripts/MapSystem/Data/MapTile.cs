/*
 * Author(s): Isaiah Mann
 * Description: Represents a tile on the map
 * Usage: [no notes]
 */

using UnityEngine;

[System.Serializable]
public class MapTile : MapData
{
    #region Instance Accessors

    public bool IsClimbable
    {
        get
        {
            return isClimbable;
        }
    }

    #endregion

    [SerializeField]
    bool isClimbable;

}
