/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class MapTuning : Tuning<MapTuning>
{
    #region Instance Accessors 

    public string JoinKey
    {
        get
        {
            return joinKey;
        }
    }

    public string DelegateKey
    {
        get
        {
            return delegateKey;
        }
    }

    public string DelegateSeparator
    {
        get
        {
            return delegateSeparator;
        }
    }

    public float CharacterMoveSpeed
    {
        get
        {
            return characterMoveSpeed;
        }
    }

    public float CharacterJumpSpeed
    {
        get
        {
            return characterJumpSpeed;
        }
    }

    public float PlayerGravityScale
    {
        get
        {
            return playerGravityScale;
        }
    }

    public string IdDelegate
    {
        get
        {
            return idDelegate;
        }
    }

    public string DestinationDelegate
    {
        get
        {
            return destinationDelegate;
        }
    }

    public string MetaSuffix
    {
        get
        {
            return metaSuffix;
        }
    }

    public string FileJoinKey
    {
        get
        {
            return fileJoinKey;
        }
    }
        
    #endregion

    #region Tuning<T> Overrides 

    protected override string fileName 
    {
        get 
        {
            return MapGlobal.MAP_TUNING;
        }
    }

    #endregion

    [SerializeField]
    string joinKey;
    [SerializeField]
    string delegateKey;
    [SerializeField]
    string delegateSeparator;
    [SerializeField]
    float characterMoveSpeed;
    [SerializeField]
    float characterJumpSpeed;
    [SerializeField]
    float playerGravityScale;
    [SerializeField]
    string idDelegate;
    [SerializeField]
    string destinationDelegate;
    [SerializeField]
    string metaSuffix;
    [SerializeField]
    string fileJoinKey;

}
