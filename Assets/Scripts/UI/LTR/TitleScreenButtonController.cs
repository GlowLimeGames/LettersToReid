/*
 * Author(s): Isaiah Mann, Martha Hollister
 * Description: Controls the functions of the title screen
 * Usage: [no notes]
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtonController : MonoBehaviourExtended 
{	
    [SerializeField]
    string mapSelectionScreenName = "MapSelection";
    [SerializeField]
    string quoteScreenName = "BeginningQuote";

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        LTRSaveController.GetInstance.Reset();
        loadQuoteScreen();
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

    void loadQuoteScreen()
    {
        SceneManager.LoadScene(quoteScreenName);
    }

}
