
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Button))]
public class SceneLoadingButton : MonoBehaviour
{
	[SerializeField]
	string sceneName;
	Button button;

	void Awake()
	{
		this.button = GetComponent<Button>();
		button.onClick.AddListener(loadScene);
	}

	void loadScene()
	{
        if(Input.GetMouseButtonUp(0))
        {
            EventController.Event ("ui_click_forward");
    		SceneManager.LoadScene(this.sceneName);
    		EventController.Event("stop_mainmenu");
    		AudioController.Instance.StartCoroutine (AudioController.Instance.musicShift());
			if (MapController.Instance.PeekMap ().MapName == "MapSelection") {
				EventController.Event("stop_beginningamb");
				EventController.Event("stop_endingamb");
				EventController.Event("stop_gameplay_music");
			}
        }
	}

}
