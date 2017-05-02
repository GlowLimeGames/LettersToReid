using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopAudio : MonoBehaviour {

	//Button button;

	// Use this for initialization
	void Start () {
		//button = GetComponent<Button>();
		EventController.Event ("stop_beginningamb");
		EventController.Event ("stop_endingamb");
	}

	// Update is called once per frame
	void Update () {
		//button.onClick.AddListener (stopGameMusic);
	}

	void stopGameMusic() {
		
	}
}
