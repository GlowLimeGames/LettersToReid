using UnityEngine;

public class TheMemories : MonoBehaviour 
{

    public TextAsset jsonMemory;

    public void Start() {

            AMemory memories = JsonUtility.FromJson<AMemory>(jsonMemory.text);
            print(memories.body);
 
    }
}
