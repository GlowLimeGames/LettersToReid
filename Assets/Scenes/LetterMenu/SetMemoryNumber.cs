using UnityEngine;

public sealed class SetMemoryNumber : MonoBehaviour
{
    public void setMemoryNumber(int number)
    {
        PlayerPrefs.SetInt("memory number", number);
    }

    public void getMemoryNumer(int number)
    {
        PlayerPrefs.GetInt("memory number");
    }

}
