
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
		for (int index = 1; index < 11; index++) {

			if (MapController.Instance.PeekMap ().MapName == "MP-" + index) {
				AudioController.Instance.StartCoroutine (AudioController.Instance.musicShift ());
			}
		}

	}

}
