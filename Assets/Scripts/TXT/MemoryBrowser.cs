using UnityEngine;
using UnityEngine.UI;
/* Author: Anisha Pai
* Description: Tests the Memory retrieval system by asking to print a random memory
*/
public sealed class MemoryBrowser : MonoBehaviour
{

    MemoriesParser parse;
    System.Random IDgen = new System.Random();
    Text text;
    public Text memoryCounter;
    public Text memoriesFound;
    public int memoryNumber;

    public void Start() {
        parse = GetComponent<MemoriesParser>();
        text = GetComponent<Text>();
        memoryNumber = PlayerPrefs.GetInt("memory number");

        updateScreen();
    }

    public void goNext() {
        if (memoryNumber < parse.getLength())
            memoryNumber++;
        else if (memoryNumber == parse.getLength())
            memoryNumber = 1;

        updateScreen();
    }

    public void goPrev() {
        if (memoryNumber > 1)
            memoryNumber--;
        else if (memoryNumber == 1)
            memoryNumber = parse.getLength();

        updateScreen();
       
    }

    private void updateScreen() {
        text.text = parse.getMemory(memoryNumber);
        memoryCounter.text = memoryNumber.ToString();
        memoriesFound.text = "Memories Found: " + memoryNumber.ToString() + "/" + parse.getLength();
    }
}
