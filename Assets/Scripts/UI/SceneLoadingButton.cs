
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
		musicShift ();

	}

	IEnumerator musicShift() {
		EventController.Event ("play_transition_mainmenuto_gameplay");
		yield return new WaitForSeconds (9);
		for (int index = 1; index < 8; index++) {

			if (MapController.Instance.PeekMap().MapName == "MP-" + index) {
				EventController.Event("amb_beginnning_01");
			}
		}
		for (int index = 8; index < 10; index++) {

			if (MapController.Instance.PeekMap().MapName == "MP-" + index) {
				EventController.Event("amb_nature_01");
			}
		}
	}
}
