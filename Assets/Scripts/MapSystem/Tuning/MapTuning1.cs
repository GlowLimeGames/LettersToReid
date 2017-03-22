/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class LTRTuning : Tuning<LTRTuning>
{
    #region Instance Accessors 

    public int ExampleNumber
    {
        get
        {
            return exampleNumber;
        }
    }

    public float ExampleDecimal
    {
        get
        {
            return exampleDecimal;
        }
    }

    public string ExampleText
    {
        get
        {
            return exampleText;
        }
    }

    public bool ExampleFlag
    {
        get
        {
            return exampleFlag;
        }
    }

    public bool JumpEnabled
    {
        get
        {
            return jumpEnabled;
        }
    }

    public float MaxPlayerSpeed
    {
        get
        {
            return maxPlayerSpeed;
        }
    }

    #endregion

    #region Tuning<T> Overrides 

    protected override string fileName 
    {
        get 
        {
            return MapGlobal.LTR_TUNING;
        }
    }

    #endregion

    [SerializeField]
    int exampleNumber;
    [SerializeField]
    float exampleDecimal;
    [SerializeField]
    string exampleText;
    [SerializeField]
    bool exampleFlag;
    [SerializeField]
    bool jumpEnabled;
    [SerializeField]
    float maxPlayerSpeed;

}
