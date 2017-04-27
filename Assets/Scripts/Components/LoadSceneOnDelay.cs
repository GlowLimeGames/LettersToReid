/*
 * Author(s): Isaiah Mann
 * Description: Timed load scene logic
 * Usage: [no notes]
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnDelay : MonoBehaviour 
{
    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    float waitTime;

    [SerializeField]
    bool loadSceneInstantlyOnButtonPress;

    float timer = 0;
    bool loadStarted = false;
    bool loadInstantly = false;

    void Update()
    {
        timer += Time.deltaTime;
        if(this.loadSceneInstantlyOnButtonPress && Input.anyKeyDown)
        {
            this.loadInstantly = true;
        }
        if((timer >= waitTime || loadInstantly) && !loadStarted)
        {
            this.loadStarted = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
