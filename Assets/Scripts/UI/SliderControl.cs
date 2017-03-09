using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderControl : MonoBehaviour {

	public Slider FXSlider = GetComponent<FXSlider>();
	public Slider MusicSlider = GetComponent<MusicSlider>();


	float startMusicValue;
	float startFXValue;
		

	// Use this for initialization
	void Start () {
		FXSlider.minValue = 0;
		FXSlider.minValue = 0;
		FXSlider.wholeNumbers = true;
		FXSlider.value = 100;
		FXSlider.onValueChanged.AddListener (ChangeFXVolume());
		MusicSlider.maxValue = 100;
		MusicSlider.maxValue = 100;
		MusicSlider.wholeNumbers = true;
		MusicSlider.value = 100;
		MusicSlider.onValueChanged.AddListener (ChangeMusicVolume());

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void ChangeFXVolume () {
		int tempValueFX;
		tempValueFX = Mathf.RoundToInt(FXSlider.value / 100 * startFXValue);//figure out a way to have topvalue
		AudioController.Instance.SetFXVolume(tempValueFX);

	}

	void ChangeMusicVolume () {
		int tempValueMusic;
		tempValueMusic = Mathf.RoundToInt(MusicSlider.value/100 * startMusicValue);
		AudioController.Instance.SetMusicVolume (tempValueMusic);

	}
}
