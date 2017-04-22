using UnityEngine;
using UnityEngine.UI;

public sealed class TutorialController : MonoBehaviour
{
    public Text title;
    public Text instructions;
    public Canvas tutorialCanvas;

    void Start()
    {
        if (PlayerPrefs.GetInt("NewGame") == 1)
        {
            tutorialCanvas.GetComponent<Canvas>().enabled = true;
            movementText();
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
