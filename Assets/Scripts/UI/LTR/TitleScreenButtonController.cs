/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtonController : MonoBehaviourExtended 
{	
    [SerializeField]
    string mapSelectionScreenName = "MapSelection";

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        LTRSaveController.GetInstance.Reset();
        loadSelectScreen();

        PlayerPrefs.SetInt("NewGame", 1);
    }

    public void LoadGame()
    {
        loadSelectScreen();

        PlayerPrefs.SetInt("NewGame", 0);
    }
  
    void loadSelectScreen()
    {
        SceneManager.LoadScene(mapSelectionScreenName);
    }

}
