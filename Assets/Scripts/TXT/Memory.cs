using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * Author: Anisha Pai
 * Description: Defines memory json parameters
*/

[System.Serializable]
public class Memory {

    public int id;
    public string Body;
	public string overlay;

    #region Object overrides 

    public override bool Equals(object obj)
    {
        if(obj is Memory)
        {
            return (obj as Memory).id.Equals(this.id);
        }
        else
        {
            return base.Equals(obj);
        }
    }

    public override int GetHashCode()
    {
        return id;
    }

    #endregion

}
