using UnityEngine;
using UnityEngine.UI;
/* Authors: Anisha Pai, Isaiah Mann
* Description: Tests the Memory retrieval system by asking to print a random memory
*/
public sealed class MemoryBrowser : MonoBehaviour
{

    MemoriesParser parse;
    System.Random IDgen = new System.Random();
    Text text;
    public Text memoryCounter;
    public int memoryNumber;

    [SerializeField]
    Scrollbar scroll;

	[SerializeField]
	Transform contentTransform;
	[SerializeField]
	Transform textTransform;

    public void Start() {
        parse = GetComponent<MemoriesParser>();
        memoryNumber = PlayerPrefs.GetInt("memory number");
        text = GetComponent<Text>();
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
		contentTransform.localScale = Vector3.one;
		textTransform.localScale = Vector3.one;
        scroll.value = 1f;
        if (MemoryController.Instance.MemoryDiscovered(memoryNumber))
        {
            text.text = parse.getMemory(memoryNumber);
            memoryCounter.text = memoryNumber.ToString();
        }
        else
        {
            memoryCounter.text = memoryNumber.ToString();
            text.text = "Locked";
        }
    }
}
