using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MemoryBehvior : MonoBehaviour
{

    //script for mouse clicking on opening memories
    string[] memoryText;
    public TextAsset textFile;
    public int memoryNumbers = 0;

    public Canvas myCanvas;
    public Text myText;

    

    // Use this for initialization
    void Start()
    {
        myCanvas.enabled = false;

        string [] memoryArray = textFile.text.Split('*');
        memoryText = new string[memoryArray.Length];

        for(int i = 0; i < memoryArray.Length; i++)
        {
            memoryText[i] = memoryArray[i];
        }

    }

    public void showMemory()
    {
        myCanvas.enabled = true;
        myText.text = memoryText[memoryNumbers];
		EventController.Event(sx_letter_open);
    }

    public void closeMemory()
    {
        myCanvas.enabled = false;
        memoryNumbers++;
		EventController.Event(sx_letter_close);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
