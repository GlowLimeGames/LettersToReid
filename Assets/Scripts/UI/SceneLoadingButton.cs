
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	}

}
