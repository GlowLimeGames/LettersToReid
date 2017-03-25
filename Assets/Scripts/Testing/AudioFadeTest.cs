/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeTest : MonoBehaviour 
{
    [SerializeField]
    KeyCode playKey;
   
    [SerializeField]
    KeyCode stopKey;

    [SerializeField]
    string playEventName;

    [SerializeField]
    string stopEventName;

    void Update()
    {
        if(Input.GetKeyDown(playKey))
        {
            EventController.Event(playEventName);
        }
        if(Input.GetKeyDown(stopKey))
        {
            EventController.Event(stopEventName);
        }
    }

}
