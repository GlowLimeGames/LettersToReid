using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderControl : MonoBehaviour {

    public Slider FXSlider;
    public Slider MusicSlider;


	float startMusicValue;
	float startFXValue;
		

	// Use this for initialization
	void Start () {
		FXSlider.minValue = 0;
		FXSlider.minValue = 0;
		FXSlider.wholeNumbers = true;
		FXSlider.value = 100;
		FXSlider.onValueChanged.AddListener (ChangeFXVolume);
		MusicSlider.maxValue = 100;
		MusicSlider.maxValue = 100;
		MusicSlider.wholeNumbers = true;
		MusicSlider.value = 100;
		MusicSlider.onValueChanged.AddListener (ChangeMusicVolume);

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

    void ChangeFXVolume (float volume) {
		int tempValueFX;
        tempValueFX = Mathf.RoundToInt(volume / 100f * startFXValue);//figure out a way to have topvalue
		AudioController.Instance.SetFXVolume(tempValueFX);

	}

    void ChangeMusicVolume (float volume) {
		int tempValueMusic;
        tempValueMusic = Mathf.RoundToInt(volume /100f * startMusicValue);
		AudioController.Instance.SetMusicVolume (tempValueMusic);

	}
}
