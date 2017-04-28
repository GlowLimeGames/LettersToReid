using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		playMainMenuMusic ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void playMainMenuMusic(){
		EventController.Event ("stop_beginningamb");
		EventController.Event ("stop_endingamb");
		EventController.Event ("play_music_menu_main_theme");
		EventController.Event ("play_music_menu_main theme");
		Debug.Log ("playmainmenu");
	}
}
