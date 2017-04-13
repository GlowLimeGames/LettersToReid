
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
		SceneManager.LoadScene(this.sceneName);
		EventController.Event ("stop_mainmenu");
		AudioController.Instance.StartCoroutine (AudioController.Instance.musicShift ());
        Debug.Log("SCENE LOADING BUTTON");
	}

}
