/*
 * Authors: Martha Hollister, Isaiah Mann
 * Description: Controls the in game tutorial
 */

using UnityEngine;
using UnityEngine.UI;

public sealed class TutorialController : MonoBehaviour
{
    const string NEW_GAME_KEY = "NewGame";
    const int TRUE_INT = 1;
    const int FALSE_INT = 0;

    public Text title;
    public Text instructions;
    public Canvas tutorialCanvas;

    void Start()
    {
        if (PlayerPrefs.GetInt(NEW_GAME_KEY) == TRUE_INT)
        {
            tutorialCanvas.GetComponent<Canvas>().enabled = true;
            movementText();
            PlayerPrefs.SetInt(NEW_GAME_KEY, FALSE_INT);
        }
        else
        {
            tutorialCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    void movementText()
    {
        title.text = "Movement";

        instructions.text = "Use W,A,S,D or the arrow keys to move\n\n\nClick on or use the spacebar to open envelopes";

    }

   public void objectiveText()
    {
        title.text = "Objective";

        instructions.text = "Explore the tunnel system and find the 50 letters within";
    }
}
