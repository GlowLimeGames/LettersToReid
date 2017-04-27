using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventController.Event ("play_music_menu_main theme");
		EventController.Event ("play_music_menu_main_theme");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
