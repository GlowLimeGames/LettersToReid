//Maia D

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFader : MonoBehaviour {


	float changesPerSecond;
	float onTimeChange;
	int fadeValue;

	// Use this for initialization
	void Start () {
		



	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void fadeOut(AudioFile audioFile) {
		fadeValue = audioFile.Fade;
		changesPerSecond = audioFile.Volumef/audioFile.Fade;
		onTimeChange = 1/changesPerSecond;
		while (fadeValue >= 1) {
			AudioController.Instance.SetMusicVolume (audioFile.Volumef-1);
			fadeValue= fadeValue-1;
			WaitForSeconds (onTimeChange);
		}

		AudioController.Instance.SetMusicVolume (0);



	}

}
