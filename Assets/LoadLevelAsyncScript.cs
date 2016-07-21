using UnityEngine;
using System.Collections;

public class LoadLevelAsyncScript : MonoBehaviour
{
    public string levelname;
    public void loadthelevel()
    {
        StartCoroutine("LoadIt");
    }
    IEnumerator LoadIt()
    {
        AsyncOperation async = Application.LoadLevelAsync(levelname);
        yield return async;
        Debug.Log("Loading complete");
    }
}