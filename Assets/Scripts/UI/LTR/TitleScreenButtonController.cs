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
    }

    public void LoadGame()
    {
        loadSelectScreen();
    }
  
    void loadSelectScreen()
    {
        SceneManager.LoadScene(mapSelectionScreenName);
    }

}
