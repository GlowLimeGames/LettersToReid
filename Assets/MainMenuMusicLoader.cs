﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventController.Event("play_music_menu_main_theme");
		EventController.Event ("stop_beginningamb");
		EventController.Event ("stop_endingamb");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
